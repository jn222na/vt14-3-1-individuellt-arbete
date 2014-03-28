using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlbumSamling.Model
{
    public class AlbumProp
    {
        public int AlbumID { get; set; }
        public int AlbumTypID { get; set; }
        public int FormatID { get; set; }
        public string AlbumTitel { get; set; }
        public string ArtistTitel { get; set; }
        public string Utgivningsår { get; set; }
    }
}