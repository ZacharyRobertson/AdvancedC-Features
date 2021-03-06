﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RotateToVelocity2D : MonoBehaviour
{
    private Rigidbody2D rigid;
    // Use this for initialization
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vel = rigid.velocity;
        float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
