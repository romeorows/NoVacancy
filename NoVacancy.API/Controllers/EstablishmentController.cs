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
    public class EstablishmentController : BaseController
    {

        public IEstablishment _IRepository;

        public EstablishmentController(IEstablishment _IRepository)
        {
            this._IRepository = _IRepository;
        }

        /// <summary>
        /// Create a new Establishment
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Create([FromBody] DTOEstablishment data)
        {
            try
            {
                var result = new DTOEstablishment();

                result = _IRepository.CreateEstablishment(data);

                return Request.CreateResponse(HttpStatusCode.Created, data);

            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Could not create establishment. Please report to issue.");
            }
        }

        public Object Get(string id)
        {
            try
            {

                var result = _IRepository.GetEstablishment(id);

                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }


    }
}
