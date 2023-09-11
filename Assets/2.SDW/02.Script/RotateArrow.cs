using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArrow : MonoBehaviour
{
    void Start()
    {
        transform.Rotate(Vector3.forward * 90f);
    }
}
