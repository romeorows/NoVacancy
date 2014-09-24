using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using NoVacancy.DAL.Interface;
using NoVacancy.Common;
using DataMapping;


namespace NoVacancy.DAL.Repository
{
    public abstract class GenericRepository<TObject> : IRepository<TObject> where TObject : class 
    {
        /// <summary>
        /// A DbContext instance represents the entity the we will use
        /// such that it can be used to query from a database and 
        /// group together changes that will then be written back to the store as a unit.
        /// </summary>
        protected DbContext _context;
        
        #region CONSTRUCTOR
        /// <summary>
        /// When GenericRepository class is inherited in another class we will need to supply which DbContext to use.
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository(DbContext context)
        {
            _context = context;
        }
        
        #endregion

        #region GET

        /// <summary>
        /// Generic method for getting all the rows in the table.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <returns>List of the output object type.</returns>
        public List<TOuput> GetAll<TOuput>()
        {
            var entity = _context.Set<TObject>();
            var result = MappingConfiguration.MapObjectsList<TObject,TOuput>(entity.ToList());
            return result;
        }

        /// <summary>
        /// Generic method for getting all the rows in the table which will match the filter.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="filter">A lambda expression that will filter the list.</param>
        /// <returns>List of the output object type.</returns>
        public List<TOuput> GetAll<TInput,TOuput >(Expression<Func<TInput, bool>> filter)
        {
            var where = AutoMapperExtensions.GetMappedSelector<TInput, TObject>(filter);
            var entity = _context.Set<TObject>().Where(where);
            var result = MappingConfiguration.MapObjectsList<TObject, TOuput>(entity.ToList());
            return result;
        }

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
        public List<TOuput> GetAll<TOuput>(String strQuery, object[] objValues, Int32 pageNo, Int32 pageSize, String orderBy)
        {
            List<TOuput> result = null;
            IQueryable<TObject> entities;
            if (string.IsNullOrWhiteSpace(strQuery))
            {
                entities = _context.Set<TObject>();
            }
            else
            {
                entities = _context.Set<TObject>().Where(strQuery, objValues);
            }

            if (entities != null && pageNo > 0)
            {
                var pagedRes = entities.OrderBy(orderBy).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
                result = MappingConfiguration.MapObjectsList<TObject, TOuput>(pagedRes);
            }
            else
            {
                result = MappingConfiguration.MapObjectsList<TObject, TOuput>(entities.ToList());
            }
            return result;
        }

        /// <summary>
        /// Generic asynchronous method for getting all the rows in the table.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <returns>List of the output object type.</returns>
        public async Task<List<TOuput>> GetAllAsync<TOuput>()
        {
            var entity = await _context.Set<TObject>().ToListAsync();
            var result = MappingConfiguration.MapObjectsList<TObject,TOuput>(entity.ToList());
            return result;
        }

        /// <summary>
        /// Generic asynchronous method for getting all the rows in the table which will match the filter.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="filter">A lambda expression that will filter the list.</param>
        /// <returns>List of the output object type.</returns>
        public async Task<List<TOuput>> GetAllAsync<TInput, TOuput>(Expression<Func<TInput, bool>> filter)
        {
            var where = AutoMapperExtensions.GetMappedSelector<TInput, TObject>(filter);
            var entity = await _context.Set<TObject>().Where(where).ToListAsync();
            var result = MappingConfiguration.MapObjectsList<TObject, TOuput>(entity.ToList());
            return result;
        }

        /// <summary>
        /// Generic method for getting a single row in the table which will match the filter.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="filter">A lambda expression that will filter the list.</param>
        /// <returns>Output object</returns>
        public TOuput Get<TInput, TOuput>(Expression<Func<TObject, bool>> filter)
        {
            //var where = AutoMapperExtensions.GetMappedSelector<TInput, TObject>(filter);
            var entity = _context.Set<TObject>().FirstOrDefault(filter);
            var result = MappingConfiguration.MapObjects<TObject, TOuput>(entity);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return result;
        }

        /// <summary>
        /// Generic method for getting a single row in the table which corresponds with the query string.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="strQuery">A string object which contains the query condition for the search.</param>
        /// <param name="objValues">An array of object values which will be referenced during the search.</param>
        /// <returns>Output object</returns>
        public TOuput Get<TOuput>(String strQuery, object[] objValues)
        {
            var entities = _context.Set<TObject>().Where(strQuery, objValues).FirstOrDefault();
            var result = MappingConfiguration.MapObjects<TObject, TOuput>(entities);
            if (entities != null)
            {
                _context.Entry(entities).State = EntityState.Detached;
            }
            return result;
        }
        
