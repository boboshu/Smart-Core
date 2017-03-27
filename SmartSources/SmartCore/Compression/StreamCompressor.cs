using System.IO;
using LZ4;

namespace Smart.Compression
{
    public static class StreamCompressor
    {
        //--------------------------------------------------------------------------------------------------------------------------

        public static void CompressStrong(MemoryStream source, MemoryStream destination)
        {
            var estimatedDataSize = source.Length + 128; // to hook situation when compressed size is bigger than original
            if (destination.Length < estimatedDataSize) destination.SetLength(estimatedDataSize);
            var len = LZ4Codec.EncodeHC(source.GetBuffer(), 0, (int)source.Length, destination.GetBuffer(), 0, (int)destination.Length);
            destination.SetLength(len);
        }

        public static void CompressFast(MemoryStream source, MemoryStream destination)
        {
            var estimatedDataSize = source.Length + 128; // to hook situation when compressed size is bigger than original
            if (destination.Length < estimatedDataSize) destination.SetLength(estimatedDataSize);
            var len = LZ4Codec.Encode(source.GetBuffer(), 0, (int)source.Length, destination.GetBuffer(), 0, (int)destination.Length);
            destination.SetLength(len);
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public static void Decompress(MemoryStream source, MemoryStream destination)
        {
            var estimatedDataSize = source.Length << 7; // * 128
            if (destination.Length < estimatedDataSize) destination.SetLength(estimatedDataSize);
            var len = LZ4Codec.Decode(source.GetBuffer(), 0, (int)source.Length, destination.GetBuffer(), 0, (int)destination.Length);
            destination.SetLength(len);
        }

        public static void Decompress(byte[] source, MemoryStream destination)
        {
            var estimatedDataSize = source.Length << 7; // * 128
            if (destination.Length < estimatedDataSize) destination.SetLength(estimatedDataSize);
            var len = LZ4Codec.Decode(source, 0, source.Length, destination.GetBuffer(), 0, (int)destination.Length);
            destination.SetLength(len);
        }

        //--------------------------------------------------------------------------------------------------------------------------
    }
}