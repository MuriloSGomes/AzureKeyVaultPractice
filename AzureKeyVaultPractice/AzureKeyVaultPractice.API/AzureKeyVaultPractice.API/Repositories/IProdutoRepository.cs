using AzureKeyVaultPractice.API.Entities;

namespace AzureKeyVaultPractice.API.Repositories
{
    public interface IProdutoRepository
    {
        Task<Produto> GetProduto(string productName);
        Task<bool> DeleteProduto(string productName);
        Task<bool> CreateProduto(Produto produto);
        Task<bool> UpdateProduto(Produto produto);
    }
}
