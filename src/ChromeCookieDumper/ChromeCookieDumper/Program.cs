using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ChromeCookieLibrary;

namespace ChromeCookieDumper
{
    class Program
    {

        private static void SaveToFile(String filePath, List<ChromeCookieEntry> entries){
            FileStream fs = File.Create(filePath);
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, entries);
            fs.Close();
        }

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

                FileInfo cookieFileInfo = new FileInfo(args[0]);
                if (!cookieFileInfo.Exists) throw new Exception("Cookie file does not exist");

                ChromeCookieService ccs = new ChromeCookieService();
                ccs.Open(cookieFileInfo.FullName);

                List<ChromeCookieEntry> entries = null;
                if (args.Length >= 3)
                {
                    //Obtain cookie entries using a specifier eg. ".facebook.com"
                    entries = ccs.ReadEntries(args[2]);
                }
                else
                {
                    //Obtain all cookie entries
                    entries = ccs.ReadEntries();
                }

                ccs.Close();

                foreach (ChromeCookieEntry entry in entries)
                {
                    Console.WriteLine(entry.ToString());
                }

                FileInfo dumpFileInfo = new FileInfo(args[1]);
                SaveToFile(dumpFileInfo.FullName, entries);
                
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
              
        
    }
}
