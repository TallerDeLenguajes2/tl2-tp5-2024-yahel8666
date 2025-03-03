using Microsoft.Data.Sqlite;
public class PresupuestoRepository
{
    string connectionString;
    public PresupuestoRepository()
    {
        connectionString = "Data Source=BD/Tienda.db;Cache=Shared";
    }

    public void Create(Presupuesto p)
    {
        string queryString = $"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@Nombre, @Fecha);";
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(queryString, connection);
            command.Parameters.AddWithValue("@Nombre", p.NombreDestinatario);
            command.Parameters.AddWithValue("@Fecha", p.FechaCreacion.ToString("yyyy-MM-dd"));
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    public List<Presupuesto> ListarPresupuestos()
    {
        string query = "SELECT * FROM Presupuestos";
        var allPresupuestos = new List<Presupuesto>();
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var nuevoPresupuesto = new Presupuesto();
                    nuevoPresupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                    nuevoPresupuesto.FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                    nuevoPresupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    allPresupuestos.Add(nuevoPresupuesto);
                }
            }
            connection.Close();
        }
        return allPresupuestos;
    }
    public Presupuesto GetPresupuestoById(int id)
    {
        string query = @"select * 
                     from Presupuestos pre
                     inner join PresupuestosDetalle pd on pd.idPresupuesto = pre.idPresupuesto
                     inner join Productos pro on pro.idProducto = pd.idProducto
                     where pre.idPresupuesto = @id";

        var presupuesto = new Presupuesto();

        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@id", id));

            using (SqliteDataReader reader = command.ExecuteReader())
            {
                presupuesto.Detalle = new List<PresupuestoDetalle>();
                while (reader.Read())
                {
                    if (presupuesto.IdPresupuesto == 0)
                    {
                        presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                        presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                        presupuesto.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);
                    }
                    string descripcion = reader["Descripcion"].ToString();
                    int idProducto = Convert.ToInt32(reader["idProducto"]);
                    double precio = Convert.ToDouble(reader["Precio"]);
                    var producto = new Producto(idProducto, descripcion, precio);

                    // creo un detalle y lo agrego a la lista
                    int cantidad = Convert.ToInt32(reader["Cantidad"]);
                    var detalle = new PresupuestoDetalle(producto, cantidad);
                    presupuesto.Detalle.Add(detalle);
                }
            }
        }
        return presupuesto;
    }

    public bool EliminarPresupuesto(int id)
    {
        string queryString = $"DELETE FROM Presupuestos WHERE idPresupuesto = @id ;";
        int filas = 0; 
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(queryString, connection);
            command.Parameters.AddWithValue("@id", id);
            filas = command.ExecuteNonQuery(); 
            connection.Close();
        }
        return filas > 0; 
    }

    public bool AgregarProducto(int id) 
    {
        string stringQuery = "Select * from productos where idProducto = @id";
        int filas = 0; 
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(stringQuery, connection);
            command.Parameters.AddWithValue("@id", id);
            var productoBuscado = new Producto();;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    productoBuscado.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    productoBuscado.Precio = Convert.ToDouble(reader["Precio"]);
                    productoBuscado.Descripcion = reader["Descripcion"].ToString();
                }
            }
            
            if (productoBuscado is not null)
            {
                string newquery = "INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idpre, @idpro, @cant); ";
                var presupuestoDetalle = new PresupuestoDetalle();
                var newcommand = new SqliteCommand(newquery, connection);
                newcommand.Parameters.AddWithValue("@id", productoBuscado.Precio);
                filas = newcommand.ExecuteNonQuery();
            }
        }
        return filas > 0; 
    }
}