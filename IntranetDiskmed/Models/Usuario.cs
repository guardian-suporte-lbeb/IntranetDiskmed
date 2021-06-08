using IntranetDiskmed.Intranet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace IntranetDiskmed.Models
{
    [Serializable]
    public class Usuario
    {
        private string ip;
        private string nome;
        private string login;
        private string email;
        private string senha;
        private string tipo;

        public Usuario() { }

        public Usuario(string ip, string login, string senha)
        {
            this.Ip = ip;
            this.Login = login;
            this.Senha = senha;
        }

        public string Ip
        {
            get { return this.ip; }
            set { this.ip = value.Trim(); }
        }

        public string Nome
        {
            get { return this.nome; }
            set { this.nome = value.ToUpper().Trim(); }
        }

        public string Login
        {
            get { return this.login; }
            set { this.login = value.ToUpper().Trim(); }
        }

        public string Email
        {
            get { return this.email; }
            set { this.email = value.ToUpper().Trim(); }
        }

        public string Senha
        {
            get { return this.senha; }
            set { this.senha = Criptografia.criptografar(value); }
        }

        public string Tipo
        {
            get { return this.tipo; }
            set { this.tipo = value.ToUpper().Trim(); }
        }

        public bool Bloq { get; set; } = false;

        public bool Empenho { get; set; } = false;

        public bool Custo { get; set; } = false;

        public bool Anvisa { get; set; } = false;

        public bool AutenticarUsuario()
        {
            Conexao cx = new Conexao();
            cx.Conectar();

            string query = " SELECT * FROM INTRANET_ACESSO ";
            query += " WHERE LOGIN = @LOGIN ";
            query += " AND SENHA = @SENHA ";

            cx.CriarComando(query);
            cx.AdicionarParametro("@LOGIN", this.login);
            cx.AdicionarParametro("@SENHA", this.senha);
            DataTable dt = cx.ExecutarLeitura();

            cx.Desconectar();

            if (dt.Rows.Count == 0)
            {
                return false;
            }
            else
            {
                this.Nome = dt.Rows[0]["NOME"].ToString();
                this.Email = dt.Rows[0]["EMAIL"].ToString();
                this.Tipo = dt.Rows[0]["TIPO"].ToString();

                if (dt.Rows[0]["BLOQ"].ToString().ToUpper().Trim() == "S")
                    this.Bloq = true;

                if (dt.Rows[0]["EMPENHO"].ToString().ToUpper().Trim() == "S")
                    this.Empenho = true;

                if (dt.Rows[0]["CUSTO"].ToString().ToUpper().Trim() == "S")
                    this.Custo = true;

                if (dt.Rows[0]["ANVISA"].ToString().ToUpper().Trim() == "S")
                    this.Anvisa = true;

                Log log = new Log(this, "ACESSO AO SISTEMA", "");
                log.GerarLog();

                return true;
            }
        }

        public void DeslogarUsuario()
        {
            Log log = new Log(this, "SAÍDA DO SISTEMA", "");
            log.GerarLog();
        }
    }
}