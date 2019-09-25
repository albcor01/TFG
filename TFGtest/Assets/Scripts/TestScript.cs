using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private const string ClientId = "SuperCollider";

    // Start is called before the first frame update
    void Start()
    {
        Invoke("soundExe", 2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void soundExe()
    {
        float test = 1.0f;
        OSCHandler.Instance.SendMessageToClient(ClientId, "/test", test);
        Invoke("soundExe", 2.0f);
    }
}
