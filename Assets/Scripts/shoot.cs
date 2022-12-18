using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//shoot a projectile prefab on mouse click straight forward with time delay between shots and show in gui shotbar slider when shot take 0value and when is ready to shot take 1 value , sound effect on shot


public class shoot : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource shotSound;
    public AudioClip shotClip;
    public Slider shotBar;
    public float timeBetweenShots = 0.5f;
    private float shotTime = 0f;
    public GameObject projectile;
    public Transform shotPoint;
    public Transform shotPointPower;
    public Transform shotPointPower2;
    public float shotForce = 20f;

    private PlayerMovement TripleShot;

    void Start()
    {
        TripleShot = GetComponent<PlayerMovement>();
        shotSound.clip = shotClip;
    }


    // Update is called once per frame
    void Update()
    {
        shotBar.value = shotTime;
        if (shotTime < 1)
        {
            shotTime += Time.deltaTime / timeBetweenShots;
        }
        if (Input.GetMouseButtonDown(0) && shotTime >= 1)
        {
            shotTime = 0;
            Shoot();
        }
    }

    void Shoot()
    {
        shotSound.Play();
        if (TripleShot.TripleShotPowerBool == true)
        {
            GameObject bullet = Instantiate(projectile, shotPoint.position, shotPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shotPoint.up * shotForce, ForceMode2D.Impulse);

            GameObject bullet2 = Instantiate(projectile, shotPointPower.position, shotPointPower.rotation);
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            rb2.AddForce(shotPointPower.up * shotForce, ForceMode2D.Impulse);

            GameObject bullet3 = Instantiate(projectile, shotPointPower2.position, shotPointPower2.rotation);
            Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
            rb3.AddForce(shotPointPower2.up * shotForce, ForceMode2D.Impulse);
        }
        else
        {
            GameObject bullet = Instantiate(projectile, shotPoint.position, shotPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(shotPoint.up * shotForce, ForceMode2D.Impulse);
        }

    }

}
