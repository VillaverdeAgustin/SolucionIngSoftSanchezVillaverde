using System.Security.Cryptography;
using System.Text;

namespace Servicios
{
    public class Encriptador
    {
        public static string EncriptarIrrev(string pass)
        {
            byte[] data = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(pass));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append(data[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
