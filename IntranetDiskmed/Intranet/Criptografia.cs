using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace IntranetDiskmed.Intranet
{
    public class Criptografia
    {
        private static byte[] _byte =
            { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18,
                0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };

        private const string chave = "Gd3X1qbjSyNkaiQJN3FJusdaI4rtyokC";

        public static string criptografar(string valor)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                byte[] _bChave = Convert.FromBase64String(chave);
                byte[] _bValor = new UTF8Encoding().GetBytes(valor);

                Rijndael rijndael = new RijndaelManaged();

                rijndael.KeySize = 256;

                MemoryStream mS = new MemoryStream();
                CryptoStream encryptor = new CryptoStream(mS, rijndael.CreateEncryptor(_bChave, _byte), CryptoStreamMode.Write);

                encryptor.Write(_bValor, 0, _bValor.Length);
                encryptor.FlushFinalBlock();
                return Convert.ToBase64String(mS.ToArray());
            }
            else
            {
                return "";
            }
        }

        public static string descriptografar(string valor)
        {
            if (!string.IsNullOrEmpty(valor))
            {
                byte[] _bChave = Convert.FromBase64String(chave);
                byte[] _bValor = Convert.FromBase64String(valor);

                Rijndael rijndael = new RijndaelManaged();

                rijndael.KeySize = 256;

                MemoryStream mStream = new MemoryStream();
                CryptoStream decryptor = new CryptoStream(mStream, rijndael.CreateDecryptor(_bChave, _byte), CryptoStreamMode.Write);

                decryptor.Write(_bValor, 0, _bValor.Length);
                decryptor.FlushFinalBlock();
                UTF8Encoding utf8 = new UTF8Encoding();
                return utf8.GetString(mStream.ToArray());
            }
            else
            {
                return "";
            }
        }
    }
}