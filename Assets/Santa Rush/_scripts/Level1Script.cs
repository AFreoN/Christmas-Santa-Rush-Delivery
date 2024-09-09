using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Script : MonoBehaviour {
	public GameObject TimePannel, OpenClosePannel, Time;
	//public GameObject ;
	//public GameObject ;
	public GameObject MapCamera;
	public GameObject CallPanel;
	public GameObject LevelComplete;
	public GameObject Level1;
	public GameObject Level2;
	public GameObject Arrow;
	public GameObject PointCollision;
	// Use this for initialization
	void Start ()
    {
		Invoke ("Call_Santa", 7);
	}

	void Call_Santa ()
    {
		CallPanel.SetActive (true);
		LevelComplete.SetActive (true);
	}
	public void Receive()
    {
		TimePannel.SetActive (true);
		Arrow.SetActive (true);
		PointCollision.SetActive (true);
		Time.SetActive (true);
		MapCamera.SetActive (false);
		CallPanel.SetActive (false);
	}
	public void Busy()
	{
        MapCamera.SetActive (false);
		CallPanel.SetActive (false);
		LevelComplete.SetActive (false);
		OpenClosePannel.SetActive (true);
		Invoke ("Test", 2.0f);
	//	Invoke ("t3",2);
	
	}
	void Test()
	{
        Level2.SetActive (true); 
        Level1.SetActive(false);
        OpenClosePannel.SetActive(false);
    }
	void t2()
	{
        

	}
}
