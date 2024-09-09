using UnityEngine;
using System;
using GoogleMobileAds.Api;
using UnityEngine.Monetization;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class Ads_Manager : MonoBehaviour
{
    public static Ads_Manager Instance;
    private string appID = "ca-app-pub-1954441980396629~6090743042";
    private string bannerID = "ca-app-pub-1954441980396629/6176171826";
    private AdPosition bannerSmallPosition = AdPosition.Top;
    private AdPosition bannerBottomPosition = AdPosition.BottomRight;
    private AdPosition bannerLargePosition = AdPosition.TopRight;
    // public bool showBannerOnStart;
    private string interstitialID = "ca-app-pub-1954441980396629/3681321689";
    private string unityAdID = "1577688"; // unity id   

    [SerializeField]
    private bool enableTestMode = false;
    //	[Header("Rewarded Video")]
    //	public string rewardedVideoID;
    private BannerView smallbannerView;
    private BannerView smallBottomBannerView;
    private BannerView largebannerView;
    private InterstitialAd interstitial;
    //private RewardBasedVideoAd rewardBasedVideo;
    private bool SmallBannerOnceLoaded, smallBottomBannerOnceLoaded;
    private bool LargeBannerOnceLoaded;
    private bool isInternet = false;
    private bool isAdInitialized = false;
    [HideInInspector]
    public int rewardNumber = 0;


    //IronSource

    

    GameObject InitText;
    GameObject LoadButton;
    GameObject LoadText;
    GameObject ShowButton;
    GameObject ShowText;

    public static String INTERSTITIAL_INSTANCE_ID = "0";


    GameObject InitText2;
    GameObject ShowButton2;
    GameObject ShowText2;
    GameObject AmountText;
    int userTotalCredits = 0;

    public static String REWARDED_INSTANCE_ID = "0";
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private bool CheckInitialization()
    {
        if (isAdInitialized)
        {
            isAdInitialized = true;
            return isAdInitialized;
        }
        else
        {
            isAdInitialized = false;
            InitializeAds();
            return false;
        }
    }
    public bool IsInternetConnection()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            isInternet = true;
        }
        else
            isInternet = false;

        return isInternet;
    }
    private void Start()
    {
        if (enableTestMode)
        {
            //test ids
            bannerID = "ca-app-pub-3940256099942544/6300978111";
            interstitialID = "ca-app-pub-3940256099942544/1033173712";
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        SmallBannerOnceLoaded = false;
        smallBottomBannerOnceLoaded = false;
        LargeBannerOnceLoaded = false;
        if (IsInternetConnection())
        {
            InitializeAds();
        }
        else
            isAdInitialized = false;
        // Invoke("SetFalse",1);

        
    }

   
    void InitializeAds()
    {
        isAdInitialized = true;
        MobileAds.Initialize(appID);
        InitUnityAds();
        if (PlayerPrefs.GetInt("ADSUNLOCK").Equals(0))
        {
            RequestBanner();
            RequestBottomBanner();
            RequestLargeBanner();
            RequestInterstitial();
        }

    }

    public void ChangeBannerTopLeft()
    {
        DestroySmallBanner();
        SmallBannerOnceLoaded = false;
        bannerSmallPosition = AdPosition.TopLeft;
        RequestBanner();
    }
    public void ChangeBannerBottomRight()
    {
        DestroySmallBanner();
        SmallBannerOnceLoaded = false;
        bannerSmallPosition = AdPosition.BottomRight;
        RequestBanner();
    }
    void SetFalse()
    {
        //showBannerOnStart = false;
    }
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
    private void RequestBanner()
    {
        if (this.smallbannerView == null)
        {
            this.smallbannerView = new BannerView(bannerID, AdSize.Banner, bannerSmallPosition);
            // Register for ad events.
            this.smallbannerView.OnAdLoaded += this.HandleAdLoaded;
            //this.smallbannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
            //this.smallbannerView.OnAdOpening += this.HandleAdOpened;
            //this.smallbannerView.OnAdClosed += this.HandleAdClosed;
            this.smallbannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;
            // Load a banner ad.
            this.smallbannerView.LoadAd(this.CreateAdRequest());
        }
    }
    private void RequestBottomBanner()
    {
        if (this.smallBottomBannerView == null)
        {
            this.smallBottomBannerView = new BannerView(bannerID, AdSize.Banner, bannerBottomPosition);
            this.smallBottomBannerView.OnAdLoaded += this.HandleBottomAdLoaded;
            this.smallBottomBannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;
            this.smallBottomBannerView.LoadAd(this.CreateAdRequest());
            this.smallBottomBannerView.Hide();
        }
    }
    private void RequestLargeBanner()
    {
        if (this.largebannerView == null)
        {
            this.largebannerView = new BannerView(bannerID, AdSize.MediumRectangle, bannerLargePosition);
            // Register for ad events.
            this.largebannerView.OnAdLoaded += this.HandleLargeBannerAdLoaded;
            //this.largebannerView.OnAdFailedToLoad += this.HandleLargeBannerAdFailedToLoad;
            //this.largebannerView.OnAdOpening += this.HandleLargeBannerAdOpened;
            //this.largebannerView.OnAdClosed += this.HandleLargeBannerAdClosed;
            this.largebannerView.OnAdLeavingApplication += this.HandleLargeBannerAdLeftApplication;
            // Load a banner ad.
            this.largebannerView.LoadAd(this.CreateAdRequest());
            this.largebannerView.Hide();
        }
    }
    public void ShowSmallAdmobBanner()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK").Equals(0))
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    //if (SmallBannerOnceLoaded)
                    this.smallbannerView.Show();
                }
            }
        }
    }
    public void HideSmallAdmobBanner()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK").Equals(0))
        {
            //if (SmallBannerOnceLoaded && CheckInitialization())
            if (CheckInitialization())
                this.smallbannerView.Hide();
        }
    }
    public void ShowBottomAdmobBanner()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK").Equals(0))
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    // if (smallBottomBannerOnceLoaded)
                    this.smallBottomBannerView.Show();
                }
            }
        }
    }
    public void HideBottomAdmobBanner()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK").Equals(0))
        {
            // if (smallBottomBannerOnceLoaded && CheckInitialization())
            if (CheckInitialization())
                this.smallBottomBannerView.Hide();
        }
    }
    public void ShowLargeAdmobBanner()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK").Equals(0))
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    //if (LargeBannerOnceLoaded)
                    this.largebannerView.Show();
                }
            }
        }
    }

    public void HideLargeAdmobBanner()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK").Equals(0))
        {
            //if (LargeBannerOnceLoaded && CheckInitialization())
            if (CheckInitialization())
                this.largebannerView.Hide();
        }
    }
    public void HideLargeOnRemoveAd()
    {
        //if (LargeBannerOnceLoaded && CheckInitialization())
        if (CheckInitialization())
            this.largebannerView.Hide();
    }
    public void DestroySmallBanner()
    {
        if (this.smallbannerView != null)
            this.smallbannerView.Destroy();
    }
    public void DestroyBottomBanner()
    {
        if (this.largebannerView != null)
            this.smallBottomBannerView.Destroy();
    }
    public void DestroyLargeBanner()
    {
        if (this.largebannerView != null)
            this.largebannerView.Destroy();
    }

    private void RequestInterstitial()
    {
        // Create an interstitial.
        this.interstitial = new InterstitialAd(interstitialID);
        // Register for ad events.
        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        //this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        //	this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;
        // Load an interstitial ad.
        this.interstitial.LoadAd(this.CreateAdRequest());
    }

    public void ShowInterstitial()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK").Equals(0))
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    if (this.interstitial.IsLoaded())
                    {
                        this.interstitial.Show();
                    }
                }
            }
        }
    }

   
    /*     public void ShowRewardBasedVideo()
        {
                    if (rewardBasedVideo.IsLoaded())
                    {
                        rewardBasedVideo.Show();
                    }
        } */

    #region Small Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        SmallBannerOnceLoaded = true;
        if (!SceneManager.GetActiveScene().name.Equals("Splash") && !SceneManager.GetActiveScene().name.Equals("HomeScene"))
        {
            HideSmallAdmobBanner();
        }
    }
    public void HandleBottomAdLoaded(object sender, EventArgs args)
    {
        smallBottomBannerOnceLoaded = true;
    }
    /*     public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {

        } */

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        this.smallbannerView.OnAdLoaded -= this.HandleAdLoaded;
        this.smallBottomBannerView.OnAdLoaded -= this.HandleBottomAdLoaded;
        this.smallbannerView.OnAdLeavingApplication -= this.HandleAdLeftApplication;

    }

    #endregion

    #region LargeBanner callback handlers

    public void HandleLargeBannerAdLoaded(object sender, EventArgs args)
    {
        LargeBannerOnceLoaded = true;
    }

    /*     public void HandleLargeBannerAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {

        } */
    /*
        public void HandleLargeBannerAdOpened(object sender, EventArgs args)
        {

        }

          public void HandleLargeBannerAdClosed(object sender, EventArgs args)
            {

            } */

    public void HandleLargeBannerAdLeftApplication(object sender, EventArgs args)
    {
        this.largebannerView.OnAdLoaded -= this.HandleLargeBannerAdLoaded;
        //this.largebannerView.OnAdFailedToLoad -= this.HandleLargeBannerAdFailedToLoad;
        //this.largebannerView.OnAdOpening -= this.HandleLargeBannerAdOpened;
        //this.largebannerView.OnAdClosed -= this.HandleLargeBannerAdClosed;
        this.largebannerView.OnAdLeavingApplication -= this.HandleLargeBannerAdLeftApplication;
    }

    #endregion

    #region Interstitial callback handlers

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {

    }

    /*     public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {

        } */

    /* 	public void HandleInterstitialOpened(object sender, EventArgs args)
        {

        } */

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {

        this.interstitial.OnAdLoaded -= this.HandleInterstitialLoaded;
        //this.interstitial.OnAdFailedToLoad -= this.HandleInterstitialFailedToLoad;
        //this.interstitial.OnAdOpening -= this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed -= this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication -= this.HandleInterstitialLeftApplication;
    }

    #endregion
    //==================================================================================================================//

    //=============================================  Unity Ads  ========================================================//

    public void InitUnityAds()
    {
        string gameId = null;
        gameId = unityAdID;

        if (string.IsNullOrEmpty(gameId))
        {
            // Make sure the Game ID is set.
            Debug.LogError("Failed to initialize Unity Ads. Game ID is null or empty.");
        }
        else if (!Monetization.isSupported)
        {
            Debug.LogWarning("Unable to initialize Unity Ads. Platform not supported.");
        }
        else if (Monetization.isInitialized)
        {
            Debug.Log("Unity Ads is already initialized.");
        }
        else
        {
            Monetization.Initialize(gameId, enableTestMode);

        }
    }


    public void ShowUnityVideoAd()
    {
        if (Monetization.IsReady("video"))
        {
            ShowAdPlacementContent uAd = Monetization.GetPlacementContent("video") as ShowAdPlacementContent;
            if (uAd != null)
                uAd.Show();
        }

    }
    public bool isRewardedReady()
    {
        bool isCheck = false;
        if (IsInternetConnection())
        {
            if (CheckInitialization())
            {
                if (Monetization.IsReady("rewardedVideo"))
                    isCheck = true;
            }

        }
        return isCheck;
    }
    public void ShowUnityRewardedVideoAd()
    {
        if (Monetization.IsReady("rewardedVideo"))
        {
            // LevelValues.instance.isSkipWithCoins = false;
            /*             ShowOptions options = new ShowOptions();
                        options.resultCallback = HandleShowResult;
                        Advertisement.Show("rewardedVideo", options); */

            ShowAdCallbacks options = new ShowAdCallbacks();
            options.finishCallback = HandleShowResult;
            ShowAdPlacementContent uVad = Monetization.GetPlacementContent("rewardedVideo") as ShowAdPlacementContent;
            if (uVad != null)
            {
                uVad.Show(options);
            }
        }
    }
    void Reward()
    {        
        if (MainMenuScript.RewardedVideo == 1)
        {
            MainMenuScript.Instance.GetReward100();            
        }
        else if (MainMenuCode.RewardVideo == 2)
        {
            LevelManager.Instance.x2Vedio();
        }
    }
    private void HandleShowResult(ShowResult result)
    {

        switch (result)
        {
            case ShowResult.Finished:
                {
                    Reward();
                }
                break;
            case ShowResult.Skipped:
                //Debug.LogWarning("Video was skipped.");
                break;
            case ShowResult.Failed:
                Debug.LogError("Video failed to show.");
                break;
        }
    }

    //==================================================================================================================//

    //=============================================  Priority Ads  =====================================================//
    public void Show_Admob_IronSource_Interstitial()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK").Equals(0))
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    if (this.interstitial.IsLoaded())
                    {
                        this.interstitial.Show();
                    }
                }
            }

            else
            {
          //      ShowInterstitialIronSource();
            }
        }
    }

    public void Show_IronSource_Unity_Admob()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK").Equals(0))
        {
         if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    if (Monetization.IsReady("video"))
                    {
                        ShowAdPlacementContent uAd = Monetization.GetPlacementContent("video") as ShowAdPlacementContent;
                        if (uAd != null)
                            uAd.Show();
                    }
                    else
                    {
                        ShowInterstitial();
                    }
                }
            }
        }
    }

    public void Show_Unity()
    {
        ShowUnityVideoAd();
    }
    public void Show_Unity_Admob()
    {
        if (PlayerPrefs.GetInt("ADSUNLOCK").Equals(0))
        {
            if (IsInternetConnection())
            {
                if (CheckInitialization())
                {
                    if (Monetization.IsReady("video"))
                    {
                        ShowAdPlacementContent uAd = Monetization.GetPlacementContent("video") as ShowAdPlacementContent;
                        if (uAd != null)
                            uAd.Show();
                    }
                    else
                    {
                        ShowInterstitial();
                    }
                }
            }
        }
    }
    public void RemoveAdmobAds()
    {
        DestroySmallBanner();
        DestroyBottomBanner();
        DestroyLargeBanner();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("currentUnlockedsling", 0);
        RemoveAdmobAds();
    }
}
