using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private bool hasDead = false;
    [SerializeField] private GameManager gameManager;


    private void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }
    private void Update()
    {
        if(hasDead)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {


        if(collision.gameObject.CompareTag("PlayerKiller") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Enemy"))
        {
            Defeat();
        }
    }

    public void Defeat()
    {
        gameManager.Defeat();
        hasDead = true;
    }

}
