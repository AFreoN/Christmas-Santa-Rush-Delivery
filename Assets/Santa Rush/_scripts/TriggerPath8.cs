using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPath8 : MonoBehaviour {

	public GameObject Clock;
	public GameObject Arrow;
	public GameObject Sweep;
	public GameObject Boy;
	public GameObject Station;
	public GameObject LevelPointCollisiion;
	public GameObject Santa;
	public GameObject Gift;
	public GameObject WellPlayed;
	public GameObject Level8;
	public GameObject Level9;
	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Player") {
//			Level8.GetComponent<PositionLevel8> ().enabled = false;
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
		Level9.SetActive (true);
		Sweep.SetActive (true); 
		Station.SetActive (true); 
		Arrow.SetActive (false); 
		Level8.SetActive (false);
	}
}
