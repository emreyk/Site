using Proje.Entities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proje.Business.Results
{
    public class BusinessLayerResult<T> where T:class
    {
        public List<ErrorMessageObj> Errors { get; set; } //hatalar
        public T Result { get; set; } //sonuc

        public BusinessLayerResult()
        {
            Errors =new  List<ErrorMessageObj>(); //hata gelmezse liste olusmaz.Bu yüzden herzman liste olusturduk
        }


        //hataların kdlarını ve hata mesajını alabilegimiz bir metod
        public void AddError(ErrorMessageCode code,string message)
        {
            Errors.Add(new ErrorMessageObj { Code=code,Message=message});
        }
    }
}
