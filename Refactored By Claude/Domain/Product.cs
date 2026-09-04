namespace ObjoWkPractice_3.Refactored.Domain;

/// <summary>
/// Basisklasse für alles, was der Automat verkaufen kann.
/// Die Id gehört zum Produkt selbst – kein Wrapper nötig.
/// </summary>
public abstract class Product
{
    protected Product(int id, string name, int priceInCents)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Id muss größer als 0 sein.");
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name darf nicht leer sein.", nameof(name));
        if (priceInCents <= 0)
            throw new ArgumentOutOfRangeException(nameof(priceInCents), "Preis muss größer als 0 sein.");

        Id = id;
        Name = name.Trim();
        PriceInCents = priceInCents;
    }

    public int Id { get; }
    public string Name { get; }
    public int PriceInCents { get; }

    /// <summary>Einzeilige Beschreibung für die Anzeige. Jede Produktart formuliert sie selbst.</summary>
    public abstract string Describe();
}
