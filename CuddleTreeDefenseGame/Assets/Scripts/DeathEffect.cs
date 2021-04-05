using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{
    [SerializeField] GameObject deathSprite;
    [SerializeField] GameObject deathEffect;
    [SerializeField] Vector2 delay = new Vector2(1f, 2f);
    [SerializeField] bool death = true;

    public bool Death { get => death; set { if(value) StartCoroutine(Explode()); } }

    private void Start()
    {
        if(Death)
        {
            StartCoroutine(Explode());
        }
    }
    IEnumerator Explode()
    {
        yield return new WaitForSeconds(Random.Range(delay.x, delay.y));
        Instantiate(deathSprite, transform.position, transform.rotation);
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
