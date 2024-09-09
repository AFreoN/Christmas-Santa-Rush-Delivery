using UnityEngine;
using System.Collections;

public class Traffic : MonoBehaviour {

		
		public TrafficPaths path;
		public float ReachDistance = 1f;
		public bool draw = false;
		public float speed = 5f;
		public float rot = 10f;
		private int CurrNodeID = 0 ;
		private bool nega;
		private bool once;
		
		
		
		// Use this for initialization
		void Start () {
			nega = false;
			once = false;
			
			
		}
		
		// Update is called once per frame
		void Update () {
			
			if(CurrNodeID == 0)
			{
				nega = false;
				once = false;
			}
			Vector3 dest = path.GetNodepos(CurrNodeID);
			Vector3 offset = dest - transform.position;
			if (offset.sqrMagnitude > ReachDistance)
			{
				
				offset = offset.normalized;
				transform.Translate (offset * speed * Time.deltaTime, Space.World);
				Quaternion lookrot = Quaternion.LookRotation (offset);
				transform.rotation = Quaternion.Slerp(transform.rotation, lookrot , rot* Time.deltaTime);
				
			} 
			else 
			{
				if(!nega)
				{
					ChangeDestNode();
				}
				if(nega)
				{
					ChangeDestNegNode();
				}
			}
			
			if(CurrNodeID >= path.nodes.Length)
			{
				nega = true;
				if(!once)
				{
					CurrNodeID --;

				}
				once = true;
			}
			
			
			
		}
		
		
		void ChangeDestNode()
		{
			CurrNodeID ++;
			
		}
		
		void ChangeDestNegNode()
		{
			//CurrNodeID --;
		}
	}
