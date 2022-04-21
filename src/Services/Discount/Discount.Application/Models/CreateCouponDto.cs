namespace Discount.Application.Models
{
    public class CreateCouponDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
    }
}
