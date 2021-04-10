using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] MouseButton placeMouseButton;
    private GameObject createdAsset;
    private Coroutine buildingPlacementRoutine;
    private Vector4 ghostCanPlace = new Vector4(1, 1, 1, 0.25f);
    private Vector4 ghostInvalidPlacement = new Vector4(1, 0, 0, 0.25f);
    private void Start()
    {
        //Move this if we want to be able to spawn building from the start
        EventHandler.current.onBuildingSpawn += CreateGhostBuilding;
    }
    void CreateGhostBuilding(GameObject buildingPrefab)
    {
        createdAsset = Instantiate(buildingPrefab);
        Tools.SetColorOnGameObject(createdAsset, ghostCanPlace);
        Tools.ToggleScriptsInGameObject(createdAsset, false);
        buildingPlacementRoutine = StartCoroutine(FollowMouse(createdAsset));
        EventHandler.current.onMouseClick += PlaceBuilding;
        createdAsset.tag = "Ghost";
    }
    bool canBePlacedOnSpot()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        Collider2D[] collidedThings = new Collider2D[1];

        var nrOfCollitions = createdAsset.GetComponent<Collider2D>().OverlapCollider(contactFilter, collidedThings);
        if (nrOfCollitions != 0)
        {
            return false;
        }

        return true;
    }

    void PlaceBuilding(MouseButton button)
    {
        if(button == placeMouseButton)
        {
            if(canBePlacedOnSpot())
            {
                EventHandler.current.onMouseClick -= PlaceBuilding;
                StopCoroutine(buildingPlacementRoutine);
                Tools.SetColorOnGameObject(createdAsset, new Color(1, 1, 1, 1));
                Tools.ToggleScriptsInGameObject(createdAsset, true);
                createdAsset.tag = "Tower";
            }
        }
    }
    IEnumerator FollowMouse(GameObject src)
    {
        while (true)
        {
            var worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldpos = Tools.GetScreenToWorldPoint2D(worldpos);
            src.transform.position = worldpos;
            if(!canBePlacedOnSpot())
            {
                Tools.SetColorOnGameObject(createdAsset, ghostInvalidPlacement);
            }
            else
            {
                Tools.SetColorOnGameObject(createdAsset, ghostCanPlace);
            }

            yield return null;
        }
    }
}
