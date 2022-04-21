using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRotate : MonoBehaviour
{
    public float verticalSpped;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, verticalSpped, 0f) * Time.deltaTime);
    }
}
