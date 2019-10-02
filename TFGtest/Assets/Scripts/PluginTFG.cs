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
    string myString = "Hola Mundo";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;

    public List<Object> inputs = new List<Object>();
    //Selectores
    GUILayoutOption[] addObjectsGUI = new GUILayoutOption[]
    {
        GUILayout.Width(200)
    };
    //Botones para quitar
    GUILayoutOption[] removeButtonsGUI = new GUILayoutOption[] 
    {
        GUILayout.Width(80)
    };

    /// <summary>
    /// Inicializa la ventana
    /// </summary>
    [MenuItem("Window/Plugin TFG")]
    static void Init()
    {
        PluginTFG window = (PluginTFG)EditorWindow.GetWindow(typeof(PluginTFG));
        window.Show();
    }

    /// <summary>
    /// Aquí es donde se hace todo lo importante
    /// </summary>
    void OnGUI()
    {
        {
            //Cogido directamente del manual de Unity
            GUILayout.Label("Ejemplo de prueba", EditorStyles.boldLabel);
            myString = EditorGUILayout.TextField("Campo de texto", myString);

            groupEnabled = EditorGUILayout.BeginToggleGroup("Configuración avanzada", groupEnabled);
            myBool = EditorGUILayout.Toggle("Toggle", myBool);
            myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
            EditorGUILayout.EndToggleGroup();

            //Para nuestro plugin
            GUILayout.Label("Música adaptativa", EditorStyles.boldLabel);

            //Cada GameObject cuyas variables queremos usar
            for (int i = 0; i < inputs.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                inputs[i] = EditorGUILayout.ObjectField(inputs[i], typeof(Object), true, addObjectsGUI);
                if (GUILayout.Button("Quitar", removeButtonsGUI))
                {
                    inputs.Remove(inputs[i]);
                }
                EditorGUILayout.EndHorizontal();
            }

            //Añadir uno nuevo
            if (GUILayout.Button("Añadir input"))
                inputs.Add(new Object());
        }
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
