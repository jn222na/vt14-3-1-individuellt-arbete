using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlbumSamling.Model
{
    public class AlbumProp
    {
        public int AlbumID { get; set; }

        [Required(ErrorMessage = "AlbumTitel måste anges")]
        [StringLength(30, ErrorMessage = "Max 30 tecken")]
        [RegularExpression("[a-zA-Z]+")]
        public string AlbumTitel { get; set; }

        [Required(ErrorMessage = "ArtistTitel måste anges")]
        [StringLength(30, ErrorMessage = "Max 30 tecken")]
        [RegularExpression("[a-zA-Z]+")]
        public string ArtistTitel { get; set; }

        [Required(ErrorMessage = "Utgivningsår måste anges")]
        [StringLength(4, ErrorMessage = "Max 4 tecken")]
        [RegularExpression("[0-9]+")]
        public string Utgivningsår { get; set; }
    }
}