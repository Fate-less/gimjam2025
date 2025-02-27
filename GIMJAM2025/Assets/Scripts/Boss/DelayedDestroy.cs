using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroy : MonoBehaviour
{
    [Header("Stats")]
    public float delay;
    void Start()
    {
        Destroy(gameObject, delay);
    }
}