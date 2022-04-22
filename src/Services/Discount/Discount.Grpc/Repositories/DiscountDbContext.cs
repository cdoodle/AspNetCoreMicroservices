
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Grpc
{
    public class DiscountDbContext
    {
        private readonly IConfiguration _configuration;

        private readonly NpgsqlConnection _connection;
        public DiscountDbContext(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public NpgsqlConnection Connection {
            get 
            {
                return
                    _connection != null ?
                    _connection :
                    new NpgsqlConnection(_configuration.GetConnectionString("DiscountConnection"));
            }
        }
    }
}
