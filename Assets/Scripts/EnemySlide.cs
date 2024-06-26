using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlide : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool goingRight;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ManagerSpawner spawn;


    [Header("ColisionesPared")]
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckRadius; //Radio del círculo hecho por fisicas apra la deteccion
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private bool hittingWall;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, -1.56f, gameObject.transform.position.z);
        spawn = FindFirstObjectByType<ManagerSpawner>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);
    }
    // Update is called once per frame
    void Update()
    {
        //CheckPosition();
        //Move();
        //MoveRB();
        Patrol();
    }

    private void CheckPosition()
    {
        if (gameObject.transform.position.x <= -10)
        {
            rb.velocity = new Vector2(0, 0);
            goingRight = true;
        }

        if(gameObject.transform.position.x >= 10)
        {
            rb.velocity = new Vector2(0, 0);
            goingRight = false;
        }
          
    }

    private void Move()
    {
        if (goingRight)
            gameObject.transform.position = new Vector2(gameObject.transform.position.x + moveSpeed, gameObject.transform.position.y);
        else
            gameObject.transform.position = new Vector2(gameObject.transform.position.x - moveSpeed, gameObject.transform.position.y);

    }

    private void MoveRB()
    {
        if (goingRight)
            rb.velocity = new Vector2(rb.velocity.x + moveSpeed * Time.deltaTime, 0);
        else
            rb.velocity = new Vector2(rb.velocity.x - moveSpeed * Time.deltaTime, 0);
    }

    private void Patrol()
    {
        if (hittingWall)
            goingRight = !goingRight;

        if (goingRight)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else
        {

            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);

            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            spawn.EnemyDestroyed();
            Destroy(gameObject);
        }
    }
}
