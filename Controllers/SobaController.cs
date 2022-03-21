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
    public class SobaController : ControllerBase
    {
         public MainContext Context { get; set; }

        public SobaController(MainContext context)
        {
            Context = context;
        }

        [Route("GetSobe/{PrenocisteID}")]
        [HttpGet]

        public async Task<ActionResult> GetSobe([FromRoute] int PrenocisteID)
        {
            try
            {

                var sobice = await Context.Sobe.Include(p => p.Prenociste).
                                                Where((p => p.Prenociste.ID == PrenocisteID ))
                                                .ToListAsync();
                return Ok(sobice);
            }
            catch (Exception e)
            {
                
               return BadRequest(e.Message);
            }
        }

        [Route("GetZakazivanja/{SobaId}")]
        [HttpGet]

        public async Task<ActionResult> GetZakazivanja([FromRoute] int SobaId)
        {
            try
            {
                    
                var zakazivanja = await Context.Zakazivanja.Include(p => p.Soba).Include(p => p.Korisnik)
                                                .Where((p => p.Soba.ID == SobaId ))
                                                .Select(p => new {
                                                    startTime = p.StartTime,
                                                    endTime = p.EndTime,
                                                    Ime = p.Korisnik.Ime,
                                                    Prezime = p.Korisnik.Prezime,
                                                    Email = p.Korisnik.Email
                                                })
                                                .ToListAsync();
                return Ok(zakazivanja);
            }
            catch (Exception e)
            {
                
               return BadRequest(e.Message);
            }
        }

        [Route("DODAJSOBU")]
        [HttpPost]

        public async Task<ActionResult> DODAJSOBU([FromBody] Soba obj)
        {
        
           try
           {
               Context.Sobe.Add(obj);
               await Context.SaveChangesAsync();
               return Ok("Uspešno se rezervisali sobu!");
           }
           catch (Exception e)
           {
               return BadRequest(e.Message);
           }
        }

    }

}