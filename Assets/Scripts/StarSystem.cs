using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class StarSystem : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Material _lineMaterial;

    public bool test1 = true;
    public void OnMouseDown()
    {
        ShipController.instance.SetTarget(gameObject);
        //float x = Vector3.Distance(transform.position, ShipController.instance.transform.position);
        //Debug.Log(x);
    }

    private void OnMouseEnter()
    {
        _lineRenderer.enabled = true;
        _lineMaterial = _lineRenderer.material;
    }
    private void OnMouseOver()
    {
        _lineRenderer.positionCount = 2;

        ShipController playerShip = ShipController.instance;

        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, playerShip.shipPosition);

        _lineMaterial.SetInt("_InRange", playerShip.FlyingRange < Vector3.Distance(transform.position, playerShip.transform.position) ? 0 : 1);



    }
    private void OnMouseExit()
    {
        _lineRenderer.enabled = false;
    }
}
