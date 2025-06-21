namespace Domain.Models.BasketModule
{
    //i not inherit from base class here as i not need to store that model in db 
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string PictureURL { get; set; }
        public decimal  Price{ get; set; }
        public int Quantity { get; set; }

    }
}