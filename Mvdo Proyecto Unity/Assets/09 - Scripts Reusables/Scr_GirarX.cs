using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_GirarX : MonoBehaviour
{
    public float poderX;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(poderX * Time.deltaTime, 0, 0);
    }

}
