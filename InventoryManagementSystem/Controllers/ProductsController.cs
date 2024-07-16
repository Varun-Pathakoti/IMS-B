using Microsoft.AspNetCore.Mvc;
using IMSBusinessLogic;
using IMSDomain;
using IMSDataAccess;
using MediatR;
using IMSBusinessLogic.MediatR.Queries;
using IMSBusinessLogic.MediatR.Commands;
using IMSDataAccess.Exceptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.Identity.Client;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowSpecificOrigin")]
    public class ProductsController: Controller
    {

        private readonly IMediator mediator;

       
        public ProductsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        //public List<Product>
        [HttpGet]
        public async Task<IActionResult> Getallpro()
        {
            var pro1=await mediator.Send(new GetAllProductsQuery());
            //var pro1 =await _Inventory.getAll();
            return Ok(pro1);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Getproductpro(int id)
        {
            try
            {
                var pro2 = await mediator.Send(new GetByIdQuery(id));
                return Ok(pro2);
            }
            catch (ProductNotFoundException)
            {
                return Ok("product id is invalid");
            }
            catch (Exception)
            {
                return Ok("error");
            }
        }
       [HttpPost]
        [Route("/create")]
        public async Task<IActionResult> Create([FromBody] Product prod)
        {
            var pro4 = await mediator.Send(new CreateCommand(prod.ProductName,prod.Description,prod.Price,prod.Threshold,prod.StockLevel));
            return Ok(pro4);
            

        }
        [HttpPut]
        [Route("/update/{id}/{stock}")]
        public async Task<IActionResult> Update(int id,int stock)
        {
            try
            {
                var pro = await mediator.Send(new UpdateCommand(id, stock));
              
                return Ok(pro);
            }
            catch(NegativeNumerException) 
            {
                return Ok("entered value should be positive");
            }
            catch(ProductNotFoundException)
            {
                return Ok("product not found");
            }
            catch (Exception)
            {
                return Ok("error");
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
            var k=await mediator.Send(new GenerateReportQuery());

            Email email = new Email();
            
            await email.Emailmet("dasasaipooja@gmail.com", "Generate report", k);
            return Ok(k);
        }
        [HttpGet]
        [Route("/salestable")]
        public async Task<IActionResult> GetallSales()
        {
            var sales=await mediator.Send(new GetallsalesQuery());
            return Ok(sales);
        }
        [HttpDelete]
        [Route("/delete/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            await mediator.Send(new deleteByIdCommand(id));
            return Ok();
        }
        [HttpGet]
        [Route("/getbyname/{name}")]
        public async Task<IActionResult> getByName(string name)
        {
            var product = await mediator.Send(new GetbynameQuery(name));
            return Ok(product);
        }
    }
}
