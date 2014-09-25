using Ninject.Modules;
using NoVacancy.BL;
using NoVacancy.BL.IRepository;
using NoVacancy.BL.IRepositoryServiceImpl;
using NoVacancy.DAL.Interface;
using NoVacancy.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NoVacancy.API.App_Start
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositoryEstablishment>().To<RepositoryEstablishment>();
            Bind<IEstablishment>().To<Establishment>();
        }
    }
}