using UnityEngine;
using System.Collections;
public class LevelPlayScript : MonoBehaviour
{
    public GameObject MapCamera, LevelComplete;
    private bool isCalPanel = false;
    // Use this for initialization
    public static LevelPlayScript instance;
    void Start()
    {
        Invoke("SetCallPanelActive", 6);
        instance = this;
    }

    public void SetCallPanelActive()
    {
        if ( !isCalPanel)
        {
            RCC_CarControllerV3.instance.KillEngine();
            isCalPanel = true;
            LevelComplete.SetActive(true);
            LevelManager.Instance.CallPanelStatus(true);
        }
    }
    public void RiceveOnResume(){
         RCC_CarControllerV3.instance.StartEngine(true);
        isCalPanel = false;
        MapCamera.SetActive(false);
        LevelManager.Instance.callpanel.SetActive(false);
        LevelComplete.SetActive(false);
        
        this.gameObject.SetActive(false);
       
        Invoke("SetCallPanelActive", 4.0f);
    }
    public void Receive()
    {
        RCC_CarControllerV3.instance.StartEngine(true);
        MapCamera.SetActive(false);
        LevelManager.Instance.CallPanelStatus(false);
        LevelManager.Instance.DisplayTime();
    }
    public void Busy()
    {
        RCC_CarControllerV3.instance.StartEngine(true);
        isCalPanel = false;
        MapCamera.SetActive(false);
        LevelManager.Instance.CallPanelStatus(false);
        LevelComplete.SetActive(false);
        
        Invoke("SetCallPanelActive", 6);
    }
    void SetActiveNextLevel()
    {
        LevelManager.Instance.LoadingPanelStatus(false);
    }
}
