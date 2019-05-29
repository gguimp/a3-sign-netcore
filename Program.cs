using System;
using System.Security;
using System.Security.Cryptography;

namespace net_core
{
    class Program
    {
        unsafe public static void sign1()
        {
            CspParameters csp = new CspParameters(1, "SafeSign IC Standard Windows Cryptographic Service Provider");
            csp.Flags = CspProviderFlags.UseDefaultKeyContainer;

            SecureString password;

            char[] pwdchars = {'1', '2', '3', '4'};
            
            fixed(char* pChars = pwdchars)
            {
                password = new SecureString(pChars, pwdchars.Length);
            }

            csp.KeyPassword = password;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp);

            byte[] data = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7 };

            Console.WriteLine("Data			: " + BitConverter.ToString(data));

            // Sign the data using the Smart Card CryptoGraphic Provider.
            byte[] sig = rsa.SignData(data, "SHA1");

            Console.WriteLine("Signature	: " + BitConverter.ToString(sig));

            // Verify the data using the Smart Card CryptoGraphic Provider.
            bool verified = rsa.VerifyData(data, "SHA1", sig);

            Console.WriteLine("Verified		: " + verified);

        }

        static void Main(string[] args)
        {
            Program.sign1();
        }

    }
}
