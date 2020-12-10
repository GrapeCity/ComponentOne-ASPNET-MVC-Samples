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
#if !NETCORE30 && !NET50
#if NETCORE10
    [Route("MyNorthWind/Categories")]
#endif
    public class CategoriesController : ODataController
    {
        private readonly C1NWindEntities _db;

        public CategoriesController(C1NWindEntities db)
        {
            _db = db;
        }

        // GET: odata/Categories
#if NETCORE10
        [HttpGet]
#endif
        [EnableQuery]
        public IQueryable<Category> GetCategories()
        {
            return _db.Categories;
        }

        // GET: odata/Categories(5)
#if NETCORE10
        [Route("{id}")]
        public SingleResult<Category> GetCategory(int id)
#else
        public SingleResult<Category> GetCategory([FromODataUri] int key)
#endif
        {
#if NETCORE10
            var categoryId = id;
#else
            var categoryId = key;
#endif
            return SingleResult.Create(_db.Categories.Where(category => category.CategoryID == categoryId));
        }

        // PUT: odata/Categories(5)
#if NETCORE10
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Category patch)
#else
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody]Delta<Category> patch)
#endif
        {
#if NETCORE10
            var categoryId = id;
#else
            var categoryId = key;
#endif
            return await Update(categoryId, patch);
        }

        // POST: odata/Categories
#if NETCORE10
        [HttpPost]
#endif
        public async Task<IActionResult> Post([FromBody]Category category)

        {
#if !NETCORE10
            if (!TryValidateModel(category))
            {
                return BadRequest(ModelState);
            }
#endif
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
#if NETCORE10
            var req = HttpContext.Request;
            var locationUri = $"{req.Protocol}://{req.Host}/{req.Path}(CategoryID={category.CategoryID})";
            return Created(locationUri, category);
#else
            return Created(category);
#endif
        }


        // PATCH: odata/Categories(5)
#if NETCORE10
        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody]Category patch)
#else
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IActionResult> Patch([FromODataUri] int key, [FromBody]Delta<Category> patch)
#endif
        {
#if NETCORE10
            var categoryId = id;
#else
            var categoryId = key;
#endif
            return await Update(categoryId, patch);
        }

#if NETCORE10
        private async Task<IActionResult> Update(int id, Category patch)
#else
        private async Task<IActionResult> Update(int id, Delta<Category> patch)
#endif
        {
            var category = await FindAsync(id);
            if (category == null)
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
            patch.Patch(category);
#endif
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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
            return Updated(category);
#endif
        }

        // DELETE: odata/Categories(5)
#if NETCORE10
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
#else
        public async Task<IActionResult> Delete([FromODataUri] int key)
#endif
        {
#if NETCORE10
            var categoryId = id;
#else
            var categoryId = key;
#endif
            var category = await FindAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return new NoContentResult();
        }

        private bool CategoryExists(int id)
        {
            return _db.Categories.Count(e => e.CategoryID == id) > 0;
        }

        private async Task<Category> FindAsync(int id)
        {
#if NETCORE10
            var category = await _db.Categories.Where(c => c.CategoryID == id).AsNoTracking().FirstOrDefaultAsync();
#else
            var category = await _db.Categories.FindAsync(id);
#endif
            return category;
        }
    }
#else
    public class CategoriesController : Controller
    {
    }
#endif
}
#endif