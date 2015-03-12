# robocoin-sdk-net-sample

This sample application demonstrates the necessary implementation details to integrate the Robocoin SDK into existing machine infrastructure.  It mocks out hardware events that need to take place during different events of the transaction lifecycle.

Using an extended chromium browser, it exposes additional methods and events necessary to successfully complete cash transactions.  More documentation can be found at http://docs.robocoin.com/#cash-sdk

![Sample SDK Application](https://robocoinkiosk.files.wordpress.com/2015/03/sample.png)

## Usage
1. Go to https://wallet.robocoin.com/signup.  Create an Operator Wallet, generate API keys from it, and create a new Kiosk and record its id number.
2. Click "Config..." and enter the API keys and kiosk id.  For the host url use: "https://embed.robocoin" and for the API url use: "https://api.robocoin.com"
3. Click "Browser -> Go Home".  After a few moments the browser will complete authentication and load the home screen.