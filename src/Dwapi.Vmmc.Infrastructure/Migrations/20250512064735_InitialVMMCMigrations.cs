using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dwapi.Vmmc.Infrastructure.Migrations
{
    public partial class InitialVMMCMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dockets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Instance = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dockets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MasterFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 120, nullable: false),
                    County = table.Column<string>(maxLength: 120, nullable: false),
                    SnapshotDate = table.Column<DateTime>(nullable: true),
                    SnapshotSiteCode = table.Column<int>(nullable: true),
                    SnapshotVersion = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterFacilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    AuthCode = table.Column<string>(nullable: false),
                    DocketId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscribers_Dockets_DocketId",
                        column: x => x.DocketId,
                        principalTable: "Dockets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 120, nullable: false),
                    MasterFacilityId = table.Column<int>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Emr = table.Column<string>(nullable: false),
                    SnapshotDate = table.Column<DateTime>(nullable: true),
                    SnapshotSiteCode = table.Column<int>(nullable: true),
                    SnapshotVersion = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facilities_MasterFacilities_MasterFacilityId",
                        column: x => x.MasterFacilityId,
                        principalTable: "MasterFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Manifests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    SiteCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Sent = table.Column<int>(nullable: false),
                    Recieved = table.Column<int>(nullable: false),
                    DateLogged = table.Column<DateTime>(nullable: false),
                    DateArrived = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StatusDate = table.Column<DateTime>(nullable: false),
                    FacilityId = table.Column<Guid>(nullable: false),
                    EmrId = table.Column<Guid>(nullable: true),
                    EmrName = table.Column<string>(nullable: false),
                    EmrSetup = table.Column<int>(nullable: false),
                    Session = table.Column<Guid>(nullable: true),
                    Start = table.Column<DateTime>(nullable: true),
                    End = table.Column<DateTime>(nullable: true),
                    Tag = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manifests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manifests_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VmmcDiscontinuationExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    PatientPk = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    Emr = table.Column<string>(nullable: false),
                    Project = table.Column<string>(nullable: false),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    FacilityId = table.Column<Guid>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    VMMCId = table.Column<string>(nullable: true),
                    DiscontinuationDate = table.Column<DateTime>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VmmcDiscontinuationExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VmmcDiscontinuationExtracts_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VmmcEnrollmentExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    PatientPk = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    Emr = table.Column<string>(nullable: false),
                    Project = table.Column<string>(nullable: false),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    FacilityId = table.Column<Guid>(nullable: false),
                    FacilityName = table.Column<string>(nullable: false),
                    RecordUUID = table.Column<string>(nullable: true),
                    VMMCId = table.Column<string>(nullable: true),
                    EncounterDate = table.Column<DateTime>(nullable: true),
                    ReferredBy = table.Column<string>(nullable: true),
                    SourceVMMCInformation = table.Column<string>(nullable: true),
                    CountyOfOrigin = table.Column<string>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VmmcEnrollmentExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VmmcEnrollmentExtracts_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VmmcFollowupVisitExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    PatientPk = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    Emr = table.Column<string>(nullable: false),
                    Project = table.Column<string>(nullable: false),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    FacilityId = table.Column<Guid>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    VMMCId = table.Column<string>(nullable: true),
                    EncounterDate = table.Column<DateTime>(nullable: true),
                    VisitType = table.Column<string>(nullable: true),
                    DaySinceLastCircumsicion = table.Column<string>(nullable: true),
                    AdverseEventPostCircumcision = table.Column<string>(nullable: true),
                    AEType = table.Column<string>(nullable: true),
                    AEDescription = table.Column<string>(nullable: true),
                    AESeverity = table.Column<string>(nullable: true),
                    AEManagement = table.Column<string>(nullable: true),
                    MedicationGiven = table.Column<string>(nullable: true),
                    Drug = table.Column<string>(nullable: true),
                    CadreClinician = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VmmcFollowupVisitExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VmmcFollowupVisitExtracts_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VmmcMhpeExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    PatientPk = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    Emr = table.Column<string>(nullable: false),
                    Project = table.Column<string>(nullable: false),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    FacilityId = table.Column<Guid>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    VMMCId = table.Column<string>(nullable: true),
                    EncounterDate = table.Column<DateTime>(nullable: true),
                    HIVStatus = table.Column<string>(nullable: true),
                    Reasonresultsunknown = table.Column<string>(nullable: true),
                    ReferredServices = table.Column<string>(nullable: true),
                    HIVStatusSelfReport = table.Column<string>(nullable: true),
                    FacilityReceivingCare = table.Column<string>(nullable: true),
                    CCCNumber = table.Column<string>(nullable: true),
                    CurrentRegimen = table.Column<string>(nullable: true),
                    LastVL = table.Column<string>(nullable: true),
                    CD4Count = table.Column<string>(nullable: true),
                    BleedingDisorder = table.Column<string>(nullable: true),
                    Diabetes = table.Column<string>(nullable: true),
                    UrethralDischarge = table.Column<string>(nullable: true),
                    GenitalSore = table.Column<string>(nullable: true),
                    PainUrination = table.Column<string>(nullable: true),
                    SwellingScrotum = table.Column<string>(nullable: true),
                    DifficultyRetractingForeskin = table.Column<string>(nullable: true),
                    DifficultyReturninForeskinNormal = table.Column<string>(nullable: true),
                    SexualFunctionConcerns = table.Column<string>(nullable: true),
                    Epispadia = table.Column<string>(nullable: true),
                    Hypospadia = table.Column<string>(nullable: true),
                    Other = table.Column<string>(nullable: true),
                    Anaemia = table.Column<string>(nullable: true),
                    HIVAIDS = table.Column<string>(nullable: true),
                    StartARTDate = table.Column<DateTime>(nullable: true),
                    Adherance = table.Column<string>(nullable: true),
                    NextAppointmentDate = table.Column<DateTime>(nullable: true),
                    HB = table.Column<string>(nullable: true),
                    SugarLevels = table.Column<string>(nullable: true),
                    ClientEverHadSurgery = table.Column<string>(nullable: true),
                    SpecifySurgery = table.Column<string>(nullable: true),
                    TetanusBoosterGiven = table.Column<string>(nullable: true),
                    DateTetanusBoosterGiven = table.Column<DateTime>(nullable: true),
                    BP = table.Column<string>(nullable: true),
                    PulseRate = table.Column<string>(nullable: true),
                    Temperature = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VmmcMhpeExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VmmcMhpeExtracts_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VmmcProcedureExtracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    PatientPk = table.Column<int>(nullable: false),
                    SiteCode = table.Column<int>(nullable: false),
                    Emr = table.Column<string>(nullable: false),
                    Project = table.Column<string>(nullable: false),
                    Processed = table.Column<bool>(nullable: true),
                    QueueId = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StatusDate = table.Column<DateTime>(nullable: true),
                    DateExtracted = table.Column<DateTime>(nullable: true),
                    FacilityId = table.Column<Guid>(nullable: false),
                    FacilityName = table.Column<string>(nullable: true),
                    RecordUUID = table.Column<string>(nullable: true),
                    VMMCId = table.Column<string>(nullable: true),
                    Method = table.Column<string>(nullable: true),
                    SurgicalMethod = table.Column<string>(nullable: true),
                    DeviceName = table.Column<string>(nullable: true),
                    DeviceSize = table.Column<string>(nullable: true),
                    AnaesthesiaUsed = table.Column<string>(nullable: true),
                    Agent = table.Column<string>(nullable: true),
                    Concentration = table.Column<string>(nullable: true),
                    Volume = table.Column<string>(nullable: true),
                    TimePlacementDevice = table.Column<string>(nullable: true),
                    TimeMakingLastSllit = table.Column<string>(nullable: true),
                    AdverseEvent = table.Column<string>(nullable: true),
                    AdverseEventtype = table.Column<string>(nullable: true),
                    AEDescription = table.Column<string>(nullable: true),
                    AESeverity = table.Column<string>(nullable: true),
                    AdverseEventsManagement = table.Column<string>(nullable: true),
                    CadreClinician = table.Column<string>(nullable: true),
                    CadreAssistanClinician = table.Column<string>(nullable: true),
                    TheatreRegisterNumber = table.Column<string>(nullable: true),
                    BP = table.Column<string>(nullable: true),
                    PulseRate = table.Column<string>(nullable: true),
                    Temperature = table.Column<string>(nullable: true),
                    PenisElevatedAbdomen = table.Column<string>(nullable: true),
                    PostProcedureInstruction = table.Column<string>(nullable: true),
                    PostOperationMedicationGiven = table.Column<string>(nullable: true),
                    Drug = table.Column<string>(nullable: true),
                    RemovalDate = table.Column<DateTime>(nullable: true),
                    ScheduledNextVisit = table.Column<string>(nullable: true),
                    CadreDischargedBy = table.Column<string>(nullable: true),
                    Date_Created = table.Column<DateTime>(nullable: true),
                    Date_Last_Modified = table.Column<DateTime>(nullable: true),
                    Voided = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VmmcProcedureExtracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VmmcProcedureExtracts_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cargoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RefId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Items = table.Column<string>(nullable: false),
                    ManifestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cargoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cargoes_Manifests_ManifestId",
                        column: x => x.ManifestId,
                        principalTable: "Manifests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cargoes_ManifestId",
                table: "Cargoes",
                column: "ManifestId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_MasterFacilityId",
                table: "Facilities",
                column: "MasterFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Manifests_FacilityId",
                table: "Manifests",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_DocketId",
                table: "Subscribers",
                column: "DocketId");

            migrationBuilder.CreateIndex(
                name: "IX_VmmcDiscontinuationExtracts_FacilityId",
                table: "VmmcDiscontinuationExtracts",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_VmmcEnrollmentExtracts_FacilityId",
                table: "VmmcEnrollmentExtracts",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_VmmcFollowupVisitExtracts_FacilityId",
                table: "VmmcFollowupVisitExtracts",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_VmmcMhpeExtracts_FacilityId",
                table: "VmmcMhpeExtracts",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_VmmcProcedureExtracts_FacilityId",
                table: "VmmcProcedureExtracts",
                column: "FacilityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cargoes");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "VmmcDiscontinuationExtracts");

            migrationBuilder.DropTable(
                name: "VmmcEnrollmentExtracts");

            migrationBuilder.DropTable(
                name: "VmmcFollowupVisitExtracts");

            migrationBuilder.DropTable(
                name: "VmmcMhpeExtracts");

            migrationBuilder.DropTable(
                name: "VmmcProcedureExtracts");

            migrationBuilder.DropTable(
                name: "Manifests");

            migrationBuilder.DropTable(
                name: "Dockets");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "MasterFacilities");
        }
    }
}
