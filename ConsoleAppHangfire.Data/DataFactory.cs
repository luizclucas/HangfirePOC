using MySql.Data.MySqlClient;
using System.Data.Common;
using System.Threading.Tasks;

namespace ConsoleAppHangfire.Data
{
    public class DataFactory
    {
        private readonly string _mysqlCn = "Server=localhost; port=3306; Uid=root; Pwd=Carol146*; Database=database1; Connect Timeout=120;";

        private DbConnection _mySqlConnection;
        public DataFactory()
        {
            _mySqlConnection = CreateConnectionMySqlConnection();
            _mySqlConnection.Open();
        }

        public DbConnection CreateConnectionMySqlConnection() => new MySqlConnection(_mysqlCn);

        public DbConnection OpenConnection()
        {
            var c = CreateConnectionMySqlConnection();
            c.Open();
            return c;
        }

        public async Task<DbConnection> OpenMysqlConnectionAsync()
        {
            var c = CreateConnectionMySqlConnection();
            await c.OpenAsync();
            return c;
        }

        public async Task<DbConnection> OpenMySqlConnectionIfNotAliveAsync()
        {
            if (_mySqlConnection == null || _mySqlConnection?.State == System.Data.ConnectionState.Closed || _mySqlConnection.State == System.Data.ConnectionState.Broken)
            {
                await _mySqlConnection.OpenAsync();
            }
            return _mySqlConnection;
        }
    }
}
