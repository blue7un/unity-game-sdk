### 1. Thông báo giao dịch tức thời (Instant Payment Notification - IPN) là gì?
Ngay sau khi người dùng hoàn thành giao dịch với hệ thống, Appota sẽ gửi thông báo giao dịch sang hệ thống của Nhà phát triển. 

### 2. Lựa chọn đường dẫn (IPN URL) để gửi thông báo như thế nào?
* Nếu Nhà phát triển sử dụng biến `notice_url` khi gọi hàm `AppotaGameSDK`.`getInstance()`.`init()` trên Appota Game SDK, giá trị của biến này sẽ được sử dụng để làm đường dẫn để gửi thông báo.
* Nếu Nhà phát triển không sử dụng biến `notice_url` khi gọi hàm `AppotaGameSDK`.`getInstance()`.`init()` trên Appota Game SDK và có khai báo IPN URL mặc định khi đăng ký ứng dụng, giá trị của IPN URL mặc định sẽ được sử dụng để làm đường dẫn để gửi thông báo.
* Nếu Nhà phát triển không sử dụng biến `notice_url` khi gọi hàm `AppotaGameSDK`.`getInstance()`.`init()` trên Appota Game SDK và không khai báo IPN URL mặc định khi đăng ký ứng dụng, Appota sẽ không gửi thông báo khi phát sinh giao dịch.

### 3. Thông báo được gửi như thế nào?
* Tuỳ theo hình thức thanh toán mà người dùng sử dụng, Appota sẽ gửi các thông báo khác nhau đến hệ thống của Nhà phát triển.
* Trong trường hợp khi hệ thống của Nhà phát triển không nhận được thông báo của Appota (mã HTTP code trả về khác 200), Appota sẽ ngay lập tức gửi lại thông báo đó 2 lần và nếu hệ thống của Nhà phát triển vẫn trả về mã HTTP code khác 200, Appota sẽ gọi lại 3 lần tiếp theo sau 5 phút.

#### 3.1. [Hình thức thanh toán SMS](Hình-thức-thanh-toán-SMS)
#### 3.2. [Hình thức thanh toán Thẻ cào](Hình-thức-thanh-toán-Thẻ-cào)
#### 3.3. [Hình thức thanh toán Internet Banking](Hình-thức-thanh-toán-Internet-Banking)
#### 3.4. [Hình thức thanh toán Paypal](Hình-thức-thanh-toán-Paypal)
#### 3.5. [Hình thức thanh toán Apple Payment](Hình-thức-thanh-toán-Apple-Payment)