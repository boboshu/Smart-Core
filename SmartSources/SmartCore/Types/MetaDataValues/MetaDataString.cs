using System.IO;
using Smart.Extensions;
using UnityEngine;

namespace Smart.Types.MetaDataValues
{
    internal sealed class MetaDataString : MetaDataValue
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public string value;

        public override byte GetTypeId()
        {
            return MetaData.ID_STRING;
        }

        internal override void WriteData(BinaryWriter bw)
        {
            bw.Write(value);
        }

        internal override void ReadData(BinaryReader br)
        {
            value = br.ReadString();
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public override int AsInteger()
        {
            return int.TryParse(value, out var v) ? v : 0;
        }

        public override float AsFloat()
        {
            return float.TryParse(value, out var v) ? v : 0;
        }

        public override string AsString()
        {
            return value;
        }

        public override Color AsColor()
        {
            return value.AsHexColor();
        }

        public override Vector2 AsVector2()
        {
            return Vector2.zero; // TODO
        }

        public override Vector3 AsVector3()
        {
            return Vector3.zero; // TODO
        }

        public override bool AsBoolean()
        {
            return !string.IsNullOrEmpty(value);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public override bool IsString()
        {
            return true;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
