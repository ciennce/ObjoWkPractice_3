namespace ObjoWkPractice_3.Refactored.Domain;

public enum PurchaseStatus
{
    Success,
    ProductNotFound,
    InsufficientFunds,
}

/// <summary>
/// Ergebnis eines Kaufversuchs. Reine Daten – wie das dem Benutzer gezeigt wird,
/// entscheidet die UI, nicht die Domäne.
/// </summary>
public sealed record PurchaseResult(PurchaseStatus Status, Product? Product)
{
    public static PurchaseResult NotFound() => new(PurchaseStatus.ProductNotFound, null);
    public static PurchaseResult NotEnoughMoney(Product product) => new(PurchaseStatus.InsufficientFunds, product);
    public static PurchaseResult Ok(Product product) => new(PurchaseStatus.Success, product);
}
