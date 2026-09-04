using ObjoWkPractice_3.Refactored.Domain;
using ObjoWkPractice_3.Refactored.Ui;

namespace ObjoWkPractice_3.Refactored;

internal static class Program
{
    private static void Main()
    {
        var machine = new VendingMachine();
        var wallet = new Wallet();

        new ConsoleUi(machine, wallet).Run();
    }
}
