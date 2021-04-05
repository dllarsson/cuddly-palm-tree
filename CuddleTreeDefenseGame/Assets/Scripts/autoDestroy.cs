using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] float destroyInSeconds = 5;
    void Start()
    {
        Destroy(gameObject, destroyInSeconds);
    }
}
