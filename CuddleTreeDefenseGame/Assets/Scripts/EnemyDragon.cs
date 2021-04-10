using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{

    [SerializeField] public float Hp = 100;

    Collider2D target;
    bool hasTarget = false;

    private void Start()
    {
        target = FindNearestTarget();
    }
    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            Kill();
        }
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ghost" || collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else
        {
            Hp -= 10f;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Hp -= 0.5f;
    }
    private void Kill()
    {
        // Do kill animation here
        Destroy(gameObject);
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
}