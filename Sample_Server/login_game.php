<?php
header('Content-Type: application/json');

include 'common.php';

// Verify user with Appota User API
function verify_appota_user($appota_access_token, $appota_userid, $appota_username) {
    $url = sprintf('https://api.appota.com/game/get_user_info?access_token=%s', $appota_access_token);
    $data = call_curl_get($url, null);
    if(!$data['status'])
        return false;
    else {
        if($data["data"]["username"] == $appota_username & $data["data"]["user_id"] == $appota_userid)
            return true;
        else
            return false;
    }    
}

?>