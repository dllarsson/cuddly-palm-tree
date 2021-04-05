using UnityEngine;
using UnityEngine.UI;

public class UISpawnBuilding : MonoBehaviour
{
    [SerializeField] KeyCode hotkey;
    [SerializeField] GameObject buildingPrefab;
    void Start()
    {
        EventHandler.current.onKeyPress += HotkeyPressed;
        var button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => ButtonClickListener());
    }
    void HotkeyPressed(KeyCode key)
    {
        if(key == hotkey)
        {
            ButtonClickListener();
        }
    }
    void ButtonClickListener()
    {
        EventHandler.current.BuildingSpawn(buildingPrefab);

    }
    private void OnDestroy()
    {
        EventHandler.current.onKeyPress -= HotkeyPressed;
    }
}