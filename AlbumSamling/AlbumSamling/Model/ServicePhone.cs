using AlbumSamling.Model.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlbumSamling.Model
{
    public class ServicePhone
    {
        private PhoneDAL _phoneDal;
        //Om CustomerDAL in finns skapa en
        private PhoneDAL PhoneDAL
        {
            get { return _phoneDal ?? (_phoneDal = new PhoneDAL()); }
        }
        public IEnumerable<PhoneProp> GetPhones()
        {
            return PhoneDAL.GetPhones();
        }
    }
}