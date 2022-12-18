using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//destroy this gameobct after 3 seconds

public class Temporized : MonoBehaviour
{
    // Start is called before the first frame update

    public float timeToDestroy = 3f;

    public GameObject WallEffect;

    private PlayerMovement PenetrationPower;
    void Start()
    {
        PenetrationPower = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        Destroy(gameObject, timeToDestroy);
    }

    //if touch a enemy make damage to him and destroy this gameobject
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !PenetrationPower.PenetrationPowerBool)
        {
            collision.GetComponent<Enemy2>().TakeDamage(50);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Enemy") && PenetrationPower.PenetrationPowerBool)
        {
            collision.GetComponent<Enemy2>().TakeDamage(50);
        }
        else if (collision.CompareTag("Wall") && !PenetrationPower.PenetrationPowerBool)
        {
            Instantiate(WallEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall") && PenetrationPower.PenetrationPowerBool)
        {
            Debug.Log("Penetration");
        }
    }
}
