
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        bool run = true;
        while (run)
        {
            Console.WriteLine("\nWrite 1 for random numbers, \n2 for caeser cypher, \n3 for different hashing types");
            char switch_on = Console.ReadKey().KeyChar;

            switch (switch_on)
            {
                case '1':
                    //Set timer
                    Console.WriteLine("Starting test for Random at {0:HH:mm:ss.fff}", DateTime.Now);
                    GetRandom();
                    //Random timer stop
                    Console.WriteLine("Ending test for Random at {0:HH:mm:ss.fff}", DateTime.Now);
                    Console.WriteLine("Starting test for RandomNumberGenerator at {0:HH:mm:ss.fff}", DateTime.Now);
                    GetRandomNumberGenerator();
                    //RandomNumberGenerator stop
                    Console.WriteLine("Ending test for RandomNumberGenerator {0:HH:mm:ss.fff}", DateTime.Now);
                    Console.WriteLine();
                    break;
                case '2':
                    Encrypter.Prompt();
                    break;
                case '3':
                    HashAndHmac.WriteHashingResults();
                    break;
                default:
                    run = false;
                    break;
            }
        } 
    }
    public static void GetRandom()
    {
        //Creates 100 random numbers. 
        Random random = new Random();

        for (int ctr = 0; ctr < 100; ctr++)
        {
            Console.Write(random.Next());
            Console.WriteLine();
        }
    }
    public static void GetRandomNumberGenerator()
    {
        //Creates 100 random numbers with the RandomNumberGenerator. 

        var gen = RandomNumberGenerator.Create();
        byte[] data = new byte[4];

        for (int i = 0; i < 100; i++)
        {
            gen.GetBytes(data);
            int value = BitConverter.ToInt32(data, 0);
            Console.WriteLine(value);
        }
    }
}

//Caeser cipher encrypter.
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

public class HashAndHmac
{
    public static void WriteHashingResults()
    {
        int keySize;
        byte[]? computedKey = null;
        
        Console.WriteLine("\nWrite text to hash: ");
        string textToHash = Console.ReadLine();
        Console.WriteLine("\nDo you want to use regular hash(1) or hmac(2)?. Type '1' or '2' \n");
        int choice = Convert.ToInt32(Console.ReadLine());

        //Sets the key if the user choses hmac, if not it will be null
        if (choice == 2)
        {
            Console.WriteLine("\n Provide a key number: \n");
            keySize = Convert.ToInt32(Console.ReadLine());
            computedKey = GenerateKey(keySize);
        }

        //Create the hash with timer set so we can se how fast it resolves 
        DateTime md5_t = DateTime.Now;
        var md5 = Md5(Encoding.UTF8.GetBytes(textToHash), computedKey);
        DateTime md5_s = DateTime.Now;
        TimeSpan md5_value = md5_s.Subtract(md5_t);

        DateTime sha1_t = DateTime.Now;
        var sha1 = Sha1(Encoding.UTF8.GetBytes(textToHash), computedKey);
        DateTime sha1_s = DateTime.Now;
        TimeSpan sha1_value = sha1_s.Subtract(sha1_t);

        DateTime sha256_t = DateTime.Now;
        var sha256 = Sha256(Encoding.UTF8.GetBytes(textToHash), computedKey);
        DateTime sha256_s = DateTime.Now;
        TimeSpan sha256_value = sha256_s.Subtract(sha256_t);

        DateTime sha512_t = DateTime.Now;
        var sha512 = Sha512(Encoding.UTF8.GetBytes(textToHash), computedKey);
        DateTime sha512_s = DateTime.Now;
        TimeSpan sha512_value = sha512_s.Subtract(sha512_t);

        //Convert into ascii
        byte[] md5_ascii = Encoding.ASCII.GetBytes(Convert.ToBase64String(md5));
        byte[] sha1_ascii = Encoding.ASCII.GetBytes(Convert.ToBase64String(sha1));
        byte[] sha256_ascii = Encoding.ASCII.GetBytes(Convert.ToBase64String(sha256));
        byte[] sha512_ascii = Encoding.ASCII.GetBytes(Convert.ToBase64String(sha512));

        //Convert into Hex
        byte[] md5_byte = Encoding.UTF8.GetBytes(Convert.ToBase64String(md5));
        string md5_hex = Convert.ToHexString(md5_byte);
        byte[] sha1_byte = Encoding.UTF8.GetBytes(Convert.ToBase64String(sha1));
        string sha1_hex = Convert.ToHexString(sha1_byte);
        byte[] sha256_byte = Encoding.UTF8.GetBytes(Convert.ToBase64String(sha256));
        string sha256_hex = Convert.ToHexString(sha256_byte);
        byte[] sha512_byte = Encoding.UTF8.GetBytes(Convert.ToBase64String(sha512));
        string sha512_hex = Convert.ToHexString(sha512_byte);

        //Write results to console
        Console.WriteLine("Md5 Plaintext: " + Convert.ToBase64String(md5));
        Console.WriteLine("Md5 ASCII: " + Convert.ToBase64String(md5_ascii));
        Console.WriteLine("Md5 HEX: " + md5_hex);
        Console.WriteLine("MD5 Resolved Timespan: " + md5_value + "\n");

        Console.WriteLine("Sha1: " + Convert.ToBase64String(sha1));
        Console.WriteLine("Sha1 ASCII: " + Convert.ToBase64String(sha1_ascii));
        Console.WriteLine("Sha1 HEX: " + sha1_hex);
        Console.WriteLine("SHA1 Resolved Timespan: " + sha1_value + "\n");

        Console.WriteLine("Sha256: " + Convert.ToBase64String(sha256));
        Console.WriteLine("Sha256 ASCII: " + Convert.ToBase64String(sha256_ascii));
        Console.WriteLine("Sha256 HEX: " + sha256_hex);

        Console.WriteLine("SHA256 Resolved Timespan: " + sha256_value + "\n");

        Console.WriteLine("Sha512: " + Convert.ToBase64String(sha512));
        Console.WriteLine("Sha512 ASCII: " + Convert.ToBase64String(sha512_ascii));
        Console.WriteLine("Sha512 HEX: " + sha512_hex);

        Console.WriteLine("SHA512 Resolved Timespan: " + sha512_value + "\n");

    }

    public static byte[] GenerateKey(int KeySize)
    {
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        var randomNumber = new byte[KeySize];
        randomNumberGenerator.GetBytes(randomNumber);

        return randomNumber;
    }
    public static byte[] Sha256(byte[] textToHash, byte[]? key)
    {
        if(key != null)
        {
            using var hmac = new HMACSHA256(key);
            return hmac.ComputeHash(textToHash);
        }
        else
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(textToHash);
        }
    }
    public static byte[] Sha1(byte[] textToHash, byte[]? key)
    {
        if (key != null)
        {
            using var hmac = new HMACSHA1(key);
            return hmac.ComputeHash(textToHash);
        }
        else
        {
            using var sha1 = SHA1.Create();
            return sha1.ComputeHash(textToHash);
        }
        
    }
    public static byte[] Sha512(byte[] textToHash, byte[]? key)
    {
        if (key != null)
        {
            using var hmac = new HMACSHA512(key);
            return hmac.ComputeHash(textToHash);
        }
        else
        {
            using var sha512 = SHA512.Create();
            return sha512.ComputeHash(textToHash);
        }
        
    }
    public static byte[] Md5(byte[] textToHash, byte[]? key)
    {
        if (key != null)
        {
            using var hmac = new HMACMD5(key);
            return hmac.ComputeHash(textToHash);
        }
        else
        {
            using var md5 = MD5.Create();
            return md5.ComputeHash(textToHash);
        }
        
    }
}