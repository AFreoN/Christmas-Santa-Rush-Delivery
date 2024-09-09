using UnityEngine;

public class ArrowDirection : MonoBehaviour
{
    public static ArrowDirection Instance;
    public GameObject[] Arrows;
    int getLevel, getLevel2;

    private void Start()
    {
        Instance = this;
    }
    public void ArrowOn()
    {
        getLevel = PlayerPrefs.GetInt("LevelIndex");
        Arrows[getLevel].SetActive(true);
    }
    public void ArrowOn2()
    {
        getLevel2 = PlayerPrefs.GetInt("LevelIndex2");
        Arrows[1].SetActive(true);
    }
}
