using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    [SerializeField] GameObject Target;

    [SerializeField] public float Hp = 100;
    bool isDead = false;

    // Update is called once per frame
    void Update() 
    {


        if (Hp <= 0)
        {
            isDead = true;
            kill();
        }
        
        Debug.Log(Target.transform.position);
        Vector3 _towerPosition = Target.transform.position;
        float step = 0.5f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _towerPosition, step);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Hp -= 0.5f;
    }
    private void kill()
    {
        // Do kill animation here
        Destroy(gameObject);
    }
}