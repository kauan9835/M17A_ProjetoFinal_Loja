using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M17A_ProjetoFinal_Loja
{
    /// <summary>
    /// Funções static para ler imagens, criar ficheiros de imagens 
    /// e saber a pasta do programa
    /// </summary>
    internal class Utils
    {
        /// <summary>
        /// Verifica se a pasta existe e se necessário cria a pasta
        /// </summary>
        /// <param name="nome">Nome da pasta</param>
        /// <returns>Devolve o caminho completo</returns>
        public static string PastaDoPrograma(string nome)
        {
            //Caminho completo para a pasta appdata\local
            string pastainicial = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            pastainicial += @"\" + nome;
            if (System.IO.Directory.Exists(pastainicial) == false)
                System.IO.Directory.CreateDirectory(pastainicial);
            return pastainicial;
        }
        //TODO: funções para imagens
    }
}
