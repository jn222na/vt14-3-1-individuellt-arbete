using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AlbumSamling.Model.DAL
{


    class AlbumDAL
    {
         private static readonly string _connectionString;


        static AlbumDAL()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["DatabasConnectionString"].ConnectionString;
        }
        private static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public static AlbumProp GetAlbumById(int albumId)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //visa specifikt album
                    var cmd = new SqlCommand("AppSchema.VisaAlbum", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AlbumID", SqlDbType.Int, 4).Value = albumId;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //int AlbumIDIndex = reader.GetOrdinal("AlbumID");
                            int AlbumTitelIndex = reader.GetOrdinal("AlbumTitel");
                            int ArtistTitelIndex = reader.GetOrdinal("ArtistTitel");
                            int UtgivningsårIndex = reader.GetOrdinal("Utgivningsår");

                            return new AlbumProp
                            {
                                AlbumID = albumId,
                                AlbumTitel = reader.GetString(AlbumTitelIndex),
                                ArtistTitel = reader.GetString(ArtistTitelIndex),
                                Utgivningsår = reader.GetString(UtgivningsårIndex)
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


        public static IEnumerable<AlbumProp> GetAlbums()
        {

            // Skapar och initierar ett anslutningsobjekt.
            using (var conn = CreateConnection())
            {
                try
                {
                    // Skapar det List-objekt som initialt har plats för 100 referenser till Album-objekt.
                    var Album = new List<AlbumProp>(100);

                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    var cmd = new SqlCommand("AppSchema.VisaAlbums", conn);
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
                        var AlbumIDIndex = reader.GetOrdinal("AlbumID");
                        var AlbumTitelIndex = reader.GetOrdinal("AlbumTitel");
                        var ArtistTitelIndex = reader.GetOrdinal("ArtistTitel");
                        var UtgivningsårIndex = reader.GetOrdinal("Utgivningsår");

                        // Så länge som det finns poster att läsa returnerar Read true. Finns det inte fler 
                        // poster returnerar Read false.
                        while (reader.Read())
                        {
                            // Hämtar ut datat för en post. Använder GetXxx-metoder - vilken beror av typen av data.
                            // Du måste känna till SQL-satsen för att kunna välja rätt GetXxx-metod.
                            Album.Add(new AlbumProp
                            {

                                AlbumID = reader.GetInt32(AlbumIDIndex),
                                AlbumTitel = reader.GetString(AlbumTitelIndex),
                                ArtistTitel = reader.GetString(ArtistTitelIndex),
                                Utgivningsår = reader.GetString(UtgivningsårIndex)
                            });
                        }
                    }

                    // Sätter kapaciteten till antalet element i List-objektet, d.v.s. avallokerar minne
                    // som inte används.
                    Album.TrimExcess();

                    // Returnerar referensen till List-objektet med referenser med Album-objekt.
                    return Album;
                }
                catch
                {
                    throw new ApplicationException("An error occured while getting Albums from the database.");
                }
            }

        }
        public void InsertAlbum(AlbumProp albumProp)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {

                try
                {
                    // Skapar och initierar ett SqlCommand-objekt som används till att 
                    // exekveras specifierad lagrad procedur.
                    SqlCommand cmd = new SqlCommand("AppSchema.NyttAlbum", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@AlbumTitel", SqlDbType.VarChar, 50).Value = albumProp.AlbumTitel;
                    cmd.Parameters.Add("@ArtistTitel", SqlDbType.VarChar, 50).Value = albumProp.ArtistTitel;
                    cmd.Parameters.Add("@Utgivningsår", SqlDbType.VarChar, 50).Value = albumProp.Utgivningsår;

                    // Den här parametern är lite speciell. Den skickar inte något data till den lagrade proceduren,
                    // utan hämtar data från den. (Fungerar ungerfär som ref- och out-prameterar i C#.) Värdet 
                    // parametern kommer att ha EFTER att den lagrade proceduren exekverats är primärnycklens värde
                    // den nya posten blivit tilldelad av databasen.
                    cmd.Parameters.Add("@AlbumID", SqlDbType.Int, 4).Direction = ParameterDirection.Output;

                    // Öppnar anslutningen till databasen.
                    conn.Open();

                    // Den lagrade proceduren innehåller en INSERT-sats och returnerar inga poster varför metoden 
                    // ExecuteNonQuery används för att exekvera den lagrade proceduren.
                    cmd.ExecuteNonQuery();

                    // Hämtar primärnyckelns värde för den nya posten och tilldelar Customer-objektet värdet.
                    albumProp.AlbumID = (int)cmd.Parameters["@AlbumID"].Value;
                }
                catch
                {
                    // Kastar ett eget undantag om ett undantag kastas.
                    throw new ApplicationException("An error occured in the data access layer.");
                }
            }
        }
        public void UpdateAlbum(AlbumProp albumProp)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.UppAlbum", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till de paramterar den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@AlbumID", SqlDbType.VarChar, 50).Value = albumProp.AlbumID;
                    cmd.Parameters.Add("@AlbumTitel", SqlDbType.VarChar, 50).Value = albumProp.AlbumTitel;
                    cmd.Parameters.Add("@ArtistTitel", SqlDbType.VarChar, 50).Value = albumProp.ArtistTitel;
                    cmd.Parameters.Add("@Utgivningsår", SqlDbType.VarChar, 50).Value = albumProp.Utgivningsår;

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
        public void DeleteAlbum(int AlbumID)
        {
            // Skapar och initierar ett anslutningsobjekt.
            using (SqlConnection conn = CreateConnection())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AppSchema.RemoveAlbum", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Lägger till den paramter den lagrade proceduren kräver. Använder här det effektiva sätttet att
                    // göra det på - något "svårare" men ASP.NET behöver inte "jobba" så mycket.
                    cmd.Parameters.Add("@AlbumID", SqlDbType.Int, 4).Value = AlbumID;

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
