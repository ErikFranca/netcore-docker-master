using NUnit.Framework;
using System;

/// //////////////////////////////////////////////////////////////////////////////////////////
/// ///       ///         //////    ///////       ///  ////////  ///    ///////  ///        //
/// //  ////////////  /////////  //  //////  //// ///  ////////  ///  //  /////  ///  ////////
/// //       ///////  ////////  ////  /////  //  ////  ////////  ///  ////  ///  ///        //
/// ///////  ///////  ///////          ////     /////  ////////  ///  //////  /  ///  ////////
/// //      ////////  //////  ////////  ///  //  ////       ///  ///  ////////   ///        //
/// //////////////////////////////////////////////////////////////////////////////////////////

public class ClsCCM
{
    Connection con;

    public ClsCCM()
    {
        con = new Connection();
    }
    public void Consulta_Cliente_Cadastro(string classeProfissional)
    {
        switch (classeProfissional)
        {
            case "ASSALARIADOS":
                classeProfissional = "1";
                break;
            case "AUTONOMOS":
                classeProfissional = "2";
                break;
            case "ESTABELECIDOS":
                classeProfissional = "3";
                break;
            case "LIBERAIS":
                classeProfissional = "4";
                break;
            case "APOSENTADOS":
                classeProfissional = "5";
                break;
            case "OUTROS":
                classeProfissional = "6";
                break;
            case "PENSIONISTAS":
                classeProfissional = "7";
                break;
            case "COLABORADOR":
                classeProfissional = "8";
                break;
        }

        con.clear();
        con.sql.AppendLine("SELECT CL.CLI_CPF,");
        con.sql.AppendLine("DECODE(CL.FIL_COD_VENDA, '', 54, CL.FIL_COD_VENDA) FILIAL,");
        con.sql.AppendLine("TRUNC(CR.CRD_DT_ABERTURA) DT_CADASTRO,");
        con.sql.AppendLine("TO_CHAR(CL.CLI_DT_NASCTO, 'DD/MM/YYYY') CLI_DT_NASCTO,");
        con.sql.AppendLine("CL.CLI_NOME,");
        con.sql.AppendLine("CL.CLI_MAE,");
        con.sql.AppendLine("DECODE(CL.CLI_SEXO_DOMI, 1, 'FEMININO', 'MASCULINO') AS CLI_SEXO_DOMI,");
        con.sql.AppendLine("DECODE(CL.CLI_EC_DOMI, 1, 'SOLTEIRO', DECODE(CL.CLI_EC_DOMI, 2, 'CASADO', DECODE(CL.CLI_EC_DOMI, 3, 'DIVORCIADO', DECODE(CL.CLI_EC_DOMI, 4, 'VIUVO', DECODE(CL.CLI_EC_DOMI, 5, 'DESQUITADO/SEPARADO', 'OUTROS'))))) AS CLI_EC_DOMI,");
        con.sql.AppendLine("DECODE(CL.CLI_DDD_CELULAR || CL.CLI_NUM_CELULAR, ' ', CL.CLI_RES_DDD || CL.CLI_RES_FONE, CL.CLI_DDD_CELULAR || CL.CLI_NUM_CELULAR) CLI_NUM_CELULAR,");
        con.sql.AppendLine("CL.CLI_RES_CEP,");
        con.sql.AppendLine("CL.CLI_RES_BAI,");
        con.sql.AppendLine("CL.CLI_RES_RUA,");
        con.sql.AppendLine("CL.CLI_RES_NUM,");
        con.sql.AppendLine("DECODE(CL.CLI_RES_CASA_DOMI, 1, 'PROPRIA QUITADA', DECODE(CL.CLI_RES_CASA_DOMI, 2, 'PROPRIA FINANCIADA', DECODE(CL.CLI_RES_CASA_DOMI, 3, 'ALUGADA', DECODE(CL.CLI_RES_CASA_DOMI, 4, 'RESIDE COM PARENTES', DECODE(CL.CLI_RES_CASA_DOMI, 5, 'CASA DA EMPRESA', 'OUTROS'))))) AS CLI_RES_CASA_DOMI,");
        con.sql.AppendLine("CL.CLI_RES_DDD || CL.CLI_RES_FONE CLI_RES_FONE,");
        con.sql.AppendLine("DECODE(CL.CLI_EMP_CARGO, 1, 'ASSALARIADOS', DECODE(CL.CLI_EMP_CARGO, 2, 'AUTONOMOS', DECODE(CL.CLI_EMP_CARGO, 3, 'ESTABELECIDOS', DECODE(CL.CLI_EMP_CARGO, 4, 'LIBERAIS', DECODE(CL.CLI_EMP_CARGO, 5, 'APOSENTADOS',DECODE(CL.CLI_EMP_CARGO, 6, 'OUTROS', DECODE(CL.CLI_EMP_CARGO, 7, 'PENSIONISTAS', 'COLABORADOR'))))))) as CLI_EMP_CARGO,");
        con.sql.AppendLine("(SELECT OCUP_DESCR FROM CCM_OCUPACAO WHERE CARG_COD = CL.CLI_EMP_CARGO AND OCUP_COD = CL.CLI_EMP_OCUPACAO) CLI_EMP_OCUPACAO,");
        con.sql.AppendLine("CL.CLI_EMP_SALARIO,");
        con.sql.AppendLine("CL.CLI_EMP_CEP,");
        con.sql.AppendLine("DECODE(upper(CL.CLI_EMP_NUM), 'SN', '1', CL.CLI_EMP_NUM) CLI_EMP_NUM,");
        con.sql.AppendLine("DECODE(CL.CLI_EMP_DDD || CL.CLI_EMP_FONE, ' ', DECODE(CL.CLI_DDD_CELULAR || CL.CLI_NUM_CELULAR, ' ', CL.CLI_RES_DDD || CL.CLI_RES_FONE, CL.CLI_DDD_CELULAR || CL.CLI_NUM_CELULAR), CL.CLI_EMP_DDD || CL.CLI_EMP_FONE) CLI_EMP_FONE,");
        con.sql.AppendLine("CR.DIA_VCTO");
        if (TestContext.Parameters.Get("tnsbanco").Contains("CCMDEV01")) { 
            con.sql.AppendLine("FROM CCM_CREDITO@CCMSTB   CR, CCM_CLIENTE@CCMSTB   CL, CCM_CLI_SCORE@CCMSTB CS");
        }
        else
        {
            con.sql.AppendLine("FROM CCM_CREDITO CR, CCM_CLIENTE CL, CCM_CLI_SCORE CS");
        }
        con.sql.AppendLine("WHERE CS.DT_HR_CONSULTA > (CR.CRD_DT_ABERTURA - ((1 / 24 / 60) * 5))");
        con.sql.AppendLine("AND CL.CLI_CPF = CR.CLI_CPF");
        con.sql.AppendLine("AND CL.CLI_CPF = CS.CLI_CPF");
        con.sql.AppendLine("AND CL.CLI_EMP_CARGO = " + classeProfissional);
        con.sql.AppendLine("AND CR.CRD_DT_ABERTURA >= (SYSDATE - 22)");
        con.sql.AppendLine("AND ROWNUM <=1");
        con.sql.AppendLine("AND CL.CLI_FLG_MOBILE = 'S'");
        con.sql.AppendLine("ORDER BY CR.CRD_DT_ABERTURA DESC");
        try
        {
            con.carregaDataReader();
            if (con.dr.HasRows)
            {
                con.dr.Read();
                this.CLI_CPF = con.dr["CLI_CPF"].ToString();
                this.FILIAL = con.dr["FILIAL"].ToString();
                this.DT_CADASTRO = con.dr["DT_CADASTRO"].ToString();
                this.CLI_DT_NASCTO = con.dr["CLI_DT_NASCTO"].ToString();
                this.CLI_NOME = con.dr["CLI_NOME"].ToString();
                this.CLI_MAE = con.dr["CLI_MAE"].ToString();
                this.CLI_SEXO_DOMI = con.dr["CLI_SEXO_DOMI"].ToString();
                this.CLI_EC_DOMI = con.dr["CLI_EC_DOMI"].ToString();
                this.CLI_NUM_CELULAR = con.dr["CLI_NUM_CELULAR"].ToString();
                this.CLI_RES_CEP = con.dr["CLI_RES_CEP"].ToString();
                this.CLI_RES_BAI = con.dr["CLI_RES_BAI"].ToString();
                this.CLI_RES_RUA = con.dr["CLI_RES_RUA"].ToString();
                this.CLI_RES_NUM = con.dr["CLI_RES_NUM"].ToString();
                this.CLI_RES_CASA_DOMI = con.dr["CLI_RES_CASA_DOMI"].ToString();
                this.CLI_RES_FONE = con.dr["CLI_RES_FONE"].ToString();
                this.CLI_EMP_CARGO = con.dr["CLI_EMP_CARGO"].ToString();
                this.CLI_EMP_OCUPACAO = con.dr["CLI_EMP_OCUPACAO"].ToString();
                this.CLI_EMP_SALARIO = con.dr["CLI_EMP_SALARIO"].ToString();
                this.CLI_EMP_CEP = con.dr["CLI_EMP_CEP"].ToString();
                this.CLI_EMP_NUM = con.dr["CLI_EMP_NUM"].ToString();
                this.CLI_EMP_FONE = con.dr["CLI_EMP_FONE"].ToString();
                this.DIA_VCTO = con.dr["DIA_VCTO"].ToString();

            }
            else
            {
                this.CLI_CPF = null;
                this.FILIAL = null;
                this.DT_CADASTRO = null;
                this.CLI_DT_NASCTO = null;
                this.CLI_NOME = null;
                this.CLI_MAE = null;
                this.CLI_SEXO_DOMI = null;
                this.CLI_EC_DOMI = null;
                this.CLI_NUM_CELULAR = null;
                this.CLI_RES_CEP = null;
                this.CLI_RES_BAI = null;
                this.CLI_RES_RUA = null;
                this.CLI_RES_NUM = null;
                this.CLI_RES_CASA_DOMI = null;
                this.CLI_RES_FONE = null;
                this.CLI_EMP_CARGO = null;
                this.CLI_EMP_OCUPACAO = null;
                this.CLI_EMP_SALARIO = null;
                this.CLI_EMP_CEP = null;
                this.CLI_EMP_NUM = null;
                this.CLI_EMP_FONE = null;
                this.DIA_VCTO = null;
            }

        }


        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }


    public string CLI_CPF { get; set; }
    public string FILIAL { get; set; }
    public string DT_CADASTRO { get; set; }
    public string CLI_DT_NASCTO { get; set; }
    public string CLI_NOME { get; set; }
    public string CLI_MAE { get; set; }
    public string CLI_SEXO_DOMI { get; set; }
    public string CLI_EC_DOMI { get; set; }
    public string CLI_NUM_CELULAR { get; set; }
    public string CLI_RES_CEP { get; set; }
    public string CLI_RES_BAI { get; set; }
    public string CLI_RES_RUA { get; set; }
    public string CLI_RES_NUM { get; set; }
    public string CLI_RES_CASA_DOMI { get; set; }
    public string CLI_RES_FONE { get; set; }
    public string CLI_EMP_CARGO { get; set; }
    public string CLI_EMP_OCUPACAO { get; set; }
    public string CLI_EMP_SALARIO { get; set; }
    public string CLI_EMP_CEP { get; set; }
    public string CLI_EMP_NUM { get; set; }
    public string CLI_EMP_FONE { get; set; }
    public string DIA_VCTO { get; set; }



}
    

              


