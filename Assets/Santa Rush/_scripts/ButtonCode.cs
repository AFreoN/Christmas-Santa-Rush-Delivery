using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections;

public class ButtonCode : MonoBehaviour
{
    public static ButtonCode Instance;
    public Animator[] CartsAnimal;
    public RCC_CarControllerV3[] Cars;
    public GameObject gamePlayPanel, PausePannel, OverPanel, CameraPannel, CameraButton, GameSound, Loading_Panel, Complete_Panel, InHousePanal;
    public GameObject menuCanvas, gameplayCanvas, timeObj, timerScript, callpanel, PlayerArow, EnoughCoin, vedionotAvailable;
    int count, levelRewarded = 0;
    public bool OnlyOnce;
    int index;
    public GameObject[] AllCarts, Levels, levelPoints, levelGifts, levelArrows;
    public Material kidMaterial;
    public Texture2D[] kidsTexture;
    public Text Levelreward, TimeReward, TotalReward;
    [HideInInspector]
    public bool X2Vedio = false;
    void OnEnable()
    {
        // PlayerPrefs.SetInt("Levels",18);
        levelRewarded = 0;
        index = PlayerPrefs.GetInt("LevelIndex");
        OnlyOnce = true;
        //Loading_Panel.SetActive(false);
        PausePannel.SetActive(false);
        OverPanel.SetActive(false);
        InHousePanal.SetActive(false);
        timeObj.SetActive(false);
        //index = Random.Range(0, 9);
        this.GetComponent<SantaPosition>().SetSantaPosition(index);
        print("level prefab no = " + PlayerPrefs.GetInt("Levels"));
        int _length = Levels.Length;

    //    Levels[index].SetActive(true);
       
        for (int i = 0; i < _length; i++)
        {

            if (i.Equals(index))
            {
                print("level no = " + i);
                Levels[i].SetActive(true);
                AnalyticsEvent.LevelStart(PlayerPrefs.GetInt("LevelIndex") + 1);                
            }
            else
            {
                // Levels[i].SetActive(false);
                // levelPoints[i].SetActive(false);
                // levelGifts[i].SetActive(false);
                //levelArrows[i].SetActive(false);
            }
        }
        kidMaterial.SetTexture("_MainTex", kidsTexture[Random.Range(0, 4)]);
    }
    void Start()
    {
        Instance = this;
        PlayerArow = GameObject.FindGameObjectWithTag("Arrows");
        for (int i = 0; i < PlayerArow.transform.childCount; i++)
        {
            levelArrows[i] = PlayerArow.transform.GetChild(i).gameObject;
        }
        // Invoke("LoadingPanel",2f);

        Ads_Manager.Instance.HideLargeAdmobBanner();
    }
    public void PlayLevel()
    {

    }

    void Update()
    {
        if (callpanel.activeSelf)
        {
            if (!OnlyOnce)
            {
                callpanel.GetComponent<AudioSource>().volume = 0.0f;
            }
            else
            {
                callpanel.GetComponent<AudioSource>().volume = 1.0f;
            }
        }
                 
    }
    public void RaceDown()
    {
        foreach (RCC_CarControllerV3 CC in Cars)
        {
            if (CC.speed >= 1)
            {
                foreach (Animator CA in CartsAnimal)
                {
                    CA.SetTrigger("Run");
                }
            }
        }
    }

