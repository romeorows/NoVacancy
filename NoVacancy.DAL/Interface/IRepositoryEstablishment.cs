using NoVacancy.DAL.Entities;
using NoVacancy.DAL.Repository;
using NoVacancy.DTO.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoVacancy.DAL.Interface
{
    public interface IRepositoryEstablishment : IRepository<trEstablishment>
    {
        /// <summary>
        /// Get a single establishment
        /// </summary>
        /// <param name="id">string guid</param>
        /// <returns></returns>
        DTOEstablishmentInfo GetEstablishment(string id);
    }
}
