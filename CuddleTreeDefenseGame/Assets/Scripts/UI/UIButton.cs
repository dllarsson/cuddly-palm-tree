using UnityEngine.UI;
using UnityEngine;

public abstract class UIButton : MonoBehaviour
{
    [SerializeField] KeyCode hotkey;
    private void Start()
    {
        EventHandler.current.OnKeyPress += (key, modifier) =>
        {
            if(key == hotkey && modifier == EventModifiers.None)
                OnButtonClick();
        };
        var button = GetComponent<Button>();
        button.onClick.AddListener(() => OnButtonClick());
    }
    public abstract void OnButtonClick();
}