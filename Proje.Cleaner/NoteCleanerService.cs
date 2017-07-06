using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Proje.Cleaner
{
    public partial class NoteCleanerService : ServiceBase
    {
        public NoteCleanerService()
        {
            InitializeComponent();
        }

        // 2dk olarak ayarlndı.
        //Timer tmr = new Timer(1000 * 120);
        Timer tmr = new Timer(2000);

        protected override void OnStart(string[] args)
        {
            WriteToFile("servis başlatıldı");
            tmr.Elapsed += tmr_Elapsed;
            tmr.Start();
        }

        private void tmr_Elapsed(object sender, ElapsedEventArgs e)
        {
            WriteToFile("tmr_Elapsed begin.");
            try
            {
                //url isteği oluştur
                WebRequest request = WebRequest.Create(
                  "http://localhost:14787/Cleaner/CleanNotes");
               
                request.Credentials = CredentialCache.DefaultCredentials;
                //yanıt al
                WebResponse response = request.GetResponse();

               //durum
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

               
                Stream dataStream = response.GetResponseStream();

                //erişim için StreamReade
                StreamReader reader = new StreamReader(dataStream);
                
                string responseFromServer = reader.ReadToEnd();

                WriteToFile("Response : " + responseFromServer);

                //kapat
                reader.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                WriteToFile(ex.Message);
            }

            WriteToFile("tmr_Elapsed end.");
        }

        //belitirlen yola yaz
        private void WriteToFile(string text)
        {
            string path = "C:\\aa\\ServiceLog.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(string.Format(text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                writer.Close();
            }
        }

        protected override void OnStop()
        {
            WriteToFile("NoteCleanerService OnStop.");
        }
    }
}
