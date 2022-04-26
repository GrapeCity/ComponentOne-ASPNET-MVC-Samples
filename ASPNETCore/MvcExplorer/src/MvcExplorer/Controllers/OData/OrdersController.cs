#if ODATA_SERVER
#if NETCORE10
using ODataController = Microsoft.AspNetCore.Mvc.Controller;
using Microsoft.AspNetCore.OData;
#elif NETCORE20
using Microsoft.AspNet.OData;
#endif
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcExplorer.Models;
using System.Linq;
using System.Threading.Tasks;


namespace MvcExplorer.Controllers
{
#if !NETCORE31
#if NETCORE10
    [Route("MyNorthWind/Orders")]
#endif
    public class OrdersController : ODataController
    {
        private readonly C1NWindEntities _db;

        public OrdersController(C1NWindEntities db)
        {
            _db = db;
        }

        // GET: odata/Orders
#if NETCORE10
        [HttpGet]
#endif
        [EnableQuery]
        public IQueryable<Order> GetOrders()
        {
            return _db.Orders;
        }

        // GET: odata/Orders(5)
#if NETCORE10
        [Route("{id}")]
        public SingleResult<Order> GetOrder(int id)
#else
        public SingleResult<Order> GetOrder([FromODataUri] int key)
#endif
        {
#if NETCORE10
            var orderId = id;
#else
            var orderId = key;
#endif
            return SingleResult.Create(_db.Orders.Where(order => order.OrderID == orderId));
        }

        // PUT: odata/Orders(5)
#if NETCORE10
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Order patch)
#else
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody]Delta<Order> patch)
#endif
        {
#if NETCORE10
            var orderId = id;
#else
            var orderId = key;
#endif
            return await Update(orderId, patch);
        }

        // POST: odata/Orders
#if NETCORE10
        [HttpPost]
#endif
        public async Task<IActionResult> Post([FromBody]Order order)

        {
#if !NETCORE10
            if (!TryValidateModel(order))
            {
                return BadRequest(ModelState);
            }
#endif
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
#if NETCORE10
            var req = HttpContext.Request;
            var locationUri = $"{req.Protocol}://{req.Host}/{req.Path}(OrderID={order.OrderID})";
            return Created(locationUri, order);
#else
            return Created(order);
#endif
        }


        // PATCH: odata/Orders(5)
#if NETCORE10
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]Order patch)
#else
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody]Delta<Order> patch)
#endif
        {
#if NETCORE10
            var orderId = id;
#else
            var orderId = key;
#endif
            return await Update(orderId, patch);
        }

#if NETCORE10
        private async Task<IActionResult> Update(int id, Order patch)
#else
        private async Task<IActionResult> Update(int id, Delta<Order> patch)
#endif
        {
            var order = await FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
#if NETCORE10
            _db.Entry(patch as object).State = EntityState.Modified;
#else
            if (!TryValidateModel(patch.GetInstance()))
            {
                return BadRequest(ModelState);
            }
            patch.Patch(order);
#endif
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
#if NETCORE10
            return NoContent();
#else
            return Updated(order);
#endif
        }

        // DELETE: odata/Orders(5)
#if NETCORE10
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
#else
        public async Task<IActionResult> Delete([FromODataUri] int key)
#endif
        {
#if NETCORE10
            var orderId = id;
#else
            var orderId = key;
#endif
            var order = await FindAsync(orderId);
            if (order == null)
            {
                return NotFound();
            }

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
            return new NoContentResult();
        }

        private bool OrderExists(int id)
        {
            return _db.Orders.Count(e => e.OrderID == id) > 0;
        }

        private async Task<Order> FindAsync(int id)
        {
#if NETCORE10
            var order = await _db.Orders.Where(c => c.OrderID == id).AsNoTracking().FirstOrDefaultAsync();
#else
            var order = await _db.Orders.FindAsync(id);
#endif
            return order;
        }
    }
#else
    public class OrdersController : Controller
    {
    }
#endif
}
#endif