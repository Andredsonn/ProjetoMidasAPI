using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoMidasAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projecoes",
                columns: table => new
                {
                    IdProjecao = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProjecao = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    DescricaoProjecao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DtInicial = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjecaoReceita = table.Column<float>(type: "real", nullable: false),
                    ProjecaoDespesa = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projecoes", x => x.IdProjecao);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUsuario = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    SenhaHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Lancamentos",
                columns: table => new
                {
                    IdLancamento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataLancamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<float>(type: "real", nullable: false),
                    TipoLancamento = table.Column<int>(type: "int", nullable: false),
                    Recorrencia = table.Column<bool>(type: "bit", nullable: false),
                    QtdRecorrencia = table.Column<int>(type: "int", nullable: true),
                    DescricaoRecorrencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FK_IdProjecao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lancamentos", x => x.IdLancamento);
                    table.ForeignKey(
                        name: "FK_Lancamentos_Projecoes_FK_IdProjecao",
                        column: x => x.FK_IdProjecao,
                        principalTable: "Projecoes",
                        principalColumn: "IdProjecao",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lancamentos_FK_IdProjecao",
                table: "Lancamentos",
                column: "FK_IdProjecao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lancamentos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Projecoes");
        }
    }
}
