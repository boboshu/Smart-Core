using System;
using System.IO;

namespace Smart.Types.MetaDataValues
{
    //--------------------------------------------------------------------------------------------------------------------------

    internal abstract class MetaDataValue<T> : MetaDataValue
    {
        public T value;

        public sealed override byte GetTypeId()
        {
            return MetaDataValueType<T>.id;
        }

        public sealed override bool IsNull()
        {
            return false;
        }

        public override MetaDataValue CreateCopy()
        {
            return MetaDataValueType<T>.Create(value);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------

    internal static class MetaDataValueType<T>
    {
        public static byte id;

        public static MetaDataValue Create()
        {
            return _constructor == null ? MetaData.NULL : _constructor();
        }

        public static MetaDataValue Create(T value)
        {
            if (_constructor == null) return MetaData.NULL;
            var inst = _constructor();
            inst.value = value;
            return inst;
        }

        internal static Func<MetaDataValue<T>> _constructor;
    }

    //--------------------------------------------------------------------------------------------------------------------------

    internal sealed class MetaDataNull : MetaDataValue
    {
        public override bool IsNull()
        {
            return true;
        }

        public override byte GetTypeId()
        {
            return 0;
        }

        internal override void WriteData(BinaryWriter bw)
        {
        }

        internal override void ReadData(BinaryReader br)
        {
        }

        public override MetaDataValue CreateCopy()
        {
            return this;
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------
}
