using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) transform.position += new Vector3(0.0f, 0.0f,0.1f);
        if (Input.GetKey(KeyCode.S)) transform.position += new Vector3(0.0f, 0.0f, -0.1f);
    }
}
