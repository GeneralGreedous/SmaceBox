using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;


public class CameraController : MonoBehaviour
{
    private Vector2 _mouseOrigine;
    public Vector2 _mouseDiffrence;
    public Vector3 origine;
    private bool _isDragging;
    private bool freeCamera=false;

    public static CameraController instance;

    private void Awake()
    {
        instance = this;
    }

    public void Start()
    {
        transform.position = ShipController.instance.transform.position;
    }

    public void SetCameraTarget(Vector3 newPosition)
    {
        if (!freeCamera)
        {
            transform.position = newPosition;
        }

    }

    public void OndDrag(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            freeCamera=true;
            _mouseOrigine = Mouse.current.position.ReadValue();
            origine = transform.position;
        }
        _isDragging = ctx.started || ctx.performed;

    }

    public void ResetCamera(InputAction.CallbackContext ctx)
    {
        freeCamera=false;
        transform.position=ShipController.instance.transform.position;
    }


    private void LateUpdate()
    {
        if (_isDragging)
        {
            _mouseDiffrence = (_mouseOrigine - Mouse.current.position.ReadValue())/50;
            Vector3 diffrence = new Vector3(_mouseDiffrence.x , _mouseDiffrence.y, 0);
            _mouseDiffrence = new Vector2(diffrence.x, diffrence.y);
            transform.position = diffrence + origine;
        }
    }

}
