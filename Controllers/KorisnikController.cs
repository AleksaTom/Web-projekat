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
    public class KorisnikController : ControllerBase
    {
         public MainContext Context { get; set; }

        public KorisnikController(MainContext context)
        {
            Context = context;
        }


        [Route("LogIn")]
        [HttpPost]

        public  ActionResult LogIn([FromBody] Korisnik kor)
        {

            if(string.IsNullOrWhiteSpace(kor.Username) || kor.Username.Length > 50)
            {
                return BadRequest("Pogrešni podaci za Username/korisničko ime");
            }
            if(string.IsNullOrWhiteSpace(kor.Password) || kor.Password.Length > 50)
            {
                return BadRequest("Pogrešni podaci za šifru");
            }

            try
            {
                var korisnik = Context.Korisnici.Where(k => (k.Username == kor.Username) && (k.Password == kor.Password)).FirstOrDefault();
                if(korisnik == null)
                    {
                        return BadRequest("Korisnik ne postoji");
                    }
                return Ok(korisnik);    
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("Register")]
        [HttpPost]

        public  ActionResult Register([FromBody] Korisnik kor)
        {

            if(string.IsNullOrWhiteSpace(kor.Username) || kor.Username.Length > 50) 
               {
                   return BadRequest("Loše korisnicko ime");
               }
            if(string.IsNullOrWhiteSpace(kor.Password) || kor.Password.Length > 50) 
               {
                   return BadRequest("Loša šifra");
               }
            if(string.IsNullOrWhiteSpace(kor.Email) || kor.Email.Length > 100) 
               {
                   return BadRequest("Loš email");
               }
            if(string.IsNullOrWhiteSpace(kor.Ime) || kor.Ime.Length > 50)
               {
                   return BadRequest("Loše ime");
               }
            if(string.IsNullOrWhiteSpace(kor.Prezime) || kor.Prezime.Length > 50) 
               {
                   return BadRequest("Loše prezime");
               }

               var user = Context.Korisnici.Where(e => e.Email == kor.Email).FirstOrDefault();
                if(user != null)
                {
                    return BadRequest("Korisnik sa datim email-om već postoji");
                }
                user = Context.Korisnici.Where(e => e.Username == kor.Username).FirstOrDefault();
                if(user != null)
                {
                    return BadRequest("Korisnik sa datim korisničkim imenom vec postoji");
                }


            try
            {
                
               
               Context.Korisnici.Add(kor);
               Context.SaveChanges();
               return Ok("Uspesno registrovan korisnik");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    



    }

   

}