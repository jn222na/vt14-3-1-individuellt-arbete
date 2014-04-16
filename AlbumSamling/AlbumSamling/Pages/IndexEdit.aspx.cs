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
    public partial class IndexEdit : System.Web.UI.Page
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
                Label1.Text = Message;
                Label1.Visible = true;
            }

        }
        public AlbumSamling.Model.CustomerProp CustomerFormView_GetData([RouteData]int KundID)
        {
           
                try
                {
                    return ServiceCustomer.GetContact(KundID);
                }
            
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgifter skulle hämtas.");
                    return null;
                }
            
        }

        // The id parameter name should match the DataKeyNames value set on the control
        public void CustomerFormView_UpdateItem(int CustomerId)
        {

            if (IsValid)
            {
                // Load the item here, e.g. item = MyDataLayer.Find(id);
                try
                {
                    var customer = ServiceCustomer.GetContact(CustomerId);
                    Message = String.Format("Kontakten Uppdaterades.");

                    if (customer == null)
                    {
                        // The item wasn't found
                        ModelState.AddModelError(String.Empty,
                              String.Format("Kunden med kundnummer {0} hittades inte.", CustomerId));
                    }

                    if (TryUpdateModel(customer))
                    {

                        ServiceCustomer.SaveContact(customer);

                    }

                    Response.RedirectToRoute("Index");

                }
                catch (Exception)
                {
                    throw;
                }
            }
            
        }



    }
}