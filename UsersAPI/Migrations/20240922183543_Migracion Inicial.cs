using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UsersAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "Christopher.Oberbrunner@yahoo.com", "Christopher Oberbrunner", "w7Do_cyuIM" },
                    { 2, "Curtis_Kiehn@gmail.com", "Curtis Kiehn", "CtR_rIe8TJ" },
                    { 3, "Katherine.Nicolas98@hotmail.com", "Katherine Nicolas", "jq3YVJhcMD" },
                    { 4, "Kristen74@hotmail.com", "Kristen Beer", "KIMQu93UGg" },
                    { 5, "Lindsey_Wilderman58@gmail.com", "Lindsey Wilderman", "gOqLT4Sjc5" },
                    { 6, "Corey.Bins63@yahoo.com", "Corey Bins", "oelOJReOfO" },
                    { 7, "Beatrice71@hotmail.com", "Beatrice Metz", "EnOGqmwReX" },
                    { 8, "Kayla_Smith@gmail.com", "Kayla Smith", "VlW4V06kIE" },
                    { 9, "Sophia_Buckridge74@yahoo.com", "Sophia Buckridge", "7k03JmDQ2d" },
                    { 10, "Roman_Kovacek3@hotmail.com", "Roman Kovacek", "bkoTbUCUsn" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
