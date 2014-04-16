using AlbumSamling.Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace AlbumSamling.Model
{
    public class ServiceAlbum
    {
        private AlbumDAL _albumDAL;
        //Om AlbumDAL inte finns skapa en
        private AlbumDAL AlbumDAL
        {
            get { return _albumDAL ?? (_albumDAL = new AlbumDAL()); }
        }

        public AlbumProp GetAlbum(int albumId)
        {
            return AlbumDAL.GetAlbumById(albumId);
        }

        public IEnumerable<AlbumProp> GetAlbums()
        {
            return AlbumDAL.GetAlbums();
        }
        public void DeleteAlbum(int AlbumID)
        {
            AlbumDAL.DeleteAlbum(AlbumID);
        }
        public void SaveAlbum(AlbumProp albumProp)
        {
            var validationContext = new ValidationContext(albumProp);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(albumProp, validationContext, validationResults, true))
            {
                // Uppfyller inte objektet affärsreglerna kastas ett undantag med
                // ett allmänt felmeddelande samt en referens till samlingen med
                // resultat av valideringen.
                var ex = new ValidationException("Objektet klarade inte valideringen.");
                ex.Data.Add("ValidationResults", validationResults);
                throw ex;
            }

            //Customer-objektet sparas antingen genom att en ny post 
            //skapas eller genom att en befintlig post uppdateras.
            if (albumProp.AlbumID == 0) // Ny post om CustomerId är 0!
            {
                AlbumDAL.InsertAlbum(albumProp);
            }
            else
            {
                AlbumDAL.InsertAlbum(albumProp);

            }
        }
    }
}