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
    public event Action<KeyCode, EventModifiers> OnKeyPress;
    public void KeyPress(KeyCode key, EventModifiers modifier)
    {
        OnKeyPress?.Invoke(key, modifier);
    }
    public event Action<MouseButton> OnMouseClick;
    public void MouseClick(MouseButton button)
    {
        if(OnMouseClick != null)
        {
            OnMouseClick(button);
        }
    }
    public event Action<GameObject> OnBuildingSpawn;
    public void BuildingSpawn(GameObject buildingPrefab)
    {
        if(OnBuildingSpawn != null)
        {
            OnBuildingSpawn(buildingPrefab);
        }
    }
}