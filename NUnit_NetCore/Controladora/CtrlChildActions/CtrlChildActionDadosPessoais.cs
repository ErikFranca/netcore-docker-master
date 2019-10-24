using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluxoVendaCartoes
{
    public class CtrlChildActionDadosPessoais : CtrlActions
    {
        public CtrlChildActionDadosPessoais()
        {
        }

        public string getElementText(By elemento)
        {
            return Global.driver.FindElement(elemento).Text.ToString();
        }


    }
}
