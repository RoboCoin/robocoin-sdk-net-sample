using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RobocoinEmbedded
{
    /// <summary>
    /// Interaction logic for Config.xaml
    /// </summary>
    public partial class Config : Window
    {
        public Config()        
        {
            InitializeComponent();
            ConfigSettings configSettings = ConfigSettings.getInstance();
            ApiHost.Text = configSettings.ApiHost;
            ApiKeyText.Text = configSettings.ApiKey;
            ApiSecretText.Text = configSettings.ApiSecret;
            MachineId.Text = configSettings.MachineId;
            uri.Text = configSettings.WebHost;
        }

        public void Window_Closing(object sender, EventArgs e)
        {
            ConfigSettings configSettings = ConfigSettings.getInstance();
            configSettings.ApiHost = ApiHost.Text;
            configSettings.ApiKey = ApiKeyText.Text;
            configSettings.ApiSecret = ApiSecretText.Text;
            configSettings.MachineId = MachineId.Text;
            configSettings.WebHost = uri.Text;           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
