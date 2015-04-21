### 1. Mô hình kết nối
![Mô hình kết nối](https://github.com/appota/ios-game-sdk/raw/master/docs/user_flow.png)
### 2. API user/info
* Đường dẫn: https://api.appota.com/game/get_user_info?access_token=USER_ACCESS_TOKEN
* Giao thức: GET
* Định dạng dữ liệu trả về: JSON
    * status: Boolean (true: Truy vấn thành công, false: Truy vấn thất bại)
    * error_code: Integer (Mã lỗi truy cập)
        * error_code = 0: Truy vấn thành công.
        * error_code = 1: Dữ liệu truy vấn không chính xác.
        * error_code = 99: Hệ thống không thể truy vấn vào thời điểm này.
    * data: Object (Chi tiết dữ liệu trả về)
        * data.username: String (Tên sử dụng của người dùng trên hệ thống)
        * data.user_id: Integer (ID của người dùng trên hệ thống)
        * data.email: String (Email của người dùng trên hệ thống)
        * data.phone: String (Số điện thoại của người dùng trên hệ thống)