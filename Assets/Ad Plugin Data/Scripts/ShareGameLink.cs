using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;

public class ShareGameLink : MonoBehaviour
{
	private string subject, ShareMessage;

	void Awake ()
	{
		subject = "https://play.google.com/store/apps/details?id=" + Application.identifier;
		ShareMessage = "https://play.google.com/store/apps/details?id=" + Application.identifier;
	}

	public void Share ()
	{
		#if UNITY_ANDROID



		// Create Refernece of AndroidJavaClass class for intent
		AndroidJavaClass intentClass = new AndroidJavaClass ("android.content.Intent");
		// Create Refernece of AndroidJavaObject class intent
		AndroidJavaObject intentObject = new AndroidJavaObject ("android.content.Intent");

		// Set action for intent
		intentObject.Call<AndroidJavaObject> ("setAction", intentClass.GetStatic<string> ("ACTION_SEND"));

		intentObject.Call<AndroidJavaObject> ("setType", "text/plain");

		//Set Subject of action
		intentObject.Call<AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_SUBJECT"), subject);
		//Set title of action or intent
		intentObject.Call <AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_TITLE"), subject);
		// Set actual data which you want to share
		intentObject.Call <AndroidJavaObject> ("putExtra", intentClass.GetStatic<string> ("EXTRA_TEXT"), ShareMessage);

		AndroidJavaClass unity = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject> ("currentActivity");
		// Invoke android activity for passing intent to share data
		currentActivity.Call ("startActivity", intentObject);

		#endif
	}


	public void RateUs ()
	{

		Application.OpenURL ("https://play.google.com/store/apps/details?id=" + Application.identifier);
	}

	public void More ()
	{

	//	Application.OpenURL (AdsController.Instance.IdsContainer.MoreLink);
	}


	public void CustomLink (string link)
	{
		Application.OpenURL ("https://play.google.com/store/apps/details?id=" + link);

	}


}