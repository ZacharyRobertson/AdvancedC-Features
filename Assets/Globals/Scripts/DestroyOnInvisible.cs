using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DestroyOnInvisible : MonoBehaviour
{

    private Renderer rend;

    // Use this for initialization
    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!rend.isVisible)
        {
            Destroy(gameObject);
        }
    }
}
