﻿using UnityEngine;

public class MMTest : MonoBehaviour
{
    #region Variables
    public float floatParam { get; set; }
    public bool boolParam { get; set; }
    public bool[] boolArrayParam { get; set; }
    public int intParam { get; set; }

    #endregion

    #region Métodos de Unity
    //Inicialización
    void Awake()
    {
        floatParam = 1;
        boolParam = false;
        intParam = 0;
        boolArrayParam = new bool[4];
        for (int i = 0; i < 4; i++)
                boolArrayParam[i] = false;
    }

    //Prueba
    void Update()
    {
        ////Arrancar/cerrar el servidor
        //if (Input.GetKeyDown(KeyCode.Return))
        //    MusicMaker.Instance.LaunchServer();
        //if (Input.GetKeyDown(KeyCode.Escape))
        //    MusicMaker.Instance.CloseServer();

        ////Reproducir/pausar música
        //if (Input.GetKeyDown(KeyCode.P))
        //    MusicMaker.Instance.PlayMusic();
        //else if (Input.GetKeyDown(KeyCode.S))
        //    MusicMaker.Instance.StopMusic();

        //Parámetro float
        if (Input.GetKey(KeyCode.A))
            floatParam -= Time.deltaTime;
        else if (Input.GetKey(KeyCode.D))
            floatParam += Time.deltaTime;

        //Parámetro int
        if (Input.GetKey(KeyCode.Z))
            intParam -= (int)(Time.deltaTime * 10);
        else if (Input.GetKey(KeyCode.C))
            intParam += (int)(Time.deltaTime * 10);

        //Parámetro booleano
        if (Input.GetKeyDown(KeyCode.B))
            boolParam = !boolParam;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            boolArrayParam[0] = !boolArrayParam[0];
        if (Input.GetKeyDown(KeyCode.Alpha2))
            boolArrayParam[1] = !boolArrayParam[1];
        if (Input.GetKeyDown(KeyCode.Alpha3))
            boolArrayParam[2] = !boolArrayParam[2];

    }

    #endregion
}
