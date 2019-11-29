using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using System.IO;


//TODO:
//1. Optimizar el código quitando las listas
//2. Chequear las tuplas válidas
//3. Guardado y cargado de datos
//4. Estructura de la ventana para hacerla como un árbol
//5. Enganchar y probar con SuperCollider
//6. Toggle para usar los atributos del padre / no
//7. Poder seleccionar componentes igual que se hace con las variables

//Se encarga de gestionar la ventana del editor
public class PluginTFG : EditorWindow
{
    //Lista de tuplas <input, efecto, output>, que es lo único relevante que necesitamos
    public static List<MusicMaker.MusicTuple> tuples = new List<MusicMaker.MusicTuple>();

    private static string filePath = "/SavedData/";

    //Listas de objetos y componentes que se van a usar
    string mensajeDebug = "";
    static List<Object> objetos = new List<Object>();
    static List<List<Component>> componentes = new List<List<Component>>();
    static List<List<List<string>>> variables = new List<List<List<string>>>();
    static List<List<List<MusicMaker.MusicEffect>>> effects = new List<List<List<MusicMaker.MusicEffect>>>();
    static List<List<List<MusicMaker.MusicOutput>>> outputs = new List<List<List<MusicMaker.MusicOutput>>>();


    //"Estilo" (es como el CSS del editor)
    //Selectores
    GUILayoutOption[] addObjectsGUI = new GUILayoutOption[] { GUILayout.Width(200) };
    GUILayoutOption[] addComponentsGUI = new GUILayoutOption[] { GUILayout.Width(120) };
    GUILayoutOption[] addVariableGUI = new GUILayoutOption[] { GUILayout.Width(80) };

    //Botón de eliminado
    GUILayoutOption[] removeGUI = new GUILayoutOption[] { GUILayout.Width(20) };


    // Inicializa la ventana
    [MenuItem("Tools/Plugin TFG")]
    static void Init()
    {
        PluginTFG window = (PluginTFG)EditorWindow.GetWindow(typeof(PluginTFG));
        window.Show();
        EditorApplication.playModeStateChanged += LogPlayModeState;
        ///loadSettings();
    }
    // Aquí es donde se hace todo lo importante
    void OnGUI()
    {
        //Para nuestro plugin
        GUILayout.Label("Música adaptativa", EditorStyles.largeLabel);

        //Distinguimos 
        if (EditorApplication.isPlaying)
            drawRunningGUI();
        else
            drawStaticGUI();
    }

    //GUI del PlayMode para ver los valores de las tuplas en ejecución
    void drawRunningGUI()
    {
        List<MusicMaker.MusicTuple> tuplasNuevas = MusicMaker.MMManager.getInstance().getTuples();
        if (tuplasNuevas != null)
        {
            Debug.Log(tuplasNuevas.Count);
            foreach (MusicMaker.MusicTuple mt in tuplasNuevas)
            {
                Debug.Log("Tupla" + tuplasNuevas.IndexOf(mt));
                object o = MusicMaker.Utils.getInputValue(mt.input);
                Debug.Log(o);
            }
        }
    }

