//Weather Event Domain
namespace MyAPI.Domain

open System
open System.Collections.Generic
open System.Linq

type WeatherType = 
    | Rain = 1
    | Snow = 2
    | Sleet = 3
    | Hail = 4
    | Sun = 5
    | Cloudy = 6

[<CLIMutable>]
type Comment = { Id: int; Text: string }

[<CLIMutable>]
type Reaction = 
    { Id: int
      Name: string
      Quote: string
      WeatherEventId: int
      Comments: ICollection<Comment> }

[<CLIMutable>]
type WeatherEvent = 
    { Id: int
      Date: DateTime
      Time: TimeSpan
      Type: WeatherType 
      Reactions: ICollection<Reaction>
      MostCommonWord: string }

module Weather =

    let create (date:DateTime) (weatherType: WeatherType) =
        { Id = 0
          Date = date.Date
          Time = date.TimeOfDay
          Type = weatherType
          Reactions = List<Reaction>()
          MostCommonWord = "" }

    let createWithReactions (date:DateTime) (weatherType: WeatherType) (reactions: string[] list) = 
        
        let getReaction (reaction:string[]) =
            { Id = 0
              Name = reaction.[0]
              Quote = reaction.[1]
              WeatherEventId = 0
              Comments = List<Comment>() }
        
        { Id = 0
          Date = date.Date
          Time = date.TimeOfDay
          Type = weatherType
          Reactions = reactions.Select(getReaction).ToList()
          MostCommonWord = "" }
