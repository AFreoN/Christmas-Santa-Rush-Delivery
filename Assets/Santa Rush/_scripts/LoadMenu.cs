using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class LoadMenu : MonoBehaviour
{
    private void Awake()
    {
        Analytics.CustomEvent("Version 16");
    }
    void Start()
    {        
        SceneManager.LoadScene("MainMenu");
    }
}
