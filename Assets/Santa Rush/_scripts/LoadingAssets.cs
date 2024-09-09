using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingAssets : MonoBehaviour
{
    void Awake()
    {
        Invoke("Load", 1f);        
    }
    void Load()
    {        
        SceneManager.LoadScene("Load");
    }
}
