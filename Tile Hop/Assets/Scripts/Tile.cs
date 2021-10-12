using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag== "SpawnEnd")
        {
            SpawnManager.instance.Spawn();
            this.gameObject.SetActive(false);
        }
    }
}
