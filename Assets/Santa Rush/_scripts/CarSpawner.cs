using UnityEngine;
using System.Collections.Generic;

public class CarSpawner : MonoBehaviour
{
    public Transform pathsHolder;
    Transform[] paths;
    public List<Transform> pathList;

    public int numberOfCars = 5;

    public Transform[] carPrefab;
    public RCC_Camera rcCam;

    bool startSpawning = false;
    Transform playerCar;

    private void Start()
    {
        paths = new Transform[pathsHolder.childCount];
        for(int i = 0; i < pathsHolder.childCount; i++)
        {
            paths[i] = pathsHolder.GetChild(i);
        }

        pathList = new List<Transform>();
        foreach(Transform t in paths)
        {
            pathList.Add(t);
        }

        numberOfCars = paths.Length;

        //spawnCars();
    }

    private void Update()
    {
        if(startSpawning == false)
        {
            if(rcCam.playerCar != null)
            {
                playerCar = rcCam.playerCar.gameObject.transform;
                startSpawning = true;
                spawnCars();
                SpawnSecondWave();
            }
        }
    }

    void spawnCars()
    {
        for(int i = 0; i <  numberOfCars; i++)
        {
            int c = Random.Range(0, carPrefab.Length);
            Transform t = Instantiate(carPrefab[c], carPrefab[c].position, carPrefab[c].rotation);

            int r = Random.Range(0, pathList.Count);

            t.GetComponent<CarAIController>().path = pathList[r];
            SetPosRot(pathList[r], t);
            pathList.RemoveAt(r);
        }
    }

    void SpawnSecondWave()
    {
        pathList.Clear();
        foreach (Transform t in paths)
        {
            pathList.Add(t);
        }

        for (int i = 0; i < numberOfCars; i++)
        {
            int c = Random.Range(0, carPrefab.Length);
            Transform t = Instantiate(carPrefab[c], carPrefab[c].position, carPrefab[c].rotation);

            int r = Random.Range(0, pathList.Count);

            t.GetComponent<CarAIController>().path = pathList[r];
            setPosRot2(pathList[r], t);
            pathList.RemoveAt(r);
        }
    }

    void SetPosRot(Transform p, Transform car)
    {
        Vector3 dir = p.GetChild(1).position - p.GetChild(0).position;
        dir.y = car.position.y;

        Vector3 norm = dir.normalized;

        car.position = p.GetChild(0).position + norm * Random.Range(0,5);
        car.forward = dir;
    }

    void setPosRot2(Transform p, Transform car)
    {
        int r = Random.Range(4, p.childCount-1);
        Vector3 dis = playerCar.position - p.GetChild(r).position;

        while(dis.magnitude < 100)
        {
            r = Random.Range(4, p.childCount-1);
            dis = playerCar.position - p.GetChild(r).position;
        }

        Vector3 dir = p.GetChild(r + 1).position - p.GetChild(r).position;
        dir.y = car.position.y;

        Vector3 norm = dir.normalized;

        car.GetComponent<CarAIController>().currentNode = r + 1;
        car.position = p.GetChild(r).position;
        car.forward = dir;
    }
}
