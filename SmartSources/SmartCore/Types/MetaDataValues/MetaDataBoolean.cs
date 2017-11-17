using System.IO;
using UnityEngine;

namespace Smart.Types.MetaDataValues
{
    internal sealed class MetaDataBoolean : MetaDataValue
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public bool value;

        public override byte GetTypeId()
        {
            return MetaData.ID_BOOLEAN;
        }

        internal override void WriteData(BinaryWriter bw)
        {
            bw.Write(value);
        }

        internal override void ReadData(BinaryReader br)
        {
            value = br.ReadBoolean();
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public override int AsInteger()
        {
            return value ? 1 : 0;
        }

        public override float AsFloat()
        {
            return value ? 1 : 0;
        }

        public override string AsString()
        {
            return value.ToString();
        }

        public override Color AsColor()
        {
            return value ? Color.white : Color.black;
        }

        public override Vector2 AsVector2()
        {
            return value ? Vector2.one : Vector2.zero;
        }

        public override Vector3 AsVector3()
        {
            return value ? Vector3.one : Vector3.zero;
        }

        public override bool AsBoolean()
        {
            return value;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
