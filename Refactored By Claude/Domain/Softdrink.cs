namespace ObjoWkPractice_3.Refactored.Domain;

public sealed class Softdrink : Product
{
    public Softdrink(int id, string name, int priceInCents, int volumeInMl)
        : base(id, name, priceInCents)
    {
        if (volumeInMl <= 0)
            throw new ArgumentOutOfRangeException(nameof(volumeInMl), "Volumen muss größer als 0 sein.");

        VolumeInMl = volumeInMl;
    }

    public int VolumeInMl { get; }

    public override string Describe()
        => $"#{Id}  {Name} ({VolumeInMl} ml) – {PriceInCents}c";
}
