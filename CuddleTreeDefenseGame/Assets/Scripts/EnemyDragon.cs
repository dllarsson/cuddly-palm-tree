using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    [SerializeField] GameObject Target;

    [SerializeField] public float Hp = 100;

    // Update is called once per frame
    void Update() 
    {
        if (Hp <= 0)
        {
            Kill();
        }
        
        Vector3 _towerPosition = Target.transform.position;
        float step = 0.5f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _towerPosition, step);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        Hp -= 10f;
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
}