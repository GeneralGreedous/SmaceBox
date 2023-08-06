using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarSystem : MonoBehaviour
{

    public void OnMouseDown()
    {
        ShipController.instance.SetTarget(gameObject);
        //float x = Vector3.Distance(transform.position, ShipController.instance.transform.position);
        //Debug.Log(x);
    }
}
