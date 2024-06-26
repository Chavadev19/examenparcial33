using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private ManagerSpawner spawn;

    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        spawn = FindFirstObjectByType<ManagerSpawner>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x <= 0)
            MoveRight();
        else
            MoveLeft();

    }

    private void MoveRight()
    {
        rb.velocity = new Vector2(rb.velocity.x + moveSpeed * Time.deltaTime, rb.velocity.y);
    }
    private void MoveLeft()
    {
        rb.velocity = new Vector2(rb.velocity.x - moveSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            spawn.EnemyDestroyed();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
