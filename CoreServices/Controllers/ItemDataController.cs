using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DB_Activities;

namespace CoreServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemDataController : ControllerBase
    {
        private readonly ILogger<ItemDataController> _logger;

        public ItemDataController(ILogger<ItemDataController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] Item model)
        {
            Dataaccess db = new Dataaccess();
            if (ModelState.IsValid)
            {
                try
                {
                    var postId = await db.ItemInsert(model);
                    if (postId > 0)
                    {
                        return Ok(postId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }


        [HttpGet("GetItems")]
        public List<Item> GetAllItems()
        {
            Dataaccess db = new Dataaccess();
            return db.GetAllItems();
        }

        [HttpGet("GetItem")]
        public Item GetSelectedItem(int itemId)
        {
            Dataaccess db = new Dataaccess();
            return db.GetSelectedItem(itemId);
        }

        [HttpPut]
        public Item Put([FromForm] Item res) => UpdateItemData(res);

        private Item UpdateItemData(Item res)
        {
            Dataaccess db = new Dataaccess();
            db.UpdateItem(res);
            return res;
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => DeleteItem(id);

        private void DeleteItem(int id)
        {
            Dataaccess db = new Dataaccess();
            db.DeleteItem(id);
        }
    }
}
