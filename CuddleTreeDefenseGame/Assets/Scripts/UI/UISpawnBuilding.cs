using UnityEngine;

public class UISpawnBuilding : UIButton
{
    [SerializeField] private GameObject buildingPrefab;
    public override void OnButtonClick()
    {
        EventHandler.current.BuildingSpawn(buildingPrefab);
    }
}