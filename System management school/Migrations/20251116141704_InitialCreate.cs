using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System_management_school.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    IdClasse = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomClasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Niveau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnneeScolaire = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.IdClasse);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    IdParent = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.IdParent);
                });

            migrationBuilder.CreateTable(
                name: "Professeurs",
                columns: table => new
                {
                    IdProf = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialite = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professeurs", x => x.IdProf);
                });

            migrationBuilder.CreateTable(
                name: "Etudiants",
                columns: table => new
                {
                    IdEtudiant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateNaissance = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClasseId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etudiants", x => x.IdEtudiant);
                    table.ForeignKey(
                        name: "FK_Etudiants_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "IdClasse",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Etudiants_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "IdParent",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    IdAccount = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false),
                    ProfesseurIdProf = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.IdAccount);
                    table.ForeignKey(
                        name: "FK_Accounts_Professeurs_ProfesseurIdProf",
                        column: x => x.ProfesseurIdProf,
                        principalTable: "Professeurs",
                        principalColumn: "IdProf");
                });

            migrationBuilder.CreateTable(
                name: "Matieres",
                columns: table => new
                {
                    IdMatiere = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomMatiere = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coefficient = table.Column<float>(type: "real", nullable: false),
                    ProfesseurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matieres", x => x.IdMatiere);
                    table.ForeignKey(
                        name: "FK_Matieres_Professeurs_ProfesseurId",
                        column: x => x.ProfesseurId,
                        principalTable: "Professeurs",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Absences",
                columns: table => new
                {
                    IdAbsence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateAbsence = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Justifiee = table.Column<bool>(type: "bit", nullable: false),
                    EtudiantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Absences", x => x.IdAbsence);
                    table.ForeignKey(
                        name: "FK_Absences_Etudiants_EtudiantId",
                        column: x => x.EtudiantId,
                        principalTable: "Etudiants",
                        principalColumn: "IdEtudiant",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrateurs",
                columns: table => new
                {
                    IdAdmin = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrateurs", x => x.IdAdmin);
                    table.ForeignKey(
                        name: "FK_Administrateurs_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "IdAccount",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    IdNotification = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Contenu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEnvoi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.IdNotification);
                    table.ForeignKey(
                        name: "FK_Notifications_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "IdAccount",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmploisDuTemps",
                columns: table => new
                {
                    IdEmploi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeureDebut = table.Column<TimeSpan>(type: "time", nullable: false),
                    HeureFin = table.Column<TimeSpan>(type: "time", nullable: false),
                    ClasseId = table.Column<int>(type: "int", nullable: false),
                    MatiereId = table.Column<int>(type: "int", nullable: false),
                    ProfesseurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploisDuTemps", x => x.IdEmploi);
                    table.ForeignKey(
                        name: "FK_EmploisDuTemps_Classes_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classes",
                        principalColumn: "IdClasse",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmploisDuTemps_Matieres_MatiereId",
                        column: x => x.MatiereId,
                        principalTable: "Matieres",
                        principalColumn: "IdMatiere",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmploisDuTemps_Professeurs_ProfesseurId",
                        column: x => x.ProfesseurId,
                        principalTable: "Professeurs",
                        principalColumn: "IdProf",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Absences_EtudiantId",
                table: "Absences",
                column: "EtudiantId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ProfesseurIdProf",
                table: "Accounts",
                column: "ProfesseurIdProf");

            migrationBuilder.CreateIndex(
                name: "IX_Administrateurs_AccountId",
                table: "Administrateurs",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmploisDuTemps_ClasseId",
                table: "EmploisDuTemps",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploisDuTemps_MatiereId",
                table: "EmploisDuTemps",
                column: "MatiereId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploisDuTemps_ProfesseurId",
                table: "EmploisDuTemps",
                column: "ProfesseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Etudiants_ClasseId",
                table: "Etudiants",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Etudiants_ParentId",
                table: "Etudiants",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Matieres_ProfesseurId",
                table: "Matieres",
                column: "ProfesseurId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_AccountId",
                table: "Notifications",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Absences");

            migrationBuilder.DropTable(
                name: "Administrateurs");

            migrationBuilder.DropTable(
                name: "EmploisDuTemps");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Etudiants");

            migrationBuilder.DropTable(
                name: "Matieres");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "Professeurs");
        }
    }
}
