using UnityEngine;
using System.Collections;

public class PointCollection : MonoBehaviour
{
	public GameObject[] Points;
	public static int Index=0;		
	public static GameObject ArrowRotationGameObject;
    public Material Sign1;
    float ofset1;
	void Awake()
	{
        ofset1 = 0;
        Index = 0;
		for(int i=0;i<Points.Length;i++)
		{
			if(i==0)
			{
				Points[i].gameObject.SetActive(true);
				ArrowRotationGameObject=Points[i].gameObject;
			}
			else
				Points[i].gameObject.SetActive(false);
		}
        InvokeRepeating("offsettaling", 0.02f,0.02f);
	}
    void offsettaling()
    {
        if (ofset1 >= -100f) {
            ofset1 -= 0.1f;
        }
        else
        {
            ofset1 = 1f;
        }
        
        Sign1.SetTextureOffset("_MainTex",new Vector2(ofset1, 0));
    }
}


