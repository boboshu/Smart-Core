using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Smart.Types.MetaDataValues;
using UnityEngine;

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
                _values.Add(value.Key, value.Value);
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

        public void Set(string name, string value, string defaultValue = "")
        {
            if (value == null || value == defaultValue) { _values.Remove(name); return; }
            _values[name] = new MetaDataString { value = value };
        }

        public void Set(string name, Color value)
        {
            if (value == Color.black) { _values.Remove(name); return; }
            _values[name] = value.maxColorComponent > 1f ? (MetaDataValue) new MetaDataColorHDR {value = value} : new MetaDataColor {value = value};
        }

        public void Set(string name, Color value, Color defaultValue)
        {
            if (value == defaultValue) { _values.Remove(name); return; }
            _values[name] = value.maxColorComponent > 1f ? (MetaDataValue) new MetaDataColorHDR {value = value} : new MetaDataColor {value = value};
        }

        public void Set(string name, int value, int defaultValue = 0)
        {
            if (value == defaultValue) { _values.Remove(name); return; }
            _values[name] = new MetaDataInteger { value = value };
        }

        public void Set(string name, float value, float defaultValue = 0f)
        {
            if (value == defaultValue) { _values.Remove(name); return; }
            _values[name] = new MetaDataFloat { value = value };
        }

        public void Set(string name, Vector2 value)
        {
            if (value == Vector2.zero) { _values.Remove(name); return; }
            _values[name] = new MetaDataVector2 { value = value };
        }

        public void Set(string name, Vector2 value, Vector2 defaultValue)
        {
            if (value == defaultValue) { _values.Remove(name); return; }
            _values[name] = new MetaDataVector2 { value = value };
        }

        public void Set(string name, Vector3 value)
        {
            if (value == Vector3.zero) { _values.Remove(name); return; }
            _values[name] = new MetaDataVector3 { value = value };
        }

        public void Set(string name, Vector3 value, Vector3 defaultValue)
        {
            if (value == defaultValue) { _values.Remove(name); return; }
            _values[name] = new MetaDataVector3 { value = value };
        }

        public void Set(string name, bool value, bool defaultValue = false)
        {
            if (value == defaultValue) { _values.Remove(name); return; }
            _values[name] = new MetaDataBoolean { value = value };
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public MetaDataValue Get(string name)
        {
            return _values.TryGetValue(name, out var v) ? v : NULL;
        }

        public string Get(string name, string defaultValue)
        {
            return _values.TryGetValue(name, out var v) ? v.AsString() : defaultValue;
        }

        public Color Get(string name, Color defaultValue)
        {
            return _values.TryGetValue(name, out var v) ? v.AsColor() : defaultValue;
        }

        public int Get(string name, int defaultValue)
        {
            return _values.TryGetValue(name, out var v) ? v.AsInteger() : defaultValue;
        }

        public float Get(string name, float defaultValue)
        {
            return _values.TryGetValue(name, out var v) ? v.AsFloat() : defaultValue;
        }

        public Vector2 Get(string name, Vector2 defaultValue)
        {
            return _values.TryGetValue(name, out var v) ? v.AsVector2() : defaultValue;
        }

        public Vector3 Get(string name, Vector3 defaultValue)
        {
            return _values.TryGetValue(name, out var v) ? v.AsVector3() : defaultValue;
        }

        public bool Get(string name, bool defaultValue)
        {
            return _values.TryGetValue(name, out var v) ? v.AsBoolean() : defaultValue;
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

        public void FromString(string text)
        {
            foreach (var item in text.Split('\n'))
            {
                var pos = item.IndexOf('=');
                if (pos > 0 && pos < item.Length)
                {
                    var key = item.Substring(0, pos - 1);
                    var value = item.Substring(pos + 1);
                    if (float.TryParse(value, out var f))
                    {
                        if (int.TryParse(value, out var i)) Set(key, i); // int
                        else Set(key, f); // float
                    }
                    else if (bool.TryParse(value, out var b)) Set(key, b); // bool
                    else Set(key, value); // string
                }
            }
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
            while (cnt --> 0)
            {
                var val = _constructorByTypeId[br.ReadByte()]();
                _values.Add(br.ReadString(), val);
                val.ReadData(br);
            }
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        internal const int ID_NULL = 0;
        internal const int ID_INTEGER = 1;
        internal const int ID_FLOAT = 2;
        internal const int ID_STRING = 3;
        internal const int ID_COLOR = 4;
        internal const int ID_VECTOR2 = 5;
        internal const int ID_VECTOR3 = 6;
        internal const int ID_BOOLEAN = 7;
        internal const int ID_COLORHDR = 8;

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        private static readonly Func<MetaDataValue>[] _constructorByTypeId = new Func<MetaDataValue>[255];
        public static readonly MetaDataValue NULL = new MetaDataNull();

        static MetaData()
        {
            _constructorByTypeId[ID_NULL] = () => NULL;
            _constructorByTypeId[ID_INTEGER] = () => new MetaDataInteger();
            _constructorByTypeId[ID_FLOAT] = () => new MetaDataFloat();
            _constructorByTypeId[ID_STRING] = () => new MetaDataString();
            _constructorByTypeId[ID_COLOR] = () => new MetaDataColor();
            _constructorByTypeId[ID_VECTOR2] = () => new MetaDataVector2();
            _constructorByTypeId[ID_VECTOR3] = () => new MetaDataVector3();
            _constructorByTypeId[ID_BOOLEAN] = () => new MetaDataBoolean();
            _constructorByTypeId[ID_COLORHDR] = () => new MetaDataColorHDR();
        }

        //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    }
}