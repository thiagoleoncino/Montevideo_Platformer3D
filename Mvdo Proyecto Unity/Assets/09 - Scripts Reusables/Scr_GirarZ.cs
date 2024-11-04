using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GirarZ : MonoBehaviour
{
    public float poderZ = 100;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, poderZ * Time.deltaTime);
    }
}
