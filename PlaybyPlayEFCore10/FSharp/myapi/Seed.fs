module MyAPI.DataAccess.Seeder

open System
open System.Linq
open System.Collections.Generic
open Microsoft.Extensions.DependencyInjection
open MyAPI.Domain
open Newtonsoft.Json

let Seedit (serviceProvider:  IServiceProvider) jsonData =
    let events = JsonConvert.DeserializeObject<List<WeatherEvent>>(jsonData)

    use serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope()
    let context = serviceScope.ServiceProvider.GetService<WeatherContext>()

    let inline addWeather w = context.Add(w) |> ignore
    
    if not(context.WeatherEvents.Any()) then
        Seq.iter addWeather events
        context.SaveChanges() 
        |> ignore
      