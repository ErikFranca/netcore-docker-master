using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace FluxoVendaCartoes
{
    public class ProcessosCCM
    {
        CtrlRelatorio relatorio = new CtrlRelatorio();
        public CtrlActions ctrlActions = null;
        public ProcessosCCM()
        {
            ctrlActions = new CtrlActions();
        }

        public void acessarSite()
        {
            //relatorio.startStep("Acessar Site");
            Global.driver.Navigate().GoToUrl("https://www.kabum.com.br/");
            //relatorio.endStep("Acesso realizado com sucesso");
        }
        public void pesquisarProduto()
        {
            //relatorio.startStep("Pesquisar Produto");
            CtrlChildActionLogin ctrlChildActionLogin = new CtrlChildActionLogin();
            ctrlChildActionLogin.SendKeys(By.CssSelector("#form-busca > input.sprocura"), "iPhone");
            ctrlChildActionLogin.Click(By.Id("bt-busca"));
            ctrlChildActionLogin.IsElementPresent(By.XPath("//*[@id='BlocoConteudo']/div/div/div[2]/section"));
            //relatorio.endStep("Pesquisa realizada com sucesso");
        }

        public void validaLogin()
        {

            relatorio.startStep("Validar Login");

            CtrlChildActionLogin ctrlChildActionLogin = new CtrlChildActionLogin();

            ctrlChildActionLogin.wait(By.XPath("//button[@type='submit']"));
            ctrlChildActionLogin.wait(By.CssSelector("img.png_bg"));

            
            try
            {
                ctrlChildActionLogin.assertAreEqual("Login", By.CssSelector("strong"));
                ctrlChildActionLogin.assertIsTrue(By.Id("login_username"));
                ctrlChildActionLogin.assertIsTrue(By.Id("login_password"));
                ctrlChildActionLogin.assertIsTrue(By.XPath("//button[@type='submit']"));

                //Validar Tela de Login
                ctrlChildActionLogin.assertAreEqualTitle("PSF Security");
                ctrlChildActionLogin.assertIsTrue(By.CssSelector("img.png_bg"));
                ctrlChildActionLogin.assertAreEqual("Portal Lojas", By.Id("tituloPagina"));
            }
            catch (AssertionException e)
            {
                Global.verificationErrors.Append(e.Message);
            }


            relatorio.endStep("Tela validada com sucesso");

        }

        public void Login(string login, string senha)
        { //Validar Seção de Login

            relatorio.startStep("Efetuar Login");

            //Efetuar Login
            CtrlChildActionLogin ctrlChildActionLogin = new CtrlChildActionLogin();
            ctrlChildActionLogin.Clear(By.Id("login_username"));
            ctrlChildActionLogin.SendKeys(By.Id("login_username"), login);
            ctrlChildActionLogin.Clear(By.Id("login_password"));
            ctrlChildActionLogin.SendKeys(By.Id("login_password"),senha);
            Thread.Sleep(2000);

            ctrlChildActionLogin.Click(By.XPath("//button[@type='submit']"));
            ctrlChildActionLogin.waitText(By.Id("text-login-msg"), "Login efetuado com sucesso!");
            ctrlChildActionLogin.assertAreEqual("Login efetuado com sucesso!", By.Id("text-login-msg"));
            

            relatorio.endStep("Login realizado com sucesso");

        }

        public void validaPaginaInicial()
        {
            CtrlChildActionLogin ctrlChildActionLogin = new CtrlChildActionLogin();

            relatorio.startStep("Validar Página Inicial");


            ctrlChildActionLogin.assertAreEqual("Portal Lojas", By.Id("tituloPagina"));

            ctrlChildActionLogin.assertIsTrue(By.Id("cpfSearch"));

            ctrlChildActionLogin.assertIsTrue(By.Id("btnSearch"));
            try
            {

                ctrlChildActionLogin.assertAreEqual("Concessao do Cartao", By.Id("menuCollapse3162"));
            }
            catch (AssertionException e)
            {
                Global.verificationErrors.Append(e.Message);
            }
            try
            {
                ctrlChildActionLogin.assertAreEqual("Atendimento", By.Id("menuCollapse3164"));
            }
            catch (AssertionException e)
            {
                Global.verificationErrors.Append(e.Message);
            }

            relatorio.endStep("Validação concluida com sucesso");

        }

        public void cadastroCliente()
        {
            CtrlChildActionCadastroCliente CtrlChildActionCadastroCliente = new CtrlChildActionCadastroCliente();
            relatorio.startStep("Cadastrar CPF Cliente");

            //Consultar Cliente
            CtrlChildActionCadastroCliente.Clear(By.Id("cpfSearch"));
            CtrlChildActionCadastroCliente.SendKeys(By.Id("cpfSearch"), Global.consulta.CLI_CPF);

            CtrlChildActionCadastroCliente.Click(By.Id("btnSearch"));

            CtrlChildActionCadastroCliente.wait(By.Id("iniciarCadastro"));

            relatorio.endStep("CPF cadastrado com sucesso");
        }

        public void validaCadastroCliente()
        {
            CtrlChildActionCadastroCliente CtrlChildActionCadastroCliente = new CtrlChildActionCadastroCliente();

            relatorio.startStep("Validar Cadastro de Cliente");

            //Validar Página Cadastro de Clientes
            CtrlChildActionCadastroCliente.assertIsTrue(By.Id("formPreCadastro"));

            relatorio.endStep("Validação realizada com sucesso");

        }

        public void complementaCadastroCliente()
        {
            CtrlChildActionCadastroCliente CtrlChildActionCadastroCliente = new CtrlChildActionCadastroCliente();

            relatorio.startStep("Complementar dados de Cadastro de Cliente");

            CtrlChildActionCadastroCliente.Click(By.Id("dataNascCad"));
            CtrlChildActionCadastroCliente.SendKeys(By.Id("dataNascCad"), Global.consulta.CLI_DT_NASCTO);

            relatorio.endStep("Cadastro complementar preenchido com sucesso");

            CtrlChildActionCadastroCliente.Click(By.Id("iniciarCadastro"));

            

        }

        public void alteraFilial()
        {
            CtrlChildActionTelaInicial CtrlChildActionTelaInicial = new CtrlChildActionTelaInicial();

            relatorio.startStep("Alterar Filial");

            ////Alterar Filial
            CtrlChildActionTelaInicial.wait(By.Id("btnYesOk"));
           

            CtrlChildActionTelaInicial.wait(By.Id("btnYesOk"));
            CtrlChildActionTelaInicial.Click(By.Id("btnYesOk"));
            Thread.Sleep(2000);
            CtrlChildActionTelaInicial.wait(By.XPath("//*[@id='labelFilial']"));
            CtrlChildActionTelaInicial.ElementIsClickable(By.XPath("//*[@id='labelFilial']"));
            CtrlChildActionTelaInicial.Click(By.XPath("//*[@id='labelFilial']"));

            CtrlChildActionTelaInicial.wait(By.Id("changeFilial"));
            CtrlChildActionTelaInicial.SelectByValue(By.Id("changeFilial"), Global.consulta.FILIAL);
            CtrlChildActionTelaInicial.SendKeys(By.Id("changeFilial"), OpenQA.Selenium.Keys.Enter + OpenQA.Selenium.Keys.Tab);
            //CtrlChildActionTelaInicial.Click(By.Id("changeFilial"));
            //CtrlChildActionTelaInicial.SendKeys(By.Id("changeFilial"), Global.consulta.FILIAL + OpenQA.Selenium.Keys.Tab);
            Thread.Sleep(2000);
            CtrlChildActionTelaInicial.wait(By.XPath("//button[contains(.,'Confirmar')]"));
            CtrlChildActionTelaInicial.Click(By.XPath("//button[contains(.,'Confirmar')]"));
            Thread.Sleep(1000);
            relatorio.endStep("Filial alterada com sucesso");

        }

        public void dadosPessoais()
        {
            CtrlChildActionDadosPessoais CtrlChildActionDadosPessoais = new CtrlChildActionDadosPessoais();

            relatorio.startStep("Preencher dados pessoais");
            //Avançar dados pessoais
            CtrlChildActionDadosPessoais.wait(By.Id("avancarDadosPessoais"));

            //Validar Página Cadastro de Clientes
            CtrlChildActionDadosPessoais.wait(By.Id("formDadosPessoais"));
            CtrlChildActionDadosPessoais.assertIsTrue(By.Id("formDadosPessoais"));

            //verifica se nome da mãe está preenchido e preenche caso não esteja

            if (CtrlChildActionDadosPessoais.getElementText(By.Id("nomeMaeDadosPessoais")) == "")
            {
                CtrlChildActionDadosPessoais.Click(By.Id("nomeMaeDadosPessoais"));
                CtrlChildActionDadosPessoais.SendKeys(By.Id("nomeMaeDadosPessoais"), Global.consulta.CLI_MAE + OpenQA.Selenium.Keys.Tab);
            }

            //Estado Civil
            CtrlChildActionDadosPessoais.Click(By.Id("estadoCivil"));
            CtrlChildActionDadosPessoais.SendKeys(By.Id("estadoCivil"), Global.consulta.CLI_EC_DOMI + OpenQA.Selenium.Keys.Enter);

            //Sexo       
            CtrlChildActionDadosPessoais.Click(By.Id("sexoDadosPessoais"));
            CtrlChildActionDadosPessoais.SendKeys(By.Id("sexoDadosPessoais"), Global.consulta.CLI_SEXO_DOMI + OpenQA.Selenium.Keys.Enter);

            //Deseja Receber a Fatura por email?         
            CtrlChildActionDadosPessoais.Click(By.Id("faturaEmailOutros"));
            CtrlChildActionDadosPessoais.SendKeys(By.Id("faturaEmailOutros"), "Não");

            //Email
            CtrlChildActionDadosPessoais.Click(By.Id("cliEmail"));
            CtrlChildActionDadosPessoais.Clear(By.Id("cliEmail"));
            CtrlChildActionDadosPessoais.SendKeys(By.Id("cliEmail"), "starline@starline.com" + OpenQA.Selenium.Keys.Enter);

            //celular
            if (Global.consulta.CLI_NUM_CELULAR != "")
            {
                CtrlChildActionDadosPessoais.Click(By.Id("celularDadosPessoais"));
                CtrlChildActionDadosPessoais.Clear(By.Id("celularDadosPessoais"));
                CtrlChildActionDadosPessoais.SendKeys(By.Id("celularDadosPessoais"), Global.consulta.CLI_NUM_CELULAR + OpenQA.Selenium.Keys.Enter);
            }
            //Botão avançar
            CtrlChildActionDadosPessoais.Click("avancarDadosPessoais");

            relatorio.endStep("Dados pessoais preenchidos com sucesso");

        }

        public void dadosResidenciais()
        {
            CtrlChildActionDadosResidenciais CtrlChildActionDadosResidenciais = new CtrlChildActionDadosResidenciais();

            relatorio.startStep("Preencher dados residenciais");

            //Validar Página Dados Residenciais
            CtrlChildActionDadosResidenciais.wait(By.Id("avancarDadosResidenciais"));
            CtrlChildActionDadosResidenciais.assertIsTrue(By.Id("formDadosResidenciais"));

            //Cep
            CtrlChildActionDadosResidenciais.Click(By.Id("cepDadosResidenciais"));
            CtrlChildActionDadosResidenciais.Clear(By.Id("cepDadosResidenciais"));
            CtrlChildActionDadosResidenciais.SendKeys(By.Id("cepDadosResidenciais"), Global.consulta.CLI_RES_CEP + OpenQA.Selenium.Keys.Tab);

            //Preencher bairro caso necessário
            if (!CtrlChildActionDadosResidenciais.somenteLeitura(By.Id("bairroDadosResidenciais")))
            {
                CtrlChildActionDadosResidenciais.Click(By.Id("bairroDadosResidenciais"));
                CtrlChildActionDadosResidenciais.Clear(By.Id("bairroDadosResidenciais"));
                CtrlChildActionDadosResidenciais.SendKeys(By.Id("bairroDadosResidenciais"), Global.consulta.CLI_RES_BAI + OpenQA.Selenium.Keys.Tab);
            }

            //Preencher rua caso necessário
            if (!CtrlChildActionDadosResidenciais.somenteLeitura(By.Id("ruaDadosResidenciais")))
            {
                CtrlChildActionDadosResidenciais.Click(By.Id("ruaDadosResidenciais"));
                CtrlChildActionDadosResidenciais.Clear(By.Id("ruaDadosResidenciais"));
                CtrlChildActionDadosResidenciais.SendKeys(By.Id("ruaDadosResidenciais"), Global.consulta.CLI_RES_RUA + OpenQA.Selenium.Keys.Tab);
            }

            //Numero
            CtrlChildActionDadosResidenciais.Click(By.Id("numeroDadosResidenciais"));
            CtrlChildActionDadosResidenciais.Clear(By.Id("numeroDadosResidenciais"));
            CtrlChildActionDadosResidenciais.SendKeys(By.Id("numeroDadosResidenciais"), "1");

            //Tipo Residencia
            CtrlChildActionDadosResidenciais.Click(By.Id("tipoResidencia"));
            CtrlChildActionDadosResidenciais.SendKeys(By.Id("tipoResidencia"), "ALUGADA" + OpenQA.Selenium.Keys.Enter);

            //Telefone Residencia
            CtrlChildActionDadosResidenciais.Click(By.Id("telefoneResidencialDadosResidenciais"));
            CtrlChildActionDadosResidenciais.Clear(By.Id("telefoneResidencialDadosResidenciais"));
            CtrlChildActionDadosResidenciais.SendKeys(By.Id("telefoneResidencialDadosResidenciais"), Global.consulta.CLI_RES_FONE + OpenQA.Selenium.Keys.Tab);

            //Avança para próxima tela
            CtrlChildActionDadosResidenciais.Click(By.Id("avancarDadosResidenciais"));

            relatorio.endStep("Dados residenciais preenchidos com sucesso");

        }

        public void dadosComerciais()
        {
            CtrlChildActionDadosComerciais CtrlChildActionDadosComerciais = new CtrlChildActionDadosComerciais();

            relatorio.startStep("Preencher dados comerciais");

            //Validar Página Dados Comerciais

            CtrlChildActionDadosComerciais.wait(By.Id("avancarDadosComerciais"));
            CtrlChildActionDadosComerciais.assertIsTrue(By.Id("formDadosComerciais"));

            //Classe Profissisional
            CtrlChildActionDadosComerciais.Click(By.Id("classeProfDadosComerciais"));
            CtrlChildActionDadosComerciais.SendKeys(By.Id("classeProfDadosComerciais"), Global.consulta.CLI_EMP_CARGO + OpenQA.Selenium.Keys.Enter);

            //Atividade
            CtrlChildActionDadosComerciais.Click(By.Id("atividadeDadosComerciais"));
            CtrlChildActionDadosComerciais.SendKeys(By.Id("atividadeDadosComerciais"), Global.consulta.CLI_EMP_OCUPACAO + OpenQA.Selenium.Keys.Enter);

            //Renda Mensal
            if (CtrlChildActionDadosComerciais.permitePreenchimento(By.Id("rendaMensalDadosComerciais"))) { 
            CtrlChildActionDadosComerciais.Click(By.Id("rendaMensalDadosComerciais"));
            CtrlChildActionDadosComerciais.Clear(By.Id("rendaMensalDadosComerciais"));
            CtrlChildActionDadosComerciais.SendKeys(By.Id("rendaMensalDadosComerciais"), Global.consulta.CLI_EMP_SALARIO + OpenQA.Selenium.Keys.Tab);
            }

            //Preenche ano/mes tempo empresa de acordo com a classe
            if (Global.consulta.CLI_EMP_CARGO == "ASSALARIADOS" || Global.consulta.CLI_EMP_CARGO == "AUTONOMOS" || Global.consulta.CLI_EMP_CARGO == "ESTABELECIDOS" || Global.consulta.CLI_EMP_CARGO == "LIBERAIS" )
            {
                Thread.Sleep(2000);
                CtrlChildActionDadosComerciais.SendKeys(By.Id("tempoEmpresaAnoDadosComerciais"), "2" + OpenQA.Selenium.Keys.Tab);
                Thread.Sleep(2000);
                CtrlChildActionDadosComerciais.SendKeys(By.Id("tempoEmpresaMes"), "7" + OpenQA.Selenium.Keys.Tab);
            }

            //CEP
            CtrlChildActionDadosComerciais.Click(By.Id("cepDadosComerciais"));
            CtrlChildActionDadosComerciais.Clear(By.Id("cepDadosComerciais"));
            CtrlChildActionDadosComerciais.SendKeys(By.Id("cepDadosComerciais"), Global.consulta.CLI_EMP_CEP);

            //Numero
            Thread.Sleep(1000);
            CtrlChildActionDadosComerciais.Click(By.Id("numeroDadosComerciais"));
            CtrlChildActionDadosComerciais.Clear(By.Id("numeroDadosComerciais"));
            Thread.Sleep(1000);
            CtrlChildActionDadosComerciais.Click(By.Id("numeroDadosComerciais"));
            CtrlChildActionDadosComerciais.SendKeys(By.Id("numeroDadosComerciais"), Global.consulta.CLI_EMP_NUM + OpenQA.Selenium.Keys.Tab);

            //Telefone Comercial
            CtrlChildActionDadosComerciais.Click(By.Id("telefoneComercialDadosComercial"));
            CtrlChildActionDadosComerciais.Clear(By.Id("telefoneComercialDadosComercial"));
            CtrlChildActionDadosComerciais.Click(By.Id("telefoneComercialDadosComercial"));
            CtrlChildActionDadosComerciais.SendKeys(By.Id("telefoneComercialDadosComercial"), Global.consulta.CLI_EMP_FONE + OpenQA.Selenium.Keys.Tab + OpenQA.Selenium.Keys.Tab);

            relatorio.endStep("Dados comerciais preenchidos com sucesso");

            //Botão avançar
            CtrlChildActionDadosComerciais.touchActionsClick(By.Id("avancarDadosComerciais"));

        }

        public void outros()
        {
            CtrlChildActionOutros CtrlChildActionOutros = new CtrlChildActionOutros();

            relatorio.startStep("Preencher dados outros");

            //Validar Página Outros
            CtrlChildActionOutros.wait(By.Id("avancarOutros"));
            CtrlChildActionOutros.assertIsTrue(By.Id("formOutros"));

            //Vencimento Melhor Dia
            CtrlChildActionOutros.Click(By.Id("vencimentoOutros"));
            CtrlChildActionOutros.SendKeys(By.Id("vencimentoOutros"), "10" + OpenQA.Selenium.Keys.Tab);

            //Confirmação de Vencimento
            CtrlChildActionOutros.wait(By.XPath("//button[contains(.,'Sim')]"));
            CtrlChildActionOutros.Click(By.XPath("//button[contains(.,'Sim')]"));

            //Cobrar tarifa de overlimit
            Thread.Sleep(2000);
            CtrlChildActionOutros.wait(By.CssSelector("#tarifaOverlimitOutros"));
            CtrlChildActionOutros.Click(By.CssSelector("#tarifaOverlimitOutros"));
            CtrlChildActionOutros.SendKeys(By.CssSelector("#tarifaOverlimitOutros"), "Não" + OpenQA.Selenium.Keys.Tab);

            //Conta Bônus?
            CtrlChildActionOutros.Click(By.Id("tipoCartaoOutros"));
            CtrlChildActionOutros.SendKeys(By.Id("tipoCartaoOutros"), "Não" + OpenQA.Selenium.Keys.Tab);

            //Campanha
            CtrlChildActionOutros.Click(By.Id("campanhaOutros"));
            CtrlChildActionOutros.SendKeys(By.Id("campanhaOutros"), "931 - Desc Primeira Compra Marisa Itaucard 10%" + OpenQA.Selenium.Keys.Tab);

            //Botão avançar
            CtrlChildActionOutros.wait(By.Id("avancarOutros"));

            relatorio.endStep("Dados outros preenchidos com sucesso");

            CtrlChildActionOutros.touchActionsClick(By.Id("avancarOutros"));
                       

        }

        public void biometriaFacial()
        {
            CtrlChildActionBiometriaFacial CtrlChildActionBiometriaFacial = new CtrlChildActionBiometriaFacial();
            
            relatorio.startStep("Realizar biometria facial");
            //biometria Facial
            Thread.Sleep(2000);
            CtrlChildActionBiometriaFacial.assertAreEqual("BIOMETRIA FACIAL", By.XPath("//*[@id='fieldSetBiometria']/h2"));
            //CtrlChildActionBiometriaFacial.carregaImagemFacial();


            //Aceitar a imagem 
            Thread.Sleep(2000);
            //CtrlChildActionBiometriaFacial.switchFrame("acessoFlash");
            //CtrlChildActionBiometriaFacial.touchActionsClick(By.XPath("//*[@id='pnlCapturePreview']/div/div[4]/div/div[2]/span"));
            Thread.Sleep(2000);


            //CtrlChildActionBiometriaFacial.wait(By.Id("pnlSuccess"));


            //retorna ao frame default
            CtrlChildActionBiometriaFacial.ReturndefaultContentFrame();

            CtrlChildActionBiometriaFacial.wait(By.Id("avancarBiometriaFacial"));

            relatorio.endStep("Biometria facial realizada com sucesso");


            //avança biometria facial
            CtrlChildActionBiometriaFacial.touchActionsClick(By.Id("avancarBiometriaFacial"));
                    

        }

        public void formalizacao()
        {
            CtrlChildActionFormalizacao CtrlChildActionFormalizacao = new CtrlChildActionFormalizacao();

            relatorio.startStep("Realizar formalização");

            Thread.Sleep(2000);
            CtrlChildActionFormalizacao.ReturndefaultContentFrame();
            //CtrlChildActionFormalizacao.assertAreEqual("FORMALIZAÇÃO", By.XPath("//*[@id='box - concessao - cartao']/div/div[2]/div/div/fieldset[7]/h2"));
            CtrlChildActionFormalizacao.switchFrame("assinaturaEletronica");            

            CtrlChildActionFormalizacao.clickJS(By.XPath("/html/body/form/footer/input"));

            relatorio.endStep("Formalização realizada com sucesso");

        }

        public void anexarDoc()
        {
            CtrlChildActionAnexarDoc CtrlChildActionAnexarDoc = new CtrlChildActionAnexarDoc();

            relatorio.startStep("Realizar anexo de documentos");


            string imagemDoc;
            Thread.Sleep(2000);

            if (Global.OSLinux)
            {
                imagemDoc = Global.PathDoProjeto + "//Foto//CNH.jpg";
            }
            else
            {
                imagemDoc = @Global.PathDoProjeto + "\\Foto\\CNH.jpg";
            }

            CtrlChildActionAnexarDoc.switchFrame("assinaturaEletronica");
            CtrlChildActionAnexarDoc.SendKeys(By.XPath("//*[@id='file_37']"), imagemDoc);
            CtrlChildActionAnexarDoc.waitElementVisible(By.XPath("//*[@id='ok_37']"));
            CtrlChildActionAnexarDoc.SendKeys(By.XPath("//*[@id='file_38']"), imagemDoc);
            CtrlChildActionAnexarDoc.waitElementVisible(By.XPath("//*[@id='ok_38']"));

            relatorio.endStep("Documentos anexados com sucesso");

            CtrlChildActionAnexarDoc.clickJS(By.XPath("/html/body/form/footer/input"));
        }

        public void assinaturaDigital()
        {
            CtrlChildActionAssinaturaDigital CtrlChildActionAssinaturaDigital = new CtrlChildActionAssinaturaDigital();

            relatorio.startStep("Realizar assinatura digital");

            Thread.Sleep(2000);

            CtrlChildActionAssinaturaDigital.switchFrame("assinaturaEletronica");
            CtrlChildActionAssinaturaDigital.realizaAssinaturaDigital();

            relatorio.endStep("Assinatura realizada com sucesso");

        }

        public void finalizaCadastro()
        {
            CtrlChildActionFinalizaCadastro CtrlChildActionFinalizaCadastro = new CtrlChildActionFinalizaCadastro();
            CtrlChildActionFinalizaCadastro.clickJS(By.Id("btnSave"));

            relatorio.startStep("Finaliza Cadastro");
            
            CtrlChildActionFinalizaCadastro.wait(By.XPath("/html/body/main/div/span[1]"));
            CtrlChildActionFinalizaCadastro.assertAreEqual("Assinado com sucesso!", By.XPath("/html/body/main/div/span[1]"));
            Thread.Sleep(2000);

            CtrlChildActionFinalizaCadastro.ReturndefaultContentFrame();

            CtrlChildActionFinalizaCadastro.wait(By.Id("resumoResultadoProposta"));
            //CtrlChildActionFinalizaCadastro.assertAreEqual("Proposta aprovada, realize o embossamento através do botão abaixo.", By.Id("resumoResultadoProposta"));
            CtrlChildActionFinalizaCadastro.assertAreEqual("Atenção, undefined - undefined", By.Id("resumoResultadoProposta"));

            
            relatorio.endStep("Cadastro Realizado com sucesso");
        }

        public void imprimeCartao()
        {
            CtrlChildActionFinalizaCadastro CtrlChildActionFinalizaCadastro = new CtrlChildActionFinalizaCadastro();

            relatorio.startStep("Imprime Cartão");

            CtrlChildActionFinalizaCadastro.Click(By.Id("btnEmbossarCartao"));


            CtrlChildActionFinalizaCadastro.wait(By.Id("dialog-message"));
            CtrlChildActionFinalizaCadastro.assertAreEqual("Cartão embossado com sucesso", By.Id("dialog-message"));

            relatorio.endStep("Cartão embossado com sucesso");

            CtrlChildActionFinalizaCadastro.Click(By.Id("btnYesOk"));

            CtrlChildActionFinalizaCadastro.Click(By.Id("btnInicioCadastro"));
        }

    }
}