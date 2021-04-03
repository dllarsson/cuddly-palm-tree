using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] GameObject rangeIndicatorPrefab;
    GameObject rangeIndicatorObject;
    public bool IsPlaced { get; set; } = true;

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }
    private void OnMouseEnter()
    {
        if(IsPlaced && rangeIndicatorObject == null)
        {
            rangeIndicatorObject = Instantiate(rangeIndicatorPrefab, transform.position, transform.rotation);
            rangeIndicatorObject.transform.parent = gameObject.transform;
            var radius = transform.GetChild(0).gameObject.GetComponent<Turret>().MaxTurretRange * 2;
            rangeIndicatorObject.transform.localScale = new Vector2(radius, radius);
        }
    }
    private void OnMouseExit()
    {
        if(IsPlaced && rangeIndicatorObject != null)
        {
            Destroy(rangeIndicatorObject);
            rangeIndicatorObject = null;
        }
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseDrag()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPos.x, newPos.y);
    }
}
