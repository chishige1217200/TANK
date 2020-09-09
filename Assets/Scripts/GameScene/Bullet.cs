using System;
using UnityEngine;

public class Bullet: MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        this.transform.position += new Vector3(0, 0, 0.5f);
    }
}