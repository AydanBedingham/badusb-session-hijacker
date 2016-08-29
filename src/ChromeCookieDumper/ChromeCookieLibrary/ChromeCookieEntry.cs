using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromeCookieLibrary
{

    [Serializable]
    public class ChromeCookieEntry
    {
        //creation_utc,host_key,name,value,path,expires_utc,secure,httpOnly,last_access_utc,has_expires,persistent,priority,encrypted_value,firstpartonly

        public long creation_utc{ get; set; }
        public String host_key {get; set;}
        public String name {get; set;}
        public String value {get; set;}
        public String path {get; set;}
        public long expires_utc { get; set; }
        public long secure { get; set; }
        public long httpOnly { get; set; }
        public long last_access_utc { get; set; }
        public long has_expires { get; set; }
        public long persistent { get; set; }
        public long priority { get; set; }
        public byte[] encryted_value {get; set;}
        public long firstpartyonly { get; set; }


        public override String ToString()
        {
            List<String> vars = new List<string>();
            vars.Add(creation_utc.ToString());
            vars.Add("'"+host_key+"'");
            vars.Add("'" + name + "'");
            vars.Add("'" + value + "'");
            vars.Add("'" + path + "'");
            vars.Add(expires_utc.ToString());
            vars.Add(secure.ToString());
            vars.Add(httpOnly.ToString());
            vars.Add(last_access_utc.ToString());
            vars.Add(has_expires.ToString());
            vars.Add(persistent.ToString());
            vars.Add(priority.ToString());
            vars.Add(((encryted_value!=null)?"BLOB":"NULL"));
            vars.Add(firstpartyonly.ToString());

            return string.Join(",", vars.ToArray());
        }
    }
}
