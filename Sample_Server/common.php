<?php

function call_curl_post($url,$data_array){
    $data = '';
    foreach($data_array as $key=>$value) { $data .= $key.'='.$value.'&'; }
    rtrim($data,'&');

    $ch = curl_init($url);
    curl_setopt($ch,CURLOPT_RETURNTRANSFER,true);
    curl_setopt($ch,CURLOPT_SSL_VERIFYPEER,false);
    curl_setopt($ch,CURLOPT_POST,true);
    curl_setopt($ch,CURLOPT_POSTFIELDS,$data);
    $info = curl_exec($ch);    
    curl_close($ch);    
    return json_decode($info, true);
}

function call_curl_get($url,$data_array){
    $data = '';
    if ($data_array != null) {
        foreach($data_array as $key=>$value) { $data .= $key.'='.$value.'&'; }
        rtrim($data,'&');        
    }    

    $ch = curl_init($url);
    curl_setopt($ch,CURLOPT_RETURNTRANSFER,true);
    curl_setopt($ch,CURLOPT_SSL_VERIFYPEER,false);
    $info = curl_exec($ch);    
    curl_close($ch);    
    return json_decode($info, true);
}
?>