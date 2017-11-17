using System.IO;
using UnityEngine;

namespace Smart.Types.MetaDataValues
{
    internal sealed class MetaDataFloat : MetaDataValue
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public float value;

        public override byte GetTypeId()
        {
            return MetaData.ID_FLOAT;
        }

        internal override void WriteData(BinaryWriter bw)
        {
            bw.Write(value);
        }

        internal override void ReadData(BinaryReader br)
        {
            value = br.ReadSingle();
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public override int AsInteger()
        {
            return Mathf.RoundToInt(value);
        }

        public override float AsFloat()
        {
            return value;
        }

        public override string AsString()
        {
            return value.ToString("0.##");
        }

        public override Color AsColor()
        {
            return new Color(value, value, value);
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
