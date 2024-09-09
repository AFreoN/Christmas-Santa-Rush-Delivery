using UnityEngine;
using UnityEngine.UI;

public class GameTime : MonoBehaviour
{
    public Text TimeR;
    public int Seconds = 5;
    public float at = 0;
   
    void OnEnable()
    {
      
        TimeR.text = System.String.Empty;
        Time.timeScale = 1;
    }

    void Update()
    {
        at += Time.deltaTime;
        if (at >= 1.0f && LevelManager.Instance.OnlyOnce)
        {
          TimeR.text = formateTime();
          Seconds--;
          at = 0;
        }
        if (Seconds <= -2f)
        {
            LevelManager.Instance.ShowLevelFailed();
        }
    }
    string formateTime()
    {
        int min = Seconds / 60;
        int secs = Seconds % 60;
        return min.ToString("D2") + ":" + secs.ToString("D2");

    }

}

