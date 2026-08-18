using System;
using System.Diagnostics;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Debug = UnityEngine.Debug;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InputManager : MonoBehaviour
{
    private RaycastHit _hit;
    public Vector2 Position { get; private set; }
    public event Action OnAction_RightClick;
    public event Action<RaycastHit> OnAction_RaycastHit;
    
    public void LeftClickAction(InputAction.CallbackContext ctx)
    {
        InputActionPhase phase = ctx.phase;

        switch (phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("Left Click Performed");
                bool isUI = IsUITouch(Position);
                if(isUI)
                {

                    return;
                }
                bool hasHit = GetRayHit(Position, out _hit);
                if(hasHit)
                {
                    Debug.Log($"RaycastHit: {_hit.collider.gameObject.name}");
                    OnAction_RaycastHit?.Invoke(_hit);
                }
                break;
            default:
                break;
        }
    }
    public void RightClickAction(InputAction.CallbackContext ctx)
    {
        InputActionPhase phase = ctx.phase;
        switch (phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("Right Click Performed");
                OnAction_RightClick?.Invoke();
                break;
            default:
                break;
        }
    }
    public void MousePosition(InputAction.CallbackContext ctx)
    {
        Position = ctx.ReadValue<Vector2>();
        //Debug.Log($"Mouse Position: {Position}");
    }
    public void WheelClickAction(InputAction.CallbackContext ctx)
    {
        InputActionPhase phase = ctx.phase;
        switch (phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("Wheel Click Performed");
                break;
            default:
                break;
        }
    }
    private bool GetRayHit(Vector2 pos, out RaycastHit hit)
    {
        hit = default;
        if(Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            if (Physics.Raycast(ray, out hit))
            {
                return true;
            }
        }
        return false;
    }
    private bool IsUITouch(Vector2 point)
    {
        PointerEventData pointData = new PointerEventData(EventSystem.current)
        {
            position = point
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();

        EventSystem.current.RaycastAll(pointData, raycastResults);
        if(raycastResults.Count > 0)
        {
            foreach(var r in raycastResults)
            {
                if(r.gameObject.layer == LayerMask.NameToLayer("UI"))
                {
                    Debug.Log("IsUITouch: True");
                    return true;
                }
            }
        }
        return false;

    }
}
