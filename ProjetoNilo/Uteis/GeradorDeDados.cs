using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoNilo.Uteis
{
    public static class GeradorDeDados
    {
        public static string RetornaEmailValidoAleatorio()
        {
            string alfabeto = "abcdfghijlmnopqrstvxz";

            string emailAleatorio = "";

            for (int contador = 0; contador <= alfabeto.Length; contador++)
            {
                int numeroAleatorio = new Random().Next(0, alfabeto.Length);
                emailAleatorio = emailAleatorio + alfabeto[numeroAleatorio].ToString();
            }

            return $"{emailAleatorio}@{emailAleatorio}.com";
        }
    }
}
