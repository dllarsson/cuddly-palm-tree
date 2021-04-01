using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestroy : MonoBehaviour
{
    [SerializeField] float destroyInSeconds = 5;
    void Start()
    {
        Destroy(gameObject, destroyInSeconds);
    }
}
