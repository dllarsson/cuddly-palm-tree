using UnityEngine.UI;
using UnityEngine;

public abstract class UIButton : MonoBehaviour
{
    [SerializeField] KeyCode hotkey;
    void Start()
    {
        EventHandler.current.OnKeyPress += HotkeyPressed;
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClick());
    }
    void HotkeyPressed(KeyCode key)
    {
        if(key == hotkey)
        {
            OnButtonClick();
        }
    }
    public abstract void OnButtonClick();
    void OnDestroy()
    {
        EventHandler.current.OnKeyPress -= HotkeyPressed;
    }
}