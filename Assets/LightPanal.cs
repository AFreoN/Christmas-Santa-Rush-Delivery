using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPanal : MonoBehaviour {
    public GameObject[] LightsChili;
    int lightIndex, Index,repeat;
	// Use this for initialization
	void Start () {
        repeat = Index = lightIndex = 0;
        InvokeRepeating("LightsActive", 0.1f,0.1f);
        InvokeRepeating("LightsDesActive", 0.5f, 0.1f);
        InvokeRepeating("LightsActiveRapet", 0.9f, 0.1f);
    }
	void LightsActive()
    {
        if (LightsChili[lightIndex].activeSelf)
        {
            LightsChili[lightIndex].SetActive(false);
        }
        else
        {
            LightsChili[lightIndex].SetActive(true);
        }
        if (lightIndex < LightsChili.Length-1)
        {
            lightIndex++;
            
        }
        else
        {

            lightIndex = 0;
        }
    }
    void LightsDesActive()
    {
        if (LightsChili[Index].activeSelf) {
            LightsChili[Index].SetActive(false);
        }
        else
        {
            LightsChili[Index].SetActive(true);
        }
        if (Index < LightsChili.Length - 1)
        {
            Index++;
        }
        else
        {
            Index = 0;
        }
    }
    void LightsActiveRapet()
    {
        if (LightsChili[repeat].activeSelf)
        {
            LightsChili[repeat].SetActive(false);
        }
        else
        {
            LightsChili[repeat].SetActive(true);
        }
        if (repeat < LightsChili.Length - 1)
        {
            repeat++;
        }
        else
        {
            repeat = 0;
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
