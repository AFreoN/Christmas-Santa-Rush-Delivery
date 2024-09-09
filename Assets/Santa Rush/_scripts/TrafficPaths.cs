using UnityEngine;
using System.Collections;

public class TrafficPaths : MonoBehaviour {

		
		
		
		public Transform[] nodes;
		
		public Vector3 GetNodepos(int id)
		{
return			nodes[id].position;
		}
		

	}
