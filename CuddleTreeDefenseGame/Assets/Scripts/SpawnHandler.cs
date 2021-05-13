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
        EventHandler.current.OnBuildingSpawn += CreateGhostBuilding;
    }
    void CreateGhostBuilding(GameObject buildingPrefab)
    {
        if (!isConstructing)
        {
            createdAsset = Instantiate(buildingPrefab);
            Utility.Tools.SetColorOnGameObject(createdAsset, ghostCanPlace);
            Utility.Tools.ToggleScriptsInGameObject(createdAsset, false);
            buildingPlacementRoutine = StartCoroutine(FollowMouse(createdAsset));
            EventHandler.current.OnMouseClick += PlaceBuilding;
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
                EventHandler.current.OnMouseClick -= PlaceBuilding;
                StopCoroutine(buildingPlacementRoutine);
                Utility.Tools.SetColorOnGameObject(createdAsset, ordinaryColor);
                Utility.Tools.ToggleScriptsInGameObject(createdAsset, true);
                isConstructing = false;
            }
        }
    }
    IEnumerator FollowMouse(GameObject src)
    {
        while (true)
        {
            var worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldpos = Utility.Tools.GetScreenToWorldPoint2D(worldpos);
            src.transform.position = worldpos;
            if(!CanBePlacedOnSpot())
            {
                Utility.Tools.SetColorOnGameObject(createdAsset, ghostInvalidPlacement);
            }
            else
            {
                Utility.Tools.SetColorOnGameObject(createdAsset, ghostCanPlace);
            }

            yield return null;
        }
    }
}
