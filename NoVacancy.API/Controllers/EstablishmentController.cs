using Ninject;
using NoVacancy.BL.IRepository;
using NoVacancy.DTO.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NoVacancy.API.Controllers
{
    public class EstablishmentController : ApiController
    {
        [Inject]
        public IEstablishment _IRepository;

        [HttpPost]
        public DTOEstablishment Create(DTOEstablishment data)
        {
            var result = new DTOEstablishment();
            result = _IRepository.CreateEstablishment(data);
            return result;
        }
    }
}
