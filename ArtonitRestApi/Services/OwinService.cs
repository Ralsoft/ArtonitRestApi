using Microsoft.Owin.Hosting;
using System;

namespace ArtonitRestApi.Services
{
    public class OwinService
    {
        private IDisposable _webApp;

        public void Start()
        {
            _webApp = WebApp.Start<StartOwin>();
        }

        public void Stop()
        {
            _webApp.Dispose();
        }
    }
}
