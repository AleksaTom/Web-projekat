using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{

  
    public class Zakazivanje 
        {
        [Key]
        public int ID{get; set;}

        public DateTime StartTime {get;set;}

        public DateTime EndTime { get; set; }

        [JsonIgnore]
        public Soba Soba{get; set;}

       
        public Korisnik Korisnik { get; set; }

       

    }

}