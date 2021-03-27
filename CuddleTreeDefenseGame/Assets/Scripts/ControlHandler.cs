using UnityEngine;
using System.Collections;

public class ControlHandler : MonoBehaviour
{
    [SerializeField] GameObject buildPrefabWithButtonT;
    private bool isTPressed;
    private GameObject obj;
    private Coroutine myCoroutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isTPressed = true;
            obj = Instantiate(buildPrefabWithButtonT);
            Tools.SetColorOnGameObject(obj, new Color(1, 1, 1, 0.25f));
            Tools.ToggleScriptsInGameObject(obj, false);
            myCoroutine = StartCoroutine(FollowMouse(obj));
        }
        if ( isTPressed && Input.GetMouseButton(0))
        {
            isTPressed = false;
            StopCoroutine(myCoroutine);
            Tools.SetColorOnGameObject(obj, new Color(1, 1, 1, 1));
            Tools.ToggleScriptsInGameObject(obj, true);
        }

    }
    IEnumerator FollowMouse(GameObject src)
    {
        while (true)
        {
            Vector3 worldpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldpos = Tools.GetScreenToWorldPoint2D(worldpos);
            src.transform.position = worldpos;

            yield return null;
        }
    }
}


