using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using EFCoreWebAPI;
using Newtonsoft.Json;

public static class Seeder {
  public static void Seedit(string jsonData, IServiceProvider serviceProvider) {
   
    List<WeatherEvent> events =
      JsonConvert.DeserializeObject<List<WeatherEvent>>(jsonData);
    using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
      var context = serviceScope.ServiceProvider.GetService<WeatherContext>();
      if (!context.WeatherEvents.Any()) {
        context.AddRange(events);
        context.SaveChanges();
      }
    }
  }
}