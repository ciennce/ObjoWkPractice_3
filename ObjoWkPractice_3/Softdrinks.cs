namespace ObjoWkPractice_3 
{
    class Softdrinks
    {
        public int Price { get; set; }
        public int Volume { get; set; }
        public string Name { get; set; }

        public Softdrinks(int Price, int Volume, string Name)
        {
            this.Price = Price;
            this.Volume = Volume;
            this.Name = Name;
        }
    }
}
