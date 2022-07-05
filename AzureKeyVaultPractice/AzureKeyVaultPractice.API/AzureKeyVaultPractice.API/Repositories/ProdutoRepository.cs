using AzureKeyVaultPractice.API.Entities;
using Dapper;

namespace AzureKeyVaultPractice.API.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly IConfiguration _configuration;
        public ProdutoRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentException(nameof(configuration));
        }
        public async Task<bool> CreateProduto(Produto produto)
        {
            using var connection = new Npgsql.NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var queryInsert = @"INSERT INTO public.produto(
	                            nome, descricao, quantidade)
	                            VALUES (@Nome, @Descricao, @Quantidade)";

            var affected = await connection.ExecuteAsync(queryInsert, new { ProductName = produto.Nome, Description = produto.Descricao, Quantidade = produto.Quantidade });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteProduto(string productName)
        {
            using var connection = new Npgsql.NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var queryDelete = "DELETE FROM Produto WHERE Nome = @ProductName";

            var affected = await connection.ExecuteAsync(queryDelete, new { ProductName = productName });

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<Produto> GetProduto(string productName)
        {
            using var connection = new Npgsql.NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await connection.QueryFirstOrDefaultAsync<Produto>(@"SELECT id, productname, description, amount FROM public.coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            if (coupon is null)
            {
                return new Produto
                {
                    Nome = "No Discount",
                    Quantidade = 0,
                    Descricao = "No Discount Desc"
                };
            }

            return coupon;
        }

        public async Task<bool> UpdateProduto(Produto produto)
        {
            using var connection = new Npgsql.NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var queryUpdate = @"UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id";

            var affected = await connection.ExecuteAsync(queryUpdate, new { ProductName = produto.Nome, Description = produto.Descricao, Amount = produto.Quantidade });

            if (affected == 0)
                return false;

            return true;
        }
    }
}
