using System.IO;
using Smart.Extensions;
using UnityEngine;

namespace Smart.Types.MetaDataValues
{
    internal sealed class MetaDataColor : MetaDataValue
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public Color value;

        public override byte GetTypeId()
        {
            return MetaData.ID_COLOR;
        }

        internal override void WriteData(BinaryWriter bw)
        {
            bw.Write(value.ToUInt());
        }

        internal override void ReadData(BinaryReader br)
        {
            value = br.ReadUInt32().ToColor();
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public override int AsInteger()
        {
            return value.ToInt();
        }

        public override float AsFloat()
        {
            return (value.r + value.g + value.b) / 3f;
        }

        public override string AsString()
        {
            return value.ToHex();
        }

        public override Color AsColor()
        {
            return value;
        }

        public override Vector2 AsVector2()
        {
            return new Vector2(value.r, value.g);
        }

        public override Vector3 AsVector3()
        {
            return new Vector3(value.r, value.g, value.b);
        }

        public override bool AsBoolean()
        {
            return value != Color.black;
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}
