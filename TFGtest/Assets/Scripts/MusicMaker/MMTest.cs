using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMTest : MonoBehaviour
{

    public float floatParam { get; set; }
    public bool boolParam { get; set; }

    //Inicialización
    void Start()
    {
        floatParam = 0;
        boolParam = false;
    }

    //Prueba
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            floatParam -= Time.deltaTime;
        else if (Input.GetKeyDown(KeyCode.D))
            floatParam += Time.deltaTime;
        else if (Input.GetKeyDown(KeyCode.B))
            boolParam = !boolParam;
    }
}
