using System;
using UnityEngine;

public class Tank : MonoBehaviour
{
    private Transform bodyTransform;
    private Transform batteryTransform;

    void Start()
    {
        bodyTransform = GameObject.Find("Body").GetComponent<Transform>();
        batteryTransform = GameObject.Find("Battery").GetComponent<Transform>();
    }

    void Update()
    {
        this.transform.position += new Vector3(-0.01f, 0, 0);
    }
}