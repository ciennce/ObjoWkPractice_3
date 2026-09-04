namespace ObjoWkPractice_3.Refactored.Ui;

/// <summary>
/// Robuste Konsoleneingaben an einer Stelle. Fragt so lange nach,
/// bis die Eingabe gültig ist – kein <see cref="FormatException"/> mehr bei Tippfehlern.
/// </summary>
public static class ConsoleInput
{
    public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            var raw = Console.ReadLine();

            if (int.TryParse(raw, out var value) && value >= min && value <= max)
                return value;

            Console.WriteLine(min == int.MinValue && max == int.MaxValue
                ? "Bitte eine ganze Zahl eingeben."
                : $"Bitte eine ganze Zahl zwischen {min} und {max} eingeben.");
        }
    }

    public static string ReadNonEmpty(string prompt)
    {
        while (true)
        {
            Console.Write($"{prompt}: ");
            var raw = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(raw))
                return raw.Trim();

            Console.WriteLine("Eingabe darf nicht leer sein.");
        }
    }

    public static TEnum ReadEnum<TEnum>(string prompt) where TEnum : struct, Enum
    {
        while (true)
        {
            var value = ReadInt(prompt);
            if (Enum.IsDefined(typeof(TEnum), value))
                return (TEnum)Enum.ToObject(typeof(TEnum), value);

            Console.WriteLine("Unbekannte Auswahl.");
        }
    }

    public static void WaitForKey()
    {
        Console.WriteLine();
        Console.WriteLine("Weiter mit beliebiger Taste...");

        // ReadKey braucht eine echte Konsole; bei umgeleiteter Eingabe (Pipe, Test) auf ReadLine ausweichen.
        if (Console.IsInputRedirected)
            Console.ReadLine();
        else
            Console.ReadKey(intercept: true);
    }

    /// <summary>Löscht den Bildschirm – nur wenn wirklich eine Konsole dran hängt, sonst wirft Clear().</summary>
    public static void ClearScreen()
    {
        if (!Console.IsOutputRedirected)
            Console.Clear();
    }
}
