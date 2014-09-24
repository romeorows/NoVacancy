using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NoVacancy.DTO
{

    /// <summary>
    /// Contains all the query condition
    /// </summary>
    [DataContract]
    public class DTOSearch
    {

        [DataMember]
        public List<Query> Queries { get; set; }

        [DataMember]
        public Int32 PageNo { get; set; }

        [DataMember]
        public Int32 PageSize { get; set; }

        [DataMember]
        public String SortColumn { get; set; }

        [DataMember]
        public Boolean Descending { get; set; }

    }

    /// <summary>
    /// Where condition per field
    /// </summary>
    [DataContract]
    public class SearchFilter
    {
        [DataMember]
        public String ParameterName { get; set; }
        [DataMember]
        public Object ParameterValue { get; set; }
        [DataMember]
        public String Operator { get; set; }
        [DataMember]
        public String EndOperator { get; set; }
    }

    /// <summary>
    /// Block of where condition
    /// </summary>
    [DataContract]
    public class Query
    {
        [DataMember]
        public List<SearchFilter> Filters { get; set; }
    }
}
