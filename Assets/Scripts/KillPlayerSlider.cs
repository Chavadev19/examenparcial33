using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerSlider : MonoBehaviour
{

    [SerializeField] private PlayerManager player;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Se ha tocado al player");
            player.Defeat();
        }
    }
}
