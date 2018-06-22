using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BustupParamEditor
{
    class ParamEntry
    {
        public int EntryIndex { get; set; }
        public short CharacterId { get; set; }
        public short ExpressionId { get; set; }
        public short OutfitId { get; set; }

        public float OffsetX { get; set; }
        public float OffsetY { get; set; }

        public float EyePositionX { get; set; }
        public float EyepositionY { get; set; }

        public float MouthPositionX { get; set; }
        public float MouthPositionY { get; set; }

        public short Unknown { get; set; }

        public enum ParamType { Normal, Assist }

        public ParamType Type { get; set; }

        public byte[] EntryData { get; set; }

        public void Read(byte[] entry)
        {
            EntryData = entry;
            using (MemoryStream memStream = new MemoryStream(entry))
            using (BinaryReader reader = new BinaryReader(memStream))
            {
                CharacterId = Params.ToShort(reader.ReadByte(), reader.ReadByte()); //2
                ExpressionId = Params.ToShort(reader.ReadByte(), reader.ReadByte()); //4
                OutfitId = Params.ToShort(reader.ReadByte(), reader.ReadByte()); //6
                reader.ReadBytes(2); //8
                OffsetX = Params.ReadSingle(reader); //12

                if (reader.BaseStream.Length == 32)
                {
                    EyePositionX = Params.ReadSingle(reader); //16
                    EyepositionY = Params.ReadSingle(reader); //20
                    MouthPositionX = Params.ReadSingle(reader); //24
                    MouthPositionY = Params.ReadSingle(reader); //28
                    reader.ReadBytes(2); //30
                    Unknown = Params.ToShort(reader.ReadByte(), reader.ReadByte()); //32
                    Type = ParamType.Assist;
                }
                else if (reader.BaseStream.Length == 40)
                {
                    OffsetY = Params.ReadSingle(reader); //16
                    EyePositionX = Params.ReadSingle(reader); //20
                    EyepositionY = Params.ReadSingle(reader); //24
                    MouthPositionX = Params.ReadSingle(reader); //28
                    MouthPositionY = Params.ReadSingle(reader); //32
                    reader.ReadBytes(2); //34
                    Unknown = Params.ToShort(reader.ReadByte(), reader.ReadByte()); //38
                    reader.ReadSingle(); //40
                    Type = ParamType.Normal;
                }
            }
        }

        public void Replace(byte[] entry, int index, string path, ParamType type)
        {
            EntryData = entry;
            using (FileStream stream = new FileStream(path, FileMode.Open))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                if (type == ParamEntry.ParamType.Assist)
                    writer.BaseStream.Position = (index * 32);
                else if (type == ParamEntry.ParamType.Normal)
                    writer.BaseStream.Position = (index * 40);

                writer.Write(entry);
            }
        }

        public static byte[] WriteFloat(byte[] entry, long position, float newValue)
        {
            using (MemoryStream memStream = new MemoryStream(entry))
            using (BinaryWriter writer = new BinaryWriter(memStream))
            {
                writer.BaseStream.Seek(position, SeekOrigin.Begin);
                writer.Write(Params.ReverseSingleEndian(newValue));
            }
            return entry;
        }

        public static byte[] WriteShort(byte[] entry, long position, short newValue)
        {
            byte[] shortBytes = BitConverter.GetBytes(newValue);
            Array.Reverse(shortBytes);
            using (MemoryStream memStream = new MemoryStream(entry))
            using (BinaryWriter writer = new BinaryWriter(memStream))
            {
                writer.BaseStream.Seek(position, SeekOrigin.Begin);
                writer.Write(shortBytes);
            }
            return entry;
        }
    }
}
