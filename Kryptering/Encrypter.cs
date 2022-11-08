using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kryptering
{
    class Encrypter
    {
        public static char C_Cip(char ch, int key)
        {
            if (!char.IsLetter(ch))
            {
                return ch;
            }

            char single = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - single) % 26) + single);
        }
        public static string C_Encip(string input, int key)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += C_Cip(ch, key);

            return output;
        }
        public static string C_Decip(string input, int key)
        {
            return C_Encip(input, 26 - key);
        }

        public static void Prompt()
        {
            Console.WriteLine("\nType what you want to encrypt: ");
            string UserString = Console.ReadLine();

            Console.Write("\nProvide a key");
            int key = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nEncrypted Data with key: " + key);
            string encryptedString = C_Encip(UserString, key);
            Console.WriteLine(encryptedString);

            Console.WriteLine("\nDecrypted Data:");
            string decryptedString = C_Decip(encryptedString, key);
            Console.WriteLine(decryptedString + "\n");
        }
    }

}
