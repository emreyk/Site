using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Common
{
    //newlenmeden kullan
    public static class App
    {
        //hiçbisey verilmezse burası calısır
        public static IComman Comman = new DefaultCommon();
    }
}
