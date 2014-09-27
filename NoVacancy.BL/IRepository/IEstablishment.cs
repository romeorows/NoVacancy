using NoVacancy.DTO.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoVacancy.BL.IRepository
{
    public interface IEstablishment
    {
        DTOEstablishment CreateEstablishment(DTOEstablishment data);

        /// <summary>
        /// Get a single establishment
        /// </summary>
        /// <param name="id">string guid</param>
        /// <returns></returns>
        DTOEstablishmentInfo GetEstablishment(string id);
    }
}
