using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Entities.Messages
{
    public class ErrorMessageObj
    {
        //mesaj kodlarını ve mesajı tutan fieldlar
        public ErrorMessageCode Code { get; set; }
        public string Message { get; set; }
    }
}
