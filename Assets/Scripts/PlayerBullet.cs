using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveBullet(int direction)
    {
        if(direction == 1)
        {
            rb.velocity = new Vector2(rb.velocity.x + bulletSpeed * Time.deltaTime, 0);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x + bulletSpeed * Time.deltaTime, 0);
        }
    }


    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
