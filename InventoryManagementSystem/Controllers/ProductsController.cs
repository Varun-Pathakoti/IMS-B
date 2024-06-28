using Microsoft.AspNetCore.Mvc;
using IMSBusinessLogic;
using IMSDomain;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController: Controller
    {

        private readonly IInventory _Inventory;
        public ProductsController(IInventory inventory)
        {
            _Inventory = inventory;
        }


        //public List<Product>
        [HttpGet]
        public async Task<IActionResult> Getallpro()
        {
            var pro1 =await _Inventory.getAll();
            return Ok(pro1);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Getproductpro(int id)
        {
            var pro2 =await _Inventory.getbyId(id);
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
            var pro4=await _Inventory.create(prod);
            return Ok(pro4);
            

        }
        [HttpPut]
        [Route("/update/{id}/{stock}")]
        public async Task<IActionResult> Update(int id,int stock)
        {
             await _Inventory.update(id,stock);
            return Ok();
        }
        [HttpPut]
        [Route("/recordsale/id/{id}/quantity/{quantity}")]
        public async Task<IActionResult> Sale(int id,int quantity)
        {
            await _Inventory.RecordSale(id,quantity);
            return Ok();
        }
        [HttpGet]
        [Route("/generatereport")]
        public async Task<IActionResult> GenRep()
        {
            var k=await _Inventory.GenerateReport();
            return Ok(k);
        }
    }
}
