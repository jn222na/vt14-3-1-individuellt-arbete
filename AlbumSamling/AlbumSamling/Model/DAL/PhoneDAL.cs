using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AlbumSamling.Model.DAL
{
    public class PhoneDAL
    {
                private static readonly string _connectionString;


        static PhoneDAL()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["DatabasConnectionString"].ConnectionString;
        }
        private static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
        public static IEnumerable<PhoneProp> GetPhones()
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (var conn = CreateConnection())
            {
                try
                {
                    // Skapar det List-objekt som initialt har plats för 100 referenser till Customer-objekt.
                    var phones = new List<PhoneProp>(100);

                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("AppSchema.VisaTelefoner", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en SELECT-sats som kan returnera flera poster varför
                    // ett SqlDataReader-objekt måste ta hand om alla poster. Metoden ExecuteReader skapar ett
                    // SqlDataReader-objekt och returnerar en referens till objektet.
                    using (var reader = cmd.ExecuteReader())
                    {
                        // Tar reda på vilket index de olika kolumnerna har. Det är mycket effektivare att göra detta
                        // en gång för alla innan while-loopen. Genom att använda GetOrdinal behöver du inte känna till
                        // i vilken ordning de olika kolumnerna kommer, bara vad de heter.
                        
                        var telefonIDIndex = reader.GetOrdinal("TelefonID");
                        var teleNummerIndex = reader.GetOrdinal("TelefoNnummer");
                        var förnamnIndex = reader.GetOrdinal("Förnamn");
                        

                        // Så länge som det finns poster att läsa returnerar Read true. Finns det inte fler 
                        // poster returnerar Read false.
                        while (reader.Read())
                        {
                            // Hämtar ut datat för en post. Använder GetXxx-metoder - vilken beror av typen av data.
                            // Du måste känna till SQL-satsen för att kunna välja rätt GetXxx-metod.
                            phones.Add(new PhoneProp
                            {
                                
                                TelefonID = reader.GetInt32(telefonIDIndex),
                                Number = reader.GetString(teleNummerIndex),
                                Förnamn = reader.GetString(förnamnIndex)
                                

                            });
                        }
                    }

                    // Sätter kapaciteten till antalet element i List-objektet, d.v.s. avallokerar minne
                    // som inte används.
                    phones.TrimExcess();

                    // Returnerar referensen till List-objektet med referenser med Customer-objekt.
                    return phones;
                }
                catch
                {
                    throw new ApplicationException("An error occured while getting customers from the database.");
                }
            }
        }
    }
}