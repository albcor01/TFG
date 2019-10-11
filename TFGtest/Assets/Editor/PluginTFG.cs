using UnityEngine;
using UnityEditor;

using System.Collections;
using System.Collections.Generic;


/// <summary>
/// GUILayout.Width - overrides the fixedWidth of the used style

///GUILayout.Height - overrides the fixedHeight of the used style

///GUILayout.MaxHeight - adds an additional size-constraint

///GUILayout.MaxWidth - adds an additional size-constraint

///GUILayout.MinHeight - adds an additional size-constraint

///GUILayout.MinWidth - adds an additional size-constraint

///GUILayout.ExpandWidth - overrides stretchWidth of the used style

///GUILayout.ExpandHeight
/// </summary>
public class PluginTFG : EditorWindow
{
    //Variables de prueba
    string myString = "Hola Mundo";
    string mensajeDebug = "";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    //Listas de objetos y componentes que se van a usar
    List<Object> objetos = new List<Object>();
    List<List<Component>> componentes = new List<List<Component>>();
    List<List<List<string>>> variables = new List<List<List<string>>>();

    //"Estilo" (es como el CSS del editor)
    //private static GUIStyle myStyle;

    //Selectores
    GUILayoutOption[] addObjectsGUI = new GUILayoutOption[]
    {
        GUILayout.Width(200)
    };
    //Botones para quitar
    GUILayoutOption[] removeGUI = new GUILayoutOption[] 
    {
        GUILayout.Width(20)
    };

    //Selectores
    GUILayoutOption[] addComponentsGUI = new GUILayoutOption[]
    {
        GUILayout.Width(120)
    };

    //Selectores
    GUILayoutOption[] addVariableGUI = new GUILayoutOption[]
    {
        GUILayout.Width(80)
    };

    // Inicializa la ventana
    [MenuItem("Tools/Plugin TFG")]
    static void Init()
    {
        PluginTFG window = (PluginTFG)EditorWindow.GetWindow(typeof(PluginTFG));
        window.Show();

        //myStyle = new GUIStyle(GUI.skin.label)
        //{
        //    alignment = TextAnchor.MiddleLeft,
        //    margin = new RectOffset(20, 0, 0, 0),
        //    padding = new RectOffset(),
        //    fontSize = 15,
        //    fontStyle = FontStyle.Bold
        //};
    }
    // Aquí es donde se hace todo lo importante
    void OnGUI()
    {
        ////Cogido directamente del manual de Unity
        //GUILayout.Label("Ejemplo de prueba", EditorStyles.boldLabel);
        //myString = EditorGUILayout.TextField("Campo de texto", myString);

        //groupEnabled = EditorGUILayout.BeginToggleGroup("Configuración avanzada", groupEnabled);
        //myBool = EditorGUILayout.Toggle("Toggle", myBool);
        //myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        //EditorGUILayout.EndToggleGroup();

        //Para nuestro plugin
        GUILayout.Label("Música adaptativa", EditorStyles.largeLabel);

        //Cada GameObject cuyos componentes queremos usar
        for (int i = 0; i < objetos.Count; i++)
        {
            GUILayout.Label("Gameobject nº" + (i + 1), EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            objetos[i] = EditorGUILayout.ObjectField(objetos[i], typeof(GameObject), true, addObjectsGUI);

            if (GUILayout.Button("X", removeGUI))
                objetos.Remove(objetos[i]);

            //Añadir un componente nuevo
            if (GUILayout.Button("Añadir Componente", addComponentsGUI))
            {
                componentes[i].Add(new Component());
                variables[i].Add(new List<string>());
                //if (componentes[i].Capacity == componentes[i].Count)
                //{
                    
                //}

                //else
                //    mensajeDebug = "Selecciona un componente para el input anterior";
            }
            EditorGUILayout.EndHorizontal();

            //Cada componente cuyas variables queremos usar
            for (int j = 0; j < componentes[i].Count; j++)
            {
                string cName = "Componente";
                if (componentes[i][j] != null)
                    cName = componentes[i][j].name;
                GUILayout.Label(cName, EditorStyles.miniBoldLabel);

                EditorGUILayout.BeginHorizontal();
                componentes[i][j] = EditorGUILayout.ObjectField(componentes[i][j], typeof(Component), true, addComponentsGUI) as Component;

                //Control de errores (comprobamos que el componente pertenece a ese gameobject)
                if (componentes[i].Count > 0 && componentes[i][j] != null && componentes[i][j].gameObject != objetos[i])
                {
                    componentes[i][j] = null;
                    mensajeDebug = "Asigna un componente del GameObject seleccionado";
                }

                //Quitar un componente
                if (GUILayout.Button("X", removeGUI))
                    componentes[i].Remove(componentes[i][j]);

                //Añadir una variable nueva
                if (GUILayout.Button("Añadir variable", addVariableGUI))
                {
                    string s = "";
                    variables[i][j].Add(s);
                }

                EditorGUILayout.EndHorizontal();

                //Cada componente cuyas variables queremos usar
                for (int k = 0; k < variables[i][j].Count; k++)
                {
                    EditorGUILayout.BeginHorizontal();
                    variables[i][j][k] = EditorGUILayout.TextField(variables[i][j][k], addVariableGUI);

                    //Quitar un componente
                    if (GUILayout.Button("X", removeGUI))
                        variables[i][j].Remove(variables[i][j][k]);

                    EditorGUILayout.EndHorizontal();

                    //TODO: TENER EN CUENTA MINUSCULAS Y MAYUSCULAS
                    if(variables[i][j][k] != null)
                    {
                        if (MusicMaker.Utils.checkVariable(new MusicMaker.Variable(componentes[i][j], variables[i][j][k])))
                            mensajeDebug = "Variable correcta";
                        else
                            mensajeDebug = "La variable " + variables[i][j][k] + " no se encuentra en el componente " + componentes[i][j];
                    }
                }
            }
        }

        //Añadir un objeto nuevo
        if (GUILayout.Button("Añadir GameObject"))
        {
            objetos.Add(new Object());
            componentes.Add(new List<Component>());
            variables.Add(new List<List<string>>());
        }


        GUILayout.Label(mensajeDebug, EditorStyles.boldLabel);
    }

    /// <summary>
    /// Actualiza la aplicación 
    /// </summary>
    void Update()
    {
        if (EditorApplication.isPlaying)
        {
            Repaint();
        }
    }
}
