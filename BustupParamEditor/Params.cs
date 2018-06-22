using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BustupParamEditor
{
    public class Params
    {
        public static List<byte[]> GetParams(byte[] paramFile, int paramSize)
        {
            int paramCount = paramFile.Length / paramSize;
            List<byte[]> paramEntries = new List<byte[]>();

            using (MemoryStream memStream = new MemoryStream(paramFile))
            using (BinaryReader reader = new BinaryReader(memStream))
            {
                for (int i = 0; i < paramCount; i++)
                {
                    paramEntries.Add(reader.ReadBytes(paramSize));
                }
            }

            return paramEntries;
        }

        public static int GetEntryIndex(List<byte[]> paramEntries, int characterId, int expressionId, int outfitId)
        {
            int i = 0;
            foreach (byte[] entry in paramEntries)
            {
                using (MemoryStream memStream = new MemoryStream(entry))
                using (BinaryReader reader = new BinaryReader(memStream))
                {
                    short entryCharacterId = ToShort(reader.ReadByte(), reader.ReadByte());
                    short entryExpressionId = ToShort(reader.ReadByte(), reader.ReadByte());
                    short entryOutfitId = ToShort(reader.ReadByte(), reader.ReadByte());
                    if (entryCharacterId == characterId && entryExpressionId == expressionId && entryOutfitId == outfitId)
                        return i;
                }
                i++;
            }
            return -1;
        }

        public static int ReplaceEntry()
        {
            return -1;
        }

        public static short ToShort(byte byte1, byte byte2)
        {   
            short newShort = (short)((((int)byte1) << 8) | (int)byte2);
            return newShort;
        }


        public static void FromShort(short number, out byte byte1, out byte byte2)
        {
            byte1 = (byte)(number >> 8);
            byte2 = (byte)number;
        }

        public static float ReadSingle(BinaryReader reader)
        {
            byte[] floatBytes = reader.ReadBytes(4);
            Array.Reverse(floatBytes);
            return BitConverter.ToSingle(floatBytes, 0);
        }

        public static float ReverseSingleEndian(float single)
        {
            byte[] floatBytes = BitConverter.GetBytes(single);
            Array.Reverse(floatBytes);
            return BitConverter.ToSingle(floatBytes, 0);
        }
    }
}
