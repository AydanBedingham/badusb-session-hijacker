using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChromeCookieLibrary
{
    public class SystemEncryption
    {
        public static byte[] Decrypt(ref byte[] encryptedData)
        {
            return System.Security.Cryptography.ProtectedData.Unprotect(encryptedData, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
        }


        public static byte[] Encrypt(ref byte[] decryptedData)
        {
            return System.Security.Cryptography.ProtectedData.Protect(decryptedData, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
        }


    }
}
