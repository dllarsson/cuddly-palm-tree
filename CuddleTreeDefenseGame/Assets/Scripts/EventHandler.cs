using System;
using UnityEngine;
using UnityEngine.UIElements;

public class EventHandler : MonoBehaviour
{
    public static EventHandler current;
    private void Awake()
    {
        current = this;
    }
    public event Action<KeyCode> onKeyPress;
    public void KeyPress(KeyCode key)
    {
        if(onKeyPress != null)
        {
            onKeyPress(key);
        }
    }
    public event Action<MouseButton> onMouseClick;
    public void MouseClick(MouseButton button)
    {
        if(onMouseClick != null)
        {
            onMouseClick(button);
        }
    }
    public event Action<GameObject> onBuildingSpawn;
    public void BuildingSpawn(GameObject buildingPrefab)
    {
        if(onBuildingSpawn != null)
        {
            onBuildingSpawn(buildingPrefab);
        }
    }
}