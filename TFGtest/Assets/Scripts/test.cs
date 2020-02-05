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

        else if (Input.GetKeyDown(KeyCode.T))
        {
            float test = 1.0f;
            OSCHandler.Instance.SendMessageToClient(ClientId, "/test5", test);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            float test = 1.0f;
            OSCHandler.Instance.SendMessageToClient(ClientId, "/test6", test);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            float test = 1.0f;
            OSCHandler.Instance.SendMessageToClient(ClientId, "/test7", test);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            string tempo = OSCHandler.Instance.GetValue("SCReciver", "tempo");
            Debug.Log(tempo);

        }
    }

}
