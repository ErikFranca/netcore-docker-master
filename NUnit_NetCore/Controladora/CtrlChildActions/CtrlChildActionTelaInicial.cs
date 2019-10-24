using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluxoVendaCartoes
{
    class CtrlChildActionTelaInicial : CtrlActions
    {
        public CtrlChildActionTelaInicial()
        {
        }

        public void moveToElement(By elemento)
        {
            Actions touchActions = new Actions(Global.driver);
            touchActions.MoveToElement(Global.driver.FindElement(elemento)).Build().Perform();
        }

        public void SelectByValue(By elemento, string valor)
        {
            new SelectElement(Global.driver.FindElement(elemento)).SelectByValue(valor);
        }
    }
}
