using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IDamageDealer
{
    [SerializeField] float projectileDamage;
    [Header("Effects")]
    [SerializeField] GameObject deathSprite;
    [SerializeField] GameObject deathEffect;
    [SerializeField] float deathDelay = 0.2f;

    public List<string> TargetTag { get; set; } = new List<string>();
    public float Damage { get => projectileDamage; set => projectileDamage = value; }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        var target = collision.gameObject;
        var targetHealth = target.GetComponent<IHealthHandler>();
        if(targetHealth != null && TargetTag.Contains(target.tag))
        {
            targetHealth.OnDamage(Damage);
        }
        OnDeath();
    }
    public void OnDeath()
    {
        Instantiate(deathSprite, transform.position, transform.rotation);
        Instantiate(deathEffect, transform.position, transform.rotation);
        Tools.SetColorOnGameObject(gameObject, new Color(1, 1, 1, 0));
        Destroy(gameObject, deathDelay);
    }
}