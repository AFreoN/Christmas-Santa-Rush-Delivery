using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class CollionEnter : MonoBehaviour {
    public Text SpinDeilyCoins;
    public GameObject SpinRing, spinhandler;
    public Button SpinButton;
    [HideInInspector]
    public float spinspeed, spinangle = 0;
    bool SpinCheck, SpinStopcheck = true;
    int day,Hourcur, Mintcur, scecur;
    public Text ReminingTime,NextDay,ClamText,CLIKtEXT;
    bool ONCE;
    // Use this for initialization
    private void Start()
    {
       // PlayerPrefs.DeleteKey("CurrentDay");
        //SpinButton.interactable = false;
        //SpinButton.GetComponent<Animator>().enabled = false;
        if (!PlayerPrefs.HasKey("CurrentDay"))
        {
            PlayerPrefs.SetInt("timetoSpin",1);
       
            day = int.Parse(System.DateTime.Now.Day.ToString());
            PlayerPrefs.SetInt("CurrentDay", day);
            SpinButton.interactable = true;
            SpinButton.GetComponent<Animator>().enabled = true;
            CLIKtEXT.gameObject.SetActive(true);
            PlayerPrefs.SetInt("SpinDay", day-1);
            ONCE = true;
        }
        
        day = int.Parse(System.DateTime.Now.Day.ToString());
        if (day > PlayerPrefs.GetInt("CurrentDay"))
        {
            SpinButton.interactable = true;
            SpinButton.GetComponent<Animator>().enabled = true;
            PlayerPrefs.SetInt("CurrentDay", day);
            ONCE = true;

        }
        if (SpinButton.interactable == true) {
            int curenday = (day) - PlayerPrefs.GetInt("SpinDay");
            if (curenday >7)
            {
                curenday = 1;
                PlayerPrefs.SetInt("SpinDay", 1);
            }
            NextDay.text = curenday.ToString() + " : DAY REWARD";
            CLIKtEXT.gameObject.SetActive(true);
        }
        else
        {
            CLIKtEXT.gameObject.SetActive(false);
            int curenday = (day + 1) - PlayerPrefs.GetInt("SpinDay");
            NextDay.text = curenday.ToString() + " : DAY REWARD";
        }
    }
    private void Update()
    {
        if (spinspeed > -0.3f && !SpinCheck)
        {
            spinspeed -= 0.01f;
        }
        else
        if (spinspeed < 0.3f && !SpinStopcheck)
        {
            SpinCheck = true;
            spinspeed += 0.01f;
        }
        else
        {
            SpinStopcheck = true;
            spinhandler.GetComponent<BoxCollider>().enabled = true;
        }

        SpinRing.transform.Rotate(new Vector3(0, 0, spinangle) * spinspeed * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        if (!SpinButton.interactable) {
            Hourcur = int.Parse(System.DateTime.Now.Hour.ToString());
            Mintcur = int.Parse(System.DateTime.Now.Minute.ToString());
            scecur = int.Parse(System.DateTime.Now.Second.ToString());
            Hourcur = 24 - Hourcur;
            Mintcur = 60 - Mintcur;
            scecur = 60 - scecur;
            string ReMinig = Hourcur.ToString() + ":" + Mintcur.ToString() + ":" + scecur.ToString();
            ReminingTime.text = ReMinig;
           
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetChild(0).gameObject.GetComponent<Text>().text == SpinDeilyCoins.GetComponent<Text>().text && ONCE) {
            spinspeed = 0.0f;
            SpinDeilyCoins.gameObject.SetActive(true);
            other.transform.GetChild(0).gameObject.SetActive(false);
            MainMenuScript.Instance.ClamPanalFun();
            spinhandler.GetComponent<BoxCollider>().enabled = false;
            SpinButton.interactable = false;
            int curenday = (day + 1) - PlayerPrefs.GetInt("SpinDay");
            NextDay.text = curenday.ToString() + " : DAY REWARD";
            ONCE = false;
        }
        
    }
    public void spin(Animator ANIM)
    {
        ANIM.enabled = false;
        spinangle = 360;
        spinhandler.GetComponent<BoxCollider>().enabled = false;
        spinspeed = Random.Range(1f, 5f);
        SpinCheck = SpinStopcheck = false;
        SpinDeilyCoins.gameObject.SetActive(false);
        CLIKtEXT.gameObject.SetActive(false);
        
        if (PlayerPrefs.GetInt("timetoSpin") == 1)
        {
            PlayerPrefs.SetInt("timetoSpin", PlayerPrefs.GetInt("timetoSpin")+1);
            SpinDeilyCoins.text = "50";
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + int.Parse(SpinDeilyCoins.text.ToString()));
        }
        else
        if(PlayerPrefs.GetInt("timetoSpin") == 2)
        {
            PlayerPrefs.SetInt("timetoSpin", PlayerPrefs.GetInt("timetoSpin") + 1);
            SpinDeilyCoins.text = "100";
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + int.Parse(SpinDeilyCoins.text.ToString()));
        }
        else
        if (PlayerPrefs.GetInt("timetoSpin") == 3)
        {
            PlayerPrefs.SetInt("timetoSpin", PlayerPrefs.GetInt("timetoSpin") + 1);
            SpinDeilyCoins.text = "50";
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + int.Parse(SpinDeilyCoins.text.ToString()));
        }
        else
        if (PlayerPrefs.GetInt("timetoSpin") == 4)
        {
            PlayerPrefs.SetInt("timetoSpin", PlayerPrefs.GetInt("timetoSpin") + 1);
            SpinDeilyCoins.text = "200";
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + int.Parse(SpinDeilyCoins.text.ToString()));
        }
        else
        if (PlayerPrefs.GetInt("timetoSpin") == 5)
        {
            PlayerPrefs.SetInt("timetoSpin", PlayerPrefs.GetInt("timetoSpin") + 1);
            SpinDeilyCoins.text = "300";
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + int.Parse(SpinDeilyCoins.text.ToString()));
        }
        else
        if (PlayerPrefs.GetInt("timetoSpin") == 6)
        {
            PlayerPrefs.SetInt("timetoSpin", PlayerPrefs.GetInt("timetoSpin") + 1);
            SpinDeilyCoins.text = "500";
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + int.Parse(SpinDeilyCoins.text.ToString()));
        }
        else
        if (PlayerPrefs.GetInt("timetoSpin") == 4)
        {
            PlayerPrefs.SetInt("timetoSpin", 1);
            SpinDeilyCoins.text = "1000";
            PlayerPrefs.SetInt("TotalSP", PlayerPrefs.GetInt("TotalSP") + int.Parse(SpinDeilyCoins.text.ToString()));
        }
        ClamText.text = SpinDeilyCoins.text;
        
        MainMenuScript.Instance.ScoreText.text = PlayerPrefs.GetInt("TotalSP").ToString();
    }
}
