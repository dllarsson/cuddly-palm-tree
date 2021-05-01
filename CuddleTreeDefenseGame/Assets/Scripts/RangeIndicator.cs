using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RangeIndicator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject rangeIndicatorPrefab;
    Renderer rangeIndicator;
    private float radius = 10f;

    public float Radius { get => radius; set => radius = value * 2; }
    public void Awake()
    {
        var obj = Instantiate(rangeIndicatorPrefab, transform.position, transform.rotation);
        rangeIndicator = obj.GetComponent<Renderer>();
        rangeIndicator.enabled = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
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