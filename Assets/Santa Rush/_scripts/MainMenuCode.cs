using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
public class MainMenuCode : MonoBehaviour
{    
    public static MainMenuCode instance;
    bool OnlyOnce = true;
    public GameObject[] levelLocks, MyCarts, buy, CartsDetails;
    public GameObject InappBack, SelectBtn,UnlockLevelsButton, UnlockCarsButton, RemoveAdButton, ManinmanuPanal,PoPPanal,GragPanal,InAppPanal,Cart1buy,CartBuy,ClamPanal,GotoStore,Buyit,Purchesetext,LoadingPannel, menuCanvas, gamePlayCanvas, environment, SelectCart, BuyCart, soundOn, soundOff, soundOn_Gplay, soundOff_Gplay,levelPanal;
    public Image[] Stars;
    public Button Ratebutton,nextBtn,BackBtn;
    public Text RateUsText, SCTotalText, CartPriceText;
    public AudioClip Click;
    public AudioSource JingaleBell,BGSound;
    public static bool isShowMenu = true;
    public static bool CameMove = false;
    public GameObject Levels, Carts,Spinbtn;
    public static int index,InAppIdex, Cartindex, y = 0;
    public int[] CartsPrice;
    public Slider Acccleration, Speed, Handling;
    public static int DefultCarts = 0;
    float loodingTime;

