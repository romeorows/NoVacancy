using Microsoft.Owin;
using NoVacancy.API.App_Start;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace NoVacancy.API
{
    //This will tell the application to run the Startup class when it starts
    [assembly: OwinStartup(typeof(NoVacancy.API.Startup))]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            NinjectWebCommon.Start();
        }
    }
}