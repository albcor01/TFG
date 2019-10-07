using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private const string ClientId = "SuperCollider";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            float test = 1.0f;
            OSCHandler.Instance.SendMessageToClient(ClientId, "/test", test);
            Debug.Log("hola");
        }
    }

}
