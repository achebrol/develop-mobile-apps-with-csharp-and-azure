using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Tailwinds.OData.Models;

namespace Tailwinds.OData.Controllers
{
    public class AlbumsController : ODataController
    {
        private TailwindsContext context;

        public AlbumsController(TailwindsContext context)
        {
            this.context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(context.Albums);
        }

        [EnableQuery]
        public IActionResult Get(string key)
        {
            return Ok(context.Albums.FirstOrDefault(c => c.Id == key));
        }

        public async Task<IActionResult> Post([FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await context.Albums.AddAsync(album);
            await context.SaveChangesAsync();
            return Created(album);
        }

        public async Task<IActionResult> Put([FromODataUri] string key, [FromBody] Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != album.Id)
            {
                return BadRequest();
            }
            context.Entry(album).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(album);
        }

        public async Task<IActionResult> Delete([FromODataUri] string key)
        {
            var album = await context.Albums.FindAsync(key);
            if (album == null)
            {
                return NotFound();
            }
            context.Albums.Remove(album);
            await context.SaveChangesAsync();
            return StatusCode((int)HttpStatusCode.OK);
        }

        private bool AlbumExists(string key)
        {
            return context.Albums.Any(x => x.Id == key);
        }
    }
}
