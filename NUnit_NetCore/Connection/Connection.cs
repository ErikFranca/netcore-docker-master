using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using NUnit.Framework;

/// //////////////////////////////////////////////////////////////////////////////////////////
/// ///       ///         //////    ///////       ///  ////////  ///    ///////  ///        //
/// //  ////////////  /////////  //  //////  //// ///  ////////  ///  //  /////  ///  ////////
/// //       ///////  ////////  ////  /////  //  ////  ////////  ///  ////  ///  ///        //
/// ///////  ///////  ///////          ////     /////  ////////  ///  //////  /  ///  ////////
/// //      ////////  //////  ////////  ///  //  ////       ///  ///  ////////   ///        //
/// //////////////////////////////////////////////////////////////////////////////////////////

public class Connection
{
    string conexao = TestContext.Parameters.Get("tnsbanco");
    private OracleConnection conn;

    public StringBuilder sql = new StringBuilder();

    public List<OracleParameter> listaParametros = new List<OracleParameter>();

    public OracleDataReader dr;

    public OracleCommand cmd;


    public Connection()
    {
        this.conn = new OracleConnection(conexao);
    }

    public void Close()
    {
        if (this.conn.State != ConnectionState.Closed)
        {
            this.conn.Close();
        }
    }

    public void clear()
    {
        this.sql.Clear();
        this.listaParametros.Clear();
    }

    public void carregaDataReader()
    {
        try
        {
            this.open();
            this.cmd = this.conn.CreateCommand();
            this.cmd.CommandText = this.sql.ToString();
            for (int i = 0; i < this.listaParametros.Count; i++)
            {
                this.cmd.Parameters.Add(this.listaParametros[i]);
            }
            this.dr = this.cmd.ExecuteReader();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString(), ex);
        }

    }

    public OracleConnection open()
    {
        if (this.conn.State == ConnectionState.Closed)
        {
            try
            {
                this.conn.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }
        return this.conn;
    }

    public void executa()
    {
        try
        {
            this.open();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString(), ex);
        }

        this.cmd = this.conn.CreateCommand();
        this.cmd.CommandText = this.sql.ToString();
        for (int i = 0; i < this.listaParametros.Count; i++)
        {
            this.cmd.Parameters.Add(listaParametros[i]);
        }

        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.ToString(), ex);
        }
    }
}