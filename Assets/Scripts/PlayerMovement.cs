using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    //2d tank movement script for the player, rotate to mouse position and move forward, with acceleration and deceleration, shows the heal in slider and heal the player when he dont receive damage for 5 seconds


    public int health = 100;
    public float speed = 5f;
    public float acceleration = 5f;
    public float deceleration = 5f;
    public float maxSpeed = 5f;
    public float healTime = 5f;
    public int healAmount = 10;
    public float currentHealTime = 0f;
    public Slider healthSlider;
    public Slider PowerSlider;
    public Text ScoreText;
    public Text WaveText;
    public Text WavecountdownText;
    public int score = 0;
    public GameObject Crack;
    public float countdownPower = 0f;
    public AudioClip DamageSound;
    public bool PenetrationPowerBool = false;
    public bool TripleShotPowerBool = false;
    public bool FasthealthPowerBool = false;  
    public Text GameOverText;
    public Text RestartButton;

    private shoot Shoot;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 mousePos;
    private float currentSpeed = 0f;


    float tempShoot, tempHeal= 0f;


    void Start()
    {
        Shoot = GetComponent<shoot>();
        PowerSlider.gameObject.SetActive(false);
        healthSlider.maxValue = health;
        rb = GetComponent<Rigidbody2D>();
        tempShoot= Shoot.timeBetweenShots;
        tempHeal= healTime;
    }

    void Update() {
        if (health <= 0)
        {
            //when health is 0, stop game and show game over screen
            Time.timeScale = 0;
            GameOverText.gameObject.SetActive(true);
            RestartButton.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        //show score in gui
        ScoreText.text = "Score: " + score;
        //change color of slider when health is low
        if (health <= 50)
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.red;
        }
        else
        {
            healthSlider.fillRect.GetComponent<Image>().color = Color.green;
        }
        //change color of score text randomly and smoothly between all rainbow colors, wait 1 between each color change
        ScoreText.color = Color.HSVToRGB(Mathf.PingPong(Time.time, 1), 1, 1);
        //change color of wave text randomly and smoothly between all rainbow colors, wait 1 between each color change
        WaveText.color = Color.HSVToRGB(Mathf.PingPong(Time.time, 1), 1, 1);
        //change color of wave countdown text randomly and smoothly between all rainbow colors, wait 1 between each color change
        WavecountdownText.color = Color.HSVToRGB(Mathf.PingPong(Time.time, 1), 1, 1);
       
        //rotate to mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rotate 
        rb.rotation = angle;
        //heal player when he dont receive damage for 5 seconds max 100 health
        if (currentHealTime < healTime)
        {
            currentHealTime += Time.deltaTime;
        }
        else
        {
            if (health < 100)
            {
                health += healAmount % 100;
                currentHealTime = 0f;
            }
        }
    }

    void FixedUpdate()
    {
        //show health in slider
        healthSlider.value = health;

        PowerSlider.value = countdownPower;

        //move forward with acceleration and deceleration
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(-movement * speed * 100f, ForceMode2D.Impulse);
        }

        if (movement != Vector2.zero)
        {
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += acceleration * Time.deltaTime;
            }
        }
        else
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= deceleration * Time.deltaTime;
            }
        }

        rb.MovePosition(rb.position + movement * currentSpeed * Time.deltaTime);
    }

    public void PenetrationPower()
    {
        PenetrationPowerBool = true;
        //count time remaining for power up 
        if (countdownPower < 10f)
        {
            countdownPower = 10f;
            PowerSlider.maxValue = countdownPower;
        } 
        //show power up time in slider
        PowerSlider.gameObject.SetActive(true);
        //coroutine to count time remaining for power up
        StopCoroutine(FastShootPowerCountdown());
        StopCoroutine(TripleShotPowerCountdown());
        StopCoroutine(FastHealthPowerCountdown());
        StartCoroutine(PenetrationPowerCountdown());

    }

    IEnumerator PenetrationPowerCountdown()
    {
        while (countdownPower > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownPower--;
        }
        //when time is over, disable power up
        PenetrationPowerBool = false;
        Shoot.timeBetweenShots = tempShoot;
        TripleShotPowerBool = false;
        healTime = tempHeal;
        PowerSlider.gameObject.SetActive(false);
    }

    public void FastShootPower()
    {
        Shoot.timeBetweenShots = 0.5f;
        //count time remaining for power up 
        if (countdownPower < 10f)
        {
            countdownPower = 10f;
            PowerSlider.maxValue = countdownPower;
        } 
        //show power up time in slider
        PowerSlider.gameObject.SetActive(true);
        //coroutine to count time remaining for power up
        StopCoroutine(PenetrationPowerCountdown());
        StopCoroutine(TripleShotPowerCountdown());
        StopCoroutine(FastHealthPowerCountdown());
        StartCoroutine(FastShootPowerCountdown());

    }

    IEnumerator FastShootPowerCountdown()
    {
        while (countdownPower > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownPower--;
        }
        //when time is over, disable power up
        PenetrationPowerBool = false;
        Shoot.timeBetweenShots = tempShoot;
        TripleShotPowerBool = false;
        healTime = tempHeal;
        PowerSlider.gameObject.SetActive(false);
    }

    public void TripleShotPower()
    {
        TripleShotPowerBool = true;
        //count time remaining for power up
        if (countdownPower < 10f)
        {
            countdownPower = 10f;
            PowerSlider.maxValue = countdownPower;
        } 
        //show power up time in slider
        PowerSlider.gameObject.SetActive(true);
        //coroutine to count time remaining for power up
        StopCoroutine(PenetrationPowerCountdown());
        StopCoroutine(FastShootPowerCountdown());
        StopCoroutine(FastHealthPowerCountdown());
        StartCoroutine(TripleShotPowerCountdown());

    }

    IEnumerator TripleShotPowerCountdown()
    {
        while (countdownPower > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownPower--;
        }
        //when time is over, disable power up
        PenetrationPowerBool = false;
        Shoot.timeBetweenShots = tempShoot;
        TripleShotPowerBool = false;
        healTime = tempHeal;
        PowerSlider.gameObject.SetActive(false);
    }

    public void FastHealthPower()
    {
        healTime = 2;
        //count time remaining for power up 
        countdownPower = 40f;
        PowerSlider.maxValue = countdownPower;
        //show power up time in slider
        PowerSlider.gameObject.SetActive(true);
        //coroutine to count time remaining for power up
        StopCoroutine(PenetrationPowerCountdown());
        StopCoroutine(FastShootPowerCountdown());
        StopCoroutine(TripleShotPowerCountdown());
        StartCoroutine(FastHealthPowerCountdown());

    }

    IEnumerator FastHealthPowerCountdown()
    {
        while (countdownPower > 0)
        {
            yield return new WaitForSeconds(1f);
            countdownPower--;
        }
        //when time is over, disable power up
        PenetrationPowerBool = false;
        Shoot.timeBetweenShots = tempShoot;
        TripleShotPowerBool = false;
        healTime = tempHeal;
        PowerSlider.gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {

        //play a one shot sound effect when player take damage
        GetComponent<AudioSource>().PlayOneShot(DamageSound);

        //screen shake when player take damage whit camera shake script
        Camera.main.GetComponent<CameraShake>().ShakeCamera(0.3f, 0.2f);
        //fade in Crack sprite when player take damage whit camera shake script
        Camera.main.GetComponent<CameraShake>().FadeIn(Crack, 0.3f);

        score -= 20;
        health -= damage;
        currentHealTime = 0f;
    }
}
