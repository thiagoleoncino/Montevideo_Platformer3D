using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GirarY : MonoBehaviour
{
    public float poderY = 100;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, poderY * Time.deltaTime, 0);
    }
}
