using System.IO;
using UnityEngine;

namespace Smart.Types.MetaDataValues
{
    internal sealed class MetaDataInteger : MetaDataValue
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public int value;

        public override byte GetTypeId()
        {
            return MetaData.ID_INTEGER;
        }

        internal override void WriteData(BinaryWriter bw)
        {
            bw.Write(value);
        }

        internal override void ReadData(BinaryReader br)
        {
            value = br.ReadInt32();
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public override int AsInteger()
        {
            return value;
        }

        public override float AsFloat()
        {
            return value;
        }

        public override string AsString()
        {
            return value.ToString();
        }

        public override Color AsColor()
        {
            var x = value / 255f;
            return new Color(x, x, x);
        }

        public override Vector2 AsVector2()
        {
            return new Vector2(value, value);
        }

        public override Vector3 AsVector3()
        {
            return new Vector3(value, value, value);
        }

        public override bool AsBoolean()
        {
            return value != 0;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
