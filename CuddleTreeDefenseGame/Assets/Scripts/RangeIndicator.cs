using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangeIndicator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject rangeIndicatorPrefab;
    private Renderer rangeIndicator;
    private IRangeIndicator objectRange;

    private float radius;
    public float Radius { get => radius; set => radius = value * 2; }
    public void Awake()
    {
        var obj = Instantiate(rangeIndicatorPrefab, transform.position, transform.rotation);
        rangeIndicator = obj.GetComponent<Renderer>();
        rangeIndicator.enabled = false;
        objectRange = GetComponent<IRangeIndicator>();
        if(objectRange == null)
        {
            throw new UnityException($"Could not find any implementation of IRangeIndicator interface on {gameObject}. Implement the interface or remove RangeIndicator component from the game object.");
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Radius = objectRange.RangeIndicatorSize;
        rangeIndicator.gameObject.transform.position = gameObject.transform.position;
        rangeIndicator.gameObject.transform.localScale = new Vector2(Radius, Radius);
        rangeIndicator.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rangeIndicator.enabled = false;
    }
    private void OnDisable()
    {
        if(rangeIndicator != null)
            rangeIndicator.enabled = false;
    }
}