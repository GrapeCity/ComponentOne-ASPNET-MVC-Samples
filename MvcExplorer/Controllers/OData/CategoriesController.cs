using MvcExplorer.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;

namespace MvcExplorer.Controllers
{
    public class CategoriesController : ODataController
    {
        private C1NWindEntitiesOData db = new C1NWindEntitiesOData();

        // GET: odata/Categories
        [EnableQuery]
        public IQueryable<Category> GetCategories()
        {
            return db.Categories;
        }

        // GET: odata/Categories(5)
        [EnableQuery]
        public SingleResult<Category> GetCategory([FromODataUri] int key)
        {
            return SingleResult.Create(db.Categories.Where(category => category.CategoryID == key));
        }

        // PUT: odata/Categories(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<Category> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await db.Categories.FindAsync(key);
            if (category == null)
            {
                return NotFound();
            }

            patch.Put(category);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(category);
        }

        // POST: odata/Categories
        public async Task<IHttpActionResult> Post(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            await db.SaveChangesAsync();

            return Created(category);
        }

        // PATCH: odata/Categories(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<Category> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await db.Categories.FindAsync(key);
            if (category == null)
            {
                return NotFound();
            }

            patch.Patch(category);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(category);
        }

        // DELETE: odata/Categories(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            var category = await db.Categories.FindAsync(key);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int key)
        {
            return db.Categories.Count(e => e.CategoryID == key) > 0;
        }
    }
}