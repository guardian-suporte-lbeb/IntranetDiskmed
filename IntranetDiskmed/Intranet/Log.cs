using IntranetDiskmed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IntranetDiskmed.Intranet
{
    public class Log
    {
        private Usuario usuario;
        private string descricao;
        private string parametros;

        public Log() { }

        public Log(Usuario usuario, string descricao, string parametros)
        {
            this.Usuario = usuario;
            this.Descricao = descricao;
            this.Parametros = parametros;
        }

        public Usuario Usuario
        {
            get { return this.usuario; }
            set { this.usuario = value; }
        }

        public string Descricao
        {
            get { return this.descricao; }
            set { this.descricao = value.ToUpper().Trim(); }
        }

        public string Parametros
        {
            get { return this.parametros; }
            set { this.parametros = value.ToUpper().Trim(); }
        }

        public void GerarLog()
        {
            Conexao cx = new Conexao();
            cx.Conectar();

            string query = " INSERT INTO INTRANET_LOG ";
            query += " (DATA, HORA, IP, LOGIN, DESCRICAO, PARAMETROS) ";
            query += " VALUES ";
            query += " (@DATA, @HORA, @IP, @LOGIN, @DESCRICAO, @PARAMETROS) ";

            cx.CriarComando(query);
            cx.AdicionarParametro("@DATA", DateTime.Now.ToString("yyyyMMdd"));
            cx.AdicionarParametro("@HORA", DateTime.Now.ToShortTimeString());
            cx.AdicionarParametro("@IP", this.usuario.Ip);
            cx.AdicionarParametro("@LOGIN", this.usuario.Login);
            cx.AdicionarParametro("@DESCRICAO", this.descricao);
            cx.AdicionarParametro("@PARAMETROS", this.parametros);
            cx.ExecutarGravacao();

            cx.Desconectar();
        }

        public void GerarLogEdicao(string campo, byte[] conteudo, string registro)
        {
            Conexao cx = new Conexao();
            cx.Conectar();

            string query = " INSERT INTO INTRANET_LOG ";
            query += " (DATA, HORA, IP, LOGIN, DESCRICAO, PARAMETROS, CAMPO, REGISTRO, CONTEUDO) ";
            query += " VALUES ";
            query += " (@DATA, @HORA, @IP, @LOGIN, @DESCRICAO, @PARAMETROS, @CAMPO, @REGISTRO, @CONTEUDO) ";

            cx.CriarComando(query);
            cx.AdicionarParametro("@DATA", DateTime.Now.ToString("yyyyMMdd"));
            cx.AdicionarParametro("@HORA", DateTime.Now.ToShortTimeString());
            cx.AdicionarParametro("@IP", this.usuario.Ip);
            cx.AdicionarParametro("@LOGIN", this.usuario.Login);
            cx.AdicionarParametro("@DESCRICAO", this.descricao);
            cx.AdicionarParametro("@PARAMETROS", this.parametros);
            cx.AdicionarParametro("@CAMPO", campo);
            cx.AdicionarParametro("@REGISTRO", registro);
            cx.AdicionarParametro("@CONTEUDO", conteudo);
            cx.ExecutarGravacao();
            cx.Desconectar();
        }

        public void GerarLogErro(String descricaoErro)
        {
            Conexao cx = new Conexao();
            cx.Conectar();

            string query = " INSERT INTO INTRANET_LOG ";
            query += " (DATA, HORA, IP, LOGIN, DESCRICAO, PARAMETROS) ";
            query += " VALUES ";
            query += " (@DATA, @HORA, @IP, @LOGIN, @DESCRICAO, @PARAMETROS) ";

            cx.CriarComando(query);
            cx.AdicionarParametro("@DATA", DateTime.Now.ToString("yyyyMMdd"));
            cx.AdicionarParametro("@HORA", DateTime.Now.ToShortTimeString());
            cx.AdicionarParametro("@IP", this.usuario.Ip);
            cx.AdicionarParametro("@LOGIN", this.usuario.Login);
            cx.AdicionarParametro("@DESCRICAO", descricaoErro);
            cx.AdicionarParametro("@PARAMETROS", this.parametros);
            cx.ExecutarGravacao();

            cx.Desconectar();
        }
    }
}