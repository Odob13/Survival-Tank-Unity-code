using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy script that move to the player, when he touch the player he deal damage to him and destroy himself, when he die he spawn a death effect and destroy himself
//The enemy movement is based on the player position, trace 3 raycast, 1 in front of him, 1 in the left and 1 in the right, when is moving to the player he check if there is a wall in front of him, if there is a wall he will move and rotate to the left or right until any raycast hit a wall , if there is no wall he will move to the player position

public class Enemy2 : MonoBehaviour
{

    public float speed = 3f;
    public int health = 50;
    public GameObject deathEffect;
    public int damage = 20;
    public int points = 10;

    public Transform[] raycastPositions;
        
    private Transform player;
    private PlayerMovement playerScore;

    private RaycastHit2D hit, hit2, hit3;

    private float stuckTime = 0;

    private Vector3 lastPosition;

    private enum State
    {
        MovingToPlayer,
        Rotating,
        MovingToWall,
        MovingRandomly
    }

    private State state;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScore = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        switch (state)
        {
            case State.MovingToPlayer:
                MoveToPlayer();
                break;
            case State.Rotating:
                Rotate();
                break;
            case State.MovingToWall:
                MoveToWall();
                break;
            case State.MovingRandomly:
                MoveRandomly();
                break;
        }
    }

    private void MoveToPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        //rotate to player in x and y axis
        Vector3 dir = player.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private IEnumerator Rotate()
    {
        bool clockwise = Vector2.Distance(raycastPositions[1].position, player.position) < Vector2.Distance(raycastPositions[2].position, player.position);
        int direction = clockwise ? 10 : -10;

        while (true)
        {
            transform.Rotate(0, 0, direction);

            hit = Physics2D.Raycast(raycastPositions[0].position, transform.right, 1f);
            if (hit.collider == null)
            {
                state = State.MovingToWall;
                yield break;
            }

            yield return null;
        }
    }

    private void MoveToWall()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void MoveRandomly()
    {
        stuckTime += Time.deltaTime;
        if (stuckTime >= 5f)
        {
            stuckTime = 0f;
            state = State.MovingToPlayer;
            return;
        }

        float randomAngle = Random.Range(-180f, 180f);
        transform.Rotate(0, 0, randomAngle);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    // Métodos para controlar el comportamiento del enemigo cuando colisiona con un muro, recibe daño, etc.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Wall")
        {
            state = State.Rotating;
            StartCoroutine(Rotate());
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void DropRamdomPower()
    {
        int random = Random.Range(0, 100);
        if (random < 8)
        {
            PowerUp();
        }

    }

    void PowerUp()
    {
        int random = Random.Range(0, 100);
        if (random < 30)
        {
            Instantiate(Resources.Load("Powers/Penetration", typeof(GameObject)), transform.position, Quaternion.identity);
        }
        else if (30 < random && random < 60)
        {
            Instantiate(Resources.Load("Powers/FastShoot"), transform.position, Quaternion.identity);
        }
        else if (60 < random && random < 90)
        {
            Instantiate(Resources.Load("Powers/TripleShoot"), transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(Resources.Load("Powers/FastHeal"), transform.position, Quaternion.identity);
        }
    }
    void Die()
    {
        DropRamdomPower();
        playerScore.score += points;
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().TakeDamage(damage);
            Die();
        }
    }

    private void LateUpdate()
    {
        if (Vector3.Distance(transform.position, lastPosition) < 0.08f)
        {
            stuckTime += Time.deltaTime;

            if (stuckTime >= 5f)
            {
                stuckTime = 0f;
                state = State.MovingRandomly;
            }
        }
        else
        {
            stuckTime = 0f;
        }

        lastPosition = transform.position;
    }

}