    //GUI para cuando no estamos en el PlayMode
    void drawStaticGUI()
    {
        //Cada GameObject cuyos componentes queremos usar
        for (int i = 0; i < objetos.Count; i++)
        {
            GUILayout.Label("Gameobject nº" + (i + 1), EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            objetos[i] = EditorGUILayout.ObjectField(objetos[i], typeof(GameObject), true, addObjectsGUI);
            Object obj = objetos[i];

            if (GUILayout.Button("X", removeGUI))
            {
                objetos.Remove(obj);
                componentes.Remove(componentes[i]);
                variables.Remove(variables[i]);
                effects.Remove(effects[i]);
                outputs.Remove(outputs[i]);
                obj = null;
            }


            //Añadir un componente nuevo
            if (GUILayout.Button("Añadir Componente", addComponentsGUI))
            {
                componentes[i].Add(new Component());
                variables[i].Add(new List<string>());
                effects[i].Add(new List<MusicMaker.MusicEffect>());
                outputs[i].Add(new List<MusicMaker.MusicOutput>());
            }
            EditorGUILayout.EndHorizontal();

            if (obj == null)
                mensajeDebug = "Selecciona primero un objeto válido";
            else
            {
                //Cada componente cuyas variables queremos usar
                for (int j = 0; j < componentes[i].Count; j++)
                {
                    //Selección por defecto
                    string cName = "Componente";
                    if (componentes[i][j] != null)
                        cName = componentes[i][j].name;
                    GUILayout.Label(cName, EditorStyles.miniBoldLabel);

                    EditorGUILayout.BeginHorizontal();
                    componentes[i][j] = EditorGUILayout.ObjectField(componentes[i][j], typeof(Component), true, addComponentsGUI) as Component;
                    Component comp = componentes[i][j];

                    //Control de errores (comprobamos que el componente pertenece a ese gameobject)
                    if (comp != null && comp.gameObject != obj)
                    {
                        comp = null;
                        mensajeDebug = "Asigna un componente del GameObject seleccionado";
                    }

                    //Quitar un componente
                    if (GUILayout.Button("X", removeGUI))
                    {
                        componentes[i].Remove(comp);
                        variables[i].Remove(variables[i][j]);
                        effects[i].Remove(effects[i][j]);
                        outputs[i].Remove(outputs[i][j]);
                        comp = null;
                    }


                    //Añadir una variable nueva
                    if (GUILayout.Button("Añadir variable", addVariableGUI))
                    {
                        string s = "";
                        variables[i][j].Add(s);
                        effects[i][j].Add(new MusicMaker.MusicEffect());
                        outputs[i][j].Add(new MusicMaker.MusicOutput());
                    }

                    EditorGUILayout.EndHorizontal();

                    if (comp == null)
                        mensajeDebug = "Selecciona primero un componente válido";
                    else
                    {
                        //Cada componente cuyas variables queremos usar
                        for (int k = 0; k < variables[i][j].Count; k++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            List<string> props = MusicMaker.Utils.getProperties(new MusicMaker.MusicInput(componentes[i][j], variables[i][j][k]));

                            int choiceIndex = 0;
                            if (variables[i][j][k] != null)
                                choiceIndex = props.IndexOf(variables[i][j][k]);

                            choiceIndex = EditorGUILayout.Popup(choiceIndex, props.ToArray(), addVariableGUI);
                            if (choiceIndex >= 0)
                                variables[i][j][k] = props[choiceIndex]; //Actualiza el valor de la variable
                            string var = variables[i][j][k];
                            //variables[i][j][k] = EditorGUILayout.TextField(variables[i][j][k], addVariableGUI);

                            //Efecto del input sobre el output
                            effects[i][j][k] = (MusicMaker.MusicEffect)EditorGUILayout.EnumFlagsField(effects[i][j][k], addVariableGUI);
                            MusicMaker.MusicEffect effect = effects[i][j][k];

                            //Parámetro de la música a cambiar
                            outputs[i][j][k] = (MusicMaker.MusicOutput)EditorGUILayout.EnumFlagsField(outputs[i][j][k], addVariableGUI);
                            MusicMaker.MusicOutput output = outputs[i][j][k];

                            ////Tupla
                            //MusicMaker.MusicInput input = new MusicMaker.MusicInput(comp, var);
                            //MusicMaker.MusicTuple t = new MusicMaker.MusicTuple(input, effect, output);
                            ////Guardamos la variable
                            //if (var != null && effect != MusicMaker.MusicEffect.None && output != MusicMaker.MusicOutput.None)
                            //{
                            //    Debug.Log("Input: {" + t.input.owner + ", " + t.input.component + ", " + t.input.value + "} " +
                            //        "Efecto: " + t.effect + "\n" + "Output:" + t.output);
                            //    if (!tuples.Contains(t))
                            //        tuples.Add(t);
                            //    Debug.Log(tuples.Count);
                            //}


                            //Quitar una variable
                            if (GUILayout.Button("X", removeGUI))
                            {
                                variables[i][j].Remove(var);
                                effects[i][j].Remove(effect);
                                outputs[i][j].Remove(output);
                            }


                            EditorGUILayout.EndHorizontal();

                            //TODO: CHQUEAR QUE LA COMBINACIÓN DE {INPUT, EFECTO, OUTPUT ES VÁLIDA}
                            //if (var != null)
                            //{
                            //    if (MusicMaker.Utils.checkCorrectInput(new MusicMaker.MusicInput(comp, var)))
                            //        mensajeDebug = "Variable correcta";
                            //    else
                            //        mensajeDebug = "La variable " + var + " no se encuentra en el componente " + comp;
                            //}
                        }
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
            effects.Add(new List<List<MusicMaker.MusicEffect>>());
            outputs.Add(new List<List<MusicMaker.MusicOutput>>());
        }

        //Guarda la escena
        saveSettings();

        //Mensaje de Debug
        GUILayout.BeginArea(new Rect(5, Screen.height - 45, 300, 20));
        GUILayout.Label(mensajeDebug, EditorStyles.boldLabel);
        GUILayout.EndArea();
    }

    //Carga la información desde el fichero
    static void loadSettings()
    {
        string path = Application.dataPath + filePath;
        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path + "Tuples.json");
            tuples = JsonUtility.FromJson<List<MusicMaker.MusicTuple>>(jsonData);
        }
        else
        {
            tuples = new List<MusicMaker.MusicTuple>();
        }
    }


    //Guarda la información proporcionada a un fichero
    void saveSettings()
    {
        GUILayout.BeginArea(new Rect((Screen.width - 90), Screen.height - 45, 85, 20));
        if (GUILayout.Button("Aplicar", addVariableGUI))
        {
            tuples.Clear();
            for (int i = 0; i < variables.Count; i++)
            {
                for (int j = 0; j < variables[i].Count; j++)
                {
                    for (int k = 0; k < variables[i][j].Count; k++)
                    {
                        MusicMaker.MusicInput input = new MusicMaker.MusicInput(componentes[i][j], variables[i][j][k]);
                        MusicMaker.MusicTuple t = new MusicMaker.MusicTuple(input, effects[i][j][k], outputs[i][j][k]);
                        if (!tuples.Contains(t))
                            tuples.Add(t);
                    }
                }
            }
            //string path = Application.dataPath + filePath;
            //string jsonData = JsonUtility.ToJson(tuples);
            //File.WriteAllText(path + "Tuples.json", jsonData);

            mensajeDebug = "Cambios aplicados: " + tuples.Count + " tuplas guardadas";
            MusicMaker.MMManager.getInstance().setTuples(tuples);
        }
        GUILayout.EndArea();
    }
    // Actualiza la aplicación 
    void Update()
    {
        if (EditorApplication.isPlaying)
        {
            Repaint();
            //MusicMaker.MMManager.Instance.addTuples(ref tuples);
        }
        //tupleHandler.update();
    }


    private static void LogPlayModeState(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            Debug.Log("e");
            //MusicMaker.MMManager.Instance.setTuples(tuples);
        }
            
        //Debug.Log(state);
    }

    //public void Awake()
    //{
    //    Debug.Log("Awake");
    //}

    ////Se cierra la ventana
    //void OnDestroy()
    //{
    //    Debug.Log("Destroyed...");
    //}
}
