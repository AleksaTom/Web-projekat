using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace WEBPROJ.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrenocisteController : ControllerBase
    {
         public MainContext Context { get; set; }

        public PrenocisteController(MainContext context)
        {
            Context = context;
        }

        [Route("GetPrenocista")]
        [HttpGet]

        public async Task<ActionResult> GetPrenocista()
        {
            try
            {
                var spcentri = await Context.Prenocista.ToListAsync();
                return Ok(spcentri);
            }
            catch (Exception e)
            {
                
               return BadRequest(e.Message);
            }
        }

        [Route("DodajPrenociste")]
        [HttpPost]

        public async Task<ActionResult> DodajPrenociste([FromBody] Prenociste obj)
        {
          
           try
           {
               Context.Prenocista.Add(obj);
               await Context.SaveChangesAsync();
               return Ok("Prenoćiste uspešno dodato!");
           }
           catch (Exception e)
           {
               return BadRequest(e.Message);
           }
        }

    }

}