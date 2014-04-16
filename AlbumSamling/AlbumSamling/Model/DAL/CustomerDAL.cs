using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AlbumSamling.Model.DAL
{
    public class CustomerDAL
    {
        private static readonly string _connectionString;


        static CustomerDAL()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["DatabasConnectionString"].ConnectionString;
        }
        private static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
        //TODO: ÄNDRA SQLCOMMAND
        public CustomerProp GetCustomerById(int customerId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //visa specifik kund
                    SqlCommand cmd = new SqlCommand("AppSchema.VisaKund", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@KundID", customerId);
                        
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //int customerIdIndex = reader.GetOrdinal("KundID");
                            //int telefonIDIndex = reader.GetOrdinal("TelefonID");
                            int förnamnIndex = reader.GetOrdinal("Förnamn");
                            int efternamnIndex = reader.GetOrdinal("Efternamn");
                            int ortIndex = reader.GetOrdinal("Ort");


                            return new CustomerProp
                            {
                                CustomerId = customerId,
                                //TelefonID = reader.GetInt32(telefonIDIndex),
                                Förnamn = reader.GetString(förnamnIndex),
                                Efternamn = reader.GetString(efternamnIndex),
                                Ort = reader.GetString(ortIndex)
                            };
                        }

                    }
                    return null;
                }
                catch
                {
                    throw new ApplicationException();
                }
            }
        }
        public static IEnumerable<CustomerProp> GetCustomers()
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (var conn = CreateConnection())
            {
                try
                {
                    // Skapar det List-objekt som initialt har plats för 100 referenser till Customer-objekt.
                    var customers = new List<CustomerProp>(100);

                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("AppSchema.VisaKunder", conn);
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
                        var CustomerIdIndex = reader.GetOrdinal("KundID");
                        //var telefonIDIndex = reader.GetOrdinal("TelefonID");
                        var förnamnIndex = reader.GetOrdinal("Förnamn");
                        var efternamnIndex = reader.GetOrdinal("Efternamn");
                        var ortIndex = reader.GetOrdinal("Ort");
                  

                        // Så länge som det finns poster att läsa returnerar Read true. Finns det inte fler 
                        // poster returnerar Read false.
                        while (reader.Read())
                        {
                            // Hämtar ut datat för en post. Använder GetXxx-metoder - vilken beror av typen av data.
                            // Du måste känna till SQL-satsen för att kunna välja rätt GetXxx-metod.
                            customers.Add(new CustomerProp
                            {
                                CustomerId = reader.GetInt32(CustomerIdIndex),
                                //TelefonID = reader.GetInt32(telefonIDIndex),
                                Förnamn = reader.GetString(förnamnIndex),
                                Efternamn = reader.GetString(efternamnIndex),
                                Ort = reader.GetString(ortIndex)

                            });
                        }
                    }

                    // Sätter kapaciteten till antalet element i List-objektet, d.v.s. avallokerar minne
                    // som inte används.
                    customers.TrimExcess();

                    // Returnerar referensen till List-objektet med referenser med Customer-objekt.
                    return customers;
                }
                catch
                {
                    throw new ApplicationException("An error occured while getting customers from the database.");
                }
            }
        }
        public void InsertContact(CustomerProp customerProp)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {

                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("AppSchema.NyKund", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@Förnamn", SqlDbType.VarChar, 50).Value = customerProp.Förnamn;
                    cmd.Parameters.Add("@Efternamn", SqlDbType.VarChar, 50).Value = customerProp.Efternamn;
                    cmd.Parameters.Add("@Ort", SqlDbType.VarChar, 50).Value = customerProp.Ort;

                    // Den här parametern är lite speciell. Den skickar inte något data till den lagrade proceduren,
                    // utan hämtar data från den. (Fungerar ungerfär som ref- och out-prameterar i C#.) Värdet 
                    // parametern kommer att ha EFTER att den lagrade proceduren exekverats är primärnycklens värde
                    // den nya posten blivit tilldelad av databasen.
                    cmd.Parameters.Add("@KundID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en INSERT-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();

                    // Hämtar primärnyckelns värde för den nya posten och tilldelar Customer-objektet värdet.
                    customerProp.CustomerId = (int)cmd.Parameters["@KundID"].Value;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
        public void UpdateContact(CustomerProp CustomerProp)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.UppKund", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@KundID", SqlDbType.VarChar, 50).Value = CustomerProp.CustomerId;
                    cmd.Parameters.Add("@Förnamn", SqlDbType.VarChar, 50).Value = CustomerProp.Förnamn;
                    cmd.Parameters.Add("@Efternamn", SqlDbType.VarChar, 50).Value = CustomerProp.Efternamn;
                    cmd.Parameters.Add("@Ort", SqlDbType.VarChar, 50).Value = CustomerProp.Ort;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en UPDATE-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data hgdfghaccess layer.");
                }
            }
        }
        public void DeleteContact(int KundID)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.RemoveKund", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till den paramter den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@KundID", SqlDbType.Int, 4).Value = KundID;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en DELETE-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
    }
}