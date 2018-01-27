namespace MyAPI.DataAccess

open Microsoft.EntityFrameworkCore
open MyAPI.Domain
open FSharp.Core
open System
open Unchecked

type WeatherContext =
    inherit DbContext
    
    new() = { inherit DbContext() }
    new(options: DbContextOptions<WeatherContext>) = { inherit DbContext(options) }


    //Weather Events
    [<DefaultValue>]
    val mutable weatherEvents:DbSet<WeatherEvent>
    member x.WeatherEvents 
        with get() = x.weatherEvents 
        and set v = x.weatherEvents <- v

    //Reactions
    [<DefaultValue>]
    val mutable reactions:DbSet<Reaction>
    member x.Reactions 
        with get() = x.reactions 
        and set v = x.reactions <- v

    //Comments
    [<DefaultValue>]
    val mutable comments:DbSet<Comment>
    member x.Comments 
        with get() = x.comments 
        and set v = x.comments <- v