using ObjoWkPractice_3.Model;
using static ObjoWkPractice_3.Model.Vendingmachine;

namespace ObjoWkPractice_3.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Vending Machine!");

            Vendingmachine vendingmachine = new Vendingmachine();
            Cash cash = new Cash(0);

            while (true)
            {
                Console.Clear();
                int x = UserInput(cash);

                switch (x)
                {
                    case 1:
                        Console.Clear();
                        AddItem(vendingmachine);
                        break;

                    case 2:
                        Console.Clear();
                        vendingmachine.BuyItem(cash);
                        break;

                    case 3:
                        Console.Clear();
                        vendingmachine.ShowAllitems();
                        break;

                    case 4:
                        Console.Clear();
                        vendingmachine.AddCash(cash);
                        break;

                    case 5:
                        Environment.Exit(0);
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }       
    } 
}
