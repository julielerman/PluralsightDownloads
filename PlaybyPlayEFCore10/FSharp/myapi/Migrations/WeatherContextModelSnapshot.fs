namespace MyAPI.Migrations

open System
open Microsoft.EntityFrameworkCore
open Microsoft.EntityFrameworkCore.Infrastructure
open Microsoft.EntityFrameworkCore.Metadata
open Microsoft.EntityFrameworkCore.Migrations
open MyAPI.DataAccess

[<DbContext(typeof<WeatherContext>)>]
type WeatherContextModelSnapshot() =
    inherit ModelSnapshot()

    override this.BuildModel(modelBuilder: ModelBuilder) =
        modelBuilder
            .HasAnnotation("ProductVersion", "1.0.1")
            .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
            |> ignore

        modelBuilder.Entity("MyAPI.Domain.Comment", 
            fun b ->
                b.Property<int>("CommentID").ValueGeneratedOnAdd() |> ignore 
                b.Property<int>("ReactionId") |> ignore
                b.Property<string>("Text") |> ignore
                b.HasKey("CommentID") |> ignore
                b.HasIndex("ReactionId") |> ignore
                b.ToTable("Comments") |> ignore
                
            )|> ignore

        modelBuilder.Entity("MyAPI.Domain.Reaction", 
            fun b ->
                b.Property<int>("Id").ValueGeneratedOnAdd() |> ignore
                b.Property<string>("Name") |> ignore
                b.Property<string>("Quote") |> ignore
                b.Property<int>("WeatherEventId") |> ignore
                b.HasKey("Id") |> ignore
                b.HasIndex("WeatherEventId") |> ignore
                b.ToTable("Reactions") |> ignore) |> ignore

        modelBuilder.Entity("MyAPI.Domain.WeatherEvent",
            fun b ->
                b.Property<int>("Id").ValueGeneratedOnAdd() |> ignore
                b.Property<DateTime>("Date") |> ignore
                b.Property<string>("MostCommonWord") |> ignore
                b.Property<TimeSpan>("Time") |> ignore
                b.Property<int>("Type") |> ignore
                b.HasKey("Id") |> ignore
                b.ToTable("WeatherEvents") |> ignore) |> ignore

        modelBuilder.Entity("MyAPI.Domain.Comment", 
            fun b ->
                b.HasOne("MyAPI.Domain.Reaction")
                 .WithMany("Comments")
                 .HasForeignKey("ReactionId") |> ignore) |> ignore

        modelBuilder.Entity("MyAPI.Domain.Reaction",
            fun b ->
                b.HasOne("MyAPI.Domain.WeatherEvent")
                 .WithMany("Reactions")
                 .HasForeignKey("WeatherEventId")
                 .OnDelete(DeleteBehavior.Cascade) |> ignore) |> ignore