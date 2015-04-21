### 1. Connection Diagram
![Mô hình kết nối](https://github.com/appota/ios-game-sdk/raw/master/docs/user_flow.png)
### 2. API user/info
* URL: https://api.appota.com/game/get_user_info?access_token=USER_ACCESS_TOKEN
* Protocol: GET
* Response Format: JSON
    * status: Boolean (true: query successful, false: query failed)
    * error_code: Integer (query error code)
        * error_code = 0: Successful.
        * error_code = 1: Some parameters are invalid.
        * error_code = 99: System is not available at the moment.
    * data: Object (returned data detail)
        * data.username: String (Username on system)
        * data.user_id: Integer (User's ID on system)
        * data.email: String (User's email on system)
        * data.phone: String (User's phone number on system)