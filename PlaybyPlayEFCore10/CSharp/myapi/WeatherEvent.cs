//weatherEvent domain
using System;
using System.Collections.Generic;

namespace EFCoreWebAPI
{
    public class WeatherEvent
    {
        public static WeatherEvent Create(DateTime date, WeatherType type)
        {
            return new WeatherEvent(date, type);
        }

        public static WeatherEvent Create(DateTime date, WeatherType type,  List<string[]> reactions)
        {
            var wE = new WeatherEvent(date, type);
            foreach (var reaction in reactions)
            {
                wE.Reactions.Add(new Reaction { Name = reaction[0], Quote = reaction[1] });
                Console.WriteLine($"Reactions Count {wE.Reactions.Count}");
            }
            return wE;
        }

        public WeatherEvent() { }

        private WeatherEvent(DateTime dateTime, WeatherType type)
        {
            this.Date = dateTime.Date;
            this.Time = dateTime.TimeOfDay;
            Type = type;
            Reactions = new List<Reaction>();
          
        }
        public int Id { get; set; }
        public DateTime Date { get;  set; }
        public TimeSpan Time { get;  set; }
        public WeatherType Type { get;  set; }
        public ICollection<Reaction> Reactions { get; set; }
        public string MostCommonWord { get; set; }
         }

     public class Reaction
    {
        public Reaction(){
          Comments=new List<Comment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quote { get; set; }
        public int WeatherEventId { get; set; }
        public ICollection<Comment> Comments {get;set;}

    }
    public class Comment{
        public int Id { get; set; }
        public string Text { get; set; }
       
    }
    public enum WeatherType
    {
        Rain = 1,
        Snow = 2,
        Sleet = 3,
        Hail = 4,
        Sun = 5,
        Cloudy = 6
    }

}