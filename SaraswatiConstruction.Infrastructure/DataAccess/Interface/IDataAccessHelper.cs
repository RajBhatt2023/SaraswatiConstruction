namespace SaraswatiConstruction.Infrastructure.DataAccess.Interface
{
    public interface IDataAccessHelper
    {
        public Task<IEnumerable<T>> ExecuteStoredProcedureDataTableAsync<T>(string procedureName);
        public Task<IEnumerable<T>> ExecuteStoredProcedureDataTableAsync<T>(string procedureName, object parameters);
        public Task<List<IEnumerable<T>>> ExecuteStoredProcedureDataSetAsync<T>(string procedureName);
        public Task<List<IEnumerable<T>>> ExecuteStoredProcedureDataSetAsync<T>(string procedureName, object parameters);
    }
}
