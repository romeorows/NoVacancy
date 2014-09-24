using NoVacancy.Common;
using NoVacancy.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class GenerateSearchQuery
    {
        public static void ParseQuery(out String queryText, out Object[] parameterArray, out String orderBy, out String descending, DTOSearch searchObject)
        {

            var searchQuery = new StringBuilder(); //return querytext
            var parameters = new List<Object>(); //return paramaterArray
            var filterCount = 0; //counter for the filters. increment for each filter added to the query

            if (searchObject.Queries != null)   //check if queries is not null
            {
                //loop to the queries
                foreach (var query in searchObject.Queries.Where(q => q.Filters != null && q.Filters.Count > 0))
                {
                    searchQuery.Append("(");//enclose all query into a parenthesis

                    var addedFilterCount = 0; //counter for the added filter. used for checking if we are on the last filter.

                    foreach (var item in query.Filters.Where(f => !String.IsNullOrWhiteSpace(f.ParameterName)))
                    {
                        if (item.Operator == Operators.Like) //different query format for the like condition
                        {
                            searchQuery.Append(String.Format("{0}.{1}(@{2}) ", item.ParameterName, item.Operator, filterCount));
                        }
                        else //if not like then do this formay
                        {
                            searchQuery.Append(String.Format("{0} {1} @{2} ", item.ParameterName, item.Operator, filterCount));
                        }

                        if (++addedFilterCount == query.Filters.Count) //enclose the query if we are on the last filter
                        {
                            searchQuery.Append(")");
                        }

                        searchQuery.Append(item.EndOperator); //add end operator if there are additional condition

                        parameters.Add(item.ParameterValue); //add parameters

                        filterCount++; //increment filter count for each added filter
                    }
                }
            }


            queryText = searchQuery.ToString(); //return querytext
            
            parameterArray = parameters.ToArray(); //return parameters

            //if Orderby does not contain a value and search object contains a filter, use that filter for the orderby clause
            if (searchObject.PageNo > 0 && String.IsNullOrWhiteSpace(searchObject.SortColumn) && searchObject.Queries.Count > 0 && searchObject.Queries.FirstOrDefault().Filters.Count > 0)
            {
                orderBy = searchObject.Queries.FirstOrDefault().Filters.FirstOrDefault().ParameterName;
            }
            else
            {
                orderBy = searchObject.SortColumn;
            }

            if (searchObject.Descending)
                descending = " DESC";
            else
                descending = "";
        }
    }
}
