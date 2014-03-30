using AlbumSamling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlbumSamling.Pages
{
    public partial class IndexEdit : System.Web.UI.Page
    {
        
        private ServiceCustomer _serviceCustomer;

        private ServiceCustomer ServiceCustomer
        {
            // Ett Service-objekt skapas först då det behövs för första 
            // gången (lazy initialization, http://en.wikipedia.org/wiki/Lazy_initialization).
            get { return _serviceCustomer ?? (_serviceCustomer = new ServiceCustomer()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //bool HasMessage = (bool)(Session["Message"]);
            //string Message = (string)(Session["Message"]);
            //if (HasMessage)
            //{
            //    Statuslabel.Text = Message;
            //    Statuslabel.Visible = true;
            //}


            if (Session["Förnamn"] != null)
            {
                Label1.Text = (string)Session["Förnamn"];
            }
            else
            {
                Label1.Text = "Hittade inte kund";
            }
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void CustomerListView1_UpdateItem(int KundID)
        {
            var customer = ServiceCustomer.GetContact(KundID);
            // Load the item here, e.g. item = MyDataLayer.Find(id);
            try
            {
                if (customer == null)
                {
                    // The item wasn't found
                    ModelState.AddModelError(String.Empty,
                          String.Format("Kunden med kundnummer {0} hittades inte.", KundID));
                }
                TryUpdateModel(customer);
                if (ModelState.IsValid)
                {
                    ServiceCustomer.SaveContact(customer);
                }
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CustomerListView_GetData(int KundID)
        {
            try
            {
                 ServiceCustomer.GetContact(KundID);
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgifter skulle hämtas.");
                
            }
        }
       
    }
}