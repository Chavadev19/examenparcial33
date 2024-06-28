using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] TextMeshProUGUI leftAmmo;

    [Header("Shoot Values")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Vector2 bulletSpeed;
    [SerializeField] private bool canShoot = true;
    [SerializeField] private float shotsCooldown;
    [SerializeField] private int ammoLeft;

    [Header("Movement Values")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool facingRight = true;

    [Header("Jump Values")]
    [SerializeField] private float jumpForce;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool isGrounded;

    




    // Start is called before the first frame update
    void Start()
    {
        ammoLeft = 5;
        canShoot = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jump();
        Shoot();
        leftAmmo.text = ammoLeft.ToString();
        if (ammoLeft < 0)
            ammoLeft = 0;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("ControlHorizontal");

        //No estoy seguro, pero creo que aqui esta lo que pudiera estar causando que contigo el player se siga, porque el input es diferente de 0
        if (horizontalInput != 0)
        {
            //Por aqui iria la animacion de movimiento

            transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

            if (horizontalInput > 0 && !facingRight)
            {
                Flip();
            }
            else if (horizontalInput < 0 && facingRight)
            {
                Flip();
            }
        }
    }

    private void Jump()
    {
        if(Input.GetButtonDown("JumpControl") && isGrounded)
        {
            Debug.Log("Pulsando A");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Flip()
    {
        // Cambia la dirección en la que el personaje está mirando.
        facingRight = !facingRight;

        // Multiplica la escala x del personaje por -1, cone sto la animacion se gira solita tons namas hay que acomodarla bien.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Shoot()
    {
        if (Input.GetButtonDown("ShootControl") && canShoot && ammoLeft > 0)
        {
            SpawnObject();
            StartCoroutine(ShotCooldown());
        }
    }

    private void SpawnObject()
    {
        // Esta es la funcion de disparar, aqui iria lo que active la animacion, aqui o en el Shoot que esta arribita, como prefieras
        GameObject instantiatedObject = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

        ammoLeft -= 1;

        
        Rigidbody2D rb = instantiatedObject.GetComponent<Rigidbody2D>();

        
        if (rb != null)
        {
            if (facingRight)
            {
                rb.velocity = bulletSpeed;
            }
            else
            {
                rb.velocity = bulletSpeed * -1;
            }
        }
    }

    IEnumerator ShotCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shotsCooldown);
        canShoot = true;
    }

    IEnumerator powerUp()
    {
        shotsCooldown = shotsCooldown / 3;
        ammoLeft = +10;
        yield return new WaitForSeconds(3);
        shotsCooldown = shotsCooldown * 3;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ammo"))
        {
            ammoLeft += 2;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("powerUp"))
        {
            StartCoroutine(powerUp());
            Destroy(collision.gameObject);
        }
    }
}
