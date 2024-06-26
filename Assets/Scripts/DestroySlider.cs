using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySlider : MonoBehaviour
{
    [SerializeField] private ManagerSpawner spawn;
    [SerializeField] private GameObject ammo;
    [SerializeField] private Transform reference;
    private void Start()
    {
        spawn = FindFirstObjectByType<ManagerSpawner>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Instantiate(ammo, reference.position, reference.rotation);
            spawn.EnemyDestroyed();

            StartCoroutine(DestroyParent());
        }
    }

    IEnumerator DestroyParent()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject.transform.parent.gameObject);
    }
}
