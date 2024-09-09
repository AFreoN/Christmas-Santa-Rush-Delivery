using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class loopbg : MonoBehaviour {

	/// <summary>
	/// 滚动速度
	/// </summary>
	public float _Speed ;
	/// <summary>
	/// 滚动的材质载体
	/// </summary>
	private Material _ScrollMaterial;



    [HideInInspector]
	void Start()
	{
		this._ScrollMaterial = GetComponent<Renderer>().material;
        //heroBow = GameObject.Find("hero").GetComponent<Hero_Bower>();
   
       
    }
	
	void Update()
	{
		this._ScrollMaterial.mainTextureOffset = new Vector2(_Speed * Time.time, 0);
	}



    
    public void OnMouseUp()
    {
       
        

    }
}
