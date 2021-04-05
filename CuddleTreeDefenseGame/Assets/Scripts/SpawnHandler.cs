using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class SpawnHandler : MonoBehaviour
{
    [SerializeField] MouseButton placeMouseButton;
    private GameObject createdAsset;
    private Coroutine buildingPlacementRoutine;
    private void Start()
    {
        //Move this if we want to be able to spawn building from the start
        EventHandler.current.onBuildingSpawn += CreateGhostBuilding;
    }
    void CreateGhostBuilding(GameObject buildingPrefab)
    {
        createdAsset = Instantiate(buildingPrefab);
        Tools.SetColorOnGameObject(createdAsset, new Color(1, 1, 1, 0.25f));
        Tools.ToggleScriptsInGameObject(createdAsset, false);
        buildingPlacementRoutine = StartCoroutine(FollowMouse(createdAsset));
        EventHandler.current.onMouseClick += PlaceBuilding;
    }
    void PlaceBuilding(MouseButton button)
    {
        if(button == placeMouseButton)
        {
            EventHandler.current.onMouseClick -= PlaceBuilding;
            StopCoroutine(buildingPlacementRoutine);
            Tools.SetColorOnGameObject(createdAsset, new Color(1, 1, 1, 1));
            Tools.ToggleScriptsInGameObject(createdAsset, true);
        }
    }
    IEnumerator FollowMouse(GameObject src)
    {
        while (true)
        {
            var worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldpos = Tools.GetScreenToWorldPoint2D(worldpos);
            src.transform.position = worldpos;
            yield return null;
        }
    }
}
