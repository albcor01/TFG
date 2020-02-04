using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMTest : MonoBehaviour
{

    public float floatParam { get; set; }
    public bool boolParam { get; set; }
    public int intParam { get; set; }

    //Inicialización
    void Start()
    {
        floatParam = 0;
        boolParam = false;
        intParam = 0;
    }

    //Prueba
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            floatParam -= Time.deltaTime;
        else if (Input.GetKey(KeyCode.D))
            floatParam += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.B))
            boolParam = !boolParam;
    }
}
