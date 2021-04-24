using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] MouseButton placeMouseButton;
    private GameObject createdAsset;
    private Coroutine buildingPlacementRoutine;
    private Color ghostCanPlace = new Color(1.0f, 1.0f, 1.0f, 0.25f);
    private Color ghostInvalidPlacement = new Color(1.0f, 0.0f, 0.0f, 0.25f);
    private Color ordinaryColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    bool isConstructing = false;
    private void Start()
    {
        //Move this if we want to be able to spawn building from the start
        EventHandler.current.onBuildingSpawn += CreateGhostBuilding;
    }
    void CreateGhostBuilding(GameObject buildingPrefab)
    {
        if (!isConstructing)
        {
            createdAsset = Instantiate(buildingPrefab);
            Tools.SetColorOnGameObject(createdAsset, ghostCanPlace);
            Tools.ToggleScriptsInGameObject(createdAsset, false);
            buildingPlacementRoutine = StartCoroutine(FollowMouse(createdAsset));
            EventHandler.current.onMouseClick += PlaceBuilding;
            isConstructing = true;
        }

    }
    bool CanBePlacedOnSpot()
    {
        int nrOfCollitions = createdAsset.GetComponent<Collider2D>().OverlapCollider(new ContactFilter2D(), new Collider2D[1]);
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
            if(CanBePlacedOnSpot())
            {
                EventHandler.current.onMouseClick -= PlaceBuilding;
                StopCoroutine(buildingPlacementRoutine);
                Tools.SetColorOnGameObject(createdAsset, ordinaryColor);
                Tools.ToggleScriptsInGameObject(createdAsset, true);
                isConstructing = false;
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
            if(!CanBePlacedOnSpot())
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
