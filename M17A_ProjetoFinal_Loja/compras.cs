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

        // Método para listar todas as compras
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

        // Método para obter equipamentos disponíveis (SEM Estado)
        public static DataTable ObterEquipamentosDisponiveis(BaseDados bd, string filtroNome = "",
                                                           string filtroCategoria = "",
                                                           string filtroCompatibilidade = "",
                                                           string filtroModelo = "")
        {
            // CONSULTA SIMPLES - NÃO USA Estado
            string sql = @"
                SELECT 
                    E.Id,
                    E.Nome,
                    E.CodigoProduto,
                    E.Categoria,
                    E.Marca,
                    E.Compatibilidade,
                    E.Garantia,
                    E.Preco,
                    E.Descricao,
                    E.DataEntrada,
                    E.Imagem
                FROM Equipamentos E
                WHERE NOT EXISTS (
                    SELECT 1 
                    FROM Compras C 
                    WHERE C.EquipamentoId = E.Id
                )";

            var parametros = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(filtroNome))
            {
                sql += " AND E.Nome LIKE @Nome";
                parametros.Add(new SqlParameter("@Nome", $"%{filtroNome}%"));
            }

            if (!string.IsNullOrWhiteSpace(filtroCategoria))
            {
                sql += " AND E.Categoria = @Categoria";
                parametros.Add(new SqlParameter("@Categoria", filtroCategoria));
            }

            if (!string.IsNullOrWhiteSpace(filtroCompatibilidade))
            {
                sql += " AND E.Compatibilidade = @Compatibilidade";
                parametros.Add(new SqlParameter("@Compatibilidade", filtroCompatibilidade));
            }

            if (!string.IsNullOrWhiteSpace(filtroModelo))
            {
                sql += " AND E.Marca LIKE @Modelo";
                parametros.Add(new SqlParameter("@Modelo", $"%{filtroModelo}%"));
            }

            sql += " ORDER BY E.Nome";

            return bd.DevolveSQL(sql, parametros);
        }

        // Métodos auxiliares (opcionais)
        public static DataTable ObterCategorias(BaseDados bd)
        {
            string sql = "SELECT DISTINCT Categoria FROM Equipamentos WHERE Categoria IS NOT NULL ORDER BY Categoria";
            return bd.DevolveSQL(sql);
        }

        public static DataTable ObterCompatibilidades(BaseDados bd)
        {
            string sql = "SELECT DISTINCT Compatibilidade FROM Equipamentos WHERE Compatibilidade IS NOT NULL ORDER BY Compatibilidade";
            return bd.DevolveSQL(sql);
        }

        public static DataTable ObterClientes(BaseDados bd)
        {
            string sql = "SELECT Id, Nome, NIF FROM Clientes ORDER BY Nome";
            return bd.DevolveSQL(sql);
        }
    }
}