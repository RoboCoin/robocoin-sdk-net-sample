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
using Robocoin.Events;
using Robocoin.Models;
using Robocoin.Types;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Threading;


namespace RobocoinEmbedded
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Collection<KioskCashCassette> _kioskCashCassettes;
        private int _buyLimit;
        private int _totalInserted;

        public MainWindow()
        {
            InitializeComponent();
            robocoin.OnAppRunning += onAppRunning;
            robocoin.OnBuySuccess += onBuySuccess;
            robocoin.OnGotAuthenticatedUser += onGotAuthUser;
            robocoin.OnGotBuyLimit += onGotBuyLimit;
            robocoin.OnGotInventory += onGotInventory;
            robocoin.OnGotKioskInfo += onGotKioskInfo;
            robocoin.OnGotOperator += onGotOperator;
            robocoin.OnPageChange += onPageChange;
            robocoin.OnSellSuccess += onSellSuccess;
            robocoin.OnSendSuccess += onSendSuccess;
            robocoin.OnSecretButtonTapped += onSecretButtonTapped;
        }

        #region Events
        /// <summary>
        /// The SDK has fully loaded all the elements and the interface is ready for user interaction.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onAppRunning(object sender, EventArgs e)
        {
            WriteToConsole("onAppRunning");
        }

        /// <summary>
        /// The information of a completed purchase of bitcoin.
        /// Software action: Update machine inventory by calling PostInventory()
        /// Hardware action: Print a proof of purchase receipt that includes the transaction ID, the amount of fiat, and the amount of bitcoin.
        /// Hardware action: Disable the bill validator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onBuySuccess(object sender, BuyEventArgs e)
        {
            _totalInserted = 0;
            WriteToConsole("onBuySuccess: " + "Transaction: " + e.TransactionId + " Bitcoin: " + e.BitcoinAmount);
            WriteToConsole("Hardware Action: Validator -> Disable");
            WriteToConsole("Hardware Action: Printer -> Proof of Purchase receipt");
        }

        /// <summary>
        /// The information of a successfully logged in user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onGotAuthUser(object sender, UserEventArgs e)
        {
            WriteToConsole("onGotAuthUser: " + e.Nickname);
        }

        /// <summary>
        /// The total amount of cash the user is permitted to deposit for the given time period.
        /// Hardware action: Enable the bill validator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onGotBuyLimit(object sender, BuyLimitEventArgs e)
        {            
            _buyLimit = e.BuyLimitAmount;
            WriteToConsole("onGotBuyLimit: " + e.BuyLimitAmount);
            WriteToConsole("Hardware Action: Validator -> Enable");
        }

        /// <summary>
        /// The current inventory of the operator wallet and the cash inventory of this machine.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onGotInventory(object sender, InventoryEventArgs e)
        {
            _kioskCashCassettes = e.KioskCashCassettes;
            WriteToConsole("onGotInventory: Buy -> " + e.BuyAvailableAmount + " Sell -> " + e.SellAvailableAmount);
        }

        /// <summary>
        /// The current information and settings of this machine.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onGotKioskInfo(object sender, KioskInfoEventArgs e)
        {
            WriteToConsole("onGotKioskInfo: " + e.Kiosk.KioskId);
        }

        /// <summary>
        /// The current information of the operator this machines trades under.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onGotOperator(object sender, OperatorEventArgs e)
        {
            WriteToConsole("onGotOperator: " + e.Name);
        }

        /// <summary>
        /// The current page the user has navigated to.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onPageChange(object sender, ScreenEventArgs e)
        {
            WriteToConsole("onPageChange: " + e.Screen.ToString());

            switch (e.Screen)
            {                
                case Screen.Buy:                    
                    // The deposit money screen
                    break;
                case Screen.CreatePin:
                    // The create PIN screen in enrollment
                    break;
                case Screen.EnterPin:
                    // The PIN screen in sign-in
                    break;
                case Screen.EnterVerification:
                    // The SMS verification screen in enrollment
                    break;
                case Screen.FacePicture:
                    // The face picture taking screen in enrollment
                    break;
                case Screen.Index:
                    // The main screen (either signed-out or signed-in)
                    // Software action: Update machine inventory by calling PostInventory()
                    break;
                case Screen.Receive:
                    // The QR code screen to load bitcoin into their wallet
                    break;
                case Screen.ScanBitcoinAddress:
                    // The withdraw bitcoin screen that enables the webcam to scan for a bitcoin QR code
                    break;
                case Screen.ScanBitcoinAddressAmount:
                    // The withdraw bitcoin amount screen
                    break;
                case Screen.ScanId:
                    // The ID picture taking screen in enrollment
                    break;
                case Screen.Sell:
                    // The withdraw money screen
                    break;
                case Screen.SignIn:
                    // The phone number sign-in screen
                    break;
                case Screen.SignInVerify:
                    // The 2-factor authentication screen in sign-in
                    break;
                case Screen.SignOut:
                    // The user signs out
                    break;
            }
        }

        /// <summary>
        /// The information of a completed sale of bitcoin.
        /// Software action: Update machine inventory by calling PostInventory()
        /// Hardware action: Dispense the given amount of cash.
        /// Hardware action: Print a proof of sale receipt that includes the transaction ID, the amount of fiat, and the amount of bitcoin.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onSellSuccess(object sender, SellEventArgs e)
        {
            WriteToConsole("onSellSuccess: " + "Transaction: " + e.TransactionId + " Fiat: " + e.FiatAmount + " Bitcoin: " + e.BitcoinAmount);
            WriteToConsole("Hardware Action: Dispenser -> Dispense " + e.FiatAmount);
            WriteToConsole("Hardware Action: Printer -> Proof of Sale receipt");
        }

        /// <summary>
        /// Bitcoin has been sent from a user's primary wallet account.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void onSendSuccess(object sender, SendEventArgs e)
        {
            WriteToConsole("onSendSuccess: " + "Transaction: " + e.TransactionId + " Bitcoin: " + e.BitcoinAmount);
        }

        /// <summary>
        /// The Robocoin logo has been pressed from the home screen.  It can be set to expose administrative functionality after a couple mouse clicks (if necessary).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onSecretButtonTapped(object sender, EventArgs e)
        {
            WriteToConsole("onSecretButtonTapped");
        }
        #endregion

        #region Commands
        private void goButton_Click(object sender, EventArgs e)
        {
            ConfigSettings configSettings = ConfigSettings.getInstance();
            // Loads the robocoin SDK 
            WriteToConsole("Load(...)");
            robocoin.Load(new Uri(configSettings.WebHost), configSettings.ApiKey, configSettings.ApiSecret, configSettings.ApiHost, configSettings.MachineId);
        }

        /// <summary>
        /// Hardware action: The validator should reject notes if the total amount deposited exceeds the buy limit captures in onGotBuyLimit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void insertMoneyMenuItem_Click(object sender, EventArgs e)
        {
            int denominationInserted = 1;
            int newValue = _totalInserted + denominationInserted;

            if (newValue <= _buyLimit)
            {
                _totalInserted += denominationInserted;
                // Increments the amount deposited on the screen
                robocoin.OnBillInserted(denominationInserted);
            }

            else
            {
                WriteToConsole("Hardware Action: Validator -> Reject");
            }
        }

        private void getKioskInfo_Click(object sender, EventArgs e)
        {
            // Fetches kiosk information
            WriteToConsole("GetKioskInfo()");
            robocoin.GetKioskInfo();
        }

        private void postLog_Click(object sender, EventArgs e)
        {
            // Sends a log to Robocoin
            WriteToConsole("PostLog(Log)");
            robocoin.PostLog(new Log(Level.Info, "Send test log to Robocoin"));
        }

        private void getInventory_Click(object sender, EventArgs e)
        {
            // Fetches machine inventory information
            WriteToConsole("GetInventory()");
            robocoin.GetInventory();
        }

        private void postInventory_Click(object sender, EventArgs e)
        {
            // Example of performing an action to empty the bill validator
            foreach (KioskCashCassette kioskCashCassette in _kioskCashCassettes)
            {
                if (kioskCashCassette.KioskCashCassetteType == Cassette.CashIn)
                {
                    kioskCashCassette.CurrentNotes = 0;
                    kioskCashCassette.CurrentAmount = 0;
                }
            }

            WriteToConsole("PostInventory(KioskCashCassette)");
            robocoin.PostInventory(_kioskCashCassettes);
        }

        private void getAuthedUser_Click(object sender, EventArgs e)
        {
            // Manually fetches the authenticated user (if one is logged in)
            WriteToConsole("GetAuthenticatedUser()");
            robocoin.GetAuthenticatedUser();
        }

        private void getOperator_Click(object sender, EventArgs e)
        {
            // Fetches operator information
            WriteToConsole("GetOperator()");
            robocoin.GetOperator();
        }
        #endregion

        #region Misc UI
        private void refreshButton_Click(object sender, EventArgs e)
        {
            robocoin.Refresh();
        }

        private void configButton_Click(object sender, EventArgs e)
        {
            new Config().Show();            
        }

        private void WriteToConsole(String text)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(() =>
            {
                txtConsole.AppendText(text + "\n");
                txtConsole.ScrollToEnd();
            }));
        }
        #endregion
    }
}
