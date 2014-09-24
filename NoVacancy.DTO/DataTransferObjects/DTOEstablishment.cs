using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NoVacancy.DTO.DataTransferObjects
{

    public class DTOEstablishmentComplete
    {
        public int EstablishmentID {get;set;}
        public string Guid{get;set;}
        public string Name{get;set;}
        public int EstablishmentTypeID{get;set;}
        public string ContactPerson{get;set;}
        public string Email{get;set;}
        public string Mobile{get;set;}
        public string Telephone{get;set;}
        public string Fax{get;set;}
        public string WebSite{get;set;}
        public string Address{get;set;}
        public int CountryID{get;set;}
        public int CityID{get;set;}
        public string Location{get;set;}
        public float Lat{get;set;}
        public float Lng{get;set;}
        public int Active{get;set;}
        public DateTime? DateDeactivated{get;set;}
        public int CreatedBy{get;set;}
        public DateTime? DateCreated{get;set;}
        public int UpdatedBy{get;set;}
        public DateTime? DateUpdated{get;set;}
        public int DeletedBy{get;set;}
        public DateTime? DateDeleted { get; set; }
        public int Deleted{get;set;}
        public int Year{get;set;}
    }

    [DataContract]
    public class DTOEstablishmentInfo :DTOStatus
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int EstablishmentTypeID { get; set; }
        [DataMember]
        public string ContactPerson { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public string WebSite { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int CityID { get; set; }
        [DataMember]
        public string Location { get; set; }
        [DataMember]
        public float Lat { get; set; }
        [DataMember]
        public float Lng { get; set; }
        [DataMember]
        public int Active { get; set; }
        [DataMember]
        public DateTime? DateDeactivated { get; set; }
    }

    [DataContract]
    public class DTOEstablishment : DTOStatus
    {
        [DataMember]
        public string Guid { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int EstablishmentTypeID { get; set; }
        [DataMember]
        public string ContactPerson { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string Telephone { get; set; }
        [DataMember]
        public string Fax { get; set; }
        [DataMember]
        public string WebSite { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public int CountryID { get; set; }
        [DataMember]
        public int CityID { get; set; }
        [DataMember]
        public string Location { get; set; }
    }
}
