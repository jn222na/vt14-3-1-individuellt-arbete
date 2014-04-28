using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlbumSamling.Model
{
    public class CustomerProp
    {
        public int CustomerId { get; set; }
        public int TelefonID { get; set; }

        [Required(ErrorMessage = "Förnamn måste anges")]
        [StringLength(30, ErrorMessage = "Max 30 tecken")]
        [RegularExpression("[a-zA-Z]+")]
        public string Förnamn { get; set; }

        [Required(ErrorMessage = "Efternamn måste anges")]
        [StringLength(30, ErrorMessage = "Max 30 tecken")]
        [RegularExpression("[a-zA-Z]+")]
        public string Efternamn { get; set; }

        [Required(ErrorMessage = "Ort måste anges")]
        [StringLength(30, ErrorMessage = "Max 30 tecken")]
        [RegularExpression("[a-zA-Z]+")]
        public string Ort { get; set; }


        public int AlbumID { get; set; }
       
    }
}