using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private ManagerSpawner spawn;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeBetweenShots;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawner;

    [SerializeField] private GameObject powerUp;
    [SerializeField] private Transform reference;

    void Start()
    {
        spawn = FindFirstObjectByType<ManagerSpawner>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(shoot());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x <= 0)
            MoveFromLeft();
        else
            MoveFromRight();

    }

    private void MoveFromRight()
    {
        if(gameObject.transform.position.x > 7)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            rb.velocity = new Vector2(rb.velocity.x - moveSpeed * Time.deltaTime, rb.velocity.y);
        }
        
    }

    //En ambas funciones puse que se detenga en la X = 7, entonces ahi es cuando se detienen por si quieres cambiar la animacion
    private void MoveFromLeft()
    {
        if (gameObject.transform.position.x < -7)
        {
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            rb.velocity = new Vector2(rb.velocity.x + moveSpeed * Time.deltaTime, rb.velocity.y);
        }
    }

    IEnumerator shoot()

    {

        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            Instantiate(bullet, bulletSpawner.position, bulletSpawner.rotation);
            //Esta es la funcion de disparo del enemigo, por si la ocupas para lo de la animacion
        }
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            Instantiate(powerUp, reference.position, reference.rotation);
            spawn.EnemyDestroyed();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
