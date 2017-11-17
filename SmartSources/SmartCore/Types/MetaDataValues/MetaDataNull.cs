using System.IO;

namespace Smart.Types.MetaDataValues
{
    //--------------------------------------------------------------------------------------------------------------------------

    internal sealed class MetaDataNull : MetaDataValue
    {
        public override bool IsNull()
        {
            return true;
        }

        public override byte GetTypeId()
        {
            return MetaData.ID_NULL;
        }

        internal override void WriteData(BinaryWriter bw)
        {
        }

        internal override void ReadData(BinaryReader br)
        {
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------
}
