namespace ObjoWkPractice_3.Refactored.Domain;

/// <summary>
/// Das Guthaben des Kunden in Cent.
/// Der Setter ist privat – Änderungen laufen nur über Methoden mit Prüfungen.
/// </summary>
public sealed class Wallet
{
    public Wallet(int startBalanceInCents = 0)
    {
        if (startBalanceInCents < 0)
            throw new ArgumentOutOfRangeException(nameof(startBalanceInCents), "Guthaben darf nicht negativ sein.");

        BalanceInCents = startBalanceInCents;
    }

    public int BalanceInCents { get; private set; }

    public bool CanAfford(int priceInCents) => priceInCents <= BalanceInCents;

    public void Insert(int amountInCents)
    {
        if (amountInCents <= 0)
            throw new ArgumentOutOfRangeException(nameof(amountInCents), "Betrag muss positiv sein.");

        BalanceInCents += amountInCents;
    }

    public void Deduct(int amountInCents)
    {
        if (amountInCents <= 0)
            throw new ArgumentOutOfRangeException(nameof(amountInCents), "Betrag muss positiv sein.");
        if (!CanAfford(amountInCents))
            throw new InvalidOperationException("Nicht genug Guthaben.");

        BalanceInCents -= amountInCents;
    }
}