        /// <summary>
        /// Generic asynchronous method for getting a single row in the table which will match the filter.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="filter">A lambda expression that will filter the list.</param>
        /// <returns>Output object</returns>
        public async Task<TOuput> GetAsync<TInput, TOuput>(Expression<Func<TInput, bool>> filter)
        {
            var where = AutoMapperExtensions.GetMappedSelector<TInput, TObject>(filter);
            var entity = await _context.Set<TObject>().FirstOrDefaultAsync(where);
            var result = MappingConfiguration.MapObjects<TObject, TOuput>(entity);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return result;
        }

        /// <summary>
        /// Generic method for getting a single row in table based on its ID.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="id">Integer parameted for the ID</param>
        /// <returns>Output object</returns>
        public TOuput FindID<TOuput>(int id)
        {
            var entity = _context.Set<TObject>().Find(id);
            var result = MappingConfiguration.MapObjects<TObject, TOuput>(entity);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return result;
        }

        /// <summary>
        /// Generic asynchronous method for getting a single row in table based on its ID.
        /// </summary>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="id">Integer parameted for the ID</param>
        /// <returns>Output object</returns>
        public async Task<TOuput> FindIDAsync<TOuput>(int id)
        {
            var entity = await _context.Set<TObject>().FindAsync(id);
            var result = MappingConfiguration.MapObjects<TObject, TOuput>(entity);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return result;
        }

        #endregion

        #region ADD

        /// <summary>
        /// Generic method to insert a new data in the table.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">Input object that will be inserted in the table.</param>
        /// <returns>Inserted object.</returns>
        public TOuput Add<TInput, TOuput>(TInput input)
        {
            var addobject = MappingConfiguration.MapObjects<TInput, TObject>(input);
            _context.Set<TObject>().Add(addobject);
            _context.SaveChanges();
            var result = MappingConfiguration.MapObjects<TObject, TOuput>(addobject);
            return result;
        }

        /// <summary>
        /// Generic asynchronous method to insert a new data in the table.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">Input object that will be inserted in the table.</param>
        /// <returns>Inserted object.</returns>
        public async Task<TOuput> AddAsync<TInput, TOuput>(TInput input)
        {
            var addobject = MappingConfiguration.MapObjects<TInput, TObject>(input);
            _context.Set<TObject>().Add(addobject);
            await _context.SaveChangesAsync();
            var result = MappingConfiguration.MapObjects<TObject, TOuput>(addobject);
            return result;
        }

        /// <summary>
        /// Generic method that will insert multiple rows in the table.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">List of input object that will be inserted in the table.</param>
        /// <returns>List of the inserted object.</returns>
        public List<TOutPut> AddMultiple<TInput, TOutPut>(List<TInput> input)
        {

            var entityObjList = MappingConfiguration.MapObjectsList<TInput,TObject>(input);
            foreach (var addobject in entityObjList)
            {
                _context.Set<TObject>().Add(addobject);
            }
            _context.SaveChanges();
            var result = MappingConfiguration.MapObjectsList<TObject, TOutPut>(entityObjList);
            return result;
        }

        /// <summary>
        /// Generic asynchronous method that will insert multiple rows in the table.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">List of input object that will be inserted in the table.</param>
        /// <returns>List of the inserted object.</returns>
        public async Task<List<TOutPut>> AddMultipleAsync<TInput, TOutPut>(List<TInput> input)
        {

            var entityObjList = MappingConfiguration.MapObjectsList<TInput, TObject>(input);
            foreach (var addobject in entityObjList)
            {
                 _context.Set<TObject>().Add(addobject);
            }
            await _context.SaveChangesAsync();
            var result = MappingConfiguration.MapObjectsList<TObject, TOutPut>(entityObjList);
            return result;

        }

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
        public TOutPut UpdateProperties<TOutPut, TInput>(TInput input, List<String> properties)
        {

            var entityObj = MappingConfiguration.MapObjects<TInput, TObject>(input);
            var entities = _context.Set<TObject>().Attach(entityObj);
            foreach (var property in properties)
            {
                _context.Entry(entities).Property(property).IsModified = true;
            }
            _context.SaveChanges();
            var result = MappingConfiguration.MapObjects<TObject, TOutPut>(entityObj);
            return result;
        }

