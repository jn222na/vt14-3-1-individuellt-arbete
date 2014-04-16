using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AlbumSamling.Model;
using AlbumSamling.Model.DAL;

namespace AlbumSamling.Pages
{
    public partial class Album : System.Web.UI.Page
    {
        private bool HasAlbumMessage
        {
            get
            {
                return Session["AlbumMessage"] != null;
            }
        }

        private string AlbumMessage
        {
            get
            {
                var AlbumMessage = Session["AlbumMessage"] as string;
                Session.Remove("Message");
                return AlbumMessage;
            }

            set
            {
                Session["AlbumMessage"] = value;
            }
        }
        private ServiceAlbum _serviceAlbum;

        private ServiceAlbum ServiceAlbum
        {
            get { return _serviceAlbum ?? (_serviceAlbum = new ServiceAlbum()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HasAlbumMessage)
            {
                AlbumStatuslabel.Text = AlbumMessage;
                AlbumStatuslabel.Visible = true;
            }
        }

        public IEnumerable<AlbumSamling.Model.AlbumProp> AlbumListView_GetData()
        {
            try
            {
                return ServiceAlbum.GetAlbums();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgifter skulle hämtas.");
                return null;
            }
        }
        public void AlbumFormView_InsertItem(AlbumProp AlbumProp)
        {
            try
            {
                ServiceAlbum.SaveAlbum(AlbumProp);
                AlbumMessage = String.Format("Ny kontakt lades till i databasen.");
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgiften skulle läggas till.");
            }
        }
        public void AlbumListView_DeleteItem(int AlbumID)
        {
            try
            {
                ServiceAlbum.DeleteAlbum(AlbumID);
                AlbumMessage = String.Format("Kontakten togs bort.");
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgiften skulle tas bort.");
            }
        }




    }
}