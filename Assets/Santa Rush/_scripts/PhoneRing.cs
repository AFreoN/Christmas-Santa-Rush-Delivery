using UnityEngine;

public class PhoneRing : MonoBehaviour {
	public GameObject Call1;
	public GameObject Call2;
	void Start ()
    {
		Call1.SetActive (true);
        
    }
	void Update ()
    {
        Invoke("t1", 0.2f);
    }
	void t1()
	{
		Call1.SetActive (false);
		Call2.SetActive (true);
	}
}
