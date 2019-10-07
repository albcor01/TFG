using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const string ClientId = "SuperCollider";
    private void OnTriggerEnter(Collider other)
    {
            float test = 1.0f;
            OSCHandler.Instance.SendMessageToClient(ClientId, "/test2", test);
    }
}
