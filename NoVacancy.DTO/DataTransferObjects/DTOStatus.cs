using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NoVacancy.DTO
{
    [DataContract]
    public class DTOStatus
    {
        [DataMember]
        public String ErrorMsg { get; set; }
        [DataMember]
        public Boolean IsSuccessful { get; set; }
    }
}
