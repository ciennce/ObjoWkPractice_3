using ObjoWkPractice_3.Refactored.Domain;

namespace ObjoWkPractice_3.Refactored.Ui;

/// <summary>
/// Die gesamte Konsoleninteraktion. Übersetzt Benutzereingaben in Domänenaufrufe
/// und Domänenergebnisse zurück in Text. Hier – und nur hier – steht <c>Console.*</c>.
/// </summary>
public sealed class ConsoleUi
{
    private readonly VendingMachine _machine;
    private readonly Wallet _wallet;

    public ConsoleUi(VendingMachine machine, Wallet wallet)
    {
        _machine = machine;
        _wallet = wallet;
    }

    public void Run()
    {
        var running = true;
        while (running)
        {
            ConsoleInput.ClearScreen();
            PrintMenu();

            var choice = ConsoleInput.ReadEnum<MenuOption>("Auswahl");
            ConsoleInput.ClearScreen();

            switch (choice)
            {
                case MenuOption.AddItem:
                    AddItem();
                    break;
                case MenuOption.BuyItem:
                    BuyItem();
                    break;
                case MenuOption.ShowItems:
                    ShowItems();
                    break;
                case MenuOption.AddCash:
                    AddCash();
                    break;
                case MenuOption.Exit:
                    running = false;
                    break;
            }

            if (running)
                ConsoleInput.WaitForKey();
        }

        Console.WriteLine("Bye!");
    }

    private void PrintMenu()
    {
        Console.WriteLine("=== Vending Machine ===");
        Console.WriteLine("[1] Add item");
        Console.WriteLine("[2] Buy item");
        Console.WriteLine("[3] Show all items");
        Console.WriteLine("[4] Add cash");
        Console.WriteLine("[5] Exit");
        Console.WriteLine($"Wallet: {_wallet.BalanceInCents}c");
        Console.WriteLine();
    }

    private void AddItem()
    {
        var name = ConsoleInput.ReadNonEmpty("Name");
        var price = ConsoleInput.ReadInt("Preis in Cent", min: 1);
        var volume = ConsoleInput.ReadInt("Volumen in ml", min: 1);
        var id = ConsoleInput.ReadInt("Id", min: 1);

        try
        {
            var drink = new Softdrink(id, name, price, volume);
            _machine.AddProduct(drink);
            Console.WriteLine($"Hinzugefügt: {drink.Describe()}");
        }
        catch (InvalidOperationException ex)
        {
            // Doppelte Id – Domänenregel verletzt, kein Programmierfehler.
            Console.WriteLine(ex.Message);
        }
    }

    private void BuyItem()
    {
        if (_machine.Products.Count == 0)
        {
            Console.WriteLine("Der Automat ist leer.");
            return;
        }

        ShowItems();
        Console.WriteLine();
        var id = ConsoleInput.ReadInt("Welche Id möchtest du kaufen?", min: 1);

        var result = _machine.Buy(id, _wallet);
        switch (result.Status)
        {
            case PurchaseStatus.Success:
                Console.WriteLine($"Gekauft: {result.Product!.Name} für {result.Product.PriceInCents}c. Restguthaben: {_wallet.BalanceInCents}c.");
                break;
            case PurchaseStatus.ProductNotFound:
                Console.WriteLine("Kein Produkt mit dieser Id.");
                break;
            case PurchaseStatus.InsufficientFunds:
                Console.WriteLine($"Zu wenig Guthaben: {result.Product!.Name} kostet {result.Product.PriceInCents}c, du hast {_wallet.BalanceInCents}c.");
                break;
        }
    }

    private void ShowItems()
    {
        if (_machine.Products.Count == 0)
        {
            Console.WriteLine("(keine Produkte)");
            return;
        }

        foreach (var product in _machine.Products.OrderBy(p => p.Id))
            Console.WriteLine(product.Describe());
    }

    private void AddCash()
    {
        var amount = ConsoleInput.ReadInt("Betrag in Cent", min: 1);
        _wallet.Insert(amount);
        Console.WriteLine($"Eingeworfen: {amount}c. Guthaben: {_wallet.BalanceInCents}c.");
    }
}
