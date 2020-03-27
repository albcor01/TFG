using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int VEL = 5;
    const int ROT_VEL = 2;
    public bool lightOn { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        lightOn = true;
    }

    //Prueba
    void Update()
    {
        //Mover 
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Time.deltaTime * VEL);
        else if (Input.GetKey(KeyCode.S))
            transform.Translate(-Vector3.forward * Time.deltaTime * VEL);
        //Rotar
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(-Vector3.up * ROT_VEL);
        else if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up * ROT_VEL);

        //Linterna
        if (Input.GetKeyDown(KeyCode.F))
        {
            lightOn = !lightOn;
            transform.GetChild(0).gameObject.SetActive(lightOn);
        }
    }
}
