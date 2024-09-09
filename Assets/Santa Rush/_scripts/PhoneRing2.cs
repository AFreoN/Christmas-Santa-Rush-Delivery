using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneRing2 : MonoBehaviour {
	public GameObject Call1;
	public GameObject Call3;
	public GameObject Call2;

	// Use this for initialization
	void Start () {
		Call1.SetActive (true);
	}

	// Update is called once per frame
	void Update () {
		Invoke ("t1",0.5f);
	}
	void t1()
	{
		Call3.SetActive (false);
		Call2.SetActive (false);
	}
}
