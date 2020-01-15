using ConsoleAppHangfire.Data;
using ConsoleAppHangfire.Domain.Entity;
using ConsoleAppHangfire.Domain.Repositories;
using Serilog;
using System;
using System.Threading.Tasks;

namespace ConsoleAppHangfire.Jobs
{
    public class SaveClientJob
    {
        private readonly IClientRepository _clientRepository;
        //8 Cities
        private static string[] cities = new []{ "BH", "Contagem", "Betim", "Nova Lima", "Alterosa", "Machado", "Alfenas", "Pouso-ALegre" };
        //6 Names
        private static string[] names = new[] { "Luiz", "Carol", "Gabriel", "João", "Maria", "José" };

        private readonly ILogger _log = Log.ForContext<SaveClientJob>();

        public SaveClientJob()
        {
            _clientRepository = Program.GetService<IClientRepository>();
        }

        public async Task Run()
        {
            _log.Information("Executando o job de salvar cliente");
            Random rnd = new Random();
            int city = rnd.Next(0, 7);
            int name = rnd.Next(0, 5);
            int cpf = rnd.Next(111, 999);

            var client = new Client()
            {
                Id = Guid.NewGuid(),
                City = cities[city],
                Name = names[name],
                CPF = "123456" + cpf
            };

            await _clientRepository.SaveAsync(client);
            _log
                .Information("Cliente salvo com sucesso");

        }
    }
}
