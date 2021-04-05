using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputHandler : MonoBehaviour
{
    private List<KeyCode> keyEventCache = new List<KeyCode>();
    private void OnGUI()
    {
        var currentEvent = Event.current;
        if(currentEvent.isKey)
        {
            if(currentEvent.type == EventType.KeyDown && currentEvent.keyCode != KeyCode.None
                && !keyEventCache.Contains(currentEvent.keyCode))
            {
                keyEventCache.Add(currentEvent.keyCode);
                EventHandler.current.KeyPress(currentEvent.keyCode);
            }
            else if(currentEvent.type == EventType.KeyUp)
            {
                keyEventCache.Remove(currentEvent.keyCode);
            }
        }
        else if(currentEvent.isMouse)
        {
            if(currentEvent.type == EventType.MouseDown)
            {
                EventHandler.current.MouseClick((MouseButton)currentEvent.button);
            }
        }
    }
}