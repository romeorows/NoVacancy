using NoVacancy.BL.IRepository;
using NoVacancy.DAL.Interface;
using NoVacancy.DTO.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoVacancy.BL.IRepositoryServiceImpl
{
    public class Establishment : IEstablishment
    {
        
        public IRepositoryEstablishment _IRepository;

        public Establishment(IRepositoryEstablishment _IRepository)
        {
            this._IRepository = _IRepository;
        }

        public DTOEstablishment CreateEstablishment(DTOEstablishment data)
        {
            var result = new DTOEstablishment();

            try
            {

                result = _IRepository.Add<DTOEstablishment, DTOEstablishment>(data);
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.ErrorMsg = "BL Error: " + ex.Message;
            }
            return result;
        }
    }
}
