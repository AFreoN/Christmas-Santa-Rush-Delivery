using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchControlsKit;
public class SantaControl : MonoBehaviour {
//	Animator anim;

		public float SantaSpeed, SantaRot;
		public float maxSpeed;
		private float xacc;
		public static bool HeliOn;
		public static bool HeliOn1;
		
		void Start () 
	{
//		anim = gameObject.GetComponent<Animator> ();
//		anim.SetBool ("run", true);
		HeliOn = false;
			HeliOn1 = false;
			if (!PlayerPrefs.HasKey ("TYPE")) 
				PlayerPrefs.SetInt ("TYPE",1);
		}


		void Update ()
		{
//		if (InputManager.GetAxis ("HControls", "Vertical") > 0 || InputManager.GetAxis ("HControls", "Horizontal") > 0 || InputManager.GetAxis ("HControls", "Vertical") < 0 || InputManager.GetAxis ("HControls", "Horizontal") < 0) {
//			anim.SetBool ("run", true);
//
//			anim.SetBool ("idle", false);
//
//		} else {
//				
//			anim.SetBool ("run", false);
//			anim.SetBool ("idle", true);
//
//		}	

		transform.Translate (Vector3.forward * InputManager.GetAxis ("HControls", "Vertical") * SantaSpeed);
		transform.localEulerAngles = new Vector3 (0, transform.localEulerAngles.y + (InputManager.GetAxis ("HControls", "Horizontal") * SantaRot), 0);


			}


		public void OnCollisionEnter(Collision col)
		{

		}

	}
