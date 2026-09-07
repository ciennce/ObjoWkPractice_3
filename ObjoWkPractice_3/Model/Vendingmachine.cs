namespace ObjoWkPractice_3.Model
{
    class Vendingmachine
    {
        public class VendingItem
        {
            public required Softdrinks Softdrink { get; set; }
            public int Id { get; set; }
        }

        List<VendingItem> items = new List<VendingItem>();

        public int Id { get; set; }

        public void DeleteItem(int id)
        {
            var itemToRemove = items.FirstOrDefault(item => item.Id == id);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
                Console.WriteLine($"Item with Id {id} has been removed.");
            }
            else
            {
                Console.WriteLine($"Item with Id {id} not found.");
            }
        }

        public void AddItem(Softdrinks softdrinks, int id)
        {
            items.Add(new VendingItem { Softdrink = softdrinks, Id = id });
        }

        public void ShowAllitems()
        {
            foreach (var item in items)
            {
                Console.WriteLine($"Id: {item.Id}, Item: {item.Softdrink.Name}, Price: {item.Softdrink.Price}c, Volume: {item.Softdrink.Volume}ml");
            }
        }

        public VendingItem? GetItemById(int id)
        {
            foreach (var item in items)
            {
                if (item.Id == id)
                    return item;
            }
            return null;
        }

        public bool BuyItem(Cash cash)
        {
            Console.WriteLine("Which item would you like to buy?");
            ShowAllitems();
            int itemId = Convert.ToInt32(Console.ReadLine());

            var item = GetItemById(itemId);
            if (item == null)
            {
                Console.WriteLine("Item not found.");
                return false;
            }
            else
            {
                if (item.Softdrink.Price > cash.Amount)
                {
                    Console.WriteLine("Not enough money.");
                    return false;
                }
                else
                {
                    cash.Amount -= item.Softdrink.Price;
                    Console.WriteLine($"You bought {item.Softdrink.Name} for {item.Softdrink.Price}c.");
                    DeleteItem(itemId);
                }
            }
            return true;
        }

        public void AddCash(Cash cash)
        {
            Console.WriteLine("How much money do you want to insert? (in cents)");
            int amount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"You have inserted {amount} cents.");
            cash.Amount += amount;
        }

        public static void AddItem(Vendingmachine vendingmachine)
        {
            Console.WriteLine("Add Item Name to the Vending Machine");
            string name = Console.ReadLine() ?? "";
            Console.Clear();

            Console.WriteLine("Add Price of the Item");
            int price = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Add Volume of the Item");
            int volume = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Add Id of the Item");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Softdrinks softdrinks = new Softdrinks(price, volume, name);
            vendingmachine.AddItem(softdrinks, id);

            Console.WriteLine($"Item Added:{softdrinks.Name}, Price: {softdrinks.Price}c {softdrinks.Volume}ml");
        }

        public static int UserInput(Cash cash)
        {
            Console.WriteLine("ADD ITEM [1]");
            Console.WriteLine("BUY ITEM [2]");
            Console.WriteLine("SHOW ALLITEMS [3]");
            Console.WriteLine("ADD CASH [4]");
            Console.WriteLine("EXIT [5]");
            Console.WriteLine($"Wallet: {cash.Amount}c");
            int x = Convert.ToInt32(Console.ReadKey().KeyChar.ToString());
            return x;
        }
    }
}
