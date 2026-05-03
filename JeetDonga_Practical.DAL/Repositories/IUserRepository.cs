using JeetDonga_Practical.DAL.Entities;

namespace JeetDonga_Practical.DAL.Repositories
{
    public interface IUserRepository
    {
        List<UserModel> GetAll();
        UserModel GetById(int id);
        void Save(UserModel model);
        void Delete(int id);

        List<StateModel> GetStates();
        List<CityModel> GetCities(int stateId);

    }
}