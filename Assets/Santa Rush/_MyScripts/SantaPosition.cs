using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaPosition : MonoBehaviour
{
    public static SantaPosition Instance;
    public Transform Santa;
    public GameObject PlayerParent;

    int getLevel, getLevel2;
    private void Awake()
    {
        Instance = this;
        
    }
    private void Start()
    {
        if (PlayerPrefs.GetInt("Mode") == 1)
        {
            getLevel = PlayerPrefs.GetInt("LevelIndex");
        }
        else if (PlayerPrefs.GetInt("Mode") == 2)
        {
            getLevel2 = PlayerPrefs.GetInt("LevelIndex2");
        }
    }
    public void SetSantaPosition(int getLevel)
    {     
        Santa = PlayerParent.transform;
        switch (getLevel)
        {                       //0.273f old Y axis value
            case 1:
                Santa.transform.position = new Vector3(-570f, Santa.transform.position.y, 580.9f); //Santa.transform.position = new Vector3(220.78f, 0.2f, -15.68f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 2:
                Santa.transform.position = new Vector3(-556.36f, Santa.transform.position.y, 458.8f);//Santa.transform.position = new Vector3(8.19f, 0.2f, -215.3f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 3:
                Santa.transform.position = new Vector3(-556.45f, Santa.transform.position.y, 638.9f);//Santa.transform.position = new Vector3(239.29f, 0.2f, -57.75f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 4:
                Santa.transform.position = new Vector3(14.53f, Santa.transform.position.y, -28.64f);//Santa.transform.position = new Vector3(-108.45f, 0.2f, -73.4f);
                Santa.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case 5:
                Santa.transform.position = new Vector3(-835f, Santa.transform.position.y, 480.12f);//Santa.transform.position = new Vector3(-78.32f, 0.2f, 102.16f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 6:
                Santa.transform.position = new Vector3(-500.9f, Santa.transform.position.y, 471.85f);//Santa.transform.position = new Vector3(187.56f, 0.2f, 274.91f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 7:
                Santa.transform.position = new Vector3(-721.2f, Santa.transform.position.y, 506.87f);//Santa.transform.position = new Vector3(-146.59f, 0.2f, 101.22f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 8:
                Santa.transform.position = new Vector3(-644.59f, Santa.transform.position.y, 304.31f);//Santa.transform.position = new Vector3(26.08f, 0.2f, -356.8f);
                Santa.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case 9:
                Santa.transform.position = new Vector3(-701.85f, Santa.transform.position.y, 651.1f);//Santa.transform.position = new Vector3(100.23f, 0.2f, -72.89f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 10:
                Santa.transform.position = new Vector3(-512.76f, Santa.transform.position.y, 297.61f);//Santa.transform.position = new Vector3(-246.8f, 0.2f, 36.01f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 11:
                Santa.transform.position = new Vector3(-570f, Santa.transform.position.y, 576.45f); //Santa.transform.position = new Vector3(220.78f, 0.2f, -15.68f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 12:
                Santa.transform.position = new Vector3(-556.34f, Santa.transform.position.y, 458.8f);//Santa.transform.position = new Vector3(8.19f, 0.2f, -215.3f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 13:
                Santa.transform.position = new Vector3(-556.4f, Santa.transform.position.y, 638.9f);//Santa.transform.position = new Vector3(239.29f, 0.2f, -57.75f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 14:
                Santa.transform.position = new Vector3(14.57f, Santa.transform.position.y, -28.64f);//Santa.transform.position = new Vector3(-108.45f, 0.2f, -73.4f);
                Santa.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case 15:
                Santa.transform.position = new Vector3(-834.68f, Santa.transform.position.y, 480.12f);//Santa.transform.position = new Vector3(-78.32f, 0.2f, 102.16f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 16:
                Santa.transform.position = new Vector3(-500.9f, Santa.transform.position.y, 471.89f);//Santa.transform.position = new Vector3(187.56f, 0.2f, 274.91f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 17:
                Santa.transform.position = new Vector3(-721.2f, Santa.transform.position.y, 506.69f);//Santa.transform.position = new Vector3(-146.59f, 0.2f, 101.22f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 18:
                Santa.transform.position = new Vector3(-644.59f, Santa.transform.position.y, 304.5f);//Santa.transform.position = new Vector3(26.08f, 0.2f, -356.8f);
                Santa.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case 19:
                Santa.transform.position = new Vector3(-702.13f, Santa.transform.position.y, 651.1f);//Santa.transform.position = new Vector3(100.23f, 0.2f, -72.89f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 20:
                Santa.transform.position = new Vector3(-512.76f, Santa.transform.position.y, 304.16f);//Santa.transform.position = new Vector3(-246.8f, 0.2f, 36.01f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
        }
    }

    public void SetSantaPosition2(int getLevel2)
    {
        Santa = PlayerParent.transform;
        switch (getLevel2)
        {
            case 1:
                Santa.transform.position = new Vector3(-596.6f, Santa.transform.position.y, 304.34f); //Santa.transform.position = new Vector3(220.78f, 0.2f, -15.68f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 2:
                Santa.transform.position = new Vector3(-556.59f, Santa.transform.position.y, 432.12f);//Santa.transform.position = new Vector3(8.19f, 0.2f, -215.3f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 3:
                Santa.transform.position = new Vector3(-503.19f, Santa.transform.position.y, 471.65f);//Santa.transform.position = new Vector3(239.29f, 0.2f, -57.75f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 4:
                Santa.transform.position = new Vector3(-593f, Santa.transform.position.y, 373.2f);//Santa.transform.position = new Vector3(-108.45f, 0.2f, -73.4f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 5:
                Santa.transform.position = new Vector3(-834.85f, Santa.transform.position.y, 474.63f);//Santa.transform.position = new Vector3(-78.32f, 0.2f, 102.16f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 6:
                Santa.transform.position = new Vector3(-118.23f, Santa.transform.position.y, 273.82f);//Santa.transform.position = new Vector3(187.56f, 0.2f, 274.91f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 7:
                Santa.transform.position = new Vector3(-556.47f, Santa.transform.position.y, 634.41f);//Santa.transform.position = new Vector3(-146.59f, 0.2f, 101.22f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 8:
                Santa.transform.position = new Vector3(-512.7f, Santa.transform.position.y, 576.14f);//Santa.transform.position = new Vector3(26.08f, 0.2f, -356.8f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 9:
                Santa.transform.position = new Vector3(-579.51f, Santa.transform.position.y, 158.23f);//Santa.transform.position = new Vector3(100.23f, 0.2f, -72.89f);
                Santa.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case 10:
                Santa.transform.position = new Vector3(-744.02f, Santa.transform.position.y, 234.55f);//Santa.transform.position = new Vector3(-246.8f, 0.2f, 36.01f);
                Santa.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case 11:
                Santa.transform.position = new Vector3(-118.28f, Santa.transform.position.y, 273.82f); //Santa.transform.position = new Vector3(220.78f, 0.2f, -15.68f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 12:
                Santa.transform.position = new Vector3(-503.19f, Santa.transform.position.y, 471.6f);//Santa.transform.position = new Vector3(239.29f, 0.2f, -57.75f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 13:
                Santa.transform.position = new Vector3(-593f, Santa.transform.position.y, 373.2f);//Santa.transform.position = new Vector3(-108.45f, 0.2f, -73.4f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 14:
                Santa.transform.position = new Vector3(-512.7f, Santa.transform.position.y, 575.9f);//Santa.transform.position = new Vector3(26.08f, 0.2f, -356.8f);
                Santa.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 15:
                Santa.transform.position = new Vector3(-579.51f, Santa.transform.position.y, 164.95f);//Santa.transform.position = new Vector3(100.23f, 0.2f, -72.89f);
                Santa.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case 16:
                Santa.transform.position = new Vector3(-556.27f, Santa.transform.position.y, 634.41f);//Santa.transform.position = new Vector3(-146.59f, 0.2f, 101.22f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 17:
                Santa.transform.position = new Vector3(-834.71f, Santa.transform.position.y, 474.63f);//Santa.transform.position = new Vector3(-78.32f, 0.2f, 102.16f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 18:
                Santa.transform.position = new Vector3(-743.81f, Santa.transform.position.y, 234.79f);//Santa.transform.position = new Vector3(-246.8f, 0.2f, 36.01f);
                Santa.localEulerAngles = new Vector3(0, 90, 0);
                break;
            case 19:
                Santa.transform.position = new Vector3(-556.36f, Santa.transform.position.y, 432.12f);//Santa.transform.position = new Vector3(8.19f, 0.2f, -215.3f);
                Santa.localEulerAngles = new Vector3(0, 180, 0);
                break;
            case 20:
                Santa.transform.position = new Vector3(-579.86f, Santa.transform.position.y, 165.02f);//Santa.transform.position = new Vector3(-246.8f, 0.2f, 36.01f);
                Santa.localEulerAngles = new Vector3(0, 90, 0);
                break;
        }
    }


}
