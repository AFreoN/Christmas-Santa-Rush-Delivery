using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Animator[] CartsAnimal;
    public RCC_CarControllerV3[] Cars;
    public GameObject GamePlayPanel, SoundOn, SoundOff, JingleBell, BgSound, EnoughCoin,callpanel, LoadingPanel, timeObj, timerScript, CameraPannel, PausePannel, Complete_Panel, OverPanel;
    public GameObject[] BrakeLights, Carts, Levels, Levels2, ArrowTrigger, ArrowTrigger2, ArrowQuad, ArrowQuad2, CameraButton;

    public Text CallingKid, Levelreward, TimeReward, TotalReward;
    public string[] KidNames;

    public bool OnlyOnce;

    public Material kidMaterial;
    public Texture2D[] kidsTexture;

    // Start is called before the first frame update
    int getLevel, getLevel2, count, NamesNum;
    private void Awake()
    {
        Instance = this;
        if (PlayerPrefs.GetInt("Mode") == 1)
        {            
            GamePlayPanel.SetActive(true);
            getLevel = PlayerPrefs.GetInt("LevelIndex");
            Analytics.CustomEvent("DeliveryLevelStart " + getLevel);
        }
        else if (PlayerPrefs.GetInt("Mode") == 2)
        {            
            getLevel2 = PlayerPrefs.GetInt("LevelIndex2");
            Analytics.CustomEvent("ParkingLevelStart " + getLevel2);
        }
        Carts[PlayerPrefs.GetInt("CartNo")].SetActive(true);        
    }
    void Start()
    {
        SantaPosition.Instance.Santa = Carts[PlayerPrefs.GetInt("CartNo")].transform;
        SantaPosition.Instance.PlayerParent = Carts[PlayerPrefs.GetInt("CartNo")];
        if (PlayerPrefs.GetInt("Mode") == 1)
        {
            SantaPosition.Instance.SetSantaPosition(getLevel);
            Levels[getLevel].SetActive(true);
        }
        else if(PlayerPrefs.GetInt("Mode") == 2)
        {
            SantaPosition.Instance.SetSantaPosition2(getLevel2);
            Levels2[getLevel2].SetActive(true);
        }
        if(!Application.isEditor)
        {
            Ads_Manager.Instance.HideLargeAdmobBanner();
            Ads_Manager.Instance.ShowSmallAdmobBanner();
        }

        NamesNum = Random.Range(0, KidNames.Length);
        CallingKid.text = KidNames[NamesNum] + " IS CALLING YOU";
        OnlyOnce = true;
        kidMaterial.SetTexture("_MainTex", kidsTexture[Random.Range(0, 4)]);

        if(!Application.isEditor)
        {
            Ads_Manager.Instance.ShowSmallAdmobBanner();
        }
    }
    public void RaceDown()
    {
                   foreach (Animator CA in CartsAnimal)
                {
                    CA.SetTrigger("Run");
                }
    }

    public void RaceUp()
    {
        foreach (Animator CA in CartsAnimal)
        {
            //     CA.SetTrigger("Idle");
        }
    }

    public void BrakeDown()
    {
        foreach (GameObject BL in BrakeLights)
        {
            BL.GetComponent<Light>().enabled = true;
        }
    }

    public void BrakeUp()
    {
        foreach (GameObject BL in BrakeLights)
        {
            BL.GetComponent<Light>().enabled = false;
        }
    }
    public void CallPanelStatus(bool status)
    {
        callpanel.SetActive(status);
    }
    public void LoadingPanelStatus(bool stats)
    {
        LoadingPanel.SetActive(stats);
    }
    public void DisplayTime()
    {
        //timeObj.SetActive(true);  //Original Lines
        //timerScript.SetActive(true);
        timeObj.SetActive(false);
        timerScript.SetActive(false);
    }
    public void RecieveCall()
    {
        Levels[getLevel].GetComponent<LevelPlayScript>().Receive();
        ArrowTrigger[getLevel].SetActive(true);
        ArrowQuad[getLevel].SetActive(true);
        ArrowDirection.Instance.ArrowOn();
        //   levelPoints[index].SetActive(true);       
        Camera();
    }
    public void RecieveCall2()
    {
        DisplayTime();
        ArrowTrigger2[getLevel2].SetActive(true);
        ArrowQuad2[getLevel2].SetActive(true);
        ArrowDirection.Instance.ArrowOn2();
        //   levelPoints[index].SetActive(true);       
        Camera();
    }

    public void BusyCall()
    {
        Levels[getLevel].GetComponent<LevelPlayScript>().Busy();        
    }
    public void Camera()
    {
        Time.timeScale = 1;
        if (count == 0)
        {
            CameraPannel.SetActive(true);
            foreach (GameObject CB in CameraButton)
            {
                CB.SetActive(true);
            }            
            count++;
        }
        else if (count == 1)
        {
            CameraPannel.SetActive(false);
            foreach (GameObject CB in CameraButton)
            {
                CB.SetActive(false);
            }
            count = 0;
        }
    }
    public void Pause()
    {     
        if (OnlyOnce)
        {
            RCC_CarControllerV3.instance.KillEngine();
            if(Application.isEditor == false)
            {
                Ads_Manager.Instance.HideSmallAdmobBanner();
                Ads_Manager.Instance.Show_Unity_Admob();
                Ads_Manager.Instance.ShowLargeAdmobBanner();
            }
            PausePannel.SetActive(true);            
            OnlyOnce = false;
        }
    }
    public void ShowLevelCompleted()
    {

        if (OnlyOnce)
        {
            Ads_Manager.Instance.HideSmallAdmobBanner();
            Ads_Manager.Instance.Show_Unity_Admob();
            Ads_Manager.Instance.ShowLargeAdmobBanner();

            Complete_Panel.SetActive(true);            
            OnlyOnce = false;
            if (PlayerPrefs.GetInt("Mode") == 1)
            {
                Analytics.CustomEvent("DeliveryLevelComplete " + getLevel);
                if (PlayerPrefs.GetInt("Levels") <= PlayerPrefs.GetInt("LevelIndex"))
                {
                    PlayerPrefs.SetInt("Levels", PlayerPrefs.GetInt("LevelIndex"));
                }

                PlayerPrefs.SetInt("LevelReward", 400);
                PlayerPrefs.SetInt("TimeReward", 170);

                for (int x = 0; x <= getLevel; x++)
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
            }
            else if (PlayerPrefs.GetInt("Mode") == 2)
            {
                Analytics.CustomEvent("ParkingLevelComplete " + getLevel2);
                if (PlayerPrefs.GetInt("Levels2") <= PlayerPrefs.GetInt("LevelIndex2"))
                {
                    PlayerPrefs.SetInt("Levels2", PlayerPrefs.GetInt("LevelIndex2"));
                }

                PlayerPrefs.SetInt("LevelReward", 400);
                PlayerPrefs.SetInt("TimeReward", 170);

                for (int x = 0; x <= getLevel2; x++)
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
            }
            Levelreward.text = PlayerPrefs.GetInt("LevelReward").ToString();
            TimeReward.text = PlayerPrefs.GetInt("TimeReward").ToString();
            float total = PlayerPrefs.GetInt("TimeReward") + PlayerPrefs.GetInt("LevelReward");
            TotalReward.text = total.ToString();
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + PlayerPrefs.GetInt("TimeReward") + PlayerPrefs.GetInt("LevelReward"));
            
        }
    }
    public void ShowLevelFailed()
    {        
        if (OnlyOnce)
        {
            Ads_Manager.Instance.HideSmallAdmobBanner();
            Ads_Manager.Instance.ShowInterstitial();
            Ads_Manager.Instance.ShowLargeAdmobBanner();

            OverPanel.SetActive(true);            
            OnlyOnce = false;
        }
    }
    public void SkipLevel()
    {
        if (PlayerPrefs.GetInt("TotalSP") >= 100)
        {
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") - 100);
            if (PlayerPrefs.GetInt("Mode") == 1)
            {
                if (PlayerPrefs.GetInt("LevelIndex") < Levels.Length)
                {
                    if (PlayerPrefs.GetInt("Levels") <= getLevel)
                    {
                        PlayerPrefs.SetInt("Levels", PlayerPrefs.GetInt("Levels") + 1);
                    }
                    PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("LevelIndex") + 1);
                }
                if (getLevel <= 20)
                {
                    Restart();
                }
                else if (getLevel > 20)
                {
                    MainMenu();
                }
            }
            else if (PlayerPrefs.GetInt("Mode") == 2)
            {
                if (PlayerPrefs.GetInt("LevelIndex2") < Levels2.Length)
                {
                    if (PlayerPrefs.GetInt("Levels2") <= getLevel2)
                    {
                        PlayerPrefs.SetInt("Levels2", PlayerPrefs.GetInt("Levels2") + 1);
                    }
                    PlayerPrefs.SetInt("LevelIndex2", PlayerPrefs.GetInt("LevelIndex2") + 1);
                }
                if (getLevel2 <= 20)
                {
                    Restart();
                }
                else if (getLevel2 > 20)
                {
                    MainMenu();
                }
            }
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
    public void Restart()
    {
        Time.timeScale = 1;        
        Ads_Manager.Instance.HideLargeAdmobBanner();
        LoadingPanel.SetActive(true);        
        SceneManager.LoadScene("Scene_1");
    }

    public void jingel()
    {
        JingleBell.SetActive(true);
        BgSound.SetActive(false);
    }
    public void Sound()
    {
        JingleBell.SetActive(false);
        BgSound.SetActive(true);
    }
    public void RewardedVedio()
    {
        MainMenuScript.RewardedVideo = 2;
        Ads_Manager.Instance.ShowUnityRewardedVideoAd();
    }
    public void x2Vedio()
    {
        PlayerPrefs.SetInt("LevelReward", PlayerPrefs.GetInt("LevelReward") + 100);
        Levelreward.text = PlayerPrefs.GetInt("LevelReward").ToString();
    }
    public void MainMenu()
    {
        LoadingPanel.SetActive(true);
        Ads_Manager.Instance.ShowSmallAdmobBanner();
        Ads_Manager.Instance.HideLargeAdmobBanner();
        Invoke("MainLoad", 1f);
    }
    void MainLoad()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Next()
    {
        LoadingPanel.SetActive(true);
        Ads_Manager.Instance.HideLargeAdmobBanner();
        Invoke("NextLoad", 1f);
    }
    void NextLoad()
    {
        if (PlayerPrefs.GetInt("Mode") == 1)
        {
            if (PlayerPrefs.GetInt("LevelIndex") <= 19)
            {
                PlayerPrefs.SetInt("LevelIndex", PlayerPrefs.GetInt("LevelIndex") + 1);
                SceneManager.LoadScene("Scene_1");
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else if (PlayerPrefs.GetInt("Mode") == 2)
        {
            if (PlayerPrefs.GetInt("LevelIndex2") <= 19)
            {
                PlayerPrefs.SetInt("LevelIndex2", PlayerPrefs.GetInt("LevelIndex2") + 1);
                SceneManager.LoadScene("Scene_1");
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    public void Resume()
    {
        Ads_Manager.Instance.HideLargeAdmobBanner();
        Ads_Manager.Instance.ShowSmallAdmobBanner();
        if (PlayerPrefs.GetInt("Mode") == 1)
        {
            if (callpanel.activeSelf)
            {
                print("call panal open");
                Levels[getLevel].GetComponent<LevelPlayScript>().RiceveOnResume();
            }
        }
        OnlyOnce = true;
        RCC_CarControllerV3.instance.StartEngine(true);
        // GameSound.SetActive(true);
        PausePannel.SetActive(false);
    }
    public void SoundOnButton()
    {
        AudioListener.volume = 0f;
        SoundOn.SetActive(false);
        SoundOff.SetActive(true);
    }
    public void SoundOffButton()
    {
        AudioListener.volume = 1f;
        SoundOn.SetActive(true);
        SoundOff.SetActive(false);
    }
    public void InHouseAd(string URl)
    {
        Application.OpenURL(URl);
    }
}
