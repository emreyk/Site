using Proje.Bitirme.Inıt;
using Proje.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Proje.Bitirme
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //artık webCommanla calısır.Buraya erişlerek o anki kullanıcı adını alabiliriz
            App.Comman = new WebCommon();
        }
    }
}
