using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyCube : MonoBehaviour
{
    private Player player;
    public GameObject clone;
    public bool isRedbox;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        // if player takes box, destroy the box and add the player's boxCount
        if (other.gameObject.CompareTag("Player") && other.isTrigger)
        {
            Destroy(gameObject);
            if(isRedbox == true && player.boxCount > 0)
            {

                Instantiate(clone, new Vector3(transform.position.x - 1, transform.position.y, transform.position.z), Quaternion.identity);
            }
        }
    }
}
