using ArtonitRestApi.Services;
using System;
using System.IO;
using System.ServiceProcess;

namespace ArtonitRestApi
{
    public partial class Service1 : ServiceBase
    {
        private OwinService owin;

        public static string MainPath = "C:\\ArtonitRestApi";


        public Service1()
        {
            InitializeComponent();

            try
            {
                if (!Directory.Exists($@"{MainPath}\log"))
                    Directory.CreateDirectory($@"{MainPath}\log");

                owin = new OwinService();
            }
            catch (Exception ex)
            {
                LoggerService.Log<Service1>("Exception", ex.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                owin.Start();
            }
            catch (Exception ex)
            {
                LoggerService.Log<Service1>("Exception", ex.Message);
            }
        }

        protected override void OnStop()
        {
            try
            {
                owin.Stop();
            }
            catch (Exception ex)
            {
                LoggerService.Log<Service1>("Exception", ex.Message);
            }
            
        }
    }
}
