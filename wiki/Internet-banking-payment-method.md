### 1. Protocol: HTTP/POST
### 2. Parameters
* `status`: Integer (Transaction status: 1 - successful, 0 - failed)
* `sandbox`: Integer (Sandbox/test transaction: 1 - Sandbox environment, transaction has been processed by Appota to help developer check connection, 0 - Transactions are made by users – Appota accept transaction)
* `transaction_id`: String (Transaction ID on Appota system, Developer can use this ID to verify transaction)
* `transaction_type`: `BANK` (Transaction type) 
* `amount`: Integer (The amount of money to charge)
* `currency`: String (Currency - VND)
* `state`: String (Value of `state` field in function `AppotaGameSDK`.`getInstance()`.`init()`)
* `target`: String (Appota User’s identified information)
* `country_code`: String (Country code - VN)
* `hash`: String (Secured hash code to confirm IPN is called by Appota system)


Notes: 
* hash = `md5`(`amount` + `country_code` + `currency` + `sandbox` + `state` + `status` + `target` + `transaction_id` + `transaction_type` + `client_secret`)
(String connecting parameter values arranged in order from a->z).
* `client_secret`: Value of Client Secret has been published when register application.