using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject rangeIndicatorPrefab;
    [SerializeField] GameObject turret;
    GameObject rangeIndicatorObject;
    bool isEnabled = true;

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
    }
    private void OnMouseEnter()
    {

        if(isEnabled && rangeIndicatorObject == null)
        {
            rangeIndicatorObject = Instantiate(rangeIndicatorPrefab, transform.position, transform.rotation);
            rangeIndicatorObject.transform.parent = gameObject.transform;
            var radius = transform.GetChild(0).gameObject.GetComponent<Turret>().MaxTurretRange * 2;
            rangeIndicatorObject.transform.localScale = new Vector2(radius, radius);
        }
    }
    private void OnMouseExit()
    {
        if(isEnabled && rangeIndicatorObject != null)
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
    private void OnEnable()
    {
        isEnabled = true;
    }
    private void OnDisable()
    {
        isEnabled = false;
    }
    private void Explode()
    {
        gameObject.GetComponent<DeathEffect>().Death = true;
        turret.GetComponent<DeathEffect>().Death = true;
    }

    //DEBUG
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("asd");
        if(eventData.button == PointerEventData.InputButton.Right)
            Explode();
    }
}
