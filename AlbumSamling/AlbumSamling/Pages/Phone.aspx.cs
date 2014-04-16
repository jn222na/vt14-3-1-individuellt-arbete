using AlbumSamling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AlbumSamling.Pages
{
    public partial class Phone : System.Web.UI.Page
    {
        private ServicePhone _servicePhone;

        private ServicePhone ServicePhone
        {
            get { return _servicePhone ?? (_servicePhone = new ServicePhone()); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // The return type can be changed to IEnumerable, however to support
        // paging and sorting, the following parameters must be added:
        //     int maximumRows
        //     int startRowIndex
        //     out int totalRowCount
        //     string sortByExpression
        public IEnumerable<AlbumSamling.Model.PhoneProp> PhoneListView_GetData()
        {
            try
            {
                return ServicePhone.GetPhones();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Ett oväntat fel inträffade då kunduppgifter skulle hämtas.");
                return null;
            }
        }
    }
}