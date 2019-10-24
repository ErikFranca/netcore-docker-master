using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluxoVendaCartoes
{
    class CtrlChildActionAnexarDoc : CtrlActions
    {
        public CtrlChildActionAnexarDoc()
        {
        }

        public void waitElementVisible(By elemento)
        {
            WebDriverWait wait = new WebDriverWait(Global.driver, TimeSpan.FromSeconds(60));
            wait.Until(ExpectedConditions.ElementIsVisible(elemento));
        }
    }
}
