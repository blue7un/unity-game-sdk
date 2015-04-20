### 1. 协议: HTTP/POST
### 2. 参数列表
* `status`: Integer （交易状态: 1 – 成功, 0 – 失败）
* `sandbox`: Integer （sandbox交易 - 测试交易: 1 –sandbox环境Appota 实施交易让开发商验证支付链接是否正常0 -用户实施交易，Appota 记录交易 ）
* `transaction_id`: String （系统上的交易吗，开发商使用以确认交易）
* `transaction_type`: `APPLE_ITUNES` （交易类型）
* `amount`: Float （支付数量）
* `currency`: String （价格单位）
* `productid`: String （Product IDIn App Purchase item的Product IDIn） 
* `state`: String （`AppotaGameSDK`.`getInstance()`.`init()`)函数中的`state`价值）
* `target`: String （任意选择的变量  ）
* `country_code`: String （国家吗）
* `hash`: String（ 哈希吗以确认Appota 发送的通告）


注释：
* hash = `md5`(`amount` + `country_code` + `currency` + `productid` + `sandbox` + `state` + `status` + `target` + `transaction_id` + `transaction_type` + `client_secret`)
（加密后的字符串用于保密目的）
* `client_secret`:创建应用时提供的Client Secret价值
