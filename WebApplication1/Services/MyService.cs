using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public class MyService : IMyService
    {
        public string Saudacao(string nome)
        {
            return $"Bem vindo, {nome} \n\n {DateTime.UtcNow}";
        }
    }
}