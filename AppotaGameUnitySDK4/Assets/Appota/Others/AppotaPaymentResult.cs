using System.Collections;
using SimpleJSON;

public class AppotaPaymentResult {

	private string _paymentPackageID;
	private string _paymentCurrency;	
	private string _paymentTime;	
	private string _paymentTransactionId;	
	private string _paymentType;	
	private string _paymentProductID;
	private string _paymentMethodINAPP;

	public AppotaPaymentResult(string transactionResult) {
		var json = JSON.Parse(transactionResult);
		if (json["transactionId"] != null) {
			_paymentPackageID = json["packageID"].Value;
			_paymentCurrency = json["currency"].Value;
			_paymentTime = json["time"].Value;
			_paymentTransactionId = json["transactionId"].Value;
			_paymentType = json["type"].Value;
			_paymentProductID = json["productID"].Value;
			_paymentMethodINAPP = json["methodINAPP"].Value;
		} 
		else {
			_paymentPackageID = "";
			_paymentCurrency = "";
			_paymentTime = "";
			_paymentTransactionId = "";
			_paymentType = "";
			_paymentProductID = "";
			_paymentMethodINAPP = "";
		}
	}

	public string PackageID
	{
		get { return _paymentPackageID; }
		set
		{
			if (_paymentPackageID != value)
			{
				_paymentPackageID = value;
			}
		}
	}
	
	public string Currency
	{
		get { return _paymentCurrency; }
		set
		{
			if (_paymentCurrency != value)
			{
				_paymentCurrency = value;
			}
		}
	}
	
	public string Time
	{
		get { return _paymentTime; }
		set
		{
			if (_paymentTime != value)
			{
				_paymentTime = value;
			}
		}
	}
	
	public string TransactionID
	{
		get { return _paymentTransactionId; }
		set
		{
			if (_paymentTransactionId != value)
			{
				_paymentTransactionId = value;
			}
		}
	}
	
	public string Type
	{
		get { return _paymentType; }
		set
		{
			if (_paymentType != value)
			{
				_paymentType = value;
			}
		}
	}
	
	public string ProductID
	{
		get { return _paymentProductID; }
		set
		{
			if (_paymentProductID != value)
			{
				_paymentProductID = value;
			}
		}
	}
	
	public string MethodINAPP
	{
		get { return _paymentMethodINAPP; }
		set
		{
			if (_paymentMethodINAPP != value)
			{
				_paymentMethodINAPP = value;
			}
		}
	}
}
