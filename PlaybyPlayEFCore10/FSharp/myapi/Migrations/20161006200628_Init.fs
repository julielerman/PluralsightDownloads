namespace MyAPI.Migrations

open System
open System.Collections.Generic
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Migrations
open Microsoft.EntityFrameworkCore.Metadata
open Microsoft.EntityFrameworkCore.Infrastructure

open MyAPI.DataAccess

open Microsoft.EntityFrameworkCore.Migrations.Operations
open Microsoft.EntityFrameworkCore.Migrations.Operations.Builders

//Table Types
type WeatherEventsTable = 
    { Id : OperationBuilder<AddColumnOperation>
      Date: OperationBuilder<AddColumnOperation>
      MostCommonWord: OperationBuilder<AddColumnOperation>
      Time: OperationBuilder<AddColumnOperation>
      Type: OperationBuilder<AddColumnOperation> }

type ReactionsTable =
    { Id: OperationBuilder<AddColumnOperation>
      Name: OperationBuilder<AddColumnOperation>
      Quote: OperationBuilder<AddColumnOperation>
      WeatherEventId : OperationBuilder<AddColumnOperation> }

type CommentsTable =
    { Id: OperationBuilder<AddColumnOperation>
      Text: OperationBuilder<AddColumnOperation>
      ReactionId: OperationBuilder<AddColumnOperation> }


[<DbContext(typeof<WeatherContext>)>]
[<Migration("20161006200628_Init")>]
type Init() =
    inherit Migration()
    
    override this.Up(migrationBuilder: MigrationBuilder) =
        migrationBuilder.CreateTable(
            name = "WeatherEvents",
            columns = 
                (fun table -> 
                    { Id = table.Column<int>(nullable = false)
                                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                      Date = table.Column<DateTime>(nullable = false)
                      MostCommonWord = table.Column<string>(nullable = true)
                      Time = table.Column<TimeSpan>(nullable = false)
                      Type = table.Column<int>(nullable = false) }),
            constraints = 
                fun table -> 
                    table.PrimaryKey("PK_WeatherEvents", (fun x -> x.Id :> obj))|> ignore ) |> ignore

        migrationBuilder.CreateTable(
            name = "Reactions",
            columns =
                (fun table -> 
                    { Id = table.Column<int>(nullable = false)
                                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                      Name = table.Column<string>(nullable = true)
                      Quote = table.Column<string>(nullable = true)
                      WeatherEventId = table.Column<int>(nullable = false) }),
            constraints = 
                fun table ->
                    table.PrimaryKey("PK_Reactions", fun (x: ReactionsTable) -> x.Id :> obj) |> ignore
                    table.ForeignKey(
                        name = "FK_Reactions_WeatherEvents_WeatherEventId",
                        column = (fun (x: ReactionsTable) -> x.WeatherEventId :> obj),
                        principalTable = "WeatherEvents",
                        principalColumn = "Id",
                        onDelete = ReferentialAction.Cascade) |> ignore) |> ignore

        migrationBuilder.CreateTable(
            name = "Comments",
            columns = 
                (fun table ->
                    { Id = table.Column<int>(nullable = false).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                      ReactionId = table.Column<int>(nullable = true)
                      Text = table.Column<string>(nullable = true) }),
            constraints = 
                fun table ->
                    table.PrimaryKey("PK_Comments", fun (x:CommentsTable) -> x.Id :> obj) |> ignore
                    table.ForeignKey(
                        name = "FK_Comments_Reactions_ReactionId",
                        column = (fun (x:CommentsTable) -> x.ReactionId :> obj),
                        principalTable = "Reactions",
                        principalColumn = "Id",
                        onDelete = ReferentialAction.Restrict) |> ignore) |> ignore

        migrationBuilder.CreateIndex(
            name = "IX_Comments_ReactionId",
            table = "Comments",
            column = "ReactionId") |> ignore

        migrationBuilder.CreateIndex(
            name = "IX_Reactions_WeatherEventId",
            table = "Reactions",
            column = "WeatherEventId") |> ignore
        

    override this.Down(migrationBuilder: MigrationBuilder) = 
        migrationBuilder.DropTable(name = "Comments") |> ignore
        migrationBuilder.DropTable(name = "Reactions") |> ignore
        migrationBuilder.DropTable(name = "WeatherEvents") |> ignore

    override this.BuildTargetModel(modelBuilder: ModelBuilder) =
        modelBuilder
            .HasAnnotation("ProductVersion", "1.0.1")
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
            |> ignore

        modelBuilder.Entity("MyAPI.Domain.Comment", 
            fun b ->
                b.Property<int>("CommentID").ValueGeneratedOnAdd() |> ignore
                b.Property<Nullable<int>>("ReactionId") |> ignore
                b.Property<string>("Text") |> ignore
                b.HasKey("CommentID") |> ignore
                b.HasIndex("ReactionId") |> ignore
                b.ToTable("Comments") |> ignore
        ) |> ignore

        modelBuilder.Entity("MyAPI.Domain.Reaction", 
            fun b ->
                b.Property<int>("Id").ValueGeneratedOnAdd() |> ignore
                b.Property<string>("Name") |> ignore
                b.Property<string>("Quote") |> ignore
                b.Property<int>("WeatherEventId") |> ignore
                b.HasKey("Id") |> ignore
                b.HasIndex("WeatherEventId") |> ignore
                b.ToTable("Reactions") |> ignore
        ) |> ignore

        modelBuilder.Entity("MyAPI.Domain.WeatherEvent", 
            fun b ->
                b.Property<int>("Id").ValueGeneratedOnAdd() |> ignore
                b.Property<DateTime>("Date") |> ignore
                b.Property<string>("MostCommonWord") |> ignore
                b.Property<TimeSpan>("Time") |> ignore
                b.Property<int>("Type") |> ignore
                b.HasKey("Id") |> ignore
                b.ToTable("WeatherEvents") |> ignore)|> ignore

        modelBuilder.Entity("MyAPI.Domain.Comment", 
            fun b ->
                b.HasOne("MyAPI.Domain.Reaction")
                 .WithMany("Comments")
                 .HasForeignKey("ReactionId") 
                 |> ignore) |> ignore

        modelBuilder.Entity("MyAPI.Domain.Reaction", 
            fun b ->
                b.HasOne("MyAPI.Domain.WeatherEvent")
                 .WithMany("Reactions")
                 .HasForeignKey("WeatherEventId")
                 .OnDelete(DeleteBehavior.Cascade) |> ignore) |> ignore
