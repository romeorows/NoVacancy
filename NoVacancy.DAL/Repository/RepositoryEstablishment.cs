using DataMapping;
using NoVacancy.DAL.Entities;
using NoVacancy.DAL.Interface;
using NoVacancy.DTO.DataTransferObjects;
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
            : base(new NoVacancyEntities())
        {
            MappingConfiguration.CreateEstablishment();
        }

        /// <summary>
        /// Get a single establishment
        /// </summary>
        /// <param name="id">string guid</param>
        /// <returns></returns>
        public DTOEstablishmentInfo GetEstablishment(string id)
        {
            var entity = new NoVacancyEntities();

            var result = new DTOEstablishmentInfo();

            try
            {
                var data = (from establishment in entity.trEstablishments
                           where establishment.Guid.ToString() == id
                           select new DTOEstablishmentInfo { 
                                 Guid =  establishment.Guid,
                                 Name =  establishment.Name,
                                 EstablishmentTypeID =  establishment.EstablishmentTypeID,
                                 ContactPerson =  establishment.ContactPerson,
                                 Email =  establishment.Email,
                                 Mobile =  establishment.Mobile ,
                                 Telephone =  establishment.Telephone ,
                                 Fax =  establishment.Fax ,
                                 WebSite =  establishment.WebSite ,
                                 Address =  establishment.Address ,
                                 CountryID =  establishment.CountryID ,
                                 CityID =  establishment.CityID ,
                                 Location =  establishment.Location ,
                                 Lat =  establishment.Lat ,
                                 Lng =  establishment.Lng ,
                                 Active =  establishment.Active ,
                                 DateDeactivated = establishment.DateDeactivated,
                           }).FirstOrDefault() ;

                result = data;
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.IsSuccessful = false;
                result.ErrorMsg = "DAL Error - " + ex.Message;
            }

            return result;
        }

    }
}
