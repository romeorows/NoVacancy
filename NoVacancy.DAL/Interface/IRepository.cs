using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NoVacancy.DAL.Interface
{
    public interface IRepository<TObject>
    {
        #region GET

        /// <summary>
        /// Generic method for getting all the rows in the table.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <returns>List of the output object type.</returns>
        List<TOuput> GetAll<TOuput>();
        /// <summary>
        /// Generic method for getting all the rows in the table which will match the filter.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="filter">A lambda expression that will filter the list.</param>
        /// <returns>List of the output object type.</returns>
        List<TOuput> GetAll<TInput, TOuput>(Expression<Func<TInput, bool>> filter);
        /// <summary>
        /// Generic method for getting all the rows in the table which corresponds with the query string.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="strQuery">A string object which contains the query condition for the search.</param>
        /// <param name="objValues">An array of object values which will be referenced during the search.</param>
        /// <param name="pageNo">Page number the user is currently viewing.</param>
        /// <param name="pageSize">Total number of rows per page.</param>
        /// <param name="orderBy">Column to order by the rows.</param>
        /// <returns>List of the output object type.</returns>
        List<TOuput> GetAll<TOuput>(String strQuery, object[] objValues, Int32 pageNo, Int32 pageSize, String orderBy);

        /// <summary>
        /// Generic asynchronous method for getting all the rows in the table.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <returns>List of the output object type.</returns>
        Task<List<TOuput>> GetAllAsync<TOuput>();
        /// <summary>
        /// Generic asynchronous method for getting all the rows in the table which will match the filter.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="filter">A lambda expression that will filter the list.</param>
        /// <returns>List of the output object type.</returns>
        Task<List<TOuput>> GetAllAsync<TInput, TOuput>(Expression<Func<TInput, bool>> filter);

        /// <summary>
        /// Generic method for getting a single row in the table which will match the filter.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="filter">A lambda expression that will filter the list.</param>
        /// <returns>Output object</returns>
        TOuput Get<TInput, TOuput>(Expression<Func<TObject, bool>> filter);
        /// <summary>
        /// Generic method for getting a single row in the table which corresponds with the query string.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="strQuery">A string object which contains the query condition for the search.</param>
        /// <param name="objValues">An array of object values which will be referenced during the search.</param>
        /// <returns>Output object</returns>
        TOuput Get<TOuput>(String strQuery, object[] objValues);
        /// <summary>
        /// Generic asynchronous method for getting a single row in the table which will match the filter.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="filter">A lambda expression that will filter the list.</param>
        /// <returns>Output object</returns>
        Task<TOuput> GetAsync<TInput, TOuput>(Expression<Func<TInput, bool>> filter);

        /// <summary>
        /// Generic method for getting a single row in table based on its ID.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="id">Integer parameted for the ID</param>
        /// <returns>Output object</returns>
        TOuput FindID<TOuput>(int id);
        /// <summary>
        /// Generic asynchronous method for getting a single row in table based on its ID.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="id">Integer parameted for the ID</param>
        /// <returns>Output object</returns>
        Task<TOuput> FindIDAsync<TOuput>(int id);
        
        #endregion

        #region ADD

        /// <summary>
        /// Generic method to insert a new data in the table.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">Input object that will be inserted in the table.</param>
        /// <returns>Inserted object.</returns>
        TOuput Add<TInput, TOuput>(TInput input);
        /// <summary>
        /// Generic asynchronous method to insert a new data in the table.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">Input object that will be inserted in the table.</param>
        /// <returns>Inserted object.</returns>
        Task<TOuput> AddAsync<TInput, TOuput>(TInput input);
        /// <summary>
        /// Generic method that will insert multiple rows in the table.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">List of input object that will be inserted in the table.</param>
        /// <returns>List of the inserted object.</returns>
        List<TOutPut> AddMultiple<TInput, TOutPut>(List<TInput> input);
        /// <summary>
        /// Generic asynchronous method that will insert multiple rows in the table.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">List of input object that will be inserted in the table.</param>
        /// <returns>List of the inserted object.</returns>
        Task<List<TOutPut>> AddMultipleAsync<TInput, TOutPut>(List<TInput> input);

        #endregion

        #region UPDATE

        /// <summary>
        /// Generic method to update specific properties of an object in the database.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">Input object that will be updated in the table.</param>
        /// <param name="properties">List of string which contains the property of string to update</param>
        /// <returns>Updated Object</returns>
        TOutPut UpdateProperties<TOutPut, TInput>(TInput input, List<String> properties);
        /// <summary>
        /// Generic asynchronous method to update specific properties of an object in the database.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">Input object that will be updated in the table.</param>
        /// <param name="properties">List of string which contains the property of string to update</param>
        /// <returns>Updated Object</returns>
        Task<TOutPut> UpdatePropertiesAsync<TOutPut, TInput>(TInput input, List<String> properties);
        /// <summary>
        /// Generic method to update an object in the database.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">Input object that will be updated in the table.</param>
        /// <returns>Updated Object</returns>
        TOutPut Update<TOutPut, TInput>(TInput input);
        /// <summary>
        /// Generic asynchronous method to update an object in the database.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">Input object that will be updated in the table.</param>
        /// <returns>Updated Object</returns>
        Task<TOutPut> UpdateAsync<TOutPut, TInput>(TInput input);
        
        #endregion

        #region DELETE

        /// <summary>
        /// Generic method for deleting an object in the database.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <param name="input">Input object that will be deleted in the table.</param>
        /// <returns>true or false</returns>
        Boolean Delete<TInput>(TInput input);
        /// <summary>
        /// Generic asychronous method for deleting an object in the database.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <param name="input">Input object that will be deleted in the table.</param>
        /// <returns>true or false</returns>
        Task<Boolean> DeleteAsync<TInput>(TInput input);
        /// <summary>
        /// Generic method for deleting an object in the database based on its ID.
        /// </summary>
        /// <param name="id">Integer parameted for the ID</param>
        /// <returns>true or false</returns>
        Boolean DeleteID(int id);
        /// <summary>
        /// Generic asynchronous method for deleting an object in the database based on its ID.
        /// </summary>
        /// <param name="id">Integer parameted for the ID</param>
        /// <returns>true or false</returns>
        Task<Boolean> DeleteIDAsync(int id);

        #endregion

        #region COUNT
        /// <summary>
        /// Generic method to count the records in the table.
        /// </summary>
        /// <returns>Count of the rows</returns>
        int Count();
        /// <summary>
        /// Generic asynchronous method to count the records in the table.
        /// </summary>
        /// <returns>Count of the rows</returns>
        Task<int> CountAsync();
        
        #endregion
    }
}

