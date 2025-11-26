using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace M17A_ProjetoFinal_Loja
{
    public class BaseDados
    {
        string strligacao;
        string NomeBD;
        string CaminhoBD;
        SqlConnection ligacaoSQL;

        //construtor
        public BaseDados(string NomeBD)
        {
            this.NomeBD = NomeBD;

            // String de ligação
            strligacao = @"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True";

            // Verificar a pasta do projeto
            CaminhoBD = AppDomain.CurrentDomain.BaseDirectory;
            CaminhoBD += NomeBD + ".mdf";

            // Verificar se a base dados existe
            if (File.Exists(CaminhoBD) == false)
            {
                // se não existir, cria a bd
                CriarBD();
            }

            // ligação a bd
            ligacaoSQL = new SqlConnection(strligacao.Replace("master", NomeBD));
            ligacaoSQL.Open();
        }

        //destrutor
        ~BaseDados()
        {
            // fechar a ligação a bd
            if (ligacaoSQL != null && ligacaoSQL.State == ConnectionState.Open)
            {
                ligacaoSQL.Close();
            }
        }

        void CriarBD()
        {
            // ligação ao servidor
            using (SqlConnection masterConn = new SqlConnection(strligacao))
            {
                masterConn.Open();

                // criar a bd
                string sql = $"CREATE DATABASE {NomeBD} ON PRIMARY (NAME={NomeBD}, FILENAME='{CaminhoBD}')";
                SqlCommand comando = new SqlCommand(sql, masterConn);
                comando.ExecuteNonQuery();
            }

            // Agora criar as tabelas na nova BD
            using (SqlConnection bdConn = new SqlConnection(strligacao.Replace("master", NomeBD)))
            {
                bdConn.Open();

                string sql = @"
                    CREATE TABLE Equipamentos(
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Nome NVARCHAR(255) NOT NULL,
                        CodigoProduto NVARCHAR(100) UNIQUE NOT NULL,
                        Categoria NVARCHAR(100) NOT NULL,
                        Marca NVARCHAR(100),
                        Compatibilidade NVARCHAR(255),
                        Garantia NVARCHAR(50),
                        Preco DECIMAL(10,2),
                        Descricao NVARCHAR(MAX),
                        DataEntrada DATETIME DEFAULT GETDATE()
                    );

                    CREATE TABLE Clientes(
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        Nome NVARCHAR(255) NOT NULL,
                        DataNascimento DATE,
                        Morada NVARCHAR(255),
                        CodigoPostal NVARCHAR(20),
                        NIF NVARCHAR(20) UNIQUE,
                        Telemovel NVARCHAR(20),
                        Email NVARCHAR(255),
                        DataRegistro DATETIME DEFAULT GETDATE()
                    );

                    CREATE TABLE Compras(
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        DataCompra DATETIME DEFAULT GETDATE(),
                        ClienteId INT REFERENCES Clientes(Id),
                        EquipamentoId INT REFERENCES Equipamentos(Id),
                        Quantidade INT NOT NULL,
                        PrecoUnitario DECIMAL(10,2) NOT NULL,
                        NumeroFatura NVARCHAR(100),
                        Estado BIT DEFAULT 1
                    )";

                SqlCommand comando = new SqlCommand(sql, bdConn);
                comando.ExecuteNonQuery();
            }
        }

        // função para executar comando sql (insert/delete/update/create/alter...)
        public void ExecutarSQL(string sql, List<SqlParameter> parametros = null)
        {
            using (SqlCommand comando = new SqlCommand(sql, ligacaoSQL))
            {
                if (parametros != null)
                    comando.Parameters.AddRange(parametros.ToArray());
                comando.ExecuteNonQuery();
            }
        }

        // Função para executar um select e devolver os registos da bd
        public DataTable DevolveSQL(string sql, List<SqlParameter> parametros = null)
        {
            DataTable dados = new DataTable();
            using (SqlCommand comando = new SqlCommand(sql, ligacaoSQL))
            {
                if (parametros != null)
                    comando.Parameters.AddRange(parametros.ToArray());

                using (SqlDataReader registos = comando.ExecuteReader())
                {
                    dados.Load(registos);
                }
            }
            return dados;
        }
    }
}