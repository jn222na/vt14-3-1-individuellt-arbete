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
    public partial class Index : System.Web.UI.Page
    {
        private bool HasMessage
        {
            get
            {
                return Session["Message"] != null;
            }
        }

        private string Message
        {
            get
            {
                var Message = Session["Message"] as string;
                Session.Remove("Message");
                return Message;
            }

            set
            {
                Session["Message"] = value;
            }
        }
        private object Förnamn
        {
            get
            {
                var Förnamn = Session["Förnamn"];
                Session.Remove("Förnamn");
                return Förnamn;
            }
            set
            {
                Session["Förnamn"] = value;
            }
        }
        private ServiceCustomer _serviceCustomer;

        private ServiceCustomer ServiceCustomer
        {
            // Ett Service-objekt skapas först då det behövs för första 
            // gången (lazy initialization, http://en.wikipedia.org/wiki/Lazy_initialization).
            get { return _serviceCustomer ?? (_serviceCustomer = new ServiceCustomer()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HasMessage)
            {
                Statuslabel.Text = Message;
                Statuslabel.Visible = true;
            }
        }

        public IEnumerable<AlbumSamling.Model.CustomerProp> CustomerListView_GetData()
        {
            try
            {
                return ServiceCustomer.GetCustomers();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgifter skulle hämtas.");
                return null;
            }
        }
        public void ContactFormView_InsertItem(CustomerProp CustomerProp)
        {
            try
            {
                ServiceCustomer.SaveContact(CustomerProp);
                Message = String.Format("Ny kontakt lades till i databasen.");
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgiften skulle läggas till.");
            }
        }

        public void ContactListView_DeleteItem(int KundID)
        {
            try
            {
                ServiceCustomer.DeleteCustomer(KundID);
                Message = String.Format("Kontakten togs bort.");
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgiften skulle tas bort.");
            }
        }



        protected void Edit_Command(object sender, CommandEventArgs e)
        {
            Förnamn = e.CommandArgument;

            Response.Redirect("~/Pages/IndexEdit.aspx");

        }





    }
}