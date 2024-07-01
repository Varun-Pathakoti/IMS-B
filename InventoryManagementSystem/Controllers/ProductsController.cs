using Microsoft.AspNetCore.Mvc;
using IMSBusinessLogic;
using IMSDomain;
using IMSDataAccess;
using MediatR;
using IMSBusinessLogic.MediatR.Queries;
using IMSBusinessLogic.MediatR.Commands;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var pro2 = await mediator.Send(new GetByIdQuery(id));
            if (pro2 == null)
            {
                return NotFound();
            }
            return Ok(pro2);
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
             await mediator.Send(new UpdateCommand(id,stock));
            return Ok();
        }
        [HttpPut]
        [Route("/recordsale/id/{id}/quantity/{quantity}")]
        public async Task<IActionResult> Sale(int id,int quantity)
        {
            
            var product =await mediator.Send(new RecordSaleQuery(id,quantity));
            if (product.StockLevel < product.Threshold)
            {
                Email email = new Email();
                await email.SendLowStockEmail("dasasaipooja@gmail.com", "Low Stock Alert", product.ProductName);

            }

            return Ok();
        }
        [HttpGet]
        [Route("/generatereport")]
        public async Task<IActionResult> GenRep()
        {
            var k=await mediator.Send(new GenerateReportQuery());

            Email email = new Email();
            var productsList = string.Join(", ", k.Product.Select(p => $"{p.ProductName} (Stock: {p.StockLevel})"));
      
            var fastMovingList = string.Join(", ", k.FastMovingProducts);
            var slowMovingList = string.Join(", ", k.SlowMovingProducts);



            var str= $"Products: {productsList}\nFast Moving Products: {fastMovingList}\nSlow Moving Products: {slowMovingList}";
            await email.SendReportEmail("dasasaipooja@gmail.com", "Generate report", str);
            return Ok(k);
        }
    }
}
