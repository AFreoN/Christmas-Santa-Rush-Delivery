using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPath2 : MonoBehaviour {
	public GameObject Clock;
	public GameObject Arrow;
	public GameObject Sweep;
	public GameObject Boy;
	public GameObject Station;
	public GameObject LevelPointCollisiion;
	public GameObject Santa;
	public GameObject Gift;
	public GameObject WellPlayed;
	public GameObject Level2;
	public GameObject Level3;
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") {
//			Level3.GetComponent<PositionLevel3> ().enabled = false;
			this.GetComponent<BoxCollider> ().enabled = false;
			Gift.SetActive (true);
			Santa.GetComponent<SantaControl> ().enabled = false;
			Invoke ("t1",4);

		}

	}
	void t1()
	{   Sweep.SetActive (false); 
		Station.SetActive (false); 
		Boy.SetActive (false); 
		Arrow.SetActive (false); 
		Gift.SetActive (false);
		WellPlayed.SetActive (true);
		Clock.SetActive (false);

		Invoke ("t2", 4);
	}
	void t2()
	{   
		LevelPointCollisiion.SetActive (false);
		WellPlayed.SetActive (false);
		Santa.GetComponent<SantaControl> ().enabled = true;
		Boy.SetActive (true);
		Level3.SetActive (true);
		Sweep.SetActive (true); 
		Station.SetActive (true); 
		Arrow.SetActive (false); 
		Level2.SetActive (false);
	}
}
