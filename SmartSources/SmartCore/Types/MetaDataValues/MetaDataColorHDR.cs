using System.IO;
using Smart.Extensions;
using UnityEngine;

namespace Smart.Types.MetaDataValues
{
    internal sealed class MetaDataColorHDR : MetaDataValue
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public Color value;

        public override byte GetTypeId()
        {
            return MetaData.ID_COLORHDR;
        }

        internal override void WriteData(BinaryWriter bw)
        {
            bw.Write(value.r);
            bw.Write(value.g);
            bw.Write(value.b);
            bw.Write(value.a);
        }

        internal override void ReadData(BinaryReader br)
        {
            value.r = br.ReadSingle();
            value.g = br.ReadSingle();
            value.b = br.ReadSingle();
            value.a = br.ReadSingle();
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
            return $"HDR({value.r:F3}, {value.g:F3}, {value.b:F3}, {value.a:F3})";
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
