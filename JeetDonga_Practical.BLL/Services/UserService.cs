using JeetDonga_Practical.DAL.Entities;
using JeetDonga_Practical.DAL.Repositories;

namespace JeetDonga_Practical.BLL.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public List<UserModel> GetAll() => _repo.GetAll();

        public void Save(UserModel model) => _repo.Save(model);

        public void Delete(int id) => _repo.Delete(id);

        public List<StateModel> GetStates()
        {
            return _repo.GetStates();
        }




        public List<CityModel> GetCities(int stateId)
        {
            return _repo.GetCities(stateId);
        }
    }
}