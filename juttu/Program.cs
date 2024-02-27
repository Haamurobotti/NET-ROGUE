namespace juttu
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter player name");
            //var name = Console.ReadLine();
            var name = Faker.Name.First();
            Console.WriteLine($"Hello, {name}!");
        }
    }
}