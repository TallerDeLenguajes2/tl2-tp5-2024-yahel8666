using Microsoft.Data.Sqlite;

public class ProductoRepository 
{   
    string connectionString;
    public ProductoRepository()
    {
        connectionString = "Data Source=BD/Tienda.db;Cache=Shared";
    }
    public bool CrearProducto(Producto p)
    {
        int filas = 0; 
        string querystring = "INSERT INTO Productos (Descripcion, Precio) VALUES (@nombre, @precio);" ; 
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(querystring , connection);
            command.Parameters.AddWithValue("@nombre" , p.Descripcion);
            command.Parameters.AddWithValue("@precio" , p.Precio);
            filas =  command.ExecuteNonQuery();
            connection.Close();
        }
        return filas > 0; 
    }

    public bool Delete(int id)
    {
        string query = "DELETE FROM PresupuestosDetalle WHERE idProducto = @id";
        int filas = 0;  
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open(); 
            var command = new SqliteCommand(query , connection);
            command.Parameters.AddWithValue("@id" , id);
            filas = command.ExecuteNonQuery();
            connection.Close();    
        }
        return filas > 0;
    }

    public List<Producto> GetAll()
    {
        string query = "SELECT * FROM productos"; 
        List<Producto> productos = new List<Producto>(); 
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            using (var reader = command.ExecuteReader())
            {
                while(reader.Read()){
                    var producto = new Producto();
                    producto.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);
                    productos.Add(producto);
                }
            }
            connection.Close();
        } 
        return productos;
    }
    public bool ActualizarProducto(int idProducto, string nuevoNombre)
    {
        string query = "UPDATE Productos SET Descripcion = @descripcion WHERE idProducto = @idProducto";
        int filasAfectadas=0;
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
            var command = new SqliteCommand(query, connection);
            command.Parameters.Add(new SqliteParameter("@descripcion", nuevoNombre));
            command.Parameters.Add(new SqliteParameter("@idProducto", idProducto));
            filasAfectadas = command.ExecuteNonQuery();
            connection.Close(); 
        }
        return filasAfectadas > 0;
    }
}
