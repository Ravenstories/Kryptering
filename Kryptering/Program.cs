
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Write 1 for random numbers, 2 for caeser cypher");
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
            default:
                break;
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

//Caeser cipher encrypter..
class Encrypter
{
    public static char C_Cip(char ch, int key)
    {
        if (!char.IsLetter(ch))
        {
            return ch;
        }

        char d = char.IsUpper(ch) ? 'A' : 'a';
        return (char)((((ch + key) - d) % 26) + d);
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
    
    //Make better Prompt
    public static void Prompt()
    {
        Console.WriteLine("Type a string to encrypt:");
        string UserString = Console.ReadLine();

        Console.WriteLine("\n");

        Console.Write("Enter your Key");
        int key = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("\n");


        Console.WriteLine("Encrypted Data");

        string cipherText = C_Encip(UserString, key);
        Console.WriteLine(cipherText);
        Console.Write("\n");

        Console.WriteLine("Decrypted Data:");

        string t = C_Decip(cipherText, key);
        Console.WriteLine(t);
        Console.Write("\n");

        Console.ReadKey();
    }
}

class Hashing
{
    public static void GetHash()
    {
        MD5 md5 = MD5.Create();
        var rnd = RandomNumberGenerator.Create();
        byte[] input = new byte[20];
        byte[] hashValue;

        rnd.GetBytes(input);
       
        hashValue = md5.ComputeHash(input);
    }
    public static void FilestreamHash()
    {
        MD5 md5 = MD5.Create();
        byte[] hashValue;

        FileStream fileStream = new FileStream("C:", FileMode.Open);
        hashValue= md5.ComputeHash(fileStream);
    }

}