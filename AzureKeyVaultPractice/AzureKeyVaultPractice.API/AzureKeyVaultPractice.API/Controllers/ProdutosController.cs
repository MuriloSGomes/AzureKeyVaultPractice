using AzureKeyVaultPractice.API.Entities;
using AzureKeyVaultPractice.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AzureKeyVaultPractice.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repository;
        public ProdutosController(IProdutoRepository repository)
        {
            _repository = repository ?? throw new ArgumentException(nameof(repository));
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Produto>> GetDiscount(string productName)
        {
            var coupon = await _repository.GetProduto(productName);

            return Ok(coupon);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Produto>> CreateDiscount([FromBody] Produto produto)
        {
            await _repository.CreateProduto(produto);

            return CreatedAtRoute("GetDiscount", new { productName = produto.Nome }, produto);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Produto>> UpdateDiscount([FromBody] Produto produto)
        {
            return Ok(await _repository.UpdateProduto(produto));
        }

        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(Produto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Produto>> DeleteDiscount(string productName)
        {
            return Ok(await _repository.DeleteProduto(productName));
        }
    }
}
