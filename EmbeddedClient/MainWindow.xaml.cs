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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Net;

using RobocoinEmbedded.Model;

namespace RobocoinEmbedded
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Decimal _buyLimit;
        private Decimal _totalInserted;

        public MainWindow()
        {
            InitializeComponent();
            robocoinBrowser.OnSellSuccess += onSellSuccess;
            robocoinBrowser.OnBuySuccess += onBuySuccess;
            robocoinBrowser.OnGotBuyLimit += onGotBuyLimit;
            robocoinBrowser.OnAppRunning += onAppRunning;
            robocoinBrowser.OnPageChange += onPageChange;
            robocoinBrowser.OnGotKioskInfo += onGotKioskInfo;
            robocoinBrowser.OnGotAuthUser += onGotAuthUser;
            robocoinBrowser.OnGotOperator += onGotOperator;
            robocoinBrowser.OnGotInventory += onGotInventory;
            robocoinBrowser.OnSecretButtonTapped += onSecretButtonTapped;
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            ConfigSettings configSettings = ConfigSettings.getInstance();
            robocoinBrowser.Load(configSettings.WebHost, configSettings.ApiKey, configSettings.ApiSecret, configSettings.ApiHost, configSettings.MachineId);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            robocoinBrowser.Refresh();
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            Config config = new Config();
            config.Show();
        }

        private void insertMoneyMenuItem_Click(object sender, EventArgs e)
        {
            Decimal denominationInserted = 5.00M;
            Decimal newValue = _totalInserted + denominationInserted;

            if (newValue <= _buyLimit)
            {
                _totalInserted += denominationInserted;
                robocoinBrowser.OnBillInserted(denominationInserted);
            }
            else
            {
                MessageBox.Show("Note rejected");
            }
        }

        private void getKioskInfo_Click(object sender, EventArgs e)
        {
            robocoinBrowser.GetKioskInfo();
        }

        private void postLog_Click(object sender, EventArgs e)
        {
            Log log = new Log();
            log.LogLevel = Log.Level.INFO;
            log.Message = "This is a test message";
            robocoinBrowser.PostLog(log);
        }

        private void getInventory_Click(object sender, EventArgs e)
        {
            robocoinBrowser.GetInventory();
        }

        private void postInventory_Click(object sender, EventArgs e)
        {
            List<KioskCashCassette> kioskCashCasettes = new List<KioskCashCassette>();

            KioskCashCassette kioskCashCassette = new KioskCashCassette();
            kioskCashCassette.Capacity = 2200;
            kioskCashCassette.KioskCashCassetteId = "02c27c61-d9a9-41e5-b6d6-f38b6f61c9e3";
            kioskCashCassette.Denomination = 20;
            kioskCashCassette.CurrentNotes = 50;
            kioskCashCassette.CurrentAmount = 1000;
            kioskCashCassette.Position = 0;
            kioskCashCassette.KioskCashDeviceId = "346b8f0f-dbdc-42b7-ab78-c6f27f0e87b0";
            kioskCashCassette.KioskCashCassetteType = KioskCashCassette.Types.CASH_OUT;

            kioskCashCasettes.Add(kioskCashCassette);

            robocoinBrowser.PostInventory(kioskCashCasettes);
        }

        private void getAuthedUser_Click(object sender, EventArgs e)
        {
            robocoinBrowser.GetAuthUser();
        }

        private void getOperator_Click(object sender, EventArgs e)
        {
            robocoinBrowser.GetOperator();
        }

        private void onSellSuccess(string transactionId, int bitcoinAmount, int fiatAmount)
        {
            MessageBox.Show("Successful sell: transaction " + transactionId + " amount " + bitcoinAmount + " for " + fiatAmount);
        }

        private void onBuySuccess(string transactionId, int bitcoinAmount)
        {
            _totalInserted = 0.00M;
            MessageBox.Show("Successful buy: transaction " + transactionId + " amount " + bitcoinAmount);
        }

        private void onGotBuyLimit(int buyLimit)
        {
            _buyLimit = Convert.ToDecimal(buyLimit);
        }

        private void onSendSuccess(string transactionId, int bitcoinAmount)
        {
            MessageBox.Show("Successful send: transaction " + transactionId + " for " + bitcoinAmount);
        }

        private void onAppRunning()
        {
            MessageBox.Show("App is running");
        }

        private void onPageChange(string page)
        {
            System.Console.WriteLine("page changed to " + page);
        }

        private void onGotKioskInfo(KioskInfo kioskInfo)
        {
            MessageBox.Show(kioskInfo.Kiosk.BankAccountGroupId);
        }

        private void onGotAuthUser(User user)
        {
            MessageBox.Show(user.Nickname);
        }

        private void onGotOperator(Operator theOperator)
        {
            MessageBox.Show(theOperator.Name);
        }

        private void onGotInventory(Inventory inventory)
        {
            MessageBox.Show(inventory.BuyAvailableAmount.ToString());
        }

        private void onSecretButtonTapped()
        {
            MessageBox.Show("Secret button tapped");
        }
    }
}
