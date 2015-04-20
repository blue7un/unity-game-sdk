### 1. Tại sao phải xác nhận giao dịch?
* Các giao dịch khi Appota gọi sang hệ thống của Nhà phát triển (qua IPN URL) để thông báo đã được bảo mật 1 phần nhờ việc tính và so sánh giá trị `hash`. 
* Trong trường hợp ứng dụng bị lộ thông tin về `client_secret` và IPN URL (thông qua dịch ngược mã nguồn hoặc 1 lý do nào đó), việc giả mạo các giao dịch là hoàn toàn có thể xảy ra. 

### 2. Mô tả API payment/confirm
* Đường dẫn: https://pay.appota.com/payment/confirm?api_key=API_KEY&lang=LANG
* Phương thức gửi dữ liệu: HTTP/POST
* Danh sách tham số:
    * transaction_id: String (Mã giao dịch muốn xác nhận)
* Định dạng dữ liệu trả về: JSON
    * `status`: Boolean (Trạng thái giao dịch: `true` - Giao dịch thành công, `false` -  Giao dịch thất bại hoặc không tồn tại)
    * `data`: Object (Thông tin giao dịch)
        * `data`.`transaction_id`: String (Mã giao dịch)
        * `data`.`type`: String (Loại giao dịch: `SMS` - Giao dịch SMS, `CARD` - Giao dịch thẻ cào, `BANK` - Giao dịch Internet Banking, `PAYPAL` - Giao dịch Paypal, GOOGLE_PLAY - Giao dịch qua Google Play)
        * `data`.`amount`: Float (Giá trị giao dịch)
        * `data`.`currency`: String (Đơn vị tiền tệ của giao dịch - VND, USD, ...)
        * `data`.`country_code`: String (Mã quốc gia phát sinh giao dịch - VN, US, ...)
        * `data`.`target`: String (Thông tin định danh người dùng của Appota)
        * `data`.`state`: String (Giá trị trường `state` được sử dụng trong SDK)
        * `data`.`time`: String (Thời gian giao dịch)
