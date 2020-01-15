using ConsoleAppHangfire.Domain.Entity;
using ConsoleAppHangfire.Domain.Repositories;
using Dapper;
using System.Threading.Tasks;

namespace ConsoleAppHangfire.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private DataFactory _data;

        public ClientRepository(DataFactory data)
        {
            _data = data;
        }

        private static string SaveClient = @"
INSERT IGNORE INTO Client(Id,Name,CPF,City)
VALUES(@Id,@Name,@CPF,@City)
".Trim();
        public async Task<int> SaveAsync(Client client)
        {
            using (var cn = await _data.OpenMysqlConnectionAsync())
            {
                var success = await cn.ExecuteAsync(SaveClient, new { client.Id, client.Name, client.CPF, client.City });
                return success;
            }

        }
    }
}
