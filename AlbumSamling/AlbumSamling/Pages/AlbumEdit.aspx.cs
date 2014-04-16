using AlbumSamling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlbumSamling.Pages
{
    public partial class AlbumEdit : System.Web.UI.Page
    {
        private ServiceAlbum _serviceAlbum;

        private ServiceAlbum ServiceAlbum
        {
            // Ett Service-objekt skapas först då det behövs för första 
            // gången (lazy initialization, http://en.wikipedia.org/wiki/Lazy_initialization).
            get { return _serviceAlbum ?? (_serviceAlbum = new ServiceAlbum()); }
        }
        public AlbumSamling.Model.AlbumProp AlbumFormView_GetData([RouteData]int AlbumID)
        {

            try
            {
                return ServiceAlbum.GetAlbum(AlbumID);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då Albumuppgifterna skulle hämtas.");
                return null;
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void AlbumFormView_UpdateItem(int AlbumId)
        {


            // Load the item here, e.g. item = MyDataLayer.Find(id);
            try
            {
                var album = ServiceAlbum.GetAlbum(AlbumId);
                if (album == null)
                {
                    // The item wasn't found
                    ModelState.AddModelError(String.Empty,
                          String.Format("Kunden med kundnummer {0} hittades inte.", AlbumId));
                }
                TryUpdateModel(album);
                if (ModelState.IsValid)
                {
                    ServiceAlbum.SaveAlbum(album);
                }
                Response.RedirectToRoute("Album");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}