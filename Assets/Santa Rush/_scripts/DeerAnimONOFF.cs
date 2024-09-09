using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TouchControlsKit;
public class DeerAnimONOFF : MonoBehaviour {

	Animator anim;
	public GameObject Dust;
    public static bool WalkAnim = false;
	void Start () 
	{
       
		anim = gameObject.GetComponent<Animator> ();
		anim.SetBool ("run", false);
        anim.SetBool("idle", true);
        if (!PlayerPrefs.HasKey ("TYPE")) 
			PlayerPrefs.SetInt ("TYPE",1);

    }
    public void WalkCart()
    {
       
    }
    public  void animationidile()
    {
        anim.SetBool("run", false);
        Dust.SetActive(false);
        anim.SetBool("idle", true);
    }
    
    void Update ()
	{
        if (WalkAnim) {
            this.anim.SetBool("run", true);
            this.anim.SetBool("idle", false);
        }
        else
        {
            this.anim.SetBool("run", false);
            this.anim.SetBool("idle", true);
        }
        //if (InputManager.GetAxis("HControls", "Vertical") > 0 || InputManager.GetAxis("HControls", "Horizontal") > 0 || InputManager.GetAxis("HControls", "Vertical") < 0 || InputManager.GetAxis("HControls", "Horizontal") < 0)
        //{
        //    anim.SetBool("run", true);
        //    Dust.SetActive(true);
        //    anim.SetBool("idle", false);

        //}
        //else
        //{
        //    Dust.SetActive(false);
        //    anim.SetBool("run", false);
        //    anim.SetBool("idle", true);

        //}
    }
}
