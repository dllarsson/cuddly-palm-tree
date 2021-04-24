using UnityEngine;
using UnityEngine.UI;

public class UISpawnBuilding : UIButton
{
    [SerializeField] GameObject buildingPrefab;
    public override void OnButtonClick()
    {
        EventHandler.current.BuildingSpawn(buildingPrefab);
    }
}