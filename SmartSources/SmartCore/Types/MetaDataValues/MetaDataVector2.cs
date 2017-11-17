using System.IO;
using UnityEngine;

namespace Smart.Types.MetaDataValues
{
    internal sealed class MetaDataVector2 : MetaDataValue
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public Vector2 value;

        public override byte GetTypeId()
        {
            return MetaData.ID_VECTOR2;
        }

        internal override void WriteData(BinaryWriter bw)
        {
            bw.Write(value.x);
            bw.Write(value.y);
        }

        internal override void ReadData(BinaryReader br)
        {
            value.x = br.ReadSingle();
            value.y = br.ReadSingle();
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public override int AsInteger()
        {
            return Mathf.RoundToInt(value.magnitude);
        }

        public override float AsFloat()
        {
            return value.magnitude;
        }

        public override string AsString()
        {
            return value.ToString();
        }

        public override Color AsColor()
        {
            return new Color(value.x, value.y, 0);
        }

        public override Vector2 AsVector2()
        {
            return value;
        }

        public override Vector3 AsVector3()
        {
            return value;
        }

        public override bool AsBoolean()
        {
            return value != Vector2.zero;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
