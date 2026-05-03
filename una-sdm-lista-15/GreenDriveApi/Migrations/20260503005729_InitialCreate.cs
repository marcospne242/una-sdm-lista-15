using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GreenDriveApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Baterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroSerie = table.Column<string>(type: "TEXT", nullable: false),
                    CapacidadeKWh = table.Column<double>(type: "REAL", nullable: false),
                    SaudeBateria = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baterias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstacoesCarga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Localizacao = table.Column<string>(type: "TEXT", nullable: false),
                    TipoCarga = table.Column<string>(type: "TEXT", nullable: false),
                    CargaDisponivelKW = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstacoesCarga", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosTelemetria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BateriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Temperatura = table.Column<double>(type: "REAL", nullable: false),
                    Voltagem = table.Column<double>(type: "REAL", nullable: false),
                    DataLeitura = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosTelemetria", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosTelemetria_Baterias_BateriaId",
                        column: x => x.BateriaId,
                        principalTable: "Baterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdensReciclagem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BateriaId = table.Column<int>(type: "INTEGER", nullable: false),
                    EstacaoId = table.Column<int>(type: "INTEGER", nullable: false),
                    Prioridade = table.Column<string>(type: "TEXT", nullable: false),
                    CustoProcessamento = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdensReciclagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdensReciclagem_Baterias_BateriaId",
                        column: x => x.BateriaId,
                        principalTable: "Baterias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdensReciclagem_EstacoesCarga_EstacaoId",
                        column: x => x.EstacaoId,
                        principalTable: "EstacoesCarga",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrdensReciclagem_BateriaId",
                table: "OrdensReciclagem",
                column: "BateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdensReciclagem_EstacaoId",
                table: "OrdensReciclagem",
                column: "EstacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosTelemetria_BateriaId",
                table: "RegistrosTelemetria",
                column: "BateriaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdensReciclagem");

            migrationBuilder.DropTable(
                name: "RegistrosTelemetria");

            migrationBuilder.DropTable(
                name: "EstacoesCarga");

            migrationBuilder.DropTable(
                name: "Baterias");
        }
    }
}
