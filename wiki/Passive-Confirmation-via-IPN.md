![Payment Diagram](md/payment_flow_passive.png)

### 1. What is Instant Payment Notification - IPN?
* This is passive method to confirm a transaction.
* As soon as users finish transaction in system, Appota will send Payment Notification to Developer’s system.

### 2. How to select IPN URL to send notification?
* If developer uses variable notice_url when calling function AppotaGameSDK.getInstance().init()on Appota Game SDK, value of this variable will be used to be link for sending notification.
* If developer does not use variable notice_url on Appota Game SDK and declare default IPN URL when registering application, value of default IPN URL will be used to be link for sending notification.
* If developer does not use variable notice_url when calling function AppotaGameSDK.getInstance().init()on Appota Game SDK and not declare default IPN URL when registering application, Appota will not send notification when generating transaction.

### 3. How notifications send?
* In accordance with each payment method users use, Appota will send different notifications to Developer’s system.
* In case that Developer’s system do not receive notification from Appota (HTTP code not return code 200), Appota will immediately send that notification twice and If developer’s system still do not return HTTP code 200, Appota will call back 3 times each 5 minutes.

#### 3.1. [SMS Payment Method](sms-payment-method)
#### 3.2. [Card Payment Method](card-payment-method)
#### 3.3. [Internet Banking Payment Method](internet-banking-payment-method)
#### 3.4. [Paypal Payment Method](paypal-payment-method)
#### 3.5. [Apple Payment Method](apple-payment-method)

### 4. Verify Transaction (optional)
* Transaction when Appota calls to Developer’s system (via IPN URL) in order to notify that it is partly protected based on calculating and comparing hash value.
* In case that information about client_secret and IPN URL of application is leaked (by translating wrong source code or by any reason), the fake transactions are possible.

##### Describe API payment/confirm
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
