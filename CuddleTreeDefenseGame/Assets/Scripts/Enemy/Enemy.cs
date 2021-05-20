using UnityEngine;
public abstract class Enemy : MonoBehaviour, IHealthHandler
{
    [SerializeField] float maxHealth = 100;
    public float Health { get; set; }
    public float MaxHealth => maxHealth;

    public virtual void OnDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            OnDeath();
        }
    }
    public virtual void OnDeath()
    {
        Destroy(gameObject);
    }
}