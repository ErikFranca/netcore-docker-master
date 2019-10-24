using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace FluxoVendaCartoes
{
    public class CtrlActions
    {
        public bool acceptNextAlert { get; set; }

        public CtrlActions()
        {
            acceptNextAlert = true;
        }
        public void Clear(By elemento)
        {
            Global.driver.FindElement(elemento).Clear();
        }
        public void Clear(string pAction)
        {
            try
            {
                Global.driver.FindElement(By.Id(pAction)).Clear();
            }
            catch
            {
                try
                {
                    Global.driver.FindElement(By.ClassName(pAction)).Clear();
                }
                catch
                {
                    try
                    {
                        Global.driver.FindElement(By.CssSelector(pAction)).Clear();
                    }
                    catch
                    {
                        try
                        {
                            Global.driver.FindElement(By.XPath(pAction)).Clear();
                        }
                        catch
                        {
                            Global.driver.FindElement(By.TagName(pAction)).Clear();
                        }
                    }
                }
            }
        }
        public void SendKeys(By elemento, string pCnteudo)
        {
            Global.driver.FindElement(elemento).SendKeys(pCnteudo);
        }

        public void SendKeys(string pAction,string pCnteudo)
        {
            try
            {
                Global.driver.FindElement(By.Id(pAction)).SendKeys(pCnteudo);
            }
            catch
            {
                try
                {
                    Global.driver.FindElement(By.ClassName(pAction)).SendKeys(pCnteudo);
                }
                catch
                {
                    try
                    {
                        Global.driver.FindElement(By.CssSelector(pAction)).SendKeys(pCnteudo);
                    }
                    catch
                    {
                        try
                        {
                            Global.driver.FindElement(By.XPath(pAction)).SendKeys(pCnteudo);
                        }
                        catch
                        {
                            Global.driver.FindElement(By.TagName(pAction)).SendKeys(pCnteudo);
                        }
                    }
                }
            }
        }

        public void switchFrame(string frame)
        {
            Global.driver.SwitchTo().ParentFrame();
            Global.driver.SwitchTo().Frame(frame);
        }

        public void ReturndefaultContentFrame()
        {
            Global.driver.SwitchTo().DefaultContent();
        }

        public void touchActionsClick(By elemento)
        {
            Global.touchActions = new Actions(Global.driver);
            Global.touchActions.Click(Global.driver.FindElement(elemento));
            Global.touchActions.Build();
            Global.touchActions.Perform();
        }

        public void clickJS(By elemento)
        {
            Global.driver.ExecuteJavaScript("arguments[0].click();", Global.driver.FindElement(elemento));
        }

        public void Click(By elemento)
        {
            Global.driver.FindElement(elemento).Click();
        }

        public void Click(string pAction)
        {
            try
            {
                Global.driver.FindElement(By.Id(pAction)).Click();
            }
            catch
            {
                try
                {
                    Global.driver.FindElement(By.ClassName(pAction)).Click();
                }
                catch
                {
                    try
                    {
                        Global.driver.FindElement(By.CssSelector(pAction)).Click();
                    }
                    catch
                    {
                        try
                        {
                            Global.driver.FindElement(By.XPath(pAction)).Click();
                        }
                        catch
                        {
                            Global.driver.FindElement(By.TagName(pAction)).Click();
                        }
                    }
                }
            }
        }
        //public void wait(string pAction)
        //{
        //    By elemento = GetByEment(pAction);
        //    WebDriverWait wait = new WebDriverWait(Global.driver, TimeSpan.FromSeconds(60));
        //    wait.Until(ExpectedConditions.ElementToBeClickable(elemento));
        //}


        public void wait(By elemento)
        {

            WebDriverWait wait = new WebDriverWait(Global.driver, TimeSpan.FromSeconds(60));           
            wait.Until(ExpectedConditions.ElementToBeClickable(elemento));           

        }

        public void waitText(By elemento, string texto)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(Global.driver, TimeSpan.FromSeconds(60));
                wait.Until(d => d.FindElement(elemento).Text.Contains(texto));
            }
            catch
            {
                assertAreEqual(texto, elemento);
            }
            
        }

        public void ElementIsClickable(By elemento)
        {
            var wait = new WebDriverWait(Global.driver, TimeSpan.FromMinutes(1));
            wait.Until(ExpectedConditions.ElementToBeClickable(elemento));
        }

        public bool permitePreenchimento(By elemento)
        {
            return Global.driver.FindElement(elemento).Enabled;
        }

        public bool somenteLeitura(By elemento)
        {
            var tere = Global.driver.FindElement(elemento).GetAttribute("readonly");
            if (Global.driver.FindElement(elemento).GetAttribute("readonly") == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string getElementText(By elemento)
        {
            return Global.driver.FindElement(elemento).Text.ToString();
        }

        public void invisibilityOfElementLocated(By elemento)
        {
            WebDriverWait wait3 = new WebDriverWait(Global.driver, TimeSpan.FromMinutes(1));
            wait3.Until(ExpectedConditions.InvisibilityOfElementLocated(elemento));
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                Global.driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void assertAreEqual(string texto, By elemento)
        {
            Assert.AreEqual(texto, Global.driver.FindElement(elemento).Text.ToString());
        }

        public void assertAreEqualTitle(string texto)
        {
            Assert.AreEqual(texto, Global.driver.Title.ToString());
        }

        public void assertIsTrue(By elemento)
        {
            Assert.IsTrue(IsElementPresent(elemento));
        }


        public bool isElementNotPresent(By by)
        {
           return !IsElementPresent(by);
        }

        public bool verify(string elementName)
        {
            try
            {
                Global.driver = null;
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool IsAlertPresent()
        {
            try
            {
                Global.driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        public string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = Global.driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }

}
}
