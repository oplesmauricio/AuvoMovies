using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuvoMovies.Services
{
    public class RespostaServico<T>
    {
        public string HttpStatus { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public T Resposta { get; set; }
    }
}
