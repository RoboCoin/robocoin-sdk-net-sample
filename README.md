# robocoin-sdk-net-sample

This sample application demonstrates the necessary implementation details to integrate the Romit SDK into existing machine infrastructure.  It mocks out hardware events that need to take place during different events of the transaction lifecycle.

Using an extended chromium browser, it exposes additional methods and events necessary to successfully complete cash transactions.  More documentation can be found at http://docs.romit.io/#cash-sdk

![Sample SDK Application](https://robocoinkiosk.files.wordpress.com/2015/04/romit.png)

## Usage
1. Go to https://wallet.romit.io/signup  Create an Operator Wallet, generate API keys from it, and create a new Kiosk and record its id number.
2. Click "Config..." and enter the API keys and kiosk id.  For the host url use: "https://app.romit.io/v0" and for the API url use: "https://api.romit.io"
3. Click "Browser -> Go Home".  After a few moments the browser will complete authentication and load the home screen.
