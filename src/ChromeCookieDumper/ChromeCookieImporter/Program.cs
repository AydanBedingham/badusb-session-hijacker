using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ChromeCookieLibrary;

namespace ChromeCookieImporter
{
    class Program
    {

        private static List<ChromeCookieEntry> LoadFromFile(String filePath)
        {
            FileStream fs = File.OpenRead(filePath);
            IFormatter formatter = new BinaryFormatter();
            fs.Seek(0, SeekOrigin.Begin);
            object objectType = formatter.Deserialize(fs);
            List<ChromeCookieEntry> entries = (List<ChromeCookieEntry>)objectType;
            fs.Close();
            return entries;
        }


        static void Main(string[] args)
        {


            try
            {
                if (args.Length < 1) throw new Exception("Chrome Cookie file not specified");
                if (args.Length < 2) throw new Exception("Cookie dump file not specified");

                FileInfo dumpFileInfo = new FileInfo(args[1]);
                List<ChromeCookieEntry> entries = LoadFromFile(dumpFileInfo.FullName);

                foreach (ChromeCookieEntry entry in entries)
                {
                    Console.WriteLine(entry.ToString());
                }

                FileInfo cookieFileInfo = new FileInfo(args[0]);
                if (!cookieFileInfo.Exists) throw new Exception("Cookie file does not exist");

                ChromeCookieService ccs = new ChromeCookieService();
                ccs.Open(cookieFileInfo.FullName);
                ccs.WriteEntries(entries);
                ccs.Close();

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }


        }
    }
}
