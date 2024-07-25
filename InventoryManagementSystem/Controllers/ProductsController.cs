using Microsoft.AspNetCore.Mvc;
using IMSBusinessLogic;
using IMSDataAccess;
using MediatR;
using IMSBusinessLogic.MediatR.Queries;
using IMSBusinessLogic.MediatR.Commands;
using IMSDataAccess.Exceptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.Identity.Client;
using IMSDomain.DTO;
using IMSDomain.Entities;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class ProductsController : Controller
    {

        private readonly IMediator mediator;


        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        //public List<Product>
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var product = await mediator.Send(new GetAllProductsQuery());
            //var pro1 =await _Inventory.getAll();
            return Ok(product);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var product = await mediator.Send(new GetByIdQuery(id));
                return Ok(product);
            }
            catch (ProductNotFoundException)
            {
                return BadRequest("product id is invalid");
            }
            catch (Exception)
            {
                return BadRequest("error");
            }
        }
        [HttpPost]
        [Route("/create")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            try
            {
                var _product = await mediator.Send(new CreateCommand(product.ProductName, product.Description,product.Image,product.Price, product.Threshold, product.StockLevel));
                return Ok(_product);
            }
            catch(Exception)
            {
                return BadRequest("Enter correct values");
            }


        }
        [HttpPut]
        [Route("/update/{id}/{stock}")]
        public async Task<IActionResult> Update(int id, int stock)
        {
            try
            {
                var pro = await mediator.Send(new UpdateCommand(id, stock));

                return Ok(pro);
            }
            catch (NegativeNumerException)
            {
                return BadRequest("entered value should be positive");
            }
            catch (ProductNotFoundException)
            {
                return BadRequest("product not found");
            }
            catch (Exception)
            {
                return BadRequest("error");
            }
        }
        [HttpPost]
        [Route("/recordsale")]

        public async Task<IActionResult> Sale(List<Order> sale)

        {
            //var salesList = sale.Select(s => (s.prodId, s.quantity)).ToList();
            var sales = await mediator.Send(new RecordSaleQuery(sale));


            return Ok(sales);
        }
        [HttpGet]
        [Route("/generatereport")]
        public async Task<IActionResult> GenRep()
        {
            var report = await mediator.Send(new GenerateReportQuery());

            Email email = new Email();

            await email.Emailmet("dasasaipooja@gmail.com", "Generate report", report);
            return Ok(report);
        }
        [HttpGet]
        [Route("/salestable")]
        public async Task<IActionResult> GetallSales()
        {
            var sales = await mediator.Send(new GetallSalesQuery());
            return Ok(sales);
        }
        [HttpDelete]
        [Route("/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteByIdCommand(id));
            return Ok();
        }
        [HttpGet]
        [Route("/getbyname/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            try {
                var product = await mediator.Send(new GetByNameQuery(name));
                return Ok(product);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO product)
        {
            try {
                var _product = await mediator.Send(new UpdateProductCommand(id, product));
                return Ok(_product);

            }
            catch (ProductNotFoundException)
            {
                return BadRequest($"product with id : {id} is not found");

            }
            catch (Exception)
            {
                return BadRequest("enter correct values");
            }
        }
    }
}
