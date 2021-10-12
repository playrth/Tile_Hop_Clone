using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    GameObject TilePrefab;
    public Queue<GameObject> ClosedTilePool = new Queue<GameObject>();
    public Queue<GameObject> OpenTilePool = new Queue<GameObject>();
    [SerializeField]
    int ClosedPoolSize=15;
    [SerializeField]
    int OpenPoolSize = 5;

    [SerializeField]
    GameObject PlanePrefab;
    public Queue<GameObject> ClosedPlanePool = new Queue<GameObject>();
    public Queue<GameObject> OpenPlanePool = new Queue<GameObject>();


    public static SpawnManager instance;
    float z = 0, x = 0, pz = 20;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        for (int i = 0; i < ClosedPoolSize; i++)
        {
            GameObject o = Instantiate(TilePrefab,Vector3.zero,Quaternion.identity);
            o.SetActive(false);
            ClosedTilePool.Enqueue(o);
        }
        for (int i = 0; i < 4; i++)
        {
            GameObject o = Instantiate(PlanePrefab, Vector3.zero, Quaternion.identity);
            o.SetActive(false);
            ClosedPlanePool.Enqueue(o);
        }


        for (int i = 0; i < OpenPoolSize; i++)
        {
            GameObject o = ClosedTilePool.Dequeue();
            o.SetActive(true);     
            o.transform.position = new Vector3(x,0,z);
            z += (3.5f * Random.Range(1, 4));
            x = Random.Range(-2f, 2f);
            OpenTilePool.Enqueue(o);
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject o = ClosedPlanePool.Dequeue();
            o.SetActive(true);
            o.transform.position = new Vector3(0, -3.5f, pz);
            pz += 50f;
            OpenPlanePool.Enqueue(o);
        }
    }

    public void SpawnTile()
    {
        GameObject o = ClosedTilePool.Dequeue();
        o.SetActive(true);
        o.transform.position = new Vector3(x, 0, z);
        z += (3.5f * Random.Range(1, 4));
        x = Random.Range(-2f, 2f);
        OpenTilePool.Enqueue(o);
    }
    public void SpawnPlane()
    {
        GameObject o = ClosedPlanePool.Dequeue();
        o.SetActive(true);
        o.transform.position = new Vector3(0, -3.5f, pz);
        pz += 50f;
        OpenPlanePool.Enqueue(o);
    }
}
