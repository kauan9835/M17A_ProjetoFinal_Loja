using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M17A_ProjetoFinal_Loja
{
    internal interface Item
    {
        //Adicionar
        void Adicionar();   //TODO: classe base dados
        //Atualizar 
        void Atualizar();
        //Apagar
        void Apagar();
        //Validar
        List<string> Validar();
    }
}