        /// <summary>
        /// Generic asynchronous method to update specific properties of an object in the database.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">Input object that will be updated in the table.</param>
        /// <param name="properties">List of string which contains the property of string to update</param>
        /// <returns>Updated Object</returns>
        public async Task<TOutPut> UpdatePropertiesAsync<TOutPut, TInput>(TInput input, List<String> properties)
        {

            var entityObj = MappingConfiguration.MapObjects<TInput, TObject>(input);
            var entities = _context.Set<TObject>().Attach(entityObj);
            foreach (var property in properties)
            {
                _context.Entry(entities).Property(property).IsModified = true;
            }
            await _context.SaveChangesAsync();
            var result = MappingConfiguration.MapObjects<TObject, TOutPut>(entityObj);
            return result;
        }

        /// <summary>
        /// Generic method to update an object in the database.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">Input object that will be updated in the table.</param>
        /// <returns>Updated Object</returns>
        public TOutPut Update<TOutPut, TInput>(TInput input)
        {

            var entityObj = MappingConfiguration.MapObjects<TInput, TObject>(input);
            var entities = _context.Set<TObject>().Attach(entityObj);
            _context.Entry(entities).State = EntityState.Modified;
            _context.SaveChanges();
            var result = MappingConfiguration.MapObjects<TObject, TOutPut>(entityObj);
            return result;
        }

        /// <summary>
        /// Generic asynchronous method to update an object in the database.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <typeparam name="TOuput">Object type of the output data.</typeparam>
        /// <param name="input">Input object that will be updated in the table.</param>
        /// <returns>Updated Object</returns>
        public async Task<TOutPut> UpdateAsync<TOutPut, TInput>(TInput input)
        {

            var entityObj = MappingConfiguration.MapObjects<TInput, TObject>(input);
            var entities = _context.Set<TObject>().Attach(entityObj);
            _context.Entry(entities).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            var result = MappingConfiguration.MapObjects<TObject, TOutPut>(entityObj);
            return result;
        }
        
        #endregion

        #region DELETE

        /// <summary>
        /// Generic method for deleting an object in the database.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <param name="input">Input object that will be deleted in the table.</param>
        /// <returns>true or false</returns>
        public Boolean Delete<TInput>(TInput input)
        {
            var entityObj = MappingConfiguration.MapObjects<TInput, TObject>(input);
            var entities = _context.Set<TObject>().Attach(entityObj);
            _context.Entry(entities).State = EntityState.Deleted;
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Generic asychronous method for deleting an object in the database.
        /// </summary>
        /// <typeparam name="TInput">Object type of the input data.</typeparam>
        /// <param name="input">Input object that will be deleted in the table.</param>
        /// <returns>true or false</returns>
        public async Task<Boolean> DeleteAsync<TInput>(TInput input)
        {
            var entityObj = MappingConfiguration.MapObjects<TInput, TObject>(input);
            var entities = _context.Set<TObject>().Attach(entityObj);
            _context.Entry(entities).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Generic method for deleting an object in the database based on its ID.
        /// </summary>
        /// <param name="id">Integer parameted for the ID</param>
        /// <returns>true or false</returns>
        public Boolean DeleteID(int id)
        {
            var entity = _context.Set<TObject>().Find(id);
            _context.Entry(entity).State = EntityState.Deleted;
            _context.SaveChanges();

            return true;
        }

        /// <summary>
        /// Generic asynchronous method for deleting an object in the database based on its ID.
        /// </summary>
        /// <param name="id">Integer parameted for the ID</param>
        /// <returns>true or false</returns>
        public async Task<Boolean> DeleteIDAsync(int id)
        {
            var entity = _context.Set<TObject>().Find(id);
            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return true;
        }

        #endregion

        #region COUNT

        /// <summary>
        /// Generic method to count the records in the table.
        /// </summary>
        /// <returns>Count of the rows</returns>
        public int Count()
        {
            return _context.Set<TObject>().Count();
        }

        /// <summary>
        /// Generic asynchronous method to count the records in the table.
        /// </summary>
        /// <returns>Count of the rows</returns>
        public async Task<int> CountAsync()
        {
            return await _context.Set<TObject>().CountAsync();
        }
        
        #endregion

    }
}
