using System;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject insideWall;
    private GameObject cloneObject;
    private Transform cloneObjectTransform;
    void Start()
    {
        insideObject(1,50,1);
    }

    void Update()
    {

    }
    void insideObject(int x, int y, int z)
    {
        //this.Transform.Scale(0,y,0);
        cloneObject = Instantiate(insideWall, new Vector3(-5.0f*x-2.5f, y/2.0f, 5.0f*z+2.5f), Quaternion.identity);
        cloneObjectTransform = cloneObject.GetComponent<Transform>();
        cloneObjectTransform.localScale = new Vector3(5, y, 5);
    }
}