    public void RaceUp()
    {       
                foreach (Animator CA in CartsAnimal)
                {
               //     CA.SetTrigger("Idle");
                }             
    }
    public void Pause()
    {
        //CameraPannel.SetActive(false);
        // GameSound.SetActive(false);

        if (OnlyOnce)
        {
            RCC_CarControllerV3.instance.KillEngine();
            PausePannel.SetActive(true);
            InHousePanal.SetActive(true);
            OnlyOnce = false;

            Ads_Manager.Instance.HideSmallAdmobBanner();
            Ads_Manager.Instance.Show_Unity_Admob();
            Ads_Manager.Instance.ShowLargeAdmobBanner();            
        }     
    }
    public void Resume()
    {
        Time.timeScale = 1;
        if (callpanel.activeSelf)
        {
            print("call panal open");
            Levels[index].GetComponent<LevelPlayScript>().RiceveOnResume();
        }
        OnlyOnce = true;
        RCC_CarControllerV3.instance.StartEngine(true);
        // GameSound.SetActive(true);
        PausePannel.SetActive(false);
        InHousePanal.SetActive(false);
        //Levels[index].GetComponent<LevelPlayScript>().SetCallPanelActive();

        Ads_Manager.Instance.HideLargeAdmobBanner();
        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            Ads_Manager.Instance.ShowSmallAdmobBanner();
        }
    }

    public void Restart()
    {
        Time.timeScale = 1;
        Loading_Panel.SetActive(true);
        MainMenuCode.DefultCarts = MainMenuCode.Cartindex;
        SceneManager.LoadScene("rcc_santa");

    }
    public void LevelQuit()
    {
        //AnalyticsEvent.LevelQuit(PlayerPrefs.GetInt("Levels")+1);
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        MainMenuCode.CartChk = 0;
        PausePannel.SetActive(false);
        OverPanel.SetActive(false);
        Loading_Panel.SetActive(true);
        Complete_Panel.SetActive(false);
        InHousePanal.SetActive(false);
        //Invoke("Load_scene", 0.2f);
        foreach (GameObject AC in AllCarts)
        {
            AC.SetActive(false);
        }
        MainMenuCode.DefultCarts = 0;
        Invoke("Load_scene", 1f);
    }
    void Load_scene()
    {
        MainMenuCode.isShowMenu = true;
        menuCanvas.SetActive(true);
        gameplayCanvas.SetActive(false);

        Ads_Manager.Instance.HideLargeAdmobBanner();
        SceneManager.LoadScene("rcc_santa");
    }
    public void Camera()
    {
        Time.timeScale = 1;
        if (count == 0)
        {
            CameraPannel.SetActive(true);
            CameraButton.SetActive(true);
            count++;
        }
        else if (count == 1)
        {
            CameraPannel.SetActive(false);
            CameraButton.SetActive(false);
            count = 0;
        }
    }

    public void PlayLevelComplete()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Complete_Panel.SetActive(false);
        InHousePanal.SetActive(false);
        Time.timeScale = 1;
        MainMenuCode.DefultCarts = MainMenuCode.Cartindex;
        Restart();
    }

    public void ShowLevelCompleted()
    {

        if (OnlyOnce)
        {
            Complete_Panel.SetActive(true);
            InHousePanal.SetActive(true);
            OnlyOnce = false;
            AnalyticsEvent.LevelComplete(PlayerPrefs.GetInt("LevelIndex") + 1);
           if (PlayerPrefs.GetInt("LevelIndex") < Levels.Length)
            {
                if (PlayerPrefs.GetInt("Levels") <= index)
                {
                    PlayerPrefs.SetInt("Levels", PlayerPrefs.GetInt("Levels") + 1);
                }
                PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("LevelIndex") + 1);
                if (PlayerPrefs.GetInt("LevelIndex") > 10)
                {
                  //  PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("LevelIndex") - 10);                    
                }
            }
         
            if (PlayerPrefs.GetInt("Levels") <= PlayerPrefs.GetInt("LevelIndex"))
            {
                PlayerPrefs.SetInt("Levels", PlayerPrefs.GetInt("LevelIndex"));
            }
            PlayerPrefs.SetInt("LevelReward", 400);
            PlayerPrefs.SetInt("TimeReward", 170);
            for (int x = 0; x <= index; x++)
            {
                if (x % 2 == 0)
                {
                    PlayerPrefs.SetInt("LevelReward", PlayerPrefs.GetInt("LevelReward") + 50);
                }
                else
                {
                    PlayerPrefs.SetInt("LevelReward", PlayerPrefs.GetInt("LevelReward") + 100);
                }
                PlayerPrefs.SetInt("TimeReward", PlayerPrefs.GetInt("TimeReward") + 30);
            }
            Levelreward.text = PlayerPrefs.GetInt("LevelReward").ToString();
            TimeReward.text = PlayerPrefs.GetInt("TimeReward").ToString();
            float total = PlayerPrefs.GetInt("TimeReward") + PlayerPrefs.GetInt("LevelReward");
            TotalReward.text = total.ToString();
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + PlayerPrefs.GetInt("TimeReward") + PlayerPrefs.GetInt("LevelReward"));

            Ads_Manager.Instance.HideSmallAdmobBanner();
            Ads_Manager.Instance.Show_Unity_Admob();
            Ads_Manager.Instance.ShowLargeAdmobBanner();            
        }
    }
    public void ShowLevelFailed()
    {

        if (OnlyOnce)
        {
            OverPanel.SetActive(true);
            InHousePanal.SetActive(true);
            OnlyOnce = false;
         //   AnalyticsEvent.LevelFail(PlayerPrefs.GetInt("Levels"));
            //GameSound.SetActive(false);

            Ads_Manager.Instance.HideSmallAdmobBanner();
            Ads_Manager.Instance.ShowInterstitial();
            Ads_Manager.Instance.ShowLargeAdmobBanner();            
        }
    }
    public void DisplayTime()
    {
        timeObj.SetActive(true);
        timerScript.SetActive(true);
    }
    public void LoadingPanelStatus(bool stats)
    {
        Loading_Panel.SetActive(stats);
    }
    public void LoadingPanel()
    {
        Loading_Panel.SetActive(false);
    }
    public void CallPanelStatus(bool status)
    {
        callpanel.SetActive(status);
    }
    public void RecieveCall()
    {
        Levels[index].GetComponent<LevelPlayScript>().Receive();
        levelArrows[index].SetActive(true);
        levelPoints[index].SetActive(true);
   //     AnalyticsEvent.LevelStart(PlayerPrefs.GetInt("Levels") + 1);
        levelGifts[index].SetActive(true);
        Camera();
    }
    public void BusyCall()
    {
        Levels[index].GetComponent<LevelPlayScript>().Busy();
        //AnalyticsEvent.LevelSkip(PlayerPrefs.GetInt("Levels")+1);
    }
    public void SkipLevel()
    {
        if (PlayerPrefs.GetInt("TotalSP") >= 100)
        {
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") - 100);
            if (PlayerPrefs.GetInt("LevelIndex") < Levels.Length)
            {
                if (PlayerPrefs.GetInt("Levels") <= index)
                {
                    PlayerPrefs.SetInt("Levels", PlayerPrefs.GetInt("Levels") + 1);
                }
                PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("LevelIndex") + 1);
                if (PlayerPrefs.GetInt("LevelIndex") > 10)
                {
                    //  PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("LevelIndex") - 10);                   
                }
            }
            
            Restart();
        }
        else
        {
            StartCoroutine(GameObjectTrueFalse(EnoughCoin));
        }
    }
    public IEnumerator GameObjectTrueFalse(GameObject Panal)
    {
        Panal.SetActive(true);
        yield return new WaitForSeconds(5);
        Panal.SetActive(false);
    }

    public void RewardedVedio()
    {
        MainMenuCode.RewardVideo = 1;
        Ads_Manager.Instance.ShowUnityRewardedVideoAd();
    }
    public void x2Vedio()
    {
        PlayerPrefs.SetInt("LevelReward", PlayerPrefs.GetInt("LevelReward") + PlayerPrefs.GetInt("LevelReward"));
        Levelreward.text = PlayerPrefs.GetInt("LevelReward").ToString();
    }

}
