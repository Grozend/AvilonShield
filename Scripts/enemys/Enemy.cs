using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int positionOfPatrol;
    public Transform point;
    Transform player;
    bool moveingRight;
    public float stopingDistance;
    bool chill = false;
    bool angry = false;
    bool goBack = false;

    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public float stoppingDistance;

    public int health;
    public int damage;

    private float stopTime;
    public float startStopTime;
    public float normalSpeed;
    private PlayerController playere;
    private Animator anim;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        playere = FindObjectOfType<PlayerController>();
        normalSpeed = speed;
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && angry == false)
        {
            chill = true;
        }
        if (Vector2.Distance(transform.position, player.position) < stopingDistance)
        {
            angry = true;
            chill = false;
            goBack = false;
        }
        if (Vector2.Distance(transform.position, player.position) > stopingDistance)
        {
            goBack = true;
            angry = false;
        }

        if (chill == true)
        {
            Chill();
        }
        else if (angry == true)
        {
            Angry();
        }
        else if (goBack == true)
        {
            GoBack();
        }
        if (stopTime <= 0)
        {
            speed = normalSpeed;
        }
        else
        {
            speed = 0;
            stopTime -= Time.deltaTime;
        }
        if (health <= 0)
        {
            anim.SetTrigger("enemyDepth");
            StartCoroutine(DestroyAfterDelay(0.5f));
            IEnumerator DestroyAfterDelay(float delay)
            {
                yield return new WaitForSeconds(delay);

                Destroy(gameObject);
            }
        }
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < stoppingDistance)
        {
            speed =- speed;
        }
        else
        {
            speed = 1.5f;
        }
    }

    void Chill()
    {
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            moveingRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            moveingRight = true;
        }

        if (moveingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        Vector2 direction = player.transform.position - transform.position;

        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (direction.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    public void TakeDamage(int damage)
    {
        stopTime = startStopTime;
        health -= damage;
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwAttack <= 0)
            {
                anim.SetTrigger("enemyAttack");
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }
    public void OnEnemyAttack()
    {
        playere.health -= damage;
        timeBtwAttack = startTimeBtwAttack;
    }
}
