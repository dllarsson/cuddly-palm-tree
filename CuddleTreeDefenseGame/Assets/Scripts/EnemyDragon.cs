using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    [SerializeField] GameObject Target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update() 
    {
        Vector3 _towerPosition = Target.transform.position;
        float step = 0.5f * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position,_towerPosition, step);
    }
}
