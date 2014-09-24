using NoVacancy.DAL.Entities;
using NoVacancy.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoVacancy.DAL.Repository
{
    public sealed class RepositoryEstablishment : GenericRepository<trEstablishment>, IRepositoryEstablishment
    {
        public RepositoryEstablishment()
            :base (new NoVacancyEntities())
        {

        }
    }
}
