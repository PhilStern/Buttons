using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnObject : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectToSpawn;
    [SerializeField]
    private Vector3 Offset;

    public void SpawnObjectAction()
    {
        GameObject.Instantiate(ObjectToSpawn, transform.position + Offset, Quaternion.identity);
    }


}
