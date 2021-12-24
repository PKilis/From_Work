using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cameraFollow : MonoBehaviour
{

    public static cameraFollow instance = null;
    public Transform hedef;
    public float kameraTakipHizi = 5f;
    private Vector3 offsetVektor;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        hedef = GameObject.FindWithTag("FrenchPlayer").transform;
        offsetVektor = transform.position - hedef.position;
    }
    private void LateUpdate()
    {
        Vector3 targetToMove = hedef.position + offsetVektor;
        transform.position = Vector3.Lerp(transform.position, targetToMove, kameraTakipHizi * Time.deltaTime);
    }
    public void ResetCamera()
    {
        Vector3 cameram = new Vector3(7.33400011f, 2.24799991f, -22.9500008f);

    }
}
