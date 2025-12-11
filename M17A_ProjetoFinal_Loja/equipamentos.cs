using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;

namespace M17A_ProjetoFinal_Loja
{
    public class Equipamentos : Item
    {
        // Propriedades (como campos da tabela)
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CodigoProduto { get; set; }
        public string Categoria { get; set; }
        public string Marca { get; set; }
        public string Compatibilidade { get; set; }
        public string Garantia { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public DateTime DataEntrada { get; set; }
        public string Imagem { get; set; }

        private BaseDados bd;

        // Construtor
        public Equipamentos(BaseDados bd)
        {
            this.bd = bd;
        }

        // Método para validar os dados
        public List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrWhiteSpace(Nome))
                erros.Add("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(CodigoProduto))
                erros.Add("Código do produto é obrigatório");

            if (string.IsNullOrWhiteSpace(Categoria))
                erros.Add("Categoria é obrigatória");

            if (Preco <= 0)
                erros.Add("Preço deve ser maior que zero");

            return erros;
        }

        // Método para adicionar equipamento
        public void Adicionar()
        {
            string sql = @"INSERT INTO Equipamentos 
                          (Nome, CodigoProduto, Categoria, Marca, Compatibilidade, Garantia, Preco, Descricao, DataEntrada) 
                          VALUES (@Nome, @Codigo, @Categoria, @Marca, @Compatibilidade, @Garantia, @Preco, @Descricao, @DataEntrada)";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Nome", Nome),
                new SqlParameter("@Codigo", CodigoProduto),
                new SqlParameter("@Categoria", Categoria),
                new SqlParameter("@Marca", Marca),
                new SqlParameter("@Compatibilidade", Compatibilidade),
                new SqlParameter("@Garantia", Garantia),
                new SqlParameter("@Preco", Preco),
                new SqlParameter("@Descricao", Descricao),
                new SqlParameter("@DataEntrada", DataEntrada)
            };

            bd.ExecutarSQL(sql, parametros);
        }

        // Método para atualizar equipamento
        public void Atualizar()
        {
            string sql = @"UPDATE Equipamentos SET 
                          Nome = @Nome, 
                          CodigoProduto = @Codigo, 
                          Categoria = @Categoria, 
                          Marca = @Marca, 
                          Compatibilidade = @Compatibilidade, 
                          Garantia = @Garantia, 
                          Preco = @Preco, 
                          Descricao = @Descricao, 
                          DataEntrada = @DataEntrada
                          WHERE Id = @Id";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@Nome", Nome),
                new SqlParameter("@Codigo", CodigoProduto),
                new SqlParameter("@Categoria", Categoria),
                new SqlParameter("@Marca", Marca),
                new SqlParameter("@Compatibilidade", Compatibilidade),
                new SqlParameter("@Garantia", Garantia),
                new SqlParameter("@Preco", Preco),
                new SqlParameter("@Descricao", Descricao),
                new SqlParameter("@DataEntrada", DataEntrada)
            };

            bd.ExecutarSQL(sql, parametros);
        }

        // Método para listar todos os equipamentos
        public DataTable Listar()
        {
            string sql = "SELECT Id, Nome, CodigoProduto, Categoria, Marca, Preco, DataEntrada FROM Equipamentos ORDER BY Nome";
            return bd.DevolveSQL(sql);
        }

        // Método para procurar equipamento por ID
        public void Procurar()
        {
            string sql = "SELECT * FROM Equipamentos WHERE Id = @Id";
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Id", Id)
            };

            DataTable dt = bd.DevolveSQL(sql, parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Nome = row["Nome"].ToString();
                CodigoProduto = row["CodigoProduto"].ToString();
                Categoria = row["Categoria"].ToString();
                Marca = row["Marca"].ToString();
                Compatibilidade = row["Compatibilidade"].ToString();
                Garantia = row["Garantia"].ToString();
                Preco = Convert.ToDecimal(row["Preco"]);
                Descricao = row["Descricao"].ToString();
                DataEntrada = Convert.ToDateTime(row["DataEntrada"]);
            }
        }

        // Método para procurar por campo e valor (para pesquisa)
        public DataTable Procurar(string campo, string valor)
        {
            string sql = $"SELECT Id, Nome, CodigoProduto, Categoria, Marca, Preco FROM Equipamentos WHERE {campo} LIKE @Valor ORDER BY Nome";
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Valor", $"%{valor}%")
            };

            return bd.DevolveSQL(sql, parametros);
        }

        // Método para apagar equipamento
        public void Apagar()
        {
            string sql = "DELETE FROM Equipamentos WHERE Id = @Id";
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Id", Id)
            };

            bd.ExecutarSQL(sql, parametros);
        }


    }
}
