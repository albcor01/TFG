using UnityEngine;

public class MMTest : MonoBehaviour
{
    #region Variables
    public float floatParam { get; set; }
    public bool boolParam { get; set; }
    public int intParam { get; set; }

    #endregion

    #region Métodos de Unity
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
    }

    #endregion
}
