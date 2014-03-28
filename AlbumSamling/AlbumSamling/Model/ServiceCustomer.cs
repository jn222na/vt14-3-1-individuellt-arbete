using AlbumSamling.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlbumSamling.Model
{
    public class ServiceCustomer
    {
        private CustomerDAL _customerDal;
        //Om CustomerDAL in finns skapa en
        private CustomerDAL CustomerDAL
        {
            get { return _customerDal ?? (_customerDal = new CustomerDAL()); }
        }

        public CustomerProp GetContact(int KundID)
        {
           return CustomerDAL.GetCustomerById(KundID);
        }

        public IEnumerable<CustomerProp> GetCustomers()
        {
            return CustomerDAL.GetCustomers();
        }
        public void DeleteCustomer(int KundID)
        {
             CustomerDAL.DeleteContact(KundID);
        }
        public void SaveContact(CustomerProp customerProp)
        {
            //var validationContext = new ValidationContext(customer);
            //var validationResults = new List<ValidationResult>();
            //if (!Validator.TryValidateObject(customer, validationContext, validationResults, true))
            //{
            //    // Uppfyller inte objektet affärsreglerna kastas ett undantag med
            //    // ett allmänt felmeddelande samt en referens till samlingen med
            //    // resultat av valideringen.
            //    var ex = new ValidationException("Objektet klarade inte valideringen.");
            //    ex.Data.Add("ValidationResults", validationResults);
            //    throw ex;
            //}

            // Uppfyller inte objektet affärsreglerna...
            //ICollection<ValidationResult> validationResults;
            //if (!contact.Validate(out validationResults)) // Använder "extension method" för valideringen!
            //{                                              // Klassen finns under App_Infrastructure.
            //    // ...kastas ett undantag med ett allmänt felmeddelande samt en referens 
            //    // till samlingen med resultat av valideringen.
            //    var ex = new ValidationException("Objektet klarade inte valideringen.");
            //    ex.Data.Add("ValidationResults", validationResults);
            //    throw ex;
            //}

            // Customer-objektet sparas antingen genom att en ny post 
            // skapas eller genom att en befintlig post uppdateras.
            if (customerProp.KundID == 0) // Ny post om CustomerId är 0!
            {
                CustomerDAL.InsertContact(customerProp);
            }
            else
            {
                CustomerDAL.UpdateContact(customerProp);

            }
        }
    }
}