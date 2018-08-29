using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_Fire : BaseBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var addPos = Vector3.forward * 10;
        transform.position += transform.rotation * addPos * Time.deltaTime;
    }
}
