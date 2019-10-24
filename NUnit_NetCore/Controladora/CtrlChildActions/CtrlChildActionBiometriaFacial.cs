using OpenQA.Selenium;
using Starline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FluxoVendaCartoes
{
    class CtrlChildActionBiometriaFacial : CtrlActions
    {
        public CtrlChildActionBiometriaFacial()
        {
        }

        public void carregaImagemFacial()
        {
            ClassStarLine classStarLine = new ClassStarLine();
            string biometriadigital = "";
            if (Global.OSLinux)
            {
                biometriadigital = classStarLine.GetImageBase64(Global.PathDoProjeto + "/Foto/testando.jpg");
            }
            else
            {
                biometriadigital = classStarLine.GetImageBase64(@Global.PathDoProjeto + "\\Foto\\testando.jpg");
            }
            Thread.Sleep(2000);
            ((IJavaScriptExecutor)Global.driver).ExecuteScript("acessoFlash.faceDetect('data:image/jpeg;base64," + biometriadigital + "')");
            Thread.Sleep(2000);
        }
    }
}