    bool NextButton;
    public static int RewardVideo, CartChk;
    void Awake()
    {
        Analytics.CustomEvent("Version 14");
        if (CartChk == 0)
        { 
        Cartindex = 4;            
        }
        else if (CartChk == 1)
        {
            Cartindex = PlayerPrefs.GetInt("CartSelect");
            MyCarts[Cartindex].SetActive(true);
        }
        NextButton = true;
        loodingTime = 3f;
        // PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("RateUs",0);
        instance = this;
        environment.SetActive(true);        
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.SetInt("PoP", 1);
            PlayerPrefs.SetInt("PoP2", 1);
        }
        if (PlayerPrefs.GetInt("RateUs") == 1)
        {
          Ratebutton.interactable = false;
          RateUsText.color = new Color32(255,0,0,100);
        }
        SCTotalText.text = PlayerPrefs.GetInt("TotalSP").ToString();
        if (!PlayerPrefs.HasKey("Levels"))
        {
            PlayerPrefs.SetInt("Levels", 0);
            Levels.transform.GetChild(0).GetComponent<Animator>().enabled = true;
        }
        if (PlayerPrefs.GetInt("UNLOCKLEVELS") == 1)
        {
            PlayerPrefs.SetInt("Levels", Levels.transform.childCount-1);
        }
       /* for (int i = 0; i <= PlayerPrefs.GetInt("Levels"); i++)
        {
            levelLocks[i].GetComponent<Button>().interactable = true;
            levelLocks[i].GetComponent<Button>().interactable = true;
        }
        */
        for (int i = 0;i< Levels.transform.childCount;i++)
        {
            if (i <= PlayerPrefs.GetInt("Levels"))
            {
               Levels.transform.GetChild(i).GetComponent<Animator>().enabled = false;
               Levels.transform.GetChild(i).GetComponent<Button>().interactable = true;
               Levels.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
        Levels.transform.GetChild(PlayerPrefs.GetInt("Levels")).GetComponent<Animator>().enabled = true;
        PlayerPrefs.SetInt("Carts0", 1);
     //   CartPriceText.text = "FREE";
        if (DefultCarts == 1) {
            for (int i = 0; i < Carts.transform.childCount; i++)
            {
                if (PlayerPrefs.GetInt("Carts" + i) == 1)
                {
                    for (int s = 0; s < Carts.transform.childCount; s++)
                    {
                        if (s != i)
                        {
                            Carts.transform.GetChild(s).gameObject.SetActive(false);
                        }
                    }
                    Carts.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            Play();
            loodingTime = 0.02f;
        }
        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            Ads_Manager.Instance.ShowSmallAdmobBanner();
        }
        Ads_Manager.Instance.HideLargeAdmobBanner();
        // PlayerPrefs.SetInt("TotalSP", 50000);
        StartWork();
    }
    public void CPRewardVideo()
    {
        RewardVideo = 2;
        Ads_Manager.Instance.ShowUnityRewardedVideoAd();
    }
    public void SPVideo100()
    {
        PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + 100);
        SCTotalText.text = PlayerPrefs.GetInt("TotalSP").ToString();
    }
    public void StartWork()
    {
        print(Cartindex);
        if (PlayerPrefs.GetInt("ADSUNLOCK")== 1)
        { 
            RemoveAdButton.SetActive(false);
            Ads_Manager.Instance.HideSmallAdmobBanner();            
        }
        if (PlayerPrefs.GetInt("CartsUnlock")== 1)
        {
            UnlockCarsButton.SetActive(false);
            PoPPanal.SetActive(false);
        }
        if (PlayerPrefs.GetInt("UNLOCKLEVELS") == 1)
        {
            UnlockLevelsButton.SetActive(false);
        }
    }
    void OnEnable()
    {
        OnlyOnce = true;
        if (isShowMenu)
        {
            isShowMenu = false;
            menuCanvas.SetActive(true);
            gamePlayCanvas.SetActive(false);
        }
        else
        {
            menuCanvas.SetActive(false);
            gamePlayCanvas.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Music").Equals(0))
        {
            soundOn.SetActive(false);
            soundOn_Gplay.SetActive(false);
            soundOff.SetActive(true);
            soundOff_Gplay.SetActive(true);
            AudioListener.volume = 0;
        }
        else
        {
            soundOn.SetActive(true);
            soundOn_Gplay.SetActive(true);
            soundOff.SetActive(false);
            soundOff_Gplay.SetActive(false);
            AudioListener.volume = 1;
        }
       
    }
    void Update()
    {
        if(NextButton)
        {
            NextCartShow();            
        }      
    }
    
    public void NextCartShow()
    {
        foreach (GameObject i in MyCarts)
        {
            i.SetActive(false);
        }
        MyCarts[Cartindex].SetActive(true);
        foreach (GameObject CD in CartsDetails)
        {
            CD.SetActive(false);
        }
        CartsDetails[Cartindex].SetActive(true);

        if (PlayerPrefs.GetInt(Cartindex.ToString()) == Cartindex)
        {
            buy[Cartindex].SetActive(false);
            SelectBtn.SetActive(true);           
        }
        else
        {
            MyCarts[Cartindex].SetActive(true);
            if (PlayerPrefs.GetInt("1Cart") == 1 && Cartindex == 1)
            {
                SelectBtn.SetActive(true);
                buy[1].SetActive(false);                
            }
            else if (PlayerPrefs.GetInt("1Cart") != 1 && Cartindex == 1)
            {
                buy[1].SetActive(true);
                SelectBtn.SetActive(false);                
            }
            if (PlayerPrefs.GetInt("2Cart") == 2 && Cartindex == 2)
            {
                SelectBtn.SetActive(true);
                buy[2].SetActive(false);                
            }
            else if (PlayerPrefs.GetInt("2Cart") != 2 && Cartindex == 2)
            {
                buy[2].SetActive(true);
                SelectBtn.SetActive(false);                
            }
            if (PlayerPrefs.GetInt("3Cart") == 3 && Cartindex == 3)
            {
                SelectBtn.SetActive(true);
                buy[3].SetActive(false);
                
            }
            else if (PlayerPrefs.GetInt("3Cart") != 3 && Cartindex == 3)
            {
                buy[3].SetActive(true);
                SelectBtn.SetActive(false);
                
            }
            if (PlayerPrefs.GetInt("4Cart") == 4 && Cartindex == 4)
            {
                SelectBtn.SetActive(true);
                buy[4].SetActive(false);
                
            }
            else if (PlayerPrefs.GetInt("4Cart") != 4 && Cartindex == 4)
            {
                buy[4].SetActive(true);
                SelectBtn.SetActive(false);
                
            }

            if (PlayerPrefs.GetInt("5Cart") == 5 && Cartindex == 5)
            {
                SelectBtn.SetActive(true);
                buy[5].SetActive(false);
                
            }
            else if (PlayerPrefs.GetInt("5Cart") != 5 && Cartindex == 5)
            {
                buy[5].SetActive(true);
                SelectBtn.SetActive(false);
                
            }

            if (PlayerPrefs.GetInt("6Cart") == 6 && Cartindex == 6)
            {
                SelectBtn.SetActive(true);
                buy[6].SetActive(false);
                
            }
            else if (PlayerPrefs.GetInt("6Cart") != 6 && Cartindex == 6)
            {
                buy[6].SetActive(true);
                SelectBtn.SetActive(false);
                
            }
        }

        if (PlayerPrefs.GetInt("CartsUnlock") == 1)
        {
            foreach (GameObject BC in buy)
            {
                BC.SetActive(false);
                SelectBtn.SetActive(true);
            }
        }

        NextButton = false;
    }
    public void UnlockCartsNew()
    {
        if (Cartindex == 1 && PlayerPrefs.GetInt("TotalSP") >= 15000)
        {
            PlayerPrefs.SetInt("1Cart", 1);
            buy[Cartindex].SetActive(false);
            SelectBtn.SetActive(true);
            
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") - 15000);
            SCTotalText.text = PlayerPrefs.GetInt("TotalSP").ToString();         
        }            
        else
        {
            InAppPanal.SetActive(true);
            InappBack.SetActive(true);
        }
    }
    public void UnlockCartsNew2()
    {
        if (Cartindex == 2 && PlayerPrefs.GetInt("TotalSP") >= 25000)
        {
            PlayerPrefs.SetInt("2Cart", 2);
            buy[Cartindex].SetActive(false);
            SelectBtn.SetActive(true);
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") - 25000);
            SCTotalText.text = PlayerPrefs.GetInt("TotalSP").ToString();
        }
        else
        {
            InAppPanal.SetActive(true);
            InappBack.SetActive(true);
        }
    }

    public void NextCart()
    {
        if (Cartindex >= MyCarts.Length - 1)
            Cartindex = 0;
        else
            Cartindex++;
        for (y = 0; y < buy.Length; y++)
        {
            buy[y].SetActive(false);            
        }

        NextButton = true;
        SelectBtn.SetActive(true);
        PlayerPrefs.SetInt("CartSelect", Cartindex);        
    }
    public void PreviousCart()
    {

        if (Cartindex <= 0)
            Cartindex = MyCarts.Length - 1;
        else

            Cartindex--;
        for (y = 0; y < buy.Length; y++)
        {
            buy[y].SetActive(false);            
        }

        NextButton = true;
        SelectBtn.SetActive(true);
        PlayerPrefs.SetInt("CartSelect", Cartindex);        
    }
    private void FixedUpdate()
    {
        if (!ManinmanuPanal.activeSelf && Spinbtn.activeSelf)
        {
            Spinbtn.SetActive(false);
        }
    }
    public void SelectLevel(int index)
    {
        PlayerPrefs.SetInt("LevelIndex", index);
        CartChk = 1;
    }
     void FillStar(){
    
    for(int i = 0;i < Stars.Length;i++){
       if (index == i)
       {
          Stars[i].color = Color.yellow; 
       }
    }
    if (index < Stars.Length)
    {
        index++;
    }else
    {
        for(int i = 0;i< Stars.Length;i++){
          Stars[i].color = Color.white; 
        }
       index = 0; 
    }
}
    public void StarUp(int num){
    CancelInvoke("FillStar");
 for(int i = 0;i< Stars.Length;i++){
     if (num >= i)
     {
         Stars[i].color = Color.yellow;
     }else
     {
         Stars[i].color = Color.white;
     }
           
    }
}
    public void StarDown(int num){
CancelInvoke("FillStar");
 for(int i = 0;i< Stars.Length;i++){
     if (num >= i)
     {
         Stars[i].color = Color.yellow;
     }else
     {
         Stars[i].color = Color.white;
     }
           
    }
} 
    public void CrossRateUs(){
    CancelInvoke("FillStar");
    index = 0; 
}
    public void SoundOn()
    {
        soundOn.SetActive(true);
        soundOn_Gplay.SetActive(true);
        soundOff.SetActive(false);
        soundOff_Gplay.SetActive(false);
        PlayerPrefs.SetInt("Music", 1);
        AudioListener.volume = 1;
    }
    public void SoundOff()
    {
        soundOn.SetActive(false);
        soundOn_Gplay.SetActive(false);
        soundOff.SetActive(true);
        soundOff_Gplay.SetActive(true);
        PlayerPrefs.SetInt("Music", 0);
        AudioListener.volume = 0;
    }
    public void LevelInapp()
    {
        if (PlayerPrefs.GetInt("UNLOCKLEVELS") == 1)
        {
            PlayerPrefs.SetInt("Levels", Levels.transform.childCount - 1);
        }
        for (int i = 0; i < Levels.transform.childCount; i++)
        {
            if (i <= PlayerPrefs.GetInt("Levels"))
            {
                Levels.transform.GetChild(i).GetComponent<Button>().interactable = true;
                Levels.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    public void Play()
    { 
        Loading_Gameplay();
        CartChk = 1;
        //if (Cartindex == 0 || Cartindex == 3)
        //{
        //    CameMove = true;
        //}
       
    }

    public void RateUs()
    {
       if (PlayerPrefs.GetInt("RateUs") != 1)
       {
           for(int i = 0;i< Stars.Length;i++){
           Stars[i].color = Color.white;
         }
           InvokeRepeating("FillStar",0.5f,0.5f);
       }
        
    }
    public void RateUsDown(){
    PlayerPrefs.SetInt("RateUs",1);
    CancelInvoke("FillStar");
     Ratebutton.interactable = false;
     RateUsText.color = new Color32(255,0,0,100);
}
    public void RateUsUP()
    {
        PlayerPrefs.SetInt("RateUs",1);
        Ratebutton.interactable = false;
        RateUsText.color = new Color32(255,0,0,100);
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.door.santa.claus.gift.delivery.christmas");
    }
    public void More()
    {
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Door+to+apps");
    }

    public void PrivacyPolicy()
    {
        Application.OpenURL("https://door2appslive.wordpress.com/privacy-policy/");
    }

    public void Loading_Gameplay()
    {
        if (OnlyOnce)
        {
            OnlyOnce = false;
        }
        levelPanal.SetActive(false);
        LoadingPannel.SetActive(true);
        StartCoroutine(Load_Play());
        //Invoke("Gameplay", 0.5f);

    }
     IEnumerator Load_Play()

    {
        if (DefultCarts != 1)
       {
            Ads_Manager.Instance.ShowInterstitial();
            yield return new WaitForSeconds(loodingTime);
        }
        Gameplay();
     
    }
    public void Gameplay()
    {
        menuCanvas.SetActive(false);
        gamePlayCanvas.SetActive(true);
    }
    public void Yes()
    {
        Application.Quit();
    }
    public void No()
    {

        OnlyOnce = true;
        Ads_Manager.Instance.HideLargeAdmobBanner();
        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            Ads_Manager.Instance.ShowSmallAdmobBanner();
        }
    }
    public void Exit()
    {
        if (OnlyOnce)
        {

            OnlyOnce = false;
        }
        Ads_Manager.Instance.HideSmallAdmobBanner();
        Ads_Manager.Instance.ShowInterstitial();
        Ads_Manager.Instance.ShowLargeAdmobBanner();        
    }

    public void Bootle_Shoot()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.door.glass.bottle.shooter.expert&referrer=utm_source=Santa_Sim");
    }
    public void Modern_Car()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.gf.real.car.impossible.apps&referrer=utm_source=Santa.Door");
    }
    public void TopHighwayRacer()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.door.modren.car.traffic.apps&referrer=utm_source=Santa.Door");
    }
    public void InHouseAd(string URl)
    {
        Application.OpenURL(URl);
    }
    public void Next(bool next)
    {
        if (next)
        {
            if (Cartindex < Carts.transform.childCount-1)
            {
                Cartindex++;
            }
            else
            {
                Cartindex = 0;
            }
            Carts.transform.GetChild(Cartindex).transform.position = new Vector3(245f, 0.4f, -14.3f);
            InvokeRepeating("Next", 0.02f, 0.02f);
        }
        else
        {
            if (Cartindex != 0)
            {
                Cartindex--;
            }
            else
            {
                Cartindex = Carts.transform.childCount-1;
            }
            Carts.transform.GetChild(Cartindex).transform.position = new Vector3(160.3f, 0.4f, -14.3f);
            InvokeRepeating("Back", 0.02f, 0.02f);
        }
          Carts.transform.GetChild(Cartindex).gameObject.SetActive(true);
        if (!CartPriceText.transform.GetChild(0).gameObject.activeSelf)
        {//coins ion ture false when last two cart open
            CartPriceText.transform.GetChild(0).gameObject.SetActive(true);
            CartBuy.SetActive(false);
            Cart1buy.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Carts" + Cartindex) == 1)
        {
            SelectCart.SetActive(true);
            BuyCart.SetActive(false);
            CartBuy.SetActive(false);
            Cart1buy.SetActive(false);
            CartPriceText.text = "FREE";
            if (Cartindex == CartsPrice.Length - 2)
            {
                CartPriceText.text = "Vixen Deer Cart";
                CartPriceText.transform.GetChild(0).gameObject.SetActive(false);
            }
            if (Cartindex == CartsPrice.Length - 1)
            {
                CartPriceText.text = "Spencer Deer Cart";
                CartPriceText.transform.GetChild(0).gameObject.SetActive(false);
            }
                
        }
        else if(PlayerPrefs.GetInt("CartsUnlock") != 1)
        {
            CartBuy.SetActive(false);
            Cart1buy.SetActive(false);
            BuyCart.SetActive(true);
            SelectCart.SetActive(false);
            if (Cartindex != CartsPrice.Length-1 && Cartindex != CartsPrice.Length - 2) {
                CartPriceText.text = CartsPrice[Cartindex].ToString();
            }
            else
            {
                if (Cartindex == CartsPrice.Length - 1) {
                    CartBuy.SetActive(true);
                    Cart1buy.SetActive(false);
                    SelectCart.SetActive(false);
                    BuyCart.SetActive(false);
                }
                else
                {
                    SelectCart.SetActive(false);
                    BuyCart.SetActive(false);
                    CartBuy.SetActive(false);
                    Cart1buy.SetActive(true);
                }
            }
            if (Cartindex == CartsPrice.Length - 2)
            {
                CartPriceText.text = "Vixen Deer Cart";
                CartPriceText.transform.GetChild(0).gameObject.SetActive(false);
            }
            if (Cartindex == CartsPrice.Length - 1)
            {
                CartPriceText.text = "Spencer Deer Cart";
                CartPriceText.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        
        Acccleration.value = 0.1f * (Cartindex+1);
        Speed.value = 0.15f * (Cartindex+1);
        Handling.value = 0.2f*(Cartindex+1);
        nextBtn.interactable = BackBtn.interactable = false;
       
    }
     void Next()
    {
        float Dis = Vector3.Distance(Carts.transform.GetChild(Cartindex).transform.position, new Vector3(205f, 0.4f, -14.3f));
        if (Dis < 1f)
        {
            if (Cartindex != 0)
            {
                Carts.transform.GetChild(Cartindex - 1).gameObject.SetActive(false);
            }
            else
            {
                Carts.transform.GetChild(Carts.transform.childCount - 1).gameObject.SetActive(false);
            }
            nextBtn.interactable = BackBtn.interactable = true;
            CancelInvoke("Next");
        }
            Carts.transform.GetChild(Cartindex).transform.position = Vector3.MoveTowards(Carts.transform.GetChild(Cartindex).transform.position, new Vector3(205f, 0.4f, -14.3f), 30 * Time.deltaTime);
       
            if (Cartindex != 0)
            {
                Carts.transform.GetChild(Cartindex - 1).transform.position = Vector3.MoveTowards(Carts.transform.GetChild(Cartindex - 1).transform.position, new Vector3(160f, 0.4f, -14.3f), 30 * Time.deltaTime);
            }
            else
            {
                Carts.transform.GetChild(Carts.transform.childCount - 1).transform.position = Vector3.MoveTowards(Carts.transform.GetChild(Carts.transform.childCount - 1).transform.position, new Vector3(160f, 0.4f, -14.3f), 30 * Time.deltaTime);
            }
    }
     void Back()
    {
        float Dis = Vector3.Distance(Carts.transform.GetChild(Cartindex).transform.position, new Vector3(205f, 0.4f, -14.3f));
        if (Dis < 1f)
        {
            if (Cartindex != Carts.transform.childCount - 1)
            {
                Carts.transform.GetChild(Cartindex + 1).gameObject.SetActive(false);
            }
            else
            {
                Carts.transform.GetChild(0).gameObject.SetActive(false);
            }
            nextBtn.interactable = BackBtn.interactable = true;
            CancelInvoke("Back");
        }
        Carts.transform.GetChild(Cartindex).transform.position = Vector3.MoveTowards(Carts.transform.GetChild(Cartindex).transform.position, new Vector3(205f, 0.4f, -14.3f), 30 * Time.deltaTime);
        if (Cartindex != Carts.transform.childCount - 1)
        {
            Carts.transform.GetChild(Cartindex + 1).transform.position = Vector3.MoveTowards(Carts.transform.GetChild(Cartindex + 1).transform.position, new Vector3(245f, 0.4f, -14.3f), 30 * Time.deltaTime);
        }
        else
        {
            Carts.transform.GetChild(0).transform.position = Vector3.MoveTowards(Carts.transform.GetChild(0).transform.position, new Vector3(245f, 0.4f, -14.3f), 30 * Time.deltaTime);
        }
    }
    public void BuyCarts() {
            
        if (CartsPrice[Cartindex] < PlayerPrefs.GetInt("TotalSP"))
        {
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") - CartsPrice[Cartindex]);
            SCTotalText.text = PlayerPrefs.GetInt("TotalSP").ToString();
            CartPriceText.text = "FREE";
            PlayerPrefs.SetInt("Carts" + Cartindex, 1);
            SelectCart.SetActive(true);
            BuyCart.SetActive(false);
            Buyit.SetActive(false);
            CartPriceText.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            GotoStore.SetActive(true);
            Buyit.SetActive(false);
        }
    }
    public void ClamPanalFun()
    {
        ClamPanal.SetActive(true);
        //StartCoroutine(GameObjectTrueFalse(ClamPanal));
    }
    public IEnumerator GameObjectTrueFalse(GameObject Panal)
    {
        yield return new WaitForSeconds(1);
        Panal.SetActive(true);
        yield return new WaitForSeconds(4);
        Panal.SetActive(false);
    }
    public void InnAppFun()
    {
        if (InAppIdex == 0) {
            InAppPanal.SetActive(true);
            // InAppIdex = 1;

       //     Ads_Manager.Instance.HideSmallAdmobBanner();
        }
        else
        {
            InAppIdex = 0;
        }
    }
    public void PoPPOP1()
    {
        
        if (PlayerPrefs.GetInt("PoP") == 1)
        {
            if (PlayerPrefs.GetInt("CartsUnlock") != 1)
            {
                PoPPanal.SetActive(true);
            }
            PlayerPrefs.SetInt("PoP",3);

        }
        else
        {
            PlayerPrefs.SetInt("PoP", 1);
        }
        SelectCart.SetActive(true);
        BuyCart.SetActive(false);
        CartBuy.SetActive(false);
        Cart1buy.SetActive(false);
    }
    public void SpTotal()
    {
        SCTotalText.text = PlayerPrefs.GetInt("TotalSP").ToString();
    /*    if (PlayerPrefs.GetInt("Carts" + Cartindex) == 1 || PlayerPrefs.GetInt("CartsUnlock") == 1)
        {
            SelectCart.SetActive(true);
            BuyCart.SetActive(false);
            CartPriceText.text = "FREE";
        }
        */

        if (GragPanal.activeSelf)
        {
            Spinbtn.SetActive(false);
        }
        else
        {
            Spinbtn.SetActive(true);
        }
    }
    public void StartCartSet()
    {
        Spinbtn.SetActive(false);
        Cartindex = 0;
        PlayerPrefs.SetInt("CartSelect", Cartindex);
        NextButton = true;
        foreach (GameObject BB in buy)
        {
            BB.SetActive(false);
        }
        /*    for (int i = 0; i < Carts.transform.childCount; i++)
            {
                if (i == 0)
                {
                    SelectCart.SetActive(true);
                    BuyCart.SetActive(false);
                    CartPriceText.text = "FREE";
                    Carts.transform.GetChild(i).gameObject.SetActive(true);
                    Carts.transform.GetChild(i).transform.position = new Vector3(205f, 0.4f, -14.3f);
                }
                else
                {
                    Carts.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
            */
    }
    public void RESettCartSet()
    {
        Spinbtn.SetActive(true);
        Cartindex = 0;
            for (int i = 0; i < Carts.transform.childCount; i++)
            {
                if (i == 2)
                {
                    Carts.transform.GetChild(i).gameObject.SetActive(true);
             //       Carts.transform.GetChild(i).transform.position = new Vector3(205f, 0.4f, -14.3f);
                }
                else
                {
                    Carts.transform.GetChild(i).gameObject.SetActive(false);
                }
            }
    }
    public void clickSound()
    {
        AudioSource.PlayClipAtPoint(Click, transform.position);
    }
    public void RemoveAdsTest()
    {
        PlayerPrefs.SetInt("ADSUNLOCK", 1);
    }
    public void InappCartsNew()
    {        
        buy[Cartindex].SetActive(false);
        SelectBtn.SetActive(true);    
    }
}

