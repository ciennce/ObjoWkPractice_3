namespace ObjoWkPractice_3.Refactored.Domain;

/// <summary>
/// Kennt seinen Warenbestand und wickelt Käufe ab.
/// Enthält bewusst keine Konsolen-Ein-/Ausgabe – dafür ist die UI-Schicht zuständig.
/// Dadurch ist die Klasse ohne Konsole testbar.
/// </summary>
public sealed class VendingMachine
{
    private readonly List<Product> _products = new();

    public IReadOnlyCollection<Product> Products => _products;

    public Product? FindById(int productId)
        => _products.FirstOrDefault(p => p.Id == productId);

    public void AddProduct(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (FindById(product.Id) is not null)
            throw new InvalidOperationException($"Es gibt bereits ein Produkt mit Id {product.Id}.");

        _products.Add(product);
    }

    public bool RemoveProduct(int productId)
    {
        var product = FindById(productId);
        if (product is null)
            return false;

        _products.Remove(product);
        return true;
    }

    public PurchaseResult Buy(int productId, Wallet wallet)
    {
        ArgumentNullException.ThrowIfNull(wallet);

        var product = FindById(productId);
        if (product is null)
            return PurchaseResult.NotFound();

        if (!wallet.CanAfford(product.PriceInCents))
            return PurchaseResult.NotEnoughMoney(product);

        wallet.Deduct(product.PriceInCents);
        _products.Remove(product);
        return PurchaseResult.Ok(product);
    }
}
