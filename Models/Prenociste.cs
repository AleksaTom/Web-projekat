using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{

    public class Prenociste
    {
        [Key]
        public int ID{get; set;}

        [MaxLength(50)]
        public string Naziv{get; set;}

        [MaxLength(100)]
        public string Lokacija { get; set; }

        [JsonIgnore]
        public List<Soba> Sobe {get; set;}
    }

}