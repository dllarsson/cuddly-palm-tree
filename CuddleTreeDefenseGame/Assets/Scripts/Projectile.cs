using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject deathSprite;
    [SerializeField] GameObject deathEffect;
    [SerializeField] float deathDelay = 0.2f;
    void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }
    public void Explode()
    {
        Instantiate(deathSprite, transform.position, transform.rotation);
        Instantiate(deathEffect, transform.position, transform.rotation);
        Tools.SetColorOnGameObject(gameObject, new Color(1, 1, 1, 0));
        Destroy(gameObject, deathDelay);
    }
}
