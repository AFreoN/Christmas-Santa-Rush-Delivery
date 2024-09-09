using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardSystem : MonoBehaviour
{

	#region Global Refrence

	public static RewardSystem GlobalRefrence;

	public static RewardSystem Instance {
		get {
			if (GlobalRefrence == null)
				GlobalRefrence = GameObject.FindObjectOfType<RewardSystem> ();
	 
			return GlobalRefrence;
		}
	}
	#endregion

	public GameObject UnlockLevel;
	public GameObject RemoveAd;
	public GameObject Players;

	void Awake ()
	{

		if (!PlayerPrefs.HasKey ("DoNotShowAds")) {
		
			RemoveAd.SetActive (true);

		} else {
			RemoveAd.SetActive (false);
		}

		// --------------------------------------------------------------------
		if (!PlayerPrefs.HasKey ("PurchaseLevels")) {

			UnlockLevel.SetActive (true);

		}
		else
		{

			UnlockLevel.SetActive(false);
		}

		// -----------------------------------------------------------------------



		if (!PlayerPrefs.HasKey ("PurchasePlayers")) {


			Players.SetActive (true);

		} else {


			Players.SetActive (false);
		}


	}





	// ############################# IAP CallBacks ##########################################


	public void PurchasedRemoveAds ()
	{
		PlayerPrefs.SetString ("DoNotShowAds", "Unlocked");

        Ads_Manager.Instance.HideSmallAdmobBanner();
		RemoveAd.SetActive (false);
	}


	public void PurchasedUnlockLevels ()
	{
		UnlockLevel.SetActive (false);
		for (int i = 0; i <= 15; i++) {

			PlayerPrefs.SetString ("LevelUnlock" + i, "unlocked");
		}

		PlayerPrefs.SetString ("PurchaseLevels", "Unlocked");
	}




	public void PurchasedUnlockPlayers ()
	{
		PlayerPrefs.SetString (("Vehicle" + 1), "Unlocked");
		Players.SetActive (false);
	}



	public void ResetGame ()
	{

		PlayerPrefs.DeleteAll ();

		if (!PlayerPrefs.HasKey ("DoNotShowAds")) {


			RemoveAd.SetActive (true);

		} else {


			RemoveAd.SetActive (false);
		}

		// --------------------------------------------------------------------


		if (!PlayerPrefs.HasKey ("PurchaseLevels")) {


			UnlockLevel.SetActive (true);

		} else {


			UnlockLevel.SetActive (false);
		}

		// -----------------------------------------------------------------------



		if (!PlayerPrefs.HasKey ("PurchasePlayers")) {


			Players.SetActive (true);

		} else {


			Players.SetActive (false);
		}

	}






}
