namespace ObjoWkPractice_3.Model 
{
    class Softdrinks
    {
        public int Price { get;}
        public int Volume { get;}
        public string Name { get;}

        public Softdrinks(int price, int volume, string name)
        {
            Price = price;
            Volume = volume;
            Name = name;
        }
    }
}
