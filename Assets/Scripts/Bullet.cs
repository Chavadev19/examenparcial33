using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool goingRight;

    [SerializeField] private float bulletTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update()
    {
        CheckPosition();
        Move();
        DestroyBullet();
    }

    private void CheckPosition()
    {
        if (gameObject.transform.position.x < 0)
            goingRight = true;
        else
            goingRight = false;
    }

    private void Move()
    {
        if(goingRight)
        gameObject.transform.position = new Vector2(gameObject.transform.position.x + bulletSpeed, gameObject.transform.position.y);
        else
            gameObject.transform.position = new Vector2(gameObject.transform.position.x - bulletSpeed, gameObject.transform.position.y);

    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bulletTime);
        Destroy(gameObject);
    }

    
}
