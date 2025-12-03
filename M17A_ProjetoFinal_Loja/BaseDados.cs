using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

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
            //Ler a string ligação
            strligacao = ConfigurationManager.ConnectionStrings["sql"].ToString();
            //Verificar a pasta do projeto
            CaminhoBD = Utils.PastaDoPrograma("M17A_loja");
            CaminhoBD += @"\" + NomeBD + ".mdf";
            //Verificar se a bd existe
            if (System.IO.File.Exists(CaminhoBD) == false)
            {
                //se não existir
                //criar a bd
                CriarBD();
            }
            //ligação à bd
            ligacaoSQL = new SqlConnection(strligacao);
            ligacaoSQL.Open();
            ligacaoSQL.ChangeDatabase(this.NomeBD);
            }

        //destrutor
        ~BaseDados()
        {
            //fechar a ligação à bd
        }
        void CriarBD()
        {
            //ligação ao servidor
            ligacaoSQL = new SqlConnection(strligacao);
            ligacaoSQL.Open();
            //veirificar se a bd já existe no catalogo
            string sql = $@"
                        IF EXISTS(SELECT * FROM master.sys.databases
                                    WHERE name='{this.NomeBD}')
                          BEGIN
                                USE [master];
                                EXEC sp_detach_db {this.NomeBD};
                          END
                        ";

            SqlCommand comando = new SqlCommand(sql, ligacaoSQL);
            comando.ExecuteNonQuery();
            //criar a bd
            sql = $"CREATE DATABASE {this.NomeBD} ON PRIMARY (NAME={this.NomeBD},FILENAME='{this.CaminhoBD}')";
            comando = new SqlCommand(sql, ligacaoSQL);
            comando.ExecuteNonQuery();
            //Associação a ligação à base de dados criada
            ligacaoSQL.ChangeDatabase(this.NomeBD);
            //criar as tabelas
            //criar tabela livros
            sql = @"
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

            comando = new SqlCommand(sql, ligacaoSQL);
            comando.ExecuteNonQuery();
            comando.Dispose();
        }

        //função para executar comando sql (insert/delete/update/create/alter...)
        public void ExecutarSQL(string sql, List<SqlParameter> parametros = null)
        {
            SqlCommand comando = new SqlCommand(sql, ligacaoSQL);
            if (parametros != null)
                comando.Parameters.AddRange(parametros.ToArray());
            comando.ExecuteNonQuery();
            comando.Dispose();
        }
        //Função para executar um select e devolver os registos da bd
        public DataTable DevolveSQL(string sql, List<SqlParameter> parametros = null)
        {
            DataTable dados = new DataTable();
            SqlCommand comando = new SqlCommand(sql, ligacaoSQL);
            if (parametros != null)
                comando.Parameters.AddRange(parametros.ToArray());
            SqlDataReader registos = comando.ExecuteReader();
            dados.Load(registos);
            registos.Close();
            comando.Dispose();
            return dados;
        }
    }

}