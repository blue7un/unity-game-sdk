<?php
header('Content-Type: application/json');

define('CLIENT_KEY', '0ea800d77944a1873500c325dd62e47005593a7f5');
define('API_KEY', 'K-A175238-U00000-VEANCR-EA9C9F2DC28EE857');
define('CLIENT_SECRET', 'acc76ac2a855ebb050b8cb23d74a9e0205593a7f5');

include 'common.php';

function appota_payment($fields) {
        $status             = $fields['status'];
        $sandbox            = $fields['sandbox'];
        $trans_id           = $fields['transaction_id'];
        $trans_type         = $fields['transaction_type'];
        $amount             = $fields['amount'];
        $currency           = $fields['currency'];
        $revenue            = $fields['revenue'];
        $state              = $fields['state'];
        $target             = $fields['target'];
        $country_code       = $fields['country_code'];
        $hash               = $fields['hash'];
        $result = array();
        // check transaction status
        if ( $status != 1) {
            // Payment fail do not return anything
            return false;
        }
        
        switch($trans_type){
            case 'CARD':
                $card_code          = $fields['card_code'];
                $card_serial        = $fields['card_serial'];
                $card_vendor        = $fields['card_vendor'];
                $check_hash = md5( $amount . $card_code . $card_serial . $card_vendor . $country_code .
                                $currency . $revenue. $sandbox . $state . $status . $target . $trans_id.
                                $trans_type . CLIENT_SECRET );
                break;
            case 'SMS':
                $phone              = $fields['phone'];
                $message            = $fields['message'];
                $code               = $fields['code'];     
                $check_hash = md5( $amount . $code . $country_code .
                                $currency . $revenue. $message . $phone . $sandbox . $state . $status . $target . $trans_id.
                                $trans_type . CLIENT_SECRET );
                break;
            case 'APPLE_ITUNES':
                $productid              = $fields['productid'];    
                $check_hash = md5( $amount . $country_code .
                                $currency . $revenue. $productid . $sandbox . $state . $status . $target . $trans_id.
                                $trans_type . CLIENT_SECRET );
                break;
            case 'GOOGLE_PLAY':
                $productid              = $fields['productid'];    
                $check_hash = md5( $amount . $country_code .
                                $currency . $revenue. $productid . $sandbox . $state . $status . $target . $trans_id.
                                $trans_type . CLIENT_SECRET );
                
            default :
                $check_hash = md5( $amount . $country_code .
                                $currency . $revenue. $sandbox . $state . $status . $target . $trans_id.
                                $trans_type . CLIENT_SECRET );
                break;
        }
        // check hash                    
        if ( $check_hash != $hash ){
            // Check hash fail
            return false;
        }
                        
        // If hash is ok, verify transaciton
        if (!verify_appota_transaction($trans_id, $amount, $state, $target)){
            // Verify fail
            return false;
        }         
        // Base on state and other information increase resource for user
        increase_resource_user($trans_id, $trans_type, $amount, $target, $state);
        http_response_code(200);
        return true;
}

// Verify transaction_id, amount, state, target with Appota Confirm API
function verify_appota_transaction($transaction_id, $amount, $state, $target) {
    $url = sprintf('https://pay.appota.com/payment/confirm?api_key=%s&lang=en', API_KEY);
    $fields = array('transaction_id' => $transaction_id);
    $result = call_curl_post($url, $fields);

    if ($result['status'] == 1 and $result['data']['amount'] == $amount and $result['data']['state'] == $state and $result['data']['target']) {
        return true;
    }
    return false;
}

function increase_resource_user($transaction_id, $transaction_type, $amount, $target, $state){
}

if (isset($_POST["transaction_id"])){
    appota_payment($_POST);
}

?>
