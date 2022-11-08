
using Kryptering;
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

