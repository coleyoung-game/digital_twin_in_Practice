using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private RaycastHit _hit;
    public Vector2 Position { get; private set; }
    public event Action OnAction_RightClick;
    
    public void LeftClickAction(InputAction.CallbackContext ctx)
    {
        InputActionPhase phase = ctx.phase;
        switch (phase)
        {
            case InputActionPhase.Performed:
                Debug.Log("Left Click Performed");
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
        Debug.Log($"Mouse Position: {Position}");
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
}
