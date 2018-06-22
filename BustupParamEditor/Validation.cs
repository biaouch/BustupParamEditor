using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BustupParamEditor
{
    class Validation
    {
        public static string ParamFilePath(string path, out List<byte[]> paramEntries)
        {
            bool validPath = false;
            paramEntries = new List<byte[]>();

            while (validPath == false)
            {
                Console.Write("Path to bustup parameters file: ");
                path = Console.ReadLine();

                try
                {
                    byte[] paramFile = File.ReadAllBytes(path);
                    if (paramFile.Length == 9216)
                    {
                        paramEntries = Params.GetParams(paramFile, 32);
                        validPath = true;
                    }
                    else if (paramFile.Length == 43840)
                    {
                        paramEntries = Params.GetParams(paramFile, 40);
                        validPath = true;
                    }
                }
                catch
                {
                    Console.WriteLine("Not a valid bustup param file. Please try again.");
                    Console.ReadKey();
                }
                Console.Clear();
            }
            return path;
        }

        public static int SelectedEntry(List<byte[]> paramEntries, out ParamEntry pEntry)
        {
            pEntry = new ParamEntry();

            bool validEntry = false;
            int paramId = 0;
            
            int characterId = 0;
            int expressionId = 0;
            int outfitId = 0;

            while (validEntry == false)
            {
                Console.Write("Character ID: ");
                characterId = Convert.ToInt16(Console.ReadLine());
                Console.Write("Expression ID: ");
                expressionId = Convert.ToInt16(Console.ReadLine());
                Console.Write("Outfit ID: ");
                outfitId = Convert.ToInt16(Console.ReadLine());

                paramId = Params.GetEntryIndex(paramEntries, characterId, expressionId, outfitId);
                try
                {
                    pEntry.Read(paramEntries[paramId]);
                    validEntry = true;
                }
                catch
                {
                    Console.WriteLine("Not a valid bustup. Please try again.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            return paramId;
        }
    }
}
