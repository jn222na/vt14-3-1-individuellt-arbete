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
            

            if (Session["Customer"] != null)
            {
                Label1.Text = (string)Session["Customer"];
            }
            else
            {
                Label1.Text = "Hittade inte kund";
            }
        }

        public void CustomerListView_GetData()
        {

        }

        public void ContactListView_UpdateItem(int KundID) // Parameterns namn måste överrensstämma med värdet DataKeyNames har.
        {

            try
            {
                var customer = ServiceCustomer.GetContact(KundID);
                if (customer == null)
                {
                    // Hittade inte kunden.
                    ModelState.AddModelError(String.Empty,
                        String.Format("Kunden med kundnummer {0} hittades inte.", KundID));
                    return;
                }

                if (TryUpdateModel(customer))
                {
                    ServiceCustomer.SaveContact(customer);
                }
                //Message = String.Format("Kontakten uppdaterades i databasen.");
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IQueryable<AlbumSamling.Model.CustomerProp> CustomerListView1_GetData()
        {
            return null;
        }
    }
}