using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "SpawnEnd")
        {
            SpawnManager.instance.SpawnPlane();
            GameObject o = this.transform.parent.gameObject;
            o.SetActive(false);
            SpawnManager.instance.ClosedPlanePool.Enqueue(o);
        }
    }
}
