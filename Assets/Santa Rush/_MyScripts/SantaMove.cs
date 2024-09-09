using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaMove : MonoBehaviour
{
    public Animator Santa;
    public GameObject Deja, GamePlayPanel, EffectPanel;
    public GameObject[] Carts;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SantaWalk", 0.02f, 0.02f);
    }

    void SantaWalk()
    {
        Vector3 pos = Vector3.MoveTowards(Santa.gameObject.transform.position, Carts[PlayerPrefs.GetInt("CartNo")].transform.position, 1f * Time.deltaTime);
        Santa.gameObject.transform.position = pos;
        float dis = Vector3.Distance(Santa.gameObject.transform.position, Carts[PlayerPrefs.GetInt("CartNo")].transform.position);
        if (dis < 2f)
        {
            EffectPanel.SetActive(true);
            CancelInvoke("SantaWalk");
            Invoke("PanelOff1", 1f);
            Invoke("PanelOff", 2f);
        }
    }
    void PanelOff1()
    {
        Deja.GetComponent<SkinnedMeshRenderer>().enabled = false;
    }
    void PanelOff()
    {
        GamePlayPanel.SetActive(true);
        LevelManager.Instance.RecieveCall2();
        this.gameObject.SetActive(false);
    }
}
