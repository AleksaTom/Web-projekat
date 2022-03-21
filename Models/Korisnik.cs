using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{

  
    public class Korisnik 
        {

        [Key]
        public int ID{get; set;}

        [MaxLength(50)]
        public string Ime{get; set;} 

        [MaxLength(50)]
        public string Prezime{get; set;} 
        
        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [JsonIgnore]     
        public List<Zakazivanje> Zakazivanja { get; set; }

       

    }

}