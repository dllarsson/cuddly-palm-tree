//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Tower : MonoBehaviour
//{
//    [SerializeField] GameObject rangeIndicatorPrefab;
//    GameObject rangeIndicatorObject;
//    bool isEnabled = true;

//    private void OnMouseDown()
//    {
//        GetComponent<SpriteRenderer>().color = Color.green;
//    }
//    private void OnMouseEnter()
//    {

//        if(isEnabled && rangeIndicatorObject == null)
//        {
//            rangeIndicatorObject = Instantiate(rangeIndicatorPrefab, transform.position, transform.rotation);
//            rangeIndicatorObject.transform.parent = gameObject.transform;
//            var radius = transform.GetChild(0).gameObject.GetComponent<Turret>().MaxTurretRange * 2;
//            rangeIndicatorObject.transform.localScale = new Vector2(radius, radius);
//        }
//    }
//    private void OnMouseExit()
//    {
//        if(isEnabled && rangeIndicatorObject != null)
//        {
//            Destroy(rangeIndicatorObject);
//            rangeIndicatorObject = null;
//        }
//    }

//    private void OnMouseUp()
//    {
//        GetComponent<SpriteRenderer>().color = Color.white;
//    }

//    private void OnMouseDrag()
//    {
//        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//        transform.position = new Vector3(newPos.x, newPos.y);
//    }
//    private void OnEnable()
//    {
//        isEnabled = true;
//    }
//    private void OnDisable()
//    {
//        isEnabled = false;
//    }
//}
