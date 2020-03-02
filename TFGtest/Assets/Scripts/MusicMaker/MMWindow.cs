using UnityEngine;
using UnityEditor;
using System;
using System.IO;
//Ventana del editor para elegir parámetros sobre la música generativa
public class MMWindow : EditorWindow
{
    #region Variables
    string myString = "Sharknado";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 0.5f;
    MM.Package package;
    #endregion

    #region Métodos de Unity
    /*
     * Inicializa la ventana y crea una entrada en el menú
     */
    [MenuItem("Window/Music Maker")]
    static void Init()
    {
        MMWindow window = (MMWindow)EditorWindow.GetWindow(typeof(MMWindow));
        window.Show();
    }

    /*
     * Se llama cada vez que se abre la ventana
     */
    private void Awake()
    {
        //Para recuperar los settings que habíamos puesto
        string packageName = EditorPrefs.GetString("PackageType");
        package = (MM.Package)Enum.Parse(typeof(MM.Package), packageName);
    }

    /*
     * "Tick" de la ventana (para mostrar cosas al usuario)
     */
    void OnGUI()
    {
        GUILayout.Label("Selección de paquete temático", EditorStyles.boldLabel);
        package = (MM.Package)EditorGUILayout.EnumPopup("Elige un paquete:", package);

        //Se ha elegido un paquete
        if(package != MM.Package.None)
        {
            groupEnabled = EditorGUILayout.BeginToggleGroup("Personalizar paquete", groupEnabled);
            myBool = EditorGUILayout.Toggle("Usar VST", myBool);
            myFloat = EditorGUILayout.Slider("Tempo", myFloat, 0, 1);
            myString = EditorGUILayout.TextField("Tu película favorita", myString);
            EditorGUILayout.EndToggleGroup();
        }

        //Guardar los cambios
        if (GUILayout.Button("Guardar cambios"))
        {
            //Guardamos en EditorPrefs...
            EditorPrefs.SetString("PackageType", package.ToString());
            //...y también en archivo para que luego lo lea el MonoBehaviour
            MusicMaker.Instance.SetPackage(package);
            File.WriteAllText(Application.persistentDataPath + "/save.json", package.ToString());
            if (package != MM.Package.None)
                Debug.Log("Has elegido el paquete " + package.ToString());
            else
                Debug.Log("Ningún paquete seleccionado");
        }
    }


    /*
     * "Tick" de la ventana (para ejecutar la lógica)
     * En principio pa esto no hace falta
     */
    //void Update()
    //{

    //}
    #endregion
}
