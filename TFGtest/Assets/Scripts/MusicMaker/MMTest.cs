using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMTest : MonoBehaviour
{

    public float floatParam { get; set; }
    public bool boolParam { get; set; }
    public int intParam { get; set; }

    //Inicialización
    void Awake()
    {
        floatParam = 1;
        boolParam = false;
        intParam = 0;
    }

    //Prueba
    void Update()
    {
        float test = 1.0f;
        //Arrancar/cerrar el servidor
        if (Input.GetKeyDown(KeyCode.Return))
            MusicMaker.instance.LaunchServer();
        if (Input.GetKeyDown(KeyCode.Escape))
            MusicMaker.instance.CloseServer();

        //Reproducir/pausar música
        if (Input.GetKeyDown(KeyCode.P))
            MusicMaker.instance.PlayMusic();
        else if (Input.GetKeyDown(KeyCode.S))
            MusicMaker.instance.StopMusic();

        //Parámetro float
        if (Input.GetKey(KeyCode.A))
            floatParam -= Time.deltaTime;
        else if (Input.GetKey(KeyCode.D))
            floatParam += Time.deltaTime;

        //Parámetro booleano
        if (Input.GetKeyDown(KeyCode.B))
            boolParam = !boolParam;
    }
}
