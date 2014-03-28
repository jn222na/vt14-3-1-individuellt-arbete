using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AlbumSamling.Model.DAL
{

    public class AlbumDal
    {
        private static readonly string _connectionString;


        static AlbumDal()
        {
            _connectionString = WebConfigurationManager.ConnectionStrings["DatabasConnectionString"].ConnectionString;
        }
        private static SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public static AlbumProp GetAlbumById(int albumID)
        {
            using (var conn = CreateConnection())
            {
                try
                {
                    //visa specifikt album
                    var cmd = new SqlCommand("AppSchema.VisaAlbum----------------", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AlbumID", SqlDbType.Int, 4).Value = albumID;
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int AlbumIDIndex = reader.GetOrdinal("AlbumID");
                            int AlbumTypIDIndex = reader.GetOrdinal("AlbumTypID");
                            int FormatIDIndex = reader.GetOrdinal("FormatID");
                            int AlbumTitelIndex = reader.GetOrdinal("AlbumTitel");
                            int ArtistTitelIndex = reader.GetOrdinal("ArtistTitel");
                            int UtgivningsårIndex = reader.GetOrdinal("Utgivningsår");

                            return new AlbumProp
                            {
                                AlbumID = reader.GetInt32(AlbumIDIndex),
                                AlbumTypID = reader.GetInt32(AlbumTypIDIndex),
                                FormatID = reader.GetInt32(FormatIDIndex),
                                AlbumTitel = reader.GetString(ArtistTitelIndex),
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
        public AlbumProp GetAlbumID(int GetAlbumID)
        {

            throw new NotImplementedException();
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
                        var AlbumIDIndex = reader.GetOrdinal("AlbumID");
                        var AlbumTypIDIndex = reader.GetOrdinal("AlbumTypID");
                        var FormatIDIndex = reader.GetOrdinal("FormatID");
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
                                AlbumTypID = reader.GetInt32(AlbumTypIDIndex),
                                FormatID = reader.GetInt32(FormatIDIndex),
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
    }

}
