

namespace FluxoVendaCartoes
{
    class CtrlRelatorio
    {
        public void startTest(string analystName, string testDesc, string testType, string preCondition, string postCondition, string inputData)
        {
            Global.processTest.TestName = "Teste_de_Consumo"; //Nome do Teste (recupera o nome do metodo)
            Global.processTest.AnalystName = analystName; //Nome do Analista
            Global.processTest.TestDesc = testDesc; //Descricao do Teste
            Global.processTest.TestType = testType; //Tipo do Teste
            Global.processTest.PreCondition = preCondition;                                                      // Dados de Pre Condicao
            Global.processTest.PostCondition = postCondition;                                                     // Dados de Pos Condicao
            Global.processTest.InputData = inputData; // Detalhes sobre a Massa de Dados


            // ATENÇÃO à chamada com o deleteFlag no final -> mudança de arquitetura
            Global.processTest.ReportID = Global.processTest.StartTest(Global.processTest.CustomerName, Global.processTest.SuiteName, Global.processTest.ScenarioName, Global.processTest.TestName, Global.processTest.TestType, Global.processTest.AnalystName, Global.processTest.TestDesc, Global.processTest.ReportID);
            Global.processTest.DoTest(Global.processTest.PreCondition, Global.processTest.PostCondition, Global.processTest.InputData);

            Global.reportId = Global.processTest.ReportID;
        }

        public void instanceStep()
        {
            Global.processTest.DoStep("Acessar Site", "Login validado com sucesso");
            Global.processTest.DoStep("Pesquisar Produto", "Pesquisa efetuado com sucesso");            

        }

        public void startStep(string stepName)
        {
            Global.processTest.StepName = stepName;
            Global.processTest.StepNumber = Global.processTest.GetStepNumber(Global.processTest.StepName);
            Global.processTest.StepTurn = Global.processTest.NovoTurno(Global.processTest.TestName, Global.processTest.StepName); // Este método é o novo a ser criado, ele vai servir para retornar o incremento do turno em ações repetidas
            Global.processTest.LgsID = Global.processTest.StartStep(Global.processTest.StepName, Global.processTest.StepTurn);

        }

        public void print()
        {
            Global.printFileName = Global.processTest.PrintPageComSelenium(Global.driver, false);
        }

        public void endStep(string msgSucesso)
        {
            Global.printFileName = Global.processTest.PrintPageComSelenium(Global.driver, false);
            Global.processTest.EndStep(Global.processTest.LgsID, "sucesso", Global.printFileName, msgSucesso);
        }

    }
}
