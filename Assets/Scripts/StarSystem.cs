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
        ShipController.Instance.SetTarget(gameObject);
        
    }

    private void OnMouseEnter()
    {
        
        ShipController.Instance.LineToTarget(true);
    }
    private void OnMouseOver()
    {
        ShipController.Instance.LineToTarget(transform.position);
        
    }
    private void OnMouseExit()
    {
        ShipController.Instance.LineToTarget(false);
    }
}
