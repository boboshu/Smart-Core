using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Smart.Types.MetaDataValues;

namespace Smart.Types
{
    public class MetaData
    {
        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private readonly Dictionary<string, MetaDataValue> _values = new Dictionary<string, MetaDataValue>();

        public bool IsEmpty => _values.Count == 0;

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void Clear()
        {
            _values.Clear();
        }

        public void CopyFrom(MetaData source)
        {
            _values.Clear();
            foreach (var value in source._values)
                _values.Add(value.Key, value.Value.CreateCopy());
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public bool Has(string name)
        {
            return _values.ContainsKey(name);
        }

        public void Remove(string name)
        {
            _values.Remove(name);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        
        // Flags
        public bool this[string name]
        {
            get { return _values.ContainsKey(name); }
            set { if (value) _values[name] = NULL; else _values.Remove(name); }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void Set<T>(string name, T value)
        {
            MetaDataValue v;
            MetaDataValue<T> tv;
            if (EqualityComparer<T>.Default.Equals(value, default(T))) { _values.Remove(name); return; }
            if (_values.TryGetValue(name, out v) && (tv = v as MetaDataValue<T>) != null) tv.value = value;
            else _values[name] = MetaDataValueType<T>.Create(value);
        }

        public void Set<T>(string name, T value, T defaultValue)
        {
            MetaDataValue v;
            MetaDataValue<T> tv;
            if (EqualityComparer<T>.Default.Equals(value, defaultValue)) { _values.Remove(name); return; }
            if (_values.TryGetValue(name, out v) && (tv = v as MetaDataValue<T>) != null) tv.value = value;
            else _values[name] = MetaDataValueType<T>.Create(value);
        }

        public MetaDataValue Get(string name)
        {
            MetaDataValue v;
            return _values.TryGetValue(name, out v) ? v : NULL;
        }

        public T Get<T>(string name, T defaultValue)
        {
            MetaDataValue v;
            MetaDataValue<T> tv;
            return _values.TryGetValue(name, out v) && (tv = v as MetaDataValue<T>) != null ? tv.value : defaultValue;
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public IEnumerable<string> Enum()
        {
            return _values.Keys;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var p in _values)
                sb.Append(p.Key).Append('=').AppendLine(p.Value.AsString());
            return sb.ToString();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public void WriteData(BinaryWriter bw)
        {
            bw.Write((byte)_values.Count);
            foreach (var value in _values)
            {
                bw.Write(value.Value.GetTypeId());
                bw.Write(value.Key);
                value.Value.WriteData(bw);
            }
        }

        public void ReadData(BinaryReader br)
        {
            _values.Clear();
            var cnt = br.ReadByte();
            while (cnt-- > 0)
            {
                var val = _constructorByTypeId[br.ReadByte()]();
                _values.Add(br.ReadString(), val);
                val.ReadData(br);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static readonly MetaDataValue NULL = new MetaDataNull();

        static MetaData()
        {
            _constructorByTypeId[0] = () => NULL;            
            RegValueType(() => new MetaDataInteger());
            RegValueType(() => new MetaDataFloat());
            RegValueType(() => new MetaDataString());
            RegValueType(() => new MetaDataColor());
            RegValueType(() => new MetaDataVector2());
            RegValueType(() => new MetaDataVector3());
            RegValueType(() => new MetaDataBoolean());
        }

        private static readonly Func<MetaDataValue>[] _constructorByTypeId = new Func<MetaDataValue>[255];

        private static void RegValueType<T>(Func<MetaDataValue<T>> constructor)
        {
            for (byte id = 0; id < 255; id++)
            {
                if (_constructorByTypeId[id] != null) continue;
                MetaDataValueType<T>.id = id;
                MetaDataValueType<T>._constructor = constructor;
                _constructorByTypeId[id] = MetaDataValueType<T>.Create;
                return;
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}