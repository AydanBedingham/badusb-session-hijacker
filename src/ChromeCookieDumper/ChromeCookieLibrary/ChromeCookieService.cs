using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ChromeCookieLibrary
{
    public class ChromeCookieService
    {
        private System.Data.SQLite.SQLiteConnection conn;

        public ChromeCookieService()
        {

        }

        public void Open()
        {
            String localAppPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            String chromeCookiesPath = localAppPath + "\\Google\\Chrome\\User Data\\Default\\Cookies";
            var connectionString = "Data Source=" + chromeCookiesPath + ";pooling=false";

            conn = new System.Data.SQLite.SQLiteConnection(connectionString);
            conn.Open();
        }


        public void Open(String chromeCookiesPath)
        {
            var connectionString = "Data Source=" + chromeCookiesPath + ";pooling=false";

            conn = new System.Data.SQLite.SQLiteConnection(connectionString);
            conn.Open();
        }


        public void Close()
        {
            conn.Close();
        }



        public List<ChromeCookieEntry> ReadEntries()
        {
            return ReadEntries("");
        }

        public List<ChromeCookieEntry> ReadEntries(String hostName)
        {

            String sql = "SELECT creation_utc,host_key,name,value,path,expires_utc,secure,httpOnly,last_access_utc,has_expires,persistent,priority,encrypted_value,firstpartyonly FROM cookies";
            if (hostName != "") sql = sql + " WHERE host_key = '" + hostName + "'";


            using (var cmd = conn.CreateCommand())
            {
                var prm = cmd.CreateParameter();
                prm.ParameterName = "hostName";
                prm.Value = hostName;
                cmd.Parameters.Add(prm);

                cmd.CommandText = sql;

                List<ChromeCookieEntry> entries = new List<ChromeCookieEntry>();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ChromeCookieEntry entry = new ChromeCookieEntry();
                        entry.creation_utc = (long)reader[0];
                        entry.host_key = (String)reader[1];
                        entry.name = (String)reader[2];
                        entry.value = (String)reader[3];
                        entry.path = (String)reader[4];
                        entry.expires_utc = (long)reader[5];
                        entry.secure = (long)reader[6];
                        entry.httpOnly = (long)reader[7];
                        entry.last_access_utc = (long)reader[8];
                        entry.has_expires = (long)reader[9];
                        entry.persistent = (long)reader[10];
                        entry.priority = (long)reader[11];

                        byte[] encrypted_value = (byte[])reader[12];
                        entry.encryted_value = SystemEncryption.Decrypt(ref encrypted_value);

                        entry.firstpartyonly = (long)reader[13];
                        entries.Add(entry);
                    }
                }

                return entries;
            }


        }


        public void WriteEntries(List<ChromeCookieEntry> entries)
        {
            foreach (ChromeCookieEntry entry in entries)
            {
                WriteEntry(entry);
            }
        }

        public void WriteEntry(ChromeCookieEntry entry)
        {
            List<String> values = new List<String>();
            values.Add(entry.creation_utc.ToString());
            values.Add("'" + entry.host_key + "'");
            values.Add("'" + entry.name + "'");
            values.Add("'" + entry.value + "'");
            values.Add("'" + entry.path + "'");
            values.Add(entry.expires_utc.ToString());
            values.Add(entry.secure.ToString());
            values.Add(entry.httpOnly.ToString());
            values.Add(entry.last_access_utc.ToString());
            values.Add(entry.has_expires.ToString());
            values.Add(entry.persistent.ToString());
            values.Add(entry.priority.ToString());
            values.Add("@encryted_value");
            values.Add(entry.firstpartyonly.ToString());

            String valuesStr = string.Join(",", values.ToArray());

            String sql = "INSERT OR REPLACE INTO cookies (creation_utc,host_key,name,value,path,expires_utc,secure,httpOnly,last_access_utc,has_expires,persistent,priority,encrypted_value,firstpartyonly) VALUES(" + valuesStr + ")";


            using (var cmd = conn.CreateCommand())
            {
                var prm = cmd.CreateParameter();
                byte[] tmp_encrypt_value = entry.encryted_value;
                cmd.Parameters.Add("@encryted_value", DbType.Binary, entry.encryted_value.Length).Value = SystemEncryption.Encrypt(ref tmp_encrypt_value);

                cmd.CommandText = sql;

                cmd.ExecuteNonQuery();
            }


        }
        

    }
}
