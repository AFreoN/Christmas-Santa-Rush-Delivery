using UnityEngine;
public class BusyCode : MonoBehaviour {

    public GameObject Level1;
	public GameObject Level2;
	public GameObject Level3;
	public GameObject Level4;
	public GameObject Level5;
	public GameObject Level6;
	public GameObject Level7;
	public GameObject Level8;
	public GameObject Level9;
	public GameObject Level10;
	public GameObject BPLevel1;
	public GameObject BPLevel2;
	public GameObject BPLevel3;
	public GameObject BPLevel4;
	public GameObject BPLevel5;
	public GameObject BPLevel6;
	public GameObject BPLevel7;
	public GameObject BPLevel8;
	public GameObject BPLevel9;
	public GameObject BPLevel10;
	
	public void Busy1()
	{

        Level1.SetActive(false);
        BPLevel1.SetActive(false);
        Level2.SetActive (true);

	}
	public void Busy2()
	{

        Level2.SetActive(false);
        BPLevel2.SetActive(false);
        Level3.SetActive (true);

	}
	public void Busy3()
	{

        Level3.SetActive(false);
        BPLevel3.SetActive(false);
        Level4.SetActive (true);

	}
	public void Busy4()
	{

        Level4.SetActive(false);
        BPLevel4.SetActive(false);
        Level5.SetActive (true);

	}
	public void Busy5()
	{

        Level5.SetActive(false);
        BPLevel5.SetActive(false);
        Level6.SetActive (true);

	}
	public void Busy6()
    {
        Level6.SetActive(false);
        BPLevel6.SetActive(false);

        Level7.SetActive (true);

	}
	public void Busy7()
    {
        Level7.SetActive(false);
        BPLevel7.SetActive(false);

        Level8.SetActive (true);

	}
	public void Busy8()
    {
        Level8.SetActive(false);
        BPLevel8.SetActive(false);
        Level9.SetActive (true);

	}
	public void Busy9()
    {
        Level9.SetActive(false);
        BPLevel9.SetActive(false);
        Level10.SetActive (true);

	}
	public void Busy10()
    {
        Level10.SetActive(false);
        BPLevel10.SetActive(false);
        Level1.SetActive (true);

	}
}
