using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour, IHealthHandler
{

    [SerializeField] float health = 100;

    Collider2D target;
    bool hasTarget = false;
    public float Health { get => health; set => health = value; }

    private void Start()
    {
        target = FindNearestTarget();
    }
    void Update()
    {
        float step = 0.5f * Time.deltaTime;

        if (hasTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
        }
        else
        {
            target = FindNearestTarget();
        }
    }

    private Collider2D FindNearestTarget()
    {
        Collider2D nearestTarget = null;
        foreach (var target in Physics2D.OverlapCircleAll(transform.position, Mathf.Infinity))
        {
            if (target != null && target.gameObject.CompareTag("Tower"))
            {
                nearestTarget = target;
            }
        }
        if (nearestTarget != null) hasTarget = true;
        return nearestTarget;
    }
    public void OnDamage(float damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            OnDeath();
        }
    }
    public void OnDeath()
    {
        Destroy(gameObject);
    }
}