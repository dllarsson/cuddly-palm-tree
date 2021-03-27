using UnityEngine;

public class ControlHandler : MonoBehaviour
{
    [SerializeField] GameObject buildPrefabWithButtonT;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector3 worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldpos = Tools.GetScreenToWorldPoint2D(worldpos);

            var obj = Instantiate(buildPrefabWithButtonT, worldpos, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
    }
}
