using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameUtils : SingletonBehaviour<GameUtils>
{
    public static bool CursorOverUI => cursorOverUI ?? (bool)(cursorOverUI = IsPointerOverUIElement());

    public static bool? cursorOverUI = null;

    public static Vector2 WorldMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Mouse.current.position.value);
    }

    public int UILayer;

    private void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
    }

    private void LateUpdate()
    {
        cursorOverUI = null;
    }


    // Following code from user daveMennenoh
    // via https://discussions.unity.com/t/how-to-detect-if-mouse-is-over-ui/821330
    //Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }


    //Returns 'true' if we touched or hovering on Unity UI element.
    private static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaycastResults)
    {
        for (int index = 0; index < eventSystemRaycastResults.Count; index++)
        {
            RaycastResult curRaycastResult = eventSystemRaycastResults[index];
            if (curRaycastResult.gameObject.layer == Instance.UILayer)
                return true;
        }
        return false;
    }


    //Gets all event system raycast results of current mouse or touch position.
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new(EventSystem.current);
        eventData.position = Mouse.current.position.value;
        List<RaycastResult> raycastResults = new();
        EventSystem.current.RaycastAll(eventData, raycastResults);
        return raycastResults;
    }
}
