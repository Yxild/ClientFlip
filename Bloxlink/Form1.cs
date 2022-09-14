using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace gfnOS
{
    public partial class BLOXFLIP : Form
    {
        void ChromiumInit()
        {
            if (chromiumWebBrowser1.DeviceDpi < 100)
            {
                Cef.EnableHighDPISupport();
            }

            chromiumWebBrowser1.Load("file:///Loader/index.html");
        }

        public BLOXFLIP()
        {
            CefSharp.WinForms.CefSettings settings = new CefSharp.WinForms.CefSettings();
            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\BloxLink";
            CefSharp.Cef.Initialize(settings);

            InitializeComponent();
            ChromiumInit();
        }
    }
}
