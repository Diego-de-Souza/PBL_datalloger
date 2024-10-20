using Microsoft.AspNetCore.Http;
using System;

namespace AtmoTrack_web_page.Controllers
{
    public static class HelperController
    {
        public static Boolean VerificaUserLogado(ISession session)
        {
            string logado = session.GetString("Logado");
            if (logado == null)
                return false;
            else
                return true;
        }
    }
}
