using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace delaid_server.Controllers
{
    public class ScheduleController : ApiController
    {
        public string[] Get()
        {
            return new string[]
            {
                "Red Line",
                "Green Line",
                "Blue Line"
            };
        }
    }
}
