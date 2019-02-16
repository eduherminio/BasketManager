namespace BasketManager.Model
{
    public abstract class Item
    {
        public string Id { get; set; }

        public int Quantity { get; set; }

        public Color Color { get; set; }
    }
}
