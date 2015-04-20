![Payment Diagram](https://github.com/appota/ios-game-sdk/raw/master/md/payment_flow_active.png)

### 1. Payment Flow
* In this option, when application received payment result from SDK, application must confirm the transaction with Developer Server.
* When Developer Server received confirm request, it calls to Appota to confirm and return the result to application.

### 2. Describe API payment/confirm
* URL: https://pay.appota.com/payment/confirm?api_key=API_KEY&lang=LANG
* Method: HTTP/POST
* Parameters:
    * transaction_id: String (The transaction code needs to be verified)
* Response Data: JSON
    * `status`: Boolean (Transaction status: `true` - successful, `false` -  failed or not exist)
    * `data`: Object (Transaction detail)
        * `data`.`transaction_id`: String (Transaction code)
        * `data`.`type`: String (Transaction type: `SMS` - SMS transaction, `CARD` - Card transaction, `BANK` - Internet Banking transaction, `PAYPAL` - Paypal transaction, GOOGLE_PLAY - Google Play transaction)
        * `data`.`amount`: Float (Amount of transaction)
        * `data`.`currency`: String (Currency - VND, USD, ...)
        * `data`.`country_code`: String (Country code - VN, US, ...)
        * `data`.`target`: String (Appota User’s identified information)
        * `data`.`state`: String (Value of  string state used in SDK)
        * `data`.`time`: String (Transaction time)
