using FortCode.Model;
using FortCode.Model.Request;
using FortCode.Repository.Interfaces;
using FortCode.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FortCode.Service
{
    public class FortService: IFortService
    {
        private readonly IFortRepository _fortRepository;

        public FortService(IFortRepository fortRepository)
        {
            _fortRepository = fortRepository;
        }

        public async Task<int> AddUserAsync(AddUserRequest addUserRequest)
        {
            return await _fortRepository.AddUserAsync(addUserRequest);
        }

        public async Task<User> AuthenticateUserAsync(User authenticateUser)
        {
            var user = await _fortRepository.AuthenticateUserAsync(authenticateUser);

            if (user == null)
                return null;
            else
                return user;
        }

        public async Task<List<Country>> GetAllCountryByUserAsync(int id)
        {
            return await _fortRepository.GetAllCountryByUserAsync(id);
        }

        public async Task<int> AddCountryAsync(AddCountryRequest addCountryRequest, int userId)
        {
            return await _fortRepository.AddCountryAsync(addCountryRequest,userId);
        }
    }
}
