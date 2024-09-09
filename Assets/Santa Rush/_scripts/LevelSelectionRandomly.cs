using UnityEngine;
using System.Collections;

public class LevelSelectionRandomly : MonoBehaviour {
 
		

		
		int index;

		public GameObject[] Levels;

		// Use this for initialization
		void Start ()
	{
		index = Random.Range(0,9);
		Levels [index].SetActive (true);
	
		}


	}