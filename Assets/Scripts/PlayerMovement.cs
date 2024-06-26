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

        if (horizontalInput != 0)
        {
            
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

        // Multiplica la escala x del personaje por -1.
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
        // Instanciar el objeto en la posición y rotación del objeto de referencia
        GameObject instantiatedObject = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);

        ammoLeft -= 1;

        // Obtener el Rigidbody2D del objeto instanciado
        Rigidbody2D rb = instantiatedObject.GetComponent<Rigidbody2D>();

        // Verificar si el objeto instanciado tiene un Rigidbody2D
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
        ammoLeft -= 10;
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
