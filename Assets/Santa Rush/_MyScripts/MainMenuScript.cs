using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public static MainMenuScript Instance;
    public GameObject ModeLock, ModeQuestion, NotEnoughText, ModeSelection, UnlockLevelsButton, PoPPanal, UnlockCarsButton, RemoveAdButton, OutterSpin, InnerSpin, SpinAnim, ClamPanal, LerpScript, HighRatingPanel, LowRatingPanel, LoadingPanel, SelectBtn, MenuPanel, CartSelectionPanel, LevelSelectionPanel, LevelSelectionPanel2, ShopPanel, ExitPanel, RateUsPanel, SpinPanel, SoundOn, SoundOff;
    public Text SpinGiftText, ScoreText, CartNames;
    public string[] SpinValues;
    
    public GameObject[] EffectButton, PosCam, levelLocksImage, levelLocksImage2, levelLocks, levelLocks2, MyCarts, buy, CartsDetails;
    public Image[] Stars;
    public Slider Acceleration, Speed, Handling;
    public int Cartindex, n, SpinIndex;
    bool NextButton, ShopHide;

    public static int RewardedVideo, SpinTurn = 0;
    public Animator UnlockCartPanel;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;       
        Cartindex = 0;
        LerpScript.GetComponent<LerpCameraOnStart>().Target = PosCam[7].transform;                
        if (SpinTurn == 0)
        {
            OutterSpin.GetComponent<Button>().enabled = true;
        }
        else if (SpinTurn == 1)
        {
            OutterSpin.GetComponent<Button>().interactable = false;
            OutterSpin.GetComponent<Animator>().enabled = false;
        }
        ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
       // Ads_Manager.Instance.ShowSmallAdmobBanner();
        StartWork();
    }
    public void StartWork()
    {
        for (int i = 0; i <= PlayerPrefs.GetInt("Levels"); i++)
        {
            levelLocks[i].GetComponent<Button>().interactable = true;
            levelLocksImage[i].SetActive(false);
        }
        for (int i = 0; i <= PlayerPrefs.GetInt("Levels2"); i++)
        {
            levelLocks2[i].GetComponent<Button>().interactable = true;
            levelLocksImage2[i].SetActive(false);
        }

        if (PlayerPrefs.GetInt("ADSUNLOCK") == 1)
        {
            RemoveAdButton.SetActive(false);
            if(!Application.isEditor)
            {
                Ads_Manager.Instance.HideSmallAdmobBanner();
            }
        }
        if (PlayerPrefs.GetInt("CartsUnlock") == 1)
        {
            UnlockCarsButton.SetActive(false);
            PoPPanal.SetActive(false);
        }
        if (PlayerPrefs.GetInt("UNLOCKLEVELS") == 1)
        {
            UnlockLevelsButton.SetActive(false);
            ModeLock.SetActive(false);
        }   
        if (PlayerPrefs.GetInt("ModeLockChk") == 1)
        {
            ModeLock.SetActive(false);
        }
    }
    public void Play()
    {
        MenuPanel.SetActive(false);
        CartSelectionPanel.SetActive(true);
        CartSelect0();
        PlayerPrefs.SetInt("UnlockPanel", PlayerPrefs.GetInt("UnlockPanel") + 1);
        if (PlayerPrefs.GetInt("UnlockPanel") >= 1)
        {
            UnlockCartPanel.Play("1");            
        }
        ShopHide = true;
       // Ads_Manager.Instance.ShowSmallAdmobBanner();
    }
    public void BackUnlockCart()
    {
        UnlockCartPanel.Play("2");
    }
    public void BackCartSelection()
    {
        MenuPanel.SetActive(true);
        CartSelectionPanel.SetActive(false);
        foreach (GameObject MC in MyCarts)
        {
            MC.SetActive(false);
        }
        MyCarts[7].SetActive(true);
        LerpScript.GetComponent<LerpCameraOnStart>().Target = PosCam[7].transform;
        ShopHide = false;
    }
    public void Exit()
    {
        Ads_Manager.Instance.HideSmallAdmobBanner();
        Ads_Manager.Instance.ShowInterstitial();
        Ads_Manager.Instance.ShowLargeAdmobBanner();
        MenuPanel.SetActive(false);
        ExitPanel.SetActive(true);
    }
    public void NoExit()
    {
        Ads_Manager.Instance.HideLargeAdmobBanner();
        Ads_Manager.Instance.ShowSmallAdmobBanner();
        MenuPanel.SetActive(true);
        ExitPanel.SetActive(false);
    }
    public void YesExit()
    {
        if (Application.platform == RuntimePlatform.Android)
            new AndroidJavaClass("java.lang.System").CallStatic("exit", 0);
    }
    public void Shop()
    {
        Ads_Manager.Instance.ShowSmallAdmobBanner();
        MenuPanel.SetActive(false);
        ShopPanel.SetActive(true);
    }
    public void BackShop()
    {
        Ads_Manager.Instance.ShowSmallAdmobBanner();
        if (ShopHide == false)
        {
            MenuPanel.SetActive(true);
        }
        else if (ShopHide == true)
        {
            MenuPanel.SetActive(false);
            ShopHide = false;
        }
        ShopPanel.SetActive(false);
    }
    public void RateUs()
    {
        MenuPanel.SetActive(false);
        RateUsPanel.SetActive(true);
    }
    public void BackRateUs()
    {
        MenuPanel.SetActive(true);
        RateUsPanel.SetActive(false);
    }
    public void Spin()
    {
        MenuPanel.SetActive(false);
        SpinPanel.SetActive(true);
    }
    public void BackSpin()
    {
        MenuPanel.SetActive(true);
        SpinPanel.SetActive(false);
    }
    public void ActualSpin()
    {
        SpinAnim.GetComponent<Animator>().enabled = true;
        InnerSpin.GetComponent<Animator>().enabled = false;
        InnerSpin.GetComponent<Button>().interactable = false;
        SpinIndex = Random.Range(0, SpinValues.Length);        
        Invoke("ShowGift", 5f);
    }
    public void ShowGift()
    {
        SpinAdd();
        ClamPanal.SetActive(true);        
        SpinAnim.GetComponent<Animator>().enabled = false;
    }
    public void Add50()
    {
        ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
    }
    void SpinAdd()
    {
        if(SpinIndex == 0)
        {
            SpinGiftText.text = ": 50".ToString();
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 50);
            ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
        }
        else if (SpinIndex == 1)
        {
            SpinGiftText.text = ": 100".ToString();
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 100);
            ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
        }
        else if (SpinIndex == 2)
        {
            SpinGiftText.text = ": 200".ToString();
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 200);
            ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
        }
        else if (SpinIndex == 3)
        {
            SpinGiftText.text = ": 300".ToString();
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 300);
            ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
        }
        else if (SpinIndex == 4)
        {
            SpinGiftText.text = ": 500".ToString();
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 500);
            ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
        }
        else if (SpinIndex == 5)
        {
            SpinGiftText.text = ": 1000".ToString();
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 1000);
            ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
        }
        else if (SpinIndex == 6)
        {
            SpinGiftText.text = ": 300".ToString();
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 300);
            ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
        }
        else if (SpinIndex == 4)
        {
            SpinGiftText.text = ": 100".ToString();
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 100);
            ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
        }
        SpinTurn = 1;
        OutterSpin.GetComponent<Button>().interactable = false;
        OutterSpin.GetComponent<Animator>().enabled = false;
    }
    public void ClamPanalFun()
    {
        ClamPanal.SetActive(true);        
    }
    public void SoundOnButton()
    {
        SoundOff.SetActive(true);
        SoundOn.SetActive(false);
        AudioListener.volume = 0f;
    }
    public void SoundOffButton()
    {
        SoundOff.SetActive(false);
        SoundOn.SetActive(true);
        AudioListener.volume = 1f;
    }
    public void More()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Eagle+Studio+007");
    }
    public void RateUsButton()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.door.santa.claus.gift.delivery.christmas");
    }
    public void Reward100()
    {
        MainMenuScript.RewardedVideo = 1;
        Ads_Manager.Instance.ShowUnityRewardedVideoAd();
    }
    public void GetReward100()
    {
        PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 100);
        ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
    }
    public void UnlockCartsNew()
    {
        if (Cartindex == 1 && PlayerPrefs.GetInt("TotalSP") >= 15000)
        {
            PlayerPrefs.SetInt("1Cart", 1);
            buy[Cartindex].SetActive(false);
            SelectBtn.SetActive(true);

            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") - 15000);
            ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
        }
        else if (Cartindex == 2 && PlayerPrefs.GetInt("TotalSP") >= 25000)
        {
            PlayerPrefs.SetInt("2Cart", 2);
            buy[Cartindex].SetActive(false);
            SelectBtn.SetActive(true);
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") - 25000);
            ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
        }
        else
        {
            ShopHide = true;
            ShopPanel.SetActive(true);            
        }
    }

    public void NextCart()
    {
        if (Cartindex >= MyCarts.Length - 1)
            Cartindex = 0;
        else
            Cartindex++;
        for (n = 0; n < buy.Length; n++)
        {
            buy[n].SetActive(false);
        }

        NextButton = true;
        SelectBtn.SetActive(true);        
    }
    public void PreviousCart()
    {

        if (Cartindex <= 0)
            Cartindex = MyCarts.Length - 1;
        else

            Cartindex--;
        for (n = 0; n < buy.Length; n++)
        {
            buy[n].SetActive(false);
        }

        NextButton = true;
        SelectBtn.SetActive(true);        
    }
    public void SelectCart()
    {        
        CartSelectionPanel.SetActive(false);
        ModeSelection.SetActive(true);
    }
    public void BackMode()
    {
        CartSelectionPanel.SetActive(true);
        ModeSelection.SetActive(false);
        UnlockCartPanel.Play("1");
    }
    public void ModeLock2()
    {
        ModeSelection.SetActive(false);
        ModeQuestion.SetActive(true);
    }
    public void BackModeQuest()
    {
        ModeSelection.SetActive(true);
        ModeQuestion.SetActive(false);
    }
    public void YesUnlockMode()
    {
        if (PlayerPrefs.GetInt("TotalSP") >= 15000)
        {
            ModeQuestion.SetActive(false);
            ModeLock.SetActive(false);
            ModeSelection.SetActive(true);
            PlayerPrefs.SetInt("ModeLockChk", 1);
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") - 15000);
        }
        else
        {
            NotEnoughText.SetActive(true);
            Invoke("NotET", 4f);
        }
    }
    void NotET()
    {
        NotEnoughText.SetActive(false);
    }
    public void Mode1()
    {
        ModeSelection.SetActive(false);
        LevelSelectionPanel.SetActive(true);
        PlayerPrefs.SetInt("Mode", 1);        
        PlayerPrefs.SetInt("LevelIndex2", 0);
    }
    public void BackMode1()
    {
        ModeSelection.SetActive(true);
        LevelSelectionPanel.SetActive(false);
    }

    public void Mode2()
    {
        ModeSelection.SetActive(false);
        LevelSelectionPanel2.SetActive(true);
        PlayerPrefs.SetInt("Mode", 2);
        PlayerPrefs.SetInt("LevelIndex", 0);
    }
    public void BackMode2()
    {
        ModeSelection.SetActive(true);
        LevelSelectionPanel2.SetActive(false);
    }
    public void BackLevelSelect()
    {
        CartSelectionPanel.SetActive(true);
        LevelSelectionPanel.SetActive(false);
    }
    public void OnClickLevel(int Level)
    {
      //  Ads_Manager.Instance.HideSmallAdmobBanner();
     //   Ads_Manager.Instance.ShowInterstitial();

        LoadingPanel.SetActive(true);
        PlayerPrefs.SetInt("LevelIndex", Level);
        StartCoroutine(LoadLevel(Level));
    }
    IEnumerator LoadLevel(int levelToLoad)
    {
        yield return new WaitForSeconds(2f);
        AsyncOperation async = SceneManager.LoadSceneAsync("Scene_1");

        while (!async.isDone)
        {
            yield return null;
        }
    }
    public void OnClickLevel2(int Level)
    {
        Ads_Manager.Instance.HideSmallAdmobBanner();
        Ads_Manager.Instance.ShowInterstitial();

        LoadingPanel.SetActive(true);
        PlayerPrefs.SetInt("LevelIndex2", Level);
        StartCoroutine(LoadLevel2(Level));
    }
    IEnumerator LoadLevel2(int levelToLoad)
    {
        yield return new WaitForSeconds(2f);
        AsyncOperation async = SceneManager.LoadSceneAsync("Scene_1");

        while (!async.isDone)
        {
            yield return null;
        }
    }
    public void OnClickNextLevel()
    {
       // Ads_Manager.Instance.HideSmallAdmobBanner();
      //  Ads_Manager.Instance.ShowInterstitial();

        LoadingPanel.SetActive(true);
        if (PlayerPrefs.GetInt("LevelIndex") <=19)
        { 
        PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("Levels") + 1);
        }
        else if (PlayerPrefs.GetInt("LevelIndex") >= 20)
        {
                PlayerPrefs.SetInt("LevelIndex", 1);
        }
        StartCoroutine (Load());
    }
    IEnumerator Load()
    {
        yield return new WaitForSeconds(2f);
        AsyncOperation async = SceneManager.LoadSceneAsync("Scene_1");

        while (!async.isDone)
        {
            yield return null;
        }
    }

    public void OnClickNextLevel2()
    {
        Ads_Manager.Instance.HideSmallAdmobBanner();
        Ads_Manager.Instance.ShowInterstitial();

        LoadingPanel.SetActive(true);
        if (PlayerPrefs.GetInt("LevelIndex2") <= 19)
        {
            PlayerPrefs.SetInt("LevelIndex2", PlayerPrefs.GetInt("Levels2") + 1);
        }
        else if (PlayerPrefs.GetInt("LevelIndex2") >= 20)
        {
            PlayerPrefs.SetInt("LevelIndex2", 1);
        }
        StartCoroutine(Load2());
    }
    IEnumerator Load2()
    {
        yield return new WaitForSeconds(2f);
        AsyncOperation async = SceneManager.LoadSceneAsync("Scene_1");

        while (!async.isDone)
        {
            yield return null;
        }
    }
    public void LowStars1()
    {
        Stars[0].color = Color.yellow;
        Stars[1].color = Color.white;
        Stars[2].color = Color.white;
        Stars[3].color = Color.white;
        Stars[4].color = Color.white;
        LowRatingPanel.SetActive(true);
        HighRatingPanel.SetActive(false);
    }
    public void LowStars2()
    {
        Stars[0].color = Color.yellow;
        Stars[1].color = Color.yellow;
        Stars[2].color = Color.white;
        Stars[3].color = Color.white;
        Stars[4].color = Color.white;
        LowRatingPanel.SetActive(true);
        HighRatingPanel.SetActive(false);
    }
    public void LowStars3()
    {
        Stars[0].color = Color.yellow;
        Stars[1].color = Color.yellow;
        Stars[2].color = Color.yellow;
        Stars[3].color = Color.white;
        Stars[4].color = Color.white;
        LowRatingPanel.SetActive(true);
        HighRatingPanel.SetActive(false);
    }

    public void HighStars4()
    {
        Stars[0].color = Color.yellow;
        Stars[1].color = Color.yellow;
        Stars[2].color = Color.yellow;
        Stars[3].color = Color.yellow;
        Stars[4].color = Color.white;
        HighRatingPanel.SetActive(true);
        LowRatingPanel.SetActive(false);
    }

    public void HighStars5()
    {
        Stars[0].color = Color.yellow;
        Stars[1].color = Color.yellow;
        Stars[2].color = Color.yellow;
        Stars[3].color = Color.yellow;
        Stars[4].color = Color.yellow;
        HighRatingPanel.SetActive(true);
        LowRatingPanel.SetActive(false);
    }
    public void InHouseAd(string URl)
    {
        Application.OpenURL(URl);
    }

    public void CartSelect0()
    {
        LerpScript.GetComponent<LerpCameraOnStart>().Target = PosCam[0].transform;
        foreach (GameObject MC in MyCarts)
        {
            MC.SetActive(false);
        }        
        foreach (GameObject BB in buy)
        {
            BB.SetActive(false);
        }
        foreach (GameObject EB in EffectButton)
        {
            EB.SetActive(false);
        }
        EffectButton[0].SetActive(true);
        SelectBtn.SetActive(true);
        MyCarts[0].SetActive(true);
        Acceleration.value = 3f;
        Speed.value = 3f;
        Handling.value = 3.5f;
        CartNames.text = "MONSTER JEEP".ToString();
        Cartindex = 0;
        PlayerPrefs.SetInt("CartNo", Cartindex);
    }
    public void CartSelect1()
    {
        LerpScript.GetComponent<LerpCameraOnStart>().Target = PosCam[1].transform;
        foreach (GameObject MC in MyCarts)
        {
            MC.SetActive(false);
        }
        foreach (GameObject BB in buy)
        {
            BB.SetActive(false);
        }
        if (PlayerPrefs.GetInt("1Cart") == 1)
        {
            SelectBtn.SetActive(true);
            buy[1].SetActive(false);
        }
        else if(PlayerPrefs.GetInt("1Cart") != 1)
        {            
            SelectBtn.SetActive(false);
            buy[1].SetActive(true);
        }
        foreach (GameObject EB in EffectButton)
        {
            EB.SetActive(false);
        }
        EffectButton[1].SetActive(true);
        MyCarts[1].SetActive(true);
        Acceleration.value = 4f;
        Speed.value = 3.5f;
        Handling.value = 4.5f;
        CartNames.text = "4 X 4 CARRIAGE".ToString();
        Cartindex = 1;
        PlayerPrefs.SetInt("CartNo", Cartindex);
        if (PlayerPrefs.GetInt("CartsUnlock") == 1)
        {
            foreach (GameObject BC in buy)
            {
                BC.SetActive(false);
                SelectBtn.SetActive(true);
            }
        }
    }
    public void CartSelect2()
    {
        LerpScript.GetComponent<LerpCameraOnStart>().Target = PosCam[2].transform;
        foreach (GameObject MC in MyCarts)
        {
            MC.SetActive(false);
        }
        foreach (GameObject BB in buy)
        {
            BB.SetActive(false);
        }
        if (PlayerPrefs.GetInt("2Cart") == 2)
        {
            SelectBtn.SetActive(true);
            buy[2].SetActive(false);
        }
        else if (PlayerPrefs.GetInt("2Cart") != 2)
        {
            SelectBtn.SetActive(false);
            buy[2].SetActive(true);
        }
        foreach (GameObject EB in EffectButton)
        {
            EB.SetActive(false);
        }
        EffectButton[2].SetActive(true);
        MyCarts[2].SetActive(true);
        Acceleration.value = 4.5f;
        Speed.value = 4.5f;
        Handling.value = 5.5f;
        CartNames.text = "VIXEN DEER CART".ToString();
        Cartindex = 2;
        PlayerPrefs.SetInt("CartNo", Cartindex);
        if (PlayerPrefs.GetInt("CartsUnlock") == 1)
        {
            foreach (GameObject BC in buy)
            {
                BC.SetActive(false);
                SelectBtn.SetActive(true);
            }
        }
    }
    public void CartSelect3()
    {
        LerpScript.GetComponent<LerpCameraOnStart>().Target = PosCam[3].transform;
        foreach (GameObject MC in MyCarts)
        {
            MC.SetActive(false);
        }
        foreach (GameObject BB in buy)
        {
            BB.SetActive(false);
        }
        if (PlayerPrefs.GetInt("3Cart") == 3)
        {
            SelectBtn.SetActive(true);
            buy[3].SetActive(false);
        }
        else if (PlayerPrefs.GetInt("3Cart") != 2)
        {
            SelectBtn.SetActive(false);
            buy[3].SetActive(true);
        }
        foreach (GameObject EB in EffectButton)
        {
            EB.SetActive(false);
        }
        EffectButton[3].SetActive(true);
        MyCarts[3].SetActive(true);
        Acceleration.value = 5.5f;
        Speed.value = 5f;
        Handling.value = 6.5f;
        CartNames.text = "TESLA HIGH SPEED".ToString();
        Cartindex = 3;
        PlayerPrefs.SetInt("CartNo", Cartindex);
        if (PlayerPrefs.GetInt("CartsUnlock") == 1)
        {
            foreach (GameObject BC in buy)
            {
                BC.SetActive(false);
                SelectBtn.SetActive(true);
            }
        }
    }
    public void CartSelect4()
    {
        LerpScript.GetComponent<LerpCameraOnStart>().Target = PosCam[4].transform;
        foreach (GameObject MC in MyCarts)
        {
            MC.SetActive(false);
        }
        foreach (GameObject BB in buy)
        {
            BB.SetActive(false);
        }
        if (PlayerPrefs.GetInt("4Cart") == 4)
        {
            SelectBtn.SetActive(true);
            buy[4].SetActive(false);
        }
        else if (PlayerPrefs.GetInt("4Cart") != 4)
        {
            SelectBtn.SetActive(false);
            buy[4].SetActive(true);
        }
        foreach (GameObject EB in EffectButton)
        {
            EB.SetActive(false);
        }
        EffectButton[4].SetActive(true);
        MyCarts[4].SetActive(true);
        Acceleration.value = 6.5f;
        Speed.value = 6f;
        Handling.value = 7.5f;
        CartNames.text = "SPENCER DEER CART".ToString();
        Cartindex = 4;
        PlayerPrefs.SetInt("CartNo", Cartindex);
        if (PlayerPrefs.GetInt("CartsUnlock") == 1)
        {
            foreach (GameObject BC in buy)
            {
                BC.SetActive(false);
                SelectBtn.SetActive(true);
            }
        }
    }
    public void CartSelect5()
    {
        LerpScript.GetComponent<LerpCameraOnStart>().Target = PosCam[5].transform;
        foreach (GameObject MC in MyCarts)
        {
            MC.SetActive(false);
        }
        foreach (GameObject BB in buy)
        {
            BB.SetActive(false);
        }
        if (PlayerPrefs.GetInt("5Cart") == 5)
        {
            SelectBtn.SetActive(true);
            buy[5].SetActive(false);
        }
        else if (PlayerPrefs.GetInt("5Cart") != 5)
        {
            SelectBtn.SetActive(false);
            buy[5].SetActive(true);
        }
        foreach (GameObject EB in EffectButton)
        {
            EB.SetActive(false);
        }
        EffectButton[5].SetActive(true);
        MyCarts[5].SetActive(true);
        Acceleration.value = 7.5f;
        Speed.value = 7f;
        Handling.value = 8.5f;
        CartNames.text = "HUNTER WOLF CART".ToString();
        Cartindex = 5;
        PlayerPrefs.SetInt("CartNo", Cartindex);
        if (PlayerPrefs.GetInt("CartsUnlock") == 1)
        {
            foreach (GameObject BC in buy)
            {
                BC.SetActive(false);
                SelectBtn.SetActive(true);
            }
        }
    }
    public void CartSelect6()
    {
        LerpScript.GetComponent<LerpCameraOnStart>().Target = PosCam[6].transform;
        foreach (GameObject MC in MyCarts)
        {
            MC.SetActive(false);
        }
        foreach (GameObject BB in buy)
        {
            BB.SetActive(false);
        }
        if (PlayerPrefs.GetInt("6Cart") == 6)
        {
            SelectBtn.SetActive(true);
            buy[6].SetActive(false);
        }
        else if (PlayerPrefs.GetInt("6Cart") != 6)
        {
            SelectBtn.SetActive(false);
            buy[6].SetActive(true);
        }
        foreach (GameObject EB in EffectButton)
        {
            EB.SetActive(false);
        }
        EffectButton[6].SetActive(true);
        MyCarts[6].SetActive(true);
        Acceleration.value = 8.5f;
        Speed.value = 8f;
        Handling.value = 9.5f;
        CartNames.text = "FURIOUS BEER CART".ToString();
        Cartindex = 6;
        PlayerPrefs.SetInt("CartNo", Cartindex);
        if (PlayerPrefs.GetInt("CartsUnlock") == 1)
        {
            foreach (GameObject BC in buy)
            {
                BC.SetActive(false);
                SelectBtn.SetActive(true);
            }
        }
    }
    public void InappCartsNew()
    {
        buy[Cartindex].SetActive(false);
        SelectBtn.SetActive(true);
    }
}
