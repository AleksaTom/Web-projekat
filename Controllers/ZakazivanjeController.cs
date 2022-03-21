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
    public class ZakazivanjeController : ControllerBase
    {
         public MainContext Context { get; set; }

        public ZakazivanjeController(MainContext context)
        {
            Context = context;
        }


        [Route("GET_ZAKAZIVANJA")]
        [HttpGet]

        public async Task<ActionResult> PREUZMIZAKAZIVANJA()
        {
            try
            {
                var zakazivanja = await Context.Zakazivanja.ToListAsync();
                return Ok(zakazivanja);
            }
            catch (Exception e)
            {
                
               return BadRequest(e.Message);
            }
        }

        
        [Route("GET_SINGLE_ZAKAZIVANJA/{KorisnikID}")]
        [HttpGet]

        public async Task<ActionResult> PREUZMISINGLEZAKAZIVANJA([FromRoute] int KorisnikID)
        {

            if(KorisnikID < 0)
            {
                return BadRequest("Invalid ID!");
            }

            var user = Context.Korisnici.Find(KorisnikID);

            if(user == null)
            {
                return BadRequest("Korisnik ne postoji!");
            }
        
            try
            {
                var zakazivanja = await Context.Zakazivanja.Include(p => p.Soba).ThenInclude(p => p.Prenociste).Include(p => p.Korisnik).
                                                        Where(p => p.Korisnik.ID == KorisnikID).Select(z => new {
                                                            id = z.ID,
                                                            starttime = z.StartTime,
                                                            endtime = z.EndTime,
                                                            soba = z.Soba.Naziv,
                                                            prenociste = z.Soba.Prenociste.Naziv
                                                        }).ToListAsync();

                                                                                                               
                return Ok(zakazivanja);
            }
            catch (Exception e)
            {
                
               return BadRequest(e.Message);
            }
        }

      

        [Route("Zakazi/{startdate}/{enddate}/{idkorisnik}/{idsoba}")]
        [HttpPost]

        public async Task<ActionResult> Zakazi([FromRoute] string startdate, [FromRoute] string enddate,[FromRoute] int idkorisnik, 
                                                                                                                        [FromRoute] int idsoba)
        {
            
            try
            {
                var korisnik = Context.Korisnici.Where(e => e.ID == idkorisnik).FirstOrDefault();
                if(korisnik == null) {return BadRequest("Nije taj korisnik");}

                var soba = Context.Sobe.Include(p => p.Zakazivanja).Where(e => e.ID == idsoba).FirstOrDefault();
                if(soba == null) {return BadRequest("Nije ta soba");}

                
                DateTime start = DateTime.Parse(startdate);
                DateTime end = DateTime.Parse(enddate);
                if(start.Equals(DateTime.MinValue))
                {
                    return BadRequest("Niste podesili datum prijave u prenoćište");
                }
                if(end.Equals(DateTime.MinValue))
                {
                    return BadRequest("Niste podesili datum odjave iz prenoćišta");
                }
                if(DateTime.Compare(start, end) >= 0)
                {
                    return BadRequest("Izabrano vreme je već prošlo");
                }
         
              
                foreach (var Zakaz in soba.Zakazivanja)
                {
                    
                   
                    if( (DateTime.Compare(start, Zakaz.EndTime) < 0) &&
                        ( DateTime.Compare(end,  Zakaz.StartTime) > 0 ))
                    {
                        return BadRequest("Žao nam je, izabrani termin je zauzet");
                    }
                }

                Zakazivanje z = new Zakazivanje{StartTime = DateTime.Parse(startdate),
                                                EndTime = DateTime.Parse(enddate),
                                                Korisnik = (Korisnik)korisnik, Soba = (Soba)soba
                };

                Context.Zakazivanja.Add(z);
                await Context.SaveChangesAsync();
                return Ok("Uspešno ste zakazali");
                
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("IzmeniZakazivanje/{korisnikID}")]
        [HttpPut]

        public async Task<ActionResult> IzmeniZakazivanje([FromBody] Zakazivanje obj, [FromRoute] int korisnikID)
        {
            try
            {
                var zakazivanje = await Context.Zakazivanja.Include(p => p.Soba).Include(p => p.Korisnik).Where(p => (p.ID == obj.ID 
                                                                                                         && korisnikID == p.Korisnik.ID))
                                                 .FirstOrDefaultAsync();

                if (zakazivanje == null)
                {
                    return BadRequest($"Zakazivanje nije pronadjeno.");
                }

                var soba = Context.Sobe.Include(p => p.Zakazivanja).Where(e => e.ID == zakazivanje.Soba.ID).FirstOrDefault();
                if(soba == null) {return BadRequest("Nije ta soba");}



                if(obj.StartTime.Equals(DateTime.MinValue))
                {
                    return BadRequest("Datum dolaska nije podešen");
                }
                if(obj.EndTime.Equals(DateTime.MinValue))
                {
                    return BadRequest("Datum odlaska nije podešen");
                }

                foreach (var Zakaz in soba.Zakazivanja)
                {
                    if(Zakaz.ID == obj.ID)
                    {
                        continue;
                    }
                    if( (DateTime.Compare(obj.StartTime, Zakaz.EndTime) < 0) &&
                        ( DateTime.Compare(obj.EndTime,  Zakaz.StartTime) > 0 ))
                    {
                        return BadRequest("Žao nam je, termin je zauzet");
                    }
            
                }
                    
                    zakazivanje.StartTime = obj.StartTime;
                    zakazivanje.EndTime = obj.EndTime;

                    await Context.SaveChangesAsync();
                    return Ok($"Zakazivanje za korisnika {zakazivanje.Korisnik} uspeno promenjeno.");
            
            

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisatiZakazivanje/{zakazivanjeID}")]
        [HttpDelete]

        public async Task<ActionResult> IzbrisatiZakazivanje(int zakazivanjeID)
        {
            try
            {
                var p = await Context.Zakazivanja.Include(t => t.Korisnik).Where( t => t.ID == zakazivanjeID).FirstOrDefaultAsync();

                

                if (p == null)
                {
                    return NotFound($"Zakazivanje nije pronadjeno.");
                }

                else
                { 
                    
                    string imekorisnika = p.Korisnik.Ime;

                    Context.Zakazivanja.Remove(p);
                    await Context.SaveChangesAsync();

                    return Ok($"Zakazivanje za korisnika {imekorisnika} uspesno izbrisano.");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }

   

}