using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * 10;
        transform.eulerAngles += transform.up * Time.deltaTime * 20;
    }
}
