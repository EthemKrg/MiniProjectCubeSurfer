using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clonners : MonoBehaviour
{
    public GameObject clone;
    private void OnTriggerEnter(Collider other)
    {
        // if player touches the red boxes, this line will be instantiate clone 
        if (other.gameObject.CompareTag("clonners") && other.isTrigger)
        {
            Instantiate(clone, new Vector3(transform.position.x -1, transform.position.y, transform.position.z), Quaternion.identity);

        }
    }

}
