using System.Data;
using System.Data.SqlClient;
using JeetDonga_Practical.DAL.Entities;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace JeetDonga_Practical.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connection;

        public UserRepository(IConfiguration config)
        {
            _connection = config.GetConnectionString("DefaultConnection");
        }

        public List<UserModel> GetAll()
        {
            List<UserModel> list = new();

            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_GetUsers", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                var dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new UserModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString(),
                        Email = dr["Email"].ToString(),
                        Phone = dr["Phone"].ToString(),
                        StateId = Convert.ToInt32(dr["StateId"]),
                        CityId = Convert.ToInt32(dr["CityId"])
                    });
                }
            }

            return list;
        }

        public void Save(UserModel model)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd;

                if (model.Id == 0)
                {
                    cmd = new SqlCommand("sp_InsertUser", con);
                }
                else
                {
                    cmd = new SqlCommand("sp_UpdateUser", con);
                    cmd.Parameters.AddWithValue("@Id", model.Id); // 🔥 UPDATE FIX
                }

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", model.Name);
                cmd.Parameters.AddWithValue("@Email", model.Email);
                cmd.Parameters.AddWithValue("@Phone", model.Phone);
                cmd.Parameters.AddWithValue("@Address", model.Address ?? "");
                cmd.Parameters.AddWithValue("@StateId", model.StateId);
                cmd.Parameters.AddWithValue("@CityId", model.CityId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public UserModel GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public List<StateModel> GetStates()
        {
            List<StateModel> list = new();

            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM State", con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new StateModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString()
                    });
                }
            }

            return list;
        }

        public List<CityModel> GetCities(int stateId)
        {
            List<CityModel> list = new();

            using (SqlConnection con = new SqlConnection(_connection))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM City WHERE StateId=@StateId", con);
                cmd.Parameters.AddWithValue("@StateId", stateId);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new CityModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = dr["Name"].ToString()
                    });
                }
            }

            return list;
        }
    }
}