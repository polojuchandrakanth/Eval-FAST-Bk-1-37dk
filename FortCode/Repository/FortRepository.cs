using System.Threading.Tasks;
using FortCode.Repository.Interfaces;
using FortCode.Service.Interfaces;
using FortCode.Model.Request;
using Dapper;
using System.Transactions;
using FortCode.Common;
using FortCode.Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FortCode.Repository.FortRepository
{
    public class FortRepository : IFortRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;

        public FortRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> AddUserAsync(AddUserRequest addUserRequest)
        {
            try
            {
                using (var databaseConnection = _dbConnectionFactory.GetConnection())
                {
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var rowsAffected = await databaseConnection.ExecuteAsync(SqlQueries.InsertUserQuery, new
                        {
                            addUserRequest.Username,
                            addUserRequest.Password,
                            addUserRequest.Email
                        });
                        transaction.Complete();
                        return rowsAffected;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> AuthenticateUserAsync(User authenticateUser)
        {
            try
            {
                using (var databaseConnection = _dbConnectionFactory.GetConnection())
                {
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var userData = await databaseConnection.QueryFirstOrDefaultAsync<User>(SqlQueries.AuthenticateUserQuery, new
                        {
                            authenticateUser.Email,
                            authenticateUser.Password
                        });
                        transaction.Complete();
                        return userData;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> AddCountryAsync(List<AddCountryRequest> addCountryRequest, int userId)
        {
            try
            {
                using (var databaseConnection = _dbConnectionFactory.GetConnection())
                {
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var dynamicpar = new List<DynamicParameters>();
                        foreach (var item in addCountryRequest)
                        {
                            var param = new DynamicParameters();
                            param.Add("@userId", userId);
                            param.Add("@CountryName", item.CountryName);
                            param.Add("@City", item.City);
                            dynamicpar.Add(param);
                        }
                        var rowsAffected = await databaseConnection.ExecuteAsync(SqlQueries.InsertCountryQuery, dynamicpar);
                        transaction.Complete();
                        return rowsAffected;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Country>> GetAllCountryByUserAsync(int id)
        {
            try
            {
                using (var databaseConnection = _dbConnectionFactory.GetConnection())
                {
                    var userCountryData = await databaseConnection.QueryAsync<Country>(SqlQueries.GetAllCountryByUserQuery, new { id });
                    return userCountryData.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> DeleteCountryAsync(int CountryID)
        {
            try
            {
                using (var databaseConnection = _dbConnectionFactory.GetConnection())
                {
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var rowsAffected = await databaseConnection.ExecuteAsync(SqlQueries.DeleteCountry, new
                        {
                            CountryID
                        });
                        transaction.Complete();
                        return rowsAffected;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}

