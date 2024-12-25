
using Dapper;
using project1.Models;
using System.Data;
using System.Threading.Tasks;

namespace project1.Repositories
{
    public class UserRepository
    {
        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            const string query = @"
            SELECT 
            id AS UserId, 
            kullaniciadi AS Username, 
            sifre AS PasswordHash
            FROM userlogin
            WHERE kullaniciadi = @Username";

            // Parametre eşleştirme
            return await _dbConnection.QueryFirstOrDefaultAsync<User>(
                query,
                new { Username = username }
            );
        }
    }
}


