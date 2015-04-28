using System.Collections;
using SimpleJSON;

public class AppotaSession {
	
	private string _appotaAccessToken;
	private string _appotaEmail;	
	private string _appotaExpireDate;	
	private string _appotaRefreshToken;	
	private string _appotaUserId;	
	private string _appotaUsername;

	private static AppotaSession _instance;

	public AppotaSession(string appotaSession) {
		var json = JSON.Parse(appotaSession);
		if (json["accessToken"] != null && json["userId"] != null) {
			_appotaAccessToken = json["accessToken"].Value;
			_appotaEmail = json["email"].Value;
			_appotaExpireDate = json["expireDate"].Value;
			_appotaRefreshToken = json["refreshToken"].Value;
			_appotaUserId = json["userId"].Value;
			_appotaUsername = json["username"].Value;
		} 
		else {
			_appotaAccessToken = "";
			_appotaEmail = "";
			_appotaExpireDate = "";
			_appotaRefreshToken = "";
			_appotaUserId = "";
			_appotaUsername = "";
		}
	}

	public AppotaSession(){
		_appotaAccessToken = "";
		_appotaEmail = "";
		_appotaExpireDate = "";
		_appotaRefreshToken = "";
		_appotaUserId = "";
		_appotaUsername = "";
	}
	
	// Singleton AppotaSession
	public static AppotaSession Instance
	{
		get
		{
			if(_instance == null) _instance = new AppotaSession();
			return _instance;
		}
	}

	public void UpdateInstance(AppotaSession instance) {
		_instance = instance;
	}


	public string AccessToken
	{
		get { return _appotaAccessToken; }
		set
		{
			if (_appotaAccessToken != value)
			{
				_appotaAccessToken = value;
			}
		}
	}
	
	public string Email
	{
		get { return _appotaEmail; }
		set
		{
			if (_appotaEmail != value)
			{
				_appotaEmail = value;
			}
		}
	}
	
	public string ExpireDate
	{
		get { return _appotaExpireDate; }
		set
		{
			if (_appotaExpireDate != value)
			{
				_appotaExpireDate = value;
			}
		}
	}
	
	public string RefreshToken
	{
		get { return _appotaRefreshToken; }
		set
		{
			if (_appotaRefreshToken != value)
			{
				_appotaRefreshToken = value;
			}
		}
	}
	
	public string UserID
	{
		get { return _appotaUserId; }
		set
		{
			if (_appotaUserId != value)
			{
				_appotaUserId = value;
			}
		}
	}
	
	public string UserName
	{
		get { return _appotaUsername; }
		set
		{
			if (_appotaUsername != value)
			{
				_appotaUsername = value;
			}
		}
	}
}
