using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobocoinEmbedded
{
    class ConfigSettings
    {
        private static ConfigSettings _instance = null;
        private string _apiKey;
        private string _apiSecret;
        private string _webHost;
        private string _apiHost;
        private string _machineId;

        public static ConfigSettings getInstance()
        {
            if (_instance == null)
            {
                _instance = new ConfigSettings();
            }
            return _instance;
        }

        private ConfigSettings()
        {
        }

        public string ApiKey
        {
            get { return _apiKey; }
            set { _apiKey = value; }
        }

        public string ApiSecret
        {
            get { return _apiSecret;  }
            set { _apiSecret = value; }
        }

        public string WebHost
        {
            get { return _webHost; }
            set { _webHost = value; }
        }

        public string ApiHost
        {
            get { return _apiHost; }
            set { _apiHost = value; }
        }

        public string MachineId
        {
            get { return _machineId; }
            set { _machineId = value; }
        }
    }
}
