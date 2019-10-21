using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventThrower : MonoBehaviour
{
    private const string ClientId = "SuperCollider";

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            float t = 1.0f;
            OSCHandler.Instance.SendMessageToClient(ClientId, "/eventStop", t);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            float t = 1.0f;
            OSCHandler.Instance.SendMessageToClient(ClientId, "/event0", t);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            float t = 1.0f;
            OSCHandler.Instance.SendMessageToClient(ClientId, "/event1", t);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            float t = 1.0f;
            OSCHandler.Instance.SendMessageToClient(ClientId, "/event2", t);
        }
    }
}
