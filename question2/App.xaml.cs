using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace question2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public event System.Windows.Input.MouseEventHandler ChangeUrl;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

        }

    }
}
