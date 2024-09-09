using UnityEngine;
using System.Collections;

public class ArrowRotation : MonoBehaviour {


		
		public GameObject[] ObjectPosition;
		
		// Use this for initialization
		void Start () {
			/*this.transform.LookAt (ObjectPosition.transform);
		for (int i=0; i<ObjectPosition; i++) {
			Vector3 Temp = this.transform.position;
			Temp.x = 0;
			Temp.y = 0;
		}*/
		}
		
		// Update is called once per frame
		void Update () {

			if(PointCollection.ArrowRotationGameObject!=null)
			{
				this.transform.LookAt (PointCollection.ArrowRotationGameObject.transform);
				Vector3 Temp1 = this.transform.position;
				Temp1.x = 0;
				Temp1.y = 0;
			}
			
		}
		
		//for (int i=0; i<ObjectPosition.Length; i++) {
		//if (
		/*		if(ObjectPosition[0] != null)
		{

			this.transform.LookAt (ObjectPosition[0].transform);
		}

			Vector3 Temp = this.transform.position;
			Temp.x = 0;
			Temp.y = 0;

		if (ObjectPosition[0] == null && ObjectPosition != null )
		{
			this.transform.LookAt (ObjectPosition[1].transform);
		}Vector3 Temp1 = this.transform.position;
		Temp1.x = 0;
		Temp1.y = 0;


		if (ObjectPosition[1] == null && ObjectPosition != null )
		{
			this.transform.LookAt (ObjectPosition[2].transform);
		}

		}*/
		
	}
	
