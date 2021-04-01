using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableRigidBody : MonoBehaviour
{
    [SerializeField] Vector2 minRange;
    [SerializeField] Vector2 maxRange;
    [SerializeField] float minTorque;
    [SerializeField] float maxTorque;
    [SerializeField] float fadeDuration = 0.5f;
    GameObject[] children;
    float delay = 0;
    void Start()
    {
        children = Tools.GetChildren(gameObject);
        foreach(var child in children)
        {
            var directionX = child.transform.localPosition.x * Random.Range(minRange.x, maxRange.x);
            var directionY = child.transform.localPosition.y * Random.Range(minRange.y, maxRange.y);
            var rigidBody = child.GetComponent<Rigidbody2D>();
            var forceDirection = new Vector2(directionX, directionY);
            rigidBody.AddRelativeForce(forceDirection);
            rigidBody.AddTorque(Random.Range(minTorque, maxTorque));

            var particleSystem = child.GetComponent<ParticleSystem>();
            if(particleSystem != null)
            {
                particleSystem.Stop();
                var main = particleSystem.main;
                main.duration = fadeDuration;
                delay = main.startLifetime.constant;
                particleSystem.Play();
            }
            StartCoroutine(DestroyObject(fadeDuration, delay));
        }
    }
    IEnumerator DestroyObject(float duration, float delay)
    {
        float counter = 0;
        while(counter < duration)
        {
            counter += Time.deltaTime;
            var alpha = Mathf.Lerp(1, 0, counter / duration);
            foreach(var child in children)
            {
                var renderer = child.GetComponent<SpriteRenderer>();
                var origColor = renderer.color;
                renderer.color = new Color(origColor.r, origColor.g, origColor.b, alpha);
            }
            yield return null;
        }
        Destroy(gameObject, delay);
    }
}
