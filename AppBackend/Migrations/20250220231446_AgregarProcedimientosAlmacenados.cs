using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppBackend.Migrations
{
    /// <inheritdoc />
    public partial class AgregarProcedimientosAlmacenados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            CREATE PROCEDURE ListarProductos
            AS
            BEGIN
                SELECT ID, Nombre, Price, Stock FROM Productos;
            END;
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE ObtenerDetallesProducto
                @Id INT
            AS
            BEGIN
                SELECT * FROM Productos WHERE Id = @Id;
            END;
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE AgregarProducto
                @Nombre NVARCHAR(100),
                @Precio DECIMAL(10,2),
                @Description NVARCHAR(max),
                @stock int
            AS
            BEGIN
                INSERT INTO Productos (Nombre, Description, Price, Stock) VALUES (@Nombre, @Description, @Precio, @stock);
            END;
        ");
            //
            migrationBuilder.Sql(@"
            CREATE PROCEDURE EditarProducto
                @Id INT,
                @Nombre NVARCHAR(100),
                @Precio DECIMAL(10,2),
                @Description NVARCHAR(max),
                @stock int
            AS
            BEGIN
                UPDATE Productos SET Nombre = @Nombre, Price = @Precio, Description = @Description, stock = @stock WHERE Id = @Id;
            END;
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE VenderProducto
                @Id INT,
                @cantidadVender int
            AS
            BEGIN
                UPDATE Productos
                SET Stock = Stock - @cantidadVender
                WHERE Id = @Id AND Stock >= @cantidadVender;
            END;
        ");

            migrationBuilder.Sql(@"
            CREATE PROCEDURE EliminarProducto
                @Id INT
            AS
            BEGIN
                DELETE FROM Productos WHERE Id = @Id;
            END;
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS ListarProductos;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS ObtenerDetallesProducto;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AgregarProducto;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS EditarProducto;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS VenderProducto;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS EliminarProducto;");
        }
    }
}
