using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreWebAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace myapi.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
      WeatherContext _context;
       public WeatherController (WeatherContext context)
       {
         _context=context;
       }
        // GET api/values
        [HttpGet]
        public IEnumerable<WeatherEvent> Get()
        {
            return _context.WeatherEvents.Include(w=>w.Reactions)
            .ThenInclude(r=>r.Comments);
        }
    }
}