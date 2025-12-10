using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace M17A_ProjetoFinal_Loja
{
    public class Compra
    {
        // Propriedades
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int EquipamentoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string NumeroFatura { get; set; }
        public DateTime DataCompra { get; set; }

        private BaseDados bd;

        // Construtor
        public Compra(BaseDados bd)
        {
            this.bd = bd;
        }

        // Método para validar os dados
        public List<string> Validar()
        {
            List<string> erros = new List<string>();

            if (ClienteId <= 0)
                erros.Add("Cliente é obrigatório");

            if (EquipamentoId <= 0)
                erros.Add("Equipamento é obrigatório");

            if (Quantidade <= 0)
                erros.Add("Quantidade deve ser maior que zero");

            if (PrecoUnitario <= 0)
                erros.Add("Preço unitário deve ser maior que zero");

            return erros;
        }

        // Método para adicionar compra
        public void Adicionar()
        {
            string sql = @"INSERT INTO Compras 
                          (ClienteId, EquipamentoId, Quantidade, PrecoUnitario, NumeroFatura, DataCompra) 
                          VALUES (@ClienteId, @EquipamentoId, @Quantidade, @PrecoUnitario, @NumeroFatura, @DataCompra)";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@ClienteId", ClienteId),
                new SqlParameter("@EquipamentoId", EquipamentoId),
                new SqlParameter("@Quantidade", Quantidade),
                new SqlParameter("@PrecoUnitario", PrecoUnitario),
                new SqlParameter("@NumeroFatura", NumeroFatura),
                new SqlParameter("@DataCompra", DataCompra)
            };

            bd.ExecutarSQL(sql, parametros);
        }

        // Método para atualizar compra
        public void Atualizar()
        {
            string sql = @"UPDATE Compras SET 
                          ClienteId = @ClienteId, 
                          EquipamentoId = @EquipamentoId, 
                          Quantidade = @Quantidade, 
                          PrecoUnitario = @PrecoUnitario, 
                          NumeroFatura = @NumeroFatura, 
                          DataCompra = @DataCompra
                          WHERE Id = @Id";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Id", Id),
                new SqlParameter("@ClienteId", ClienteId),
                new SqlParameter("@EquipamentoId", EquipamentoId),
                new SqlParameter("@Quantidade", Quantidade),
                new SqlParameter("@PrecoUnitario", PrecoUnitario),
                new SqlParameter("@NumeroFatura", NumeroFatura),
                new SqlParameter("@DataCompra", DataCompra)
            };

            bd.ExecutarSQL(sql, parametros);
        }

        // Método para listar todas as compras com JOIN
        public DataTable Listar()
        {
            string sql = @"
                SELECT c.Id, cl.Nome as Cliente, e.Nome as Equipamento, 
                       c.Quantidade, c.PrecoUnitario, c.NumeroFatura, c.DataCompra
                FROM Compras c
                INNER JOIN Clientes cl ON c.ClienteId = cl.Id
                INNER JOIN Equipamentos e ON c.EquipamentoId = e.Id
                ORDER BY c.DataCompra DESC";

            return bd.DevolveSQL(sql);
        }

        // Método para procurar compra por ID
        public void Procurar()
        {
            string sql = "SELECT * FROM Compras WHERE Id = @Id";
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Id", Id)
            };

            DataTable dt = bd.DevolveSQL(sql, parametros);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                ClienteId = Convert.ToInt32(row["ClienteId"]);
                EquipamentoId = Convert.ToInt32(row["EquipamentoId"]);
                Quantidade = Convert.ToInt32(row["Quantidade"]);
                PrecoUnitario = Convert.ToDecimal(row["PrecoUnitario"]);
                NumeroFatura = row["NumeroFatura"].ToString();
                DataCompra = Convert.ToDateTime(row["DataCompra"]);
            }
        }

        // Método para procurar compras por cliente
        public DataTable ProcurarPorCliente(string nomeCliente)
        {
            string sql = @"
                SELECT c.Id, cl.Nome as Cliente, e.Nome as Equipamento, 
                       c.Quantidade, c.PrecoUnitario, c.NumeroFatura, c.DataCompra
                FROM Compras c
                INNER JOIN Clientes cl ON c.ClienteId = cl.Id
                INNER JOIN Equipamentos e ON c.EquipamentoId = e.Id
                WHERE cl.Nome LIKE @NomeCliente
                ORDER BY c.DataCompra DESC";

            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@NomeCliente", $"%{nomeCliente}%")
            };

            return bd.DevolveSQL(sql, parametros);
        }

        // Método para apagar compra
        public void Apagar()
        {
            string sql = "DELETE FROM Compras WHERE Id = @Id";
            var parametros = new List<SqlParameter>
            {
                new SqlParameter("@Id", Id)
            };

            bd.ExecutarSQL(sql, parametros);
        }

        // Método para calcular total da compra
        public decimal CalcularTotal()
        {
            return Quantidade * PrecoUnitario;
        }
    }
}