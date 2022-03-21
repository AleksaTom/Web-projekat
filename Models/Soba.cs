using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{

  
    public class Soba 
        {
        [Key]
        public int ID{get; set;}

        [MaxLength(100)]
        public string Naziv{get; set;}

        [JsonIgnore]
        public Prenociste Prenociste{get; set;}

        
        [JsonIgnore]
        public List<Zakazivanje> Zakazivanja { get; set; }

    }

}