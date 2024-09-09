using UnityEngine;

public class TriggerPathComplete : MonoBehaviour
{
    public Animator Boy;    
    public GameObject[] Carts;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {            
            Boy.SetBool("Idle", true);
            this.GetComponent<BoxCollider>().enabled = false;
            //Activate_Panel();
            
            InvokeRepeating("walkboy",0.02f,0.02f);
            Carts[PlayerPrefs.GetInt("CartNo")].GetComponent<RCC_CarControllerV3>().rigid.drag = 10;
            LevelManager.Instance.jingel();
            Invoke("Activate_Panel", 2f);
        }
    }
    void walkboy()
    {
       
          
        Vector3 pos = Vector3.MoveTowards(Boy.gameObject.transform.position, Carts[PlayerPrefs.GetInt("CartNo")].transform.position,1f*Time.deltaTime);
        Boy.gameObject.transform.position = pos;
       float dis= Vector3.Distance(Boy.gameObject.transform.position , Carts[PlayerPrefs.GetInt("CartNo")].transform.position);
        if (dis < 3f)
        {
            CancelInvoke("walkboy");
            Boy.SetBool("Idle",false);
            Invoke("Activate_Panel", 0.01f);
            LevelManager.Instance.Sound();
        }
    }
    void Activate_Panel()
    {
        LevelManager.Instance.ShowLevelCompleted();
    }
}
