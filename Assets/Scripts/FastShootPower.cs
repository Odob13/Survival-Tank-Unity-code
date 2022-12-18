using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastShootPower : MonoBehaviour
{
    private PlayerMovement player;
    void Start()
    {
        //get every class that has the Temporized script
        player = FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            {
                player.FastShootPower();
                Destroy(gameObject);
            }

    }

}
