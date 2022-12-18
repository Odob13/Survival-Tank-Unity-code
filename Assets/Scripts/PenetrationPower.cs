using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//if player take this power up, the bullets will penetrate the enemies
public class PenetrationPower : MonoBehaviour
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
                player.PenetrationPower();
                Destroy(gameObject);
            }

    }

}
