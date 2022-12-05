using Microsoft.Data.Sqlite;
namespace BlazorAppFotovoltaico.Models
{
    public class Marca
    {
        static string dataSource = @"Data source = Data/testFotovoltaico.db";
           

        public static List<Marca> ListaMarche
        {
            get {
                List<Marca> lista = new List<Marca>();
                //collego il database al progetto c#
                SqliteConnection myConnection = new SqliteConnection(dataSource);
                //mando in esecuzione la query SQL
                SqliteCommand myCommand = new SqliteCommand("SELECT * FROM tblMarche");
                myCommand.Connection = myConnection;
                SqliteDataReader myDatareader;
                myConnection.Open();
                myDatareader = myCommand.ExecuteReader();

                while (myDatareader.Read())
                {
                    Marca nuovaMarca = new Marca();
                    nuovaMarca.IdMarca =Convert.ToInt32( myDatareader["IdMarca"]);
                    nuovaMarca.NomeMarca =(string) myDatareader["NomeMarca"];
                    nuovaMarca.NazioneSede = (string)myDatareader["NazioneSede"];
                    nuovaMarca.Abituale = Convert.ToBoolean( myDatareader["Abituale"]);
                    lista.Add(nuovaMarca);    
                }
                myConnection.Close();
                return lista;  
            }
            
        }
        public Marca()
        {

        }

        public Marca(int Id)
        {
            //creiamo l'oggetto connection
            SqliteConnection myConnection=new SqliteConnection(dataSource);
            //creiamo l'oggetto command
            SqliteCommand myCommand = new SqliteCommand("SELECT * FROM tblMarche WHERE idMarca=@par1;");
            SqliteParameter myPar = new SqliteParameter("@par1", Id);
            SqliteDataReader myDatareader;
            myCommand.Parameters.Add(myPar);
            myCommand.Connection= myConnection;
            myConnection.Open();

            myDatareader = myCommand.ExecuteReader();

            myDatareader.Read();
            IdMarca = Id;
            NomeMarca = (string)myDatareader["nomeMarca"];
            NazioneSede= (string)myDatareader["nazioneSede"];
            Abituale=  Convert.ToBoolean(myDatareader["Abituale"]);       
            myConnection.Close();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomeMarca"></param>
        /// <param name="nazioneSede"></param>
        /// <param name="abituale"></param>
        public Marca(string nomeMarca, string nazioneSede, bool abituale) 
        {
            //creiamo l'oggetto connection
            SqliteConnection myConnection = new SqliteConnection(dataSource);
            //creiamo l'oggetto command
            SqliteCommand myCommand = new SqliteCommand(@"INSERT INTO tblMarche(nomeMarca,nazioneSede, Abituale) VALUES(@par1,@par2,@par3);");
            SqliteParameter myPar1 = new SqliteParameter("@par1", nomeMarca);
            SqliteParameter myPar2 = new SqliteParameter("@par2", nazioneSede);
            SqliteParameter myPar3 = new SqliteParameter("@par3", abituale);

            myCommand.Parameters.Add(myPar1);
            myCommand.Parameters.Add(myPar2);
            myCommand.Parameters.Add(myPar3);

            myCommand.Connection= myConnection;

            myConnection.Open();

            myCommand.ExecuteNonQuery();

            string sqlString = "SELECT last_insert_rowid()";
            SqliteCommand mySecondCommand = new SqliteCommand(sqlString);
            mySecondCommand.Connection = myConnection;
            int lastId = Convert.ToInt32(mySecondCommand.ExecuteScalar());

            IdMarca = lastId;
            NomeMarca=nomeMarca;
            NazioneSede = nazioneSede;
            Abituale = abituale;

            myConnection.Close();


        }

        public int IdMarca { get; set; }
        public string NomeMarca { get; set; }
        public string NazioneSede { get; set; }
        public bool Abituale { get; set; }


    }
}
