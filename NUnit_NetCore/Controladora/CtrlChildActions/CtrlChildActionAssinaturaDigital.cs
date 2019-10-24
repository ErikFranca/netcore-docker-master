using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluxoVendaCartoes
{
    class CtrlChildActionAssinaturaDigital : CtrlActions
    {
        public CtrlChildActionAssinaturaDigital()
        {
        }

        public void realizaAssinaturaDigital()
        {
            IWebElement canvas = Global.driver.FindElement(By.Id("canvas"));
            Global.touchActions = new Actions(Global.driver);
            Global.touchActions.MoveToElement(canvas, 100, 125);
            Global.touchActions.Click();
            Global.touchActions.ClickAndHold();
            Global.touchActions.MoveToElement(canvas, 150, 175);
            Global.touchActions.Release();
            Global.touchActions.Build();
            Global.touchActions.Perform();
        }
    }
}
