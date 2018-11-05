using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BustupParamEditor.IO;

namespace BustupParamEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Validate path to param file and add entries to byte array before continuing
            string path = null;
            if (args.Length > 0)
                path = args[0];

            List<byte[]> paramEntries = new List<byte[]>();
            path = Validation.ParamFilePath(path, out paramEntries);

            //Confirm that bustup parameter entry is valid before continuing

            ParamEntry pEntry = new ParamEntry();
            int paramId = Validation.SelectedEntry(paramEntries, out pEntry);

            //Lists current values, allows user to pick a value and enter a new one, then refresh list of values
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Editing parameters for b{pEntry.CharacterId.ToString("D3")}_{pEntry.ExpressionId.ToString("D3")}_{pEntry.OutfitId.ToString("D2")}");
                Console.WriteLine("\nChoose which value to replace:");
                Console.WriteLine($"(1) Offset X: {pEntry.OffsetX}");
                Console.WriteLine($"(2) Offset Y: {pEntry.OffsetY}");
                Console.WriteLine($"(3) Eye Position X: {pEntry.EyePositionX}");
                Console.WriteLine($"(4) Eye Position Y: {pEntry.EyepositionY}");
                Console.WriteLine($"(5) Mouth Position X: {pEntry.MouthPositionX}");
                Console.WriteLine($"(6) Mouth Position Y: {pEntry.MouthPositionY}");
                Console.WriteLine($"(7) Unknown Flag: {pEntry.Unknown}");
                Console.WriteLine($"(0) Save Changes and Exit");
                int selection = 11;
                while (selection > 7 || selection < 0)
                {
                    try
                    {
                        selection = Convert.ToInt32(Console.ReadLine());
                    }
                    catch
                    {
                    }
                }

                float newValue = 0;
                if (selection != 0)
                {
                    Console.Write("\nEnter the new value for your selection: ");
                    try {
                        newValue = Convert.ToSingle(Console.ReadLine());
                    }
                    catch
                    {
                    }
                }

                switch (selection)
                {
                    case 0:
                        pEntry.Replace(pEntry.EntryData, paramId, path, pEntry.Type);
                        return;
                    case 1: // Offset X
                        pEntry.Read(ParamEntry.Write(pEntry.EntryData, 8, newValue));
                        break;
                    case 2: // Offset Y
                        if (pEntry.Type == ParamEntry.ParamType.Normal)
                            pEntry.Read(ParamEntry.Write(pEntry.EntryData, 12, newValue));
                        break;
                    case 3: // Eye Pos X
                        if (pEntry.Type == ParamEntry.ParamType.Normal)
                            pEntry.Read(ParamEntry.Write(pEntry.EntryData, 16, newValue));
                        else
                            pEntry.Read(ParamEntry.Write(pEntry.EntryData, 12, newValue));
                        break;
                    case 4: // Eye Pos Y
                        if (pEntry.Type == ParamEntry.ParamType.Normal)
                            pEntry.Read(ParamEntry.Write(pEntry.EntryData, 20, newValue));
                        else
                            pEntry.Read(ParamEntry.Write(pEntry.EntryData, 16, newValue));
                        break;
                    case 5: // Mouth Pos X
                        if (pEntry.Type == ParamEntry.ParamType.Normal)
                            pEntry.Read(ParamEntry.Write(pEntry.EntryData, 24, newValue));
                        else
                            pEntry.Read(ParamEntry.Write(pEntry.EntryData, 20, newValue));
                        break;
                    case 6: // Mouth Pos Y
                        if (pEntry.Type == ParamEntry.ParamType.Normal)
                            pEntry.Read(ParamEntry.Write(pEntry.EntryData, 28, newValue));
                        else
                            pEntry.Read(ParamEntry.Write(pEntry.EntryData, 24, newValue));
                        break;
                    case 7: // Unknown Field
                        if (pEntry.Type == ParamEntry.ParamType.Normal)
                            pEntry.Read(ParamEntry.Write(pEntry.EntryData, 34, (short)Math.Round(newValue)));
                        else
                            pEntry.Read(ParamEntry.Write(pEntry.EntryData, 30, (short)Math.Round(newValue)));
                        break;
                    
                }
            }
            
        }
    }
}
