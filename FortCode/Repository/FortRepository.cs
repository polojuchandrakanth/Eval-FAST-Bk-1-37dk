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

        public async Task<int> AddCountryAsync(AddCountryRequest addCountryRequest, int userId)
        {
            try
            {
                using (var databaseConnection = _dbConnectionFactory.GetConnection())
                {
                    using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                    {
                        var rowsAffected = await databaseConnection.ExecuteAsync(SqlQueries.InsertCountryQuery, new
                        {
                            userId,
                            addCountryRequest.CountryName,
                            addCountryRequest.City
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
    }

}

