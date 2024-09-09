using UnityEngine;
using System.Collections;

public class PointCollission : MonoBehaviour {





		int index=0;

		void OnTriggerEnter(Collider col)
		{
			//print (col.gameObject.tag);
			if(col.gameObject.tag=="Player")
			{
				index = PointCollection.Index;
				if(index < this.transform.root.GetComponent<PointCollection>().Points.Length-1)
				{
					this.transform.root.GetComponent<PointCollection>().Points[index].SetActive(false);
					this.transform.root.GetComponent<PointCollection>().Points[++index].SetActive(true);
					PointCollection.ArrowRotationGameObject=this.transform.root.GetComponent<PointCollection>().Points[index];

					PointCollection.Index=index;

				}
				else
				{
					PointCollection.ArrowRotationGameObject=null;
					//PointCollection.ArrowRotationGameObject.gameObject.transform.rotation=Quaternion.identity;
					//PointCollection.ArrowRotationGameObject=this.transform.root.GetComponent<PointCollection>().Points[++index];
					this.transform.root.GetComponent<PointCollection>().Points[index].SetActive(false);
				}

				//print (index);
			}
		}
	}
