
using Dapper;
using System.Threading.Tasks;

namespace Discount.Grpc
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DiscountDbContext _dbContext;

        public DiscountRepository(DiscountDbContext dbContext) => _dbContext = dbContext;

        public async Task<bool> CreateDiscountAsync(Coupon coupon)
        {
            using var connection = _dbContext.Connection;

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                            new { ProductName = coupon.ProductName,
                                Description = coupon.Description,
                                Amount = coupon.Amount });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> UpdateDiscountAsync(Coupon coupon)
        {
            using var connection = _dbContext.Connection;

            var affected = await connection.ExecuteAsync
                    ("UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                            new { ProductName = coupon.ProductName,
                                Description = coupon.Description,
                                Amount = coupon.Amount,
                                Id = coupon.Id });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteDiscountAsync(string productName)
        {
            using var connection = _dbContext.Connection;

            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<Coupon> GetDiscountAsync(string productName)
        {
            var query = "SELECT * FROM Coupon WHERE productName=@productName";
            using var connection = _dbContext.Connection;

            return
                await
                connection
                .QueryFirstOrDefaultAsync<Coupon>(query, new { ProductName = productName});
               
        }

    }
}
