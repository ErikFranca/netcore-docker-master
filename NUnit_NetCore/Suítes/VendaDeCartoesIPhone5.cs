//ORG

using NUnit.Framework;
using System;
using System.Text;
using OpenQA.Selenium.Chrome;
using Starline;
using OpenQA.Selenium.Interactions;
using System.IO;
using System.Reflection;

namespace FluxoVendaCartoes
{
    
    [TestFixture]
    public class VendaCartoes_iPhone_5_SE
    {    
        private string baseURL;
        public string PathChromeDriver = "";
        public string PathDoExecutavel = "";
        public string tipoTeste = "iPhone 5/SE";
        public string login;
        public string senha;
        public ProcessosCCM processosCCM;

        
        [SetUp]
        public void SetupTest()

        {
            login = TestContext.Parameters.Get("loginCCM");
            senha = TestContext.Parameters.Get("senhaCCM");

            Global.PathDoProjeto = Directory.GetCurrentDirectory();
            Global.PathDoProjeto = Global.PathDoProjeto.Substring(0, Global.PathDoProjeto.IndexOf("bin"));

            
            Global.processTest = new ProcessTest();
            if (Global.reportId != 0)
            {
                Global.processTest.ReportID = Global.reportId;
            }

            Global.consulta = new ClsCCM();
            Global.processTest.mostrarLog = false;
            var options = new ChromeOptions();
            var os = Environment.OSVersion;
            Global.OSLinux = os.Platform == PlatformID.Unix;
            ChromeDriverService service = null;
            PathDoExecutavel = @System.Environment.CurrentDirectory.ToString();


            if (Global.OSLinux)
            {
                PathChromeDriver = "/usr/bin/";
                //PathChromeDriver = Global.PathDoProjeto;
                //PathChromeDriver = Global.PathDoProjeto.Substring(0, Global.PathDoProjeto.IndexOf("NUnit_NetCore"));
                service = ChromeDriverService.CreateDefaultService(PathChromeDriver, "chromedriver");
                options.AddArgument("--headless");
                options.AddArgument("use-fake-ui-for-media-stream");
            }
            else
            {
                PathChromeDriver = PathDoExecutavel;
                service = ChromeDriverService.CreateDefaultService(PathChromeDriver, "chromedriver.exe");
                //options.AddArgument("--headless");
            }

            Global.processTest.PastaBase = Global.PathDoProjeto;
            Global.processTest.CustomerName = "Kabum"; //CustomerName evitar Acentuacao e espaço
            Global.processTest.SuiteName = "CCM"; //CustomerName evitar Acentuacao e espaço
            Global.processTest.ScenarioName = MethodBase.GetCurrentMethod().DeclaringType.Name;
           

            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--touch-events=enabled");
            options.AddArgument("--start-maximized");            
            //options.AddArgument("use-fake-ui-for-media-stream");
            //options.EnableMobileEmulation(tipoTeste);
            Global.driver = new ChromeDriver(service, options);
            processosCCM = new ProcessosCCM();
            baseURL = "https://www.uol.com.br/";
            Global.verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {

                Global.driver.Quit();
                //Global.compilaRelatorio();

            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        //[OneTimeTearDown]
        //public void compilaRelatorio()
        //{
        //    string retorno = Global.compilaRelatorio();
        //    if (retorno == "[Done]\n")
        //    {
        //        Console.Error.WriteLine("Relatório gerado com sucesso! Clique no link abaixo:");
        //        Console.Error.WriteLine("http://54.233.230.119/auto/reports/marisa/" + Global.processTest.ReportID.ToString());
        //    }
        //    else
        //    {
        //        Console.Error.WriteLine("Relatório gerado erro!");
        //    }
        //}

        [Test]
        public void Teste_AcessoSite_PesquisaProduto()
        {
            Global.touchActions = new Actions(Global.driver);
            
            try
            {

                //Execução

                //Acessar Página da Kabum
                processosCCM.acessarSite();

                //Validar Tela de Pesquisa
                processosCCM.pesquisarProduto();

                //Pesquisa outro produto
                processosCCM.pesquisarOutrosProdutos();


            }

            catch (Exception)
            {
                Assert.Fail();
            }

        }


        //[Test]
        //public void CT001_Cadastro_de_Clientes_Aposentados()
        //{

        //    Global.touchActions = new Actions(Global.driver);

        //    CtrlRelatorio relatorio = new CtrlRelatorio();
        //    relatorio.startTest("Cadastro de Cliente Aposentados", "Rafael Leal", "Cadastrar cliente aposentado", tipoTeste, "Cliente sem cadastro", "Cliente cadastrado", "Cliente aposentado sem cadastro");
        //    relatorio.instanceStep();

        //    try
        //    {

        //        //Execução

        //        //Acessar Página do CCM
        //        processosCCM.acessarSite();

        //        //Validar Tela de Login
        //        processosCCM.pesquisarProduto();

        //    }

        //    catch (Exception ex)
        //    {
        //        // Chamada para Termino de Step caso ocorra algum Erro
        //        try { 
        //            string PrintFileName = Global.processTest.PrintPageComSelenium(Global.driver, false);
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", PrintFileName, ex.ToString());
        //        }
        //        catch
        //        {
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", null, ex.ToString());
        //        }
        //        Assert.Fail();
        //    }


        //    finally
        //    {
        //        // Chamada de Arquitetura para Termino do Teste
        //        Global.processTest.EndTest(Global.processTest.ReportID);

        //    }



        //}

        //[Test]
        //public void CT002_Cadastro_de_Clientes_Assalariados()
        //{
        //    Global.consulta.Consulta_Cliente_Cadastro("ASSALARIADOS");

        //    Global.touchActions = new Actions(Global.driver);

        //    CtrlRelatorio relatorio = new CtrlRelatorio();

        //    relatorio.startTest("Cadastro de Cliente Assalariado", "Rafael Leal", "Cadastrar cliente assalariado", tipoTeste, "Cliente sem cadastro", "Cliente cadastrado", "Cliente assalariado sem cadastro");

        //    relatorio.instanceStep();


        //    try
        //    {

        //        //Execução

        //        //Acessar Página do CCM
        //        Global.driver.Navigate().GoToUrl(baseURL);

        //        //Validar Tela de Login
        //        processosCCM.validaLogin();

        //        //Realizar login
        //        processosCCM.Login(login, senha);

        //        //Altera filial
        //        processosCCM.alteraFilial();

        //        //Validar Página Inicial
        //        processosCCM.validaPaginaInicial();

        //        //Cenario 1 - Cadastro de Clientes
        //        processosCCM.cadastroCliente();

        //        //Validar Página Cadastro de Clientes
        //        processosCCM.validaCadastroCliente();

        //        //Complementa Cadastro de Clientes
        //        processosCCM.complementaCadastroCliente();

        //        //Cenario 2  -- Dados pessoais
        //        processosCCM.dadosPessoais();

        //        //Cenario 3 -- Dados Residenciais 
        //        processosCCM.dadosResidenciais();

        //        //Cenario 4 -- Dados Comerciais 
        //        processosCCM.dadosComerciais();

        //        //Cenario 5 -- Outros 
        //        processosCCM.outros();

        //        //Cenario 6 -- Camera 
        //        processosCCM.biometriaFacial();

        //        //Cenario 7 -- FORMALIZAÇÃO
        //        processosCCM.formalizacao();

        //        //Cenario 7 -- anexar imagem
        //        processosCCM.anexarDoc();

        //        //Cenario 7 -- assinatura eletronica
        //        processosCCM.assinaturaDigital();

        //        //Finaliza o cadastro
        //        //processosCCM.finalizaCadastro();
        //    }

        //    catch (Exception ex)
        //    {
        //        // Chamada para Termino de Step caso ocorra algum Erro
        //        try
        //        {
        //            string PrintFileName = Global.processTest.PrintPageComSelenium(Global.driver, false);
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", PrintFileName, ex.ToString());
        //        }
        //        catch
        //        {
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", null, ex.ToString());
        //        }
        //        Assert.Fail();
        //    }


        //    finally
        //    {
        //        // Chamada de Arquitetura para Termino do Teste
        //        Global.processTest.EndTest(Global.processTest.ReportID);
        //    }
        //}

        //[Test]
        //public void CT003_Cadastro_de_Clientes_Autonomos()
        //{
        //    Global.consulta.Consulta_Cliente_Cadastro("AUTONOMOS");

        //    Global.touchActions = new Actions(Global.driver);

        //    CtrlRelatorio relatorio = new CtrlRelatorio();

        //    relatorio.startTest("Cadastro de Cliente Autonomo", "Rafael Leal", "Cadastrar cliente Autonomo", tipoTeste, "Cliente sem cadastro", "Cliente cadastrado", "Cliente Autonomo sem cadastro");

        //    relatorio.instanceStep();


        //    try
        //    {

        //        //Execução

        //        //Acessar Página do CCM
        //        Global.driver.Navigate().GoToUrl(baseURL);

        //        //Validar Tela de Login
        //        processosCCM.validaLogin();

        //        //Realizar login
        //        processosCCM.Login(login, senha);

        //        //Altera filial
        //        processosCCM.alteraFilial();

        //        //Validar Página Inicial
        //        processosCCM.validaPaginaInicial();

        //        //Cenario 1 - Cadastro de Clientes
        //        processosCCM.cadastroCliente();

        //        //Validar Página Cadastro de Clientes
        //        processosCCM.validaCadastroCliente();

        //        //Complementa Cadastro de Clientes
        //        processosCCM.complementaCadastroCliente();

        //        //Cenario 2  -- Dados pessoais
        //        processosCCM.dadosPessoais();

        //        //Cenario 3 -- Dados Residenciais 
        //        processosCCM.dadosResidenciais();

        //        //Cenario 4 -- Dados Comerciais 
        //        processosCCM.dadosComerciais();

        //        //Cenario 5 -- Outros 
        //        processosCCM.outros();

        //        //Cenario 6 -- Camera 
        //        processosCCM.biometriaFacial();

        //        //Cenario 7 -- FORMALIZAÇÃO
        //        processosCCM.formalizacao();

        //        //Cenario 7 -- anexar imagem
        //        processosCCM.anexarDoc();

        //        //Cenario 7 -- assinatura eletronica
        //        processosCCM.assinaturaDigital();

        //        //Finaliza o cadastro
        //        //processosCCM.finalizaCadastro();
        //    }

        //    catch (Exception ex)
        //    {
        //        // Chamada para Termino de Step caso ocorra algum Erro
        //        try
        //        {
        //            string PrintFileName = Global.processTest.PrintPageComSelenium(Global.driver, false);
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", PrintFileName, ex.ToString());
        //        }
        //        catch
        //        {
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", null, ex.ToString());
        //        }
        //        Assert.Fail();
        //    }


        //    finally
        //    {
        //        // Chamada de Arquitetura para Termino do Teste
        //        Global.processTest.EndTest(Global.processTest.ReportID);
        //    }
        //}

        //[Test]
        //public void CT004_Cadastro_de_Clientes_Colaborador()
        //{

        //    Global.consulta.Consulta_Cliente_Cadastro("COLABORADOR");

        //    Global.touchActions = new Actions(Global.driver);

        //    CtrlRelatorio relatorio = new CtrlRelatorio();

        //    relatorio.startTest("Cadastro de Cliente Colaborador", "Rafael Leal", "Cadastrar cliente Colaborador", tipoTeste, "Cliente sem cadastro", "Cliente cadastrado", "Cliente Colaborador sem cadastro");

        //    relatorio.instanceStep();


        //    try
        //    {

        //        //Execução

        //        //Acessar Página do CCM
        //        Global.driver.Navigate().GoToUrl(baseURL);

        //        //Validar Tela de Login
        //        processosCCM.validaLogin();

        //        //Realizar login
        //        processosCCM.Login(login, senha);

        //        //Altera filial
        //        processosCCM.alteraFilial();

        //        //Validar Página Inicial
        //        processosCCM.validaPaginaInicial();

        //        //Cenario 1 - Cadastro de Clientes
        //        processosCCM.cadastroCliente();

        //        //Validar Página Cadastro de Clientes
        //        processosCCM.validaCadastroCliente();

        //        //Complementa Cadastro de Clientes
        //        processosCCM.complementaCadastroCliente();

        //        //Cenario 2  -- Dados pessoais
        //        processosCCM.dadosPessoais();

        //        //Cenario 3 -- Dados Residenciais 
        //        processosCCM.dadosResidenciais();

        //        //Cenario 4 -- Dados Comerciais 
        //        processosCCM.dadosComerciais();

        //        //Cenario 5 -- Outros 
        //        processosCCM.outros();

        //        //Cenario 6 -- Camera 
        //        processosCCM.biometriaFacial();

        //        //Cenario 7 -- FORMALIZAÇÃO
        //        processosCCM.formalizacao();

        //        //Cenario 7 -- anexar imagem
        //        processosCCM.anexarDoc();

        //        //Cenario 7 -- assinatura eletronica
        //        processosCCM.assinaturaDigital();

        //        //Finaliza o cadastro
        //        //processosCCM.finalizaCadastro();
        //    }

        //    catch (Exception ex)
        //    {
        //        // Chamada para Termino de Step caso ocorra algum Erro
        //        try
        //        {
        //            string PrintFileName = Global.processTest.PrintPageComSelenium(Global.driver, false);
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", PrintFileName, ex.ToString());
        //        }
        //        catch
        //        {
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", null, ex.ToString());
        //        }
        //        Assert.Fail();
        //    }


        //    finally
        //    {
        //        // Chamada de Arquitetura para Termino do Teste
        //        Global.processTest.EndTest(Global.processTest.ReportID);
        //    }
        //}

        //[Test]
        //public void CT005_Cadastro_de_Clientes_Estabelecidos()
        //{
        //    Global.consulta.Consulta_Cliente_Cadastro("ESTABELECIDOS");

        //    Global.touchActions = new Actions(Global.driver);

        //    CtrlRelatorio relatorio = new CtrlRelatorio();

        //    relatorio.startTest("Cadastro de Cliente Estabelecidos", "Rafael Leal", "Cadastrar cliente Estabelecidos", tipoTeste, "Cliente sem cadastro", "Cliente cadastrado", "Cliente Estabelecidos sem cadastro");

        //    relatorio.instanceStep();


        //    try
        //    {

        //        //Execução

        //        //Acessar Página do CCM
        //        Global.driver.Navigate().GoToUrl(baseURL);

        //        //Validar Tela de Login
        //        processosCCM.validaLogin();

        //        //Realizar login
        //        processosCCM.Login(login, senha);

        //        //Altera filial
        //        processosCCM.alteraFilial();

        //        //Validar Página Inicial
        //        processosCCM.validaPaginaInicial();

        //        //Cenario 1 - Cadastro de Clientes
        //        processosCCM.cadastroCliente();

        //        //Validar Página Cadastro de Clientes
        //        processosCCM.validaCadastroCliente();

        //        //Complementa Cadastro de Clientes
        //        processosCCM.complementaCadastroCliente();

        //        //Cenario 2  -- Dados pessoais
        //        processosCCM.dadosPessoais();

        //        //Cenario 3 -- Dados Residenciais 
        //        processosCCM.dadosResidenciais();

        //        //Cenario 4 -- Dados Comerciais 
        //        processosCCM.dadosComerciais();

        //        //Cenario 5 -- Outros 
        //        processosCCM.outros();

        //        //Cenario 6 -- Camera 
        //        processosCCM.biometriaFacial();

        //        //Cenario 7 -- FORMALIZAÇÃO
        //        processosCCM.formalizacao();

        //        //Cenario 7 -- anexar imagem
        //        processosCCM.anexarDoc();

        //        //Cenario 7 -- assinatura eletronica
        //        processosCCM.assinaturaDigital();

        //        //Finaliza o cadastro
        //        //processosCCM.finalizaCadastro();
        //    }

        //    catch (Exception ex)
        //    {
        //        // Chamada para Termino de Step caso ocorra algum Erro
        //        try
        //        {
        //            string PrintFileName = Global.processTest.PrintPageComSelenium(Global.driver, false);
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", PrintFileName, ex.ToString());
        //        }
        //        catch
        //        {
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", null, ex.ToString());
        //        }
        //        Assert.Fail();
        //    }


        //    finally
        //    {
        //        // Chamada de Arquitetura para Termino do Teste
        //        Global.processTest.EndTest(Global.processTest.ReportID);
        //    }
        //}

        //[Test]
        //public void CT006_Cadastro_de_Clientes_Liberais()
        //{
        //    Global.consulta.Consulta_Cliente_Cadastro("LIBERAIS");

        //    Global.touchActions = new Actions(Global.driver);

        //    CtrlRelatorio relatorio = new CtrlRelatorio();

        //    relatorio.startTest("Cadastro de Cliente Liberais", "Rafael Leal", "Cadastrar cliente Liberais", tipoTeste, "Cliente sem cadastro", "Cliente cadastrado", "Cliente Liberais sem cadastro");

        //    relatorio.instanceStep();


        //    try
        //    {

        //        //Execução

        //        //Acessar Página do CCM
        //        Global.driver.Navigate().GoToUrl(baseURL);

        //        //Validar Tela de Login
        //        processosCCM.validaLogin();

        //        //Realizar login
        //        processosCCM.Login(login, senha);

        //        //Altera filial
        //        processosCCM.alteraFilial();

        //        //Validar Página Inicial
        //        processosCCM.validaPaginaInicial();

        //        //Cenario 1 - Cadastro de Clientes
        //        processosCCM.cadastroCliente();

        //        //Validar Página Cadastro de Clientes
        //        processosCCM.validaCadastroCliente();

        //        //Complementa Cadastro de Clientes
        //        processosCCM.complementaCadastroCliente();

        //        //Cenario 2  -- Dados pessoais
        //        processosCCM.dadosPessoais();

        //        //Cenario 3 -- Dados Residenciais 
        //        processosCCM.dadosResidenciais();

        //        //Cenario 4 -- Dados Comerciais 
        //        processosCCM.dadosComerciais();

        //        //Cenario 5 -- Outros 
        //        processosCCM.outros();

        //        //Cenario 6 -- Camera 
        //        processosCCM.biometriaFacial();

        //        //Cenario 7 -- FORMALIZAÇÃO
        //        processosCCM.formalizacao();

        //        //Cenario 7 -- anexar imagem
        //        processosCCM.anexarDoc();

        //        //Cenario 7 -- assinatura eletronica
        //        processosCCM.assinaturaDigital();

        //        //Finaliza o cadastro
        //        //processosCCM.finalizaCadastro();
        //    }

        //    catch (Exception ex)
        //    {
        //        // Chamada para Termino de Step caso ocorra algum Erro
        //        try
        //        {
        //            string PrintFileName = Global.processTest.PrintPageComSelenium(Global.driver, false);
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", PrintFileName, ex.ToString());
        //        }
        //        catch
        //        {
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", null, ex.ToString());
        //        }
        //        Assert.Fail();
        //    }


        //    finally
        //    {
        //        // Chamada de Arquitetura para Termino do Teste
        //        Global.processTest.EndTest(Global.processTest.ReportID);
        //    }
        //}

        //[Test]
        //public void CT007_Cadastro_de_Clientes_Outros()
        //{
        //    Global.consulta.Consulta_Cliente_Cadastro("OUTROS");

        //    Global.touchActions = new Actions(Global.driver);

        //    CtrlRelatorio relatorio = new CtrlRelatorio();

        //    relatorio.startTest("Cadastro de Cliente Outros", "Rafael Leal", "Cadastrar cliente Outros", tipoTeste, "Cliente sem cadastro", "Cliente cadastrado", "Cliente Outros sem cadastro");

        //    relatorio.instanceStep();


        //    try
        //    {

        //        //Execução

        //        //Acessar Página do CCM
        //        Global.driver.Navigate().GoToUrl(baseURL);

        //        //Validar Tela de Login
        //        processosCCM.validaLogin();

        //        //Realizar login
        //        processosCCM.Login(login, senha);

        //        //Altera filial
        //        processosCCM.alteraFilial();

        //        //Validar Página Inicial
        //        processosCCM.validaPaginaInicial();

        //        //Cenario 1 - Cadastro de Clientes
        //        processosCCM.cadastroCliente();

        //        //Validar Página Cadastro de Clientes
        //        processosCCM.validaCadastroCliente();

        //        //Complementa Cadastro de Clientes
        //        processosCCM.complementaCadastroCliente();

        //        //Cenario 2  -- Dados pessoais
        //        processosCCM.dadosPessoais();

        //        //Cenario 3 -- Dados Residenciais 
        //        processosCCM.dadosResidenciais();

        //        //Cenario 4 -- Dados Comerciais 
        //        processosCCM.dadosComerciais();

        //        //Cenario 5 -- Outros 
        //        processosCCM.outros();

        //        //Cenario 6 -- Camera 
        //        processosCCM.biometriaFacial();

        //        //Cenario 7 -- FORMALIZAÇÃO
        //        processosCCM.formalizacao();

        //        //Cenario 7 -- anexar imagem
        //        processosCCM.anexarDoc();

        //        //Cenario 7 -- assinatura eletronica
        //        processosCCM.assinaturaDigital();

        //        //Finaliza o cadastro
        //        //processosCCM.finalizaCadastro();
        //    }

        //    catch (Exception ex)
        //    {
        //        // Chamada para Termino de Step caso ocorra algum Erro
        //        try
        //        {
        //            string PrintFileName = Global.processTest.PrintPageComSelenium(Global.driver, false);
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", PrintFileName, ex.ToString());
        //        }
        //        catch
        //        {
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", null, ex.ToString());
        //        }
        //        Assert.Fail();
        //    }


        //    finally
        //    {
        //        // Chamada de Arquitetura para Termino do Teste
        //        Global.processTest.EndTest(Global.processTest.ReportID);
        //    }
        //}

        //[Test]        
        //public void CT008_Cadastro_de_Clientes_Pensionistas()
        //{
        //    Global.consulta.Consulta_Cliente_Cadastro("PENSIONISTAS");
        //    Global.touchActions = new Actions(Global.driver);

        //    CtrlRelatorio relatorio = new CtrlRelatorio();

        //    relatorio.startTest("Cadastro de Cliente Pensionistas", "Rafael Leal", "Cadastrar cliente Pensionistas", tipoTeste, "Cliente sem cadastro", "Cliente cadastrado", "Cliente Pensionistas sem cadastro");

        //    relatorio.instanceStep();


        //    try
        //    {

        //        //Execução

        //        //Acessar Página do CCM
        //        Global.driver.Navigate().GoToUrl(baseURL);

        //        //Validar Tela de Login
        //        processosCCM.validaLogin();

        //        //Realizar login
        //        processosCCM.Login(login, senha);

        //        //Altera filial
        //        processosCCM.alteraFilial();

        //        //Validar Página Inicial
        //        processosCCM.validaPaginaInicial();

        //        //Cenario 1 - Cadastro de Clientes
        //        processosCCM.cadastroCliente();

        //        //Validar Página Cadastro de Clientes
        //        processosCCM.validaCadastroCliente();

        //        //Complementa Cadastro de Clientes
        //        processosCCM.complementaCadastroCliente();

        //        //Cenario 2  -- Dados pessoais
        //        processosCCM.dadosPessoais();

        //        //Cenario 3 -- Dados Residenciais 
        //        processosCCM.dadosResidenciais();

        //        //Cenario 4 -- Dados Comerciais 
        //        processosCCM.dadosComerciais();

        //        //Cenario 5 -- Outros 
        //        processosCCM.outros();

        //        //Cenario 6 -- Camera 
        //        processosCCM.biometriaFacial();

        //        //Cenario 7 -- FORMALIZAÇÃO
        //        processosCCM.formalizacao();

        //        //Cenario 7 -- anexar imagem
        //        processosCCM.anexarDoc();

        //        //Cenario 7 -- assinatura eletronica
        //        processosCCM.assinaturaDigital();

        //        //Finaliza o cadastro
        //        //processosCCM.finalizaCadastro();
        //    }

        //    catch (Exception ex)
        //    {
        //        // Chamada para Termino de Step caso ocorra algum Erro
        //        try
        //        {
        //            string PrintFileName = Global.processTest.PrintPageComSelenium(Global.driver, false);
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", PrintFileName, ex.ToString());
        //        }
        //        catch
        //        {
        //            Global.processTest.EndStep(Global.processTest.LgsID, "erro", null, ex.ToString());
        //        }
        //        Assert.Fail();
        //    }


        //    finally
        //    {
        //        // Chamada de Arquitetura para Termino do Teste
        //        Global.processTest.EndTest(Global.processTest.ReportID);
        //    }
        //}
    }
}