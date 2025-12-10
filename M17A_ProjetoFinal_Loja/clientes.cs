using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace M17A_ProjetoFinal_Loja
{
    public class Cliente
    {
        // Propriedades
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Morada { get; set; }
        public string CodigoPostal { get; set; }
        public string NIF { get; set; }
        public string Telemovel { get; set; }
        public string Email { get; set; }
        public DateTime DataRegistro { get; set; }

        private BaseDados bd;

        // Construtor
        public Cliente(BaseDados bd)
        {
            this.bd = bd;
        }

        // Método para validar os dados
        public List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (string.IsNullOrWhiteSpace(Nome))
                erros.Add("Nome é obrigatório");

            if (string.IsNullOrWhiteSpace(NIF))
                erros.Add("NIF é obrigatório");

            if (NIF.Length != 9)
                erros.Add("NIF deve ter 9 dígitos");

            if (DataNascimento > DateTime.Now)
                erros.Add("Data de nascimento não pode ser futura");

            return erros;
        }

        // Método para adicionar cliente
        public void Adicionar()
        {
            string sql = @"INSERT INTO Clientes 
                          (Nome, DataNascimento, Morada, CodigoPostal, NIF, Telemovel, Email) 
                          VALUES (@Nome, @DataNascimento, @Morada, @CodigoPostal, @NIF, @Telemovel, @Email)";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Nome", Nome),
                new SqlParameter("@DataNascimento", DataNascimento),
                new SqlParameter("@Morada", Morada),
                new SqlParameter("@CodigoPostal", CodigoPostal),
                new SqlParameter("@NIF", NIF),
                new SqlParameter("@Telemovel", Telemovel),
                new SqlParameter("@Email", Email)
            };

            bd.ExecutarSQL(sql, parametros);
        }

        // Método para atualizar cliente
        public void Atualizar()
        {
            string sql = @"UPDATE Clientes SET 
                          Nome = @Nome, 
                          DataNascimento = @DataNascimento, 
                          Morada = @Morada, 
                          CodigoPostal = @CodigoPostal, 
                          NIF = @NIF, 
                          Telemovel = @Telemovel, 
                          Email = @Email
                          WHERE Id = @Id";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@Nome", Nome),
                new SqlParameter("@DataNascimento", DataNascimento),
                new SqlParameter("@Morada", Morada),
                new SqlParameter("@CodigoPostal", CodigoPostal),
                new SqlParameter("@NIF", NIF),
                new SqlParameter("@Telemovel", Telemovel),
                new SqlParameter("@Email", Email)
            };

            bd.ExecutarSQL(sql, parametros);
        }

        // Método para listar todos os clientes
        public DataTable Listar()
        {
            string sql = "SELECT Id, Nome, NIF, Telemovel, Email FROM Clientes ORDER BY Nome";
            return bd.DevolveSQL(sql);
        }

        // Método para procurar cliente por ID
        public void Procurar()
        {
            string sql = "SELECT * FROM Clientes WHERE Id = @Id";
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Id", Id)
            };

            DataTable dt = bd.DevolveSQL(sql, parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Nome = row["Nome"].ToString();
                DataNascimento = Convert.ToDateTime(row["DataNascimento"]);
                Morada = row["Morada"].ToString();
                CodigoPostal = row["CodigoPostal"].ToString();
                NIF = row["NIF"].ToString();
                Telemovel = row["Telemovel"].ToString();
                Email = row["Email"].ToString();
            }
        }

        // Método para procurar por campo e valor
        public DataTable Procurar(string campo, string valor)
        {
            string sql = $"SELECT Id, Nome, NIF, Telemovel, Email FROM Clientes WHERE {campo} LIKE @Valor ORDER BY Nome";
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Valor", $"%{valor}%")
            };

            return bd.DevolveSQL(sql, parametros);
        }

        // Método para apagar cliente
        public void Apagar()
        {
            string sql = "DELETE FROM Clientes WHERE Id = @Id";
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Id", Id)
            };

            bd.ExecutarSQL(sql, parametros);
        }
    }
}