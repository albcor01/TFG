using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace MM
{
    #region Parte de la ventana
    //Ventana del editor para elegir parámetros sobre la música generativa
    public class MMWindow : EditorWindow
    {
        #region Variables

        //Número de "aspectos" en que dividir, y nº de capas máxima para cada aspecto
        const int NUM_ASPECTS = 4;
        const int MAX_LAYERS = 4;

        //Editor
        string userMsg = "Ningún paquete seleccionado";
        bool customPackage;
        bool[] openFolds;

        //Serializable
        MM.Package package; //Paquete
        bool[,] layers; //Información sobre qué capas empezarán activas
        float tempo = 1f; //Tempo
   
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
            //Para recuperar el paquete que habíamos puesto
            string packageName = EditorPrefs.GetString("PackageType");
            package = (MM.Package)Enum.Parse(typeof(MM.Package), packageName);

            //Por defecto activamos la primera capa de cada aspecto
            layers = new bool[NUM_ASPECTS, MAX_LAYERS];
            for (int i = 0; i < NUM_ASPECTS; i++)
                layers[i,0] = true;

            //Menús abiertos
            openFolds = new bool[4];
        }

        /*
         * "Tick" de la ventana (para mostrar cosas al usuario)
         */
        void OnGUI()
        {
            //Selección de paquete
            GUILayout.Label("Selección de paquete temático", EditorStyles.boldLabel);
            package = (MM.Package)EditorGUILayout.EnumPopup("Elige un paquete:", package);

            //Hay un paquete seleccionado
            if (package != MM.Package.None)
            {
                //Personalizar paquete
                customPackage = EditorGUILayout.BeginToggleGroup("Personalizar paquete", customPackage);

                //Capa rítmica
                openFolds[0] = EditorGUILayout.Foldout(openFolds[0], "Ritmo");
                if (openFolds[0])
                {
                    layers[0,0] = EditorGUILayout.Toggle("Percusión base", layers[0,0]);
                    layers[0,1] = EditorGUILayout.Toggle("Percusión secundaria", layers[0,1]);
                    layers[0,2] = EditorGUILayout.Toggle("Efectos", layers[0,2]);
                }

                //Capa armónica
                openFolds[1] = EditorGUILayout.Foldout(openFolds[1], "Armonía");
                if (openFolds[1])
                {
                    layers[1, 0] = EditorGUILayout.Toggle("Progresión de acordes", layers[1, 0]);
                    layers[1, 1] = EditorGUILayout.Toggle("Acordes octavados", layers[1, 1]);
                }

                //Capa melódica
                openFolds[2] = EditorGUILayout.Foldout(openFolds[2], "Melodía");
                if (openFolds[2])
                {
                    layers[2, 0] = EditorGUILayout.Toggle("Melodía aleatoria", layers[2, 0]);
                    layers[2, 1] = EditorGUILayout.Toggle("Clichés (1)", layers[2, 1]);
                    layers[2, 2] = EditorGUILayout.Toggle("Clichés (2)", layers[2, 2]);
                }

                //Capa de efectos
                openFolds[3] = EditorGUILayout.Foldout(openFolds[3], "FX");
                if (openFolds[3])
                {
                    layers[3, 0] = EditorGUILayout.Toggle("Fx 1", layers[3, 0]);
                    layers[3, 1] = EditorGUILayout.Toggle("Fx 2", layers[3, 1]);
                    layers[3, 2] = EditorGUILayout.Toggle("Fx 3", layers[3, 2]);
                    layers[3, 3] = EditorGUILayout.Toggle("Fx 4", layers[3, 3]);
                }

                //Tempo deseado
                tempo = EditorGUILayout.Slider("Tempo (sobre el base)", tempo, 0.5f, 1.5f);

                //Termina la personalización
                EditorGUILayout.EndToggleGroup();
            }

            //Guardar los cambios
            if (GUILayout.Button("Guardar cambios"))
            {
                //Se ha elegido algún paquete
                if (package != MM.Package.None)
                {
                    //Guardamos en EditorPrefs...
                    EditorPrefs.SetString("PackageType", package.ToString());
                    //...y también en archivo para que luego lo lea el MonoBehaviour
                    MusicMaker.Instance.SetPackage(package);
                    File.WriteAllText(Application.persistentDataPath + "/save.json", package.ToString());

                    userMsg = "Has elegido el paquete " + package.ToString();
                }
                    
                //No se ha elegido
                else
                    userMsg = "Ningún paquete seleccionado";
            }
            GUILayout.Label(userMsg, EditorStyles.boldLabel);
        }
        #endregion
    }
    #endregion

    #region Parte del inspector (drawer)
    [CustomPropertyDrawer(typeof(MusicInput))]
    public class MusicInputDrawer : PropertyDrawer
    {
        // Draw the property inside the given rects
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Propiedades 
            SerializedProperty objProp = property.FindPropertyRelative("objeto");
            SerializedProperty compProp = property.FindPropertyRelative("componente");
            SerializedProperty varProp = property.FindPropertyRelative("variable");
            SerializedProperty minProp = property.FindPropertyRelative("min");
            SerializedProperty maxProp = property.FindPropertyRelative("max");

            // Rectángulos donde se pintan
            var objRect = new Rect(position.x, position.y, 100, 16);
            var compRect = new Rect(position.x, position.y + 18, 150, 16);
            var varRect = new Rect(position.x, position.y + 36, 120, 20);
            var minRect = new Rect(position.x + 130, position.y + 36, 35, 16);
            var maxRect = new Rect(position.x + 175, position.y + 36, 35, 16);


            // Objetos (es un field normal y corriente) NOTA: todos los objetos tienen al menos un componente (el Transform)
            EditorGUI.PropertyField(objRect, objProp, GUIContent.none);

            if (objProp.objectReferenceValue == null)
                return;
            // Componente
            DisplayComponents(property, compRect);

            // Variables
            DisplayVariables(property, varRect);

            // Para los booleanos
            object variable = Utils.GetValue(varProp.stringValue, compProp.objectReferenceValue);
            if (variable != null && Utils.GetType(varProp.stringValue, compProp.objectReferenceValue).Name != "Boolean")
            {
                EditorGUI.PropertyField(minRect, minProp, GUIContent.none);
                EditorGUI.PropertyField(maxRect, maxProp, GUIContent.none);
            }

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        //Muestra un Popup con todos los componentes que tiene "objProp", devuelve el seleccionado
        private void DisplayComponents(SerializedProperty property, Rect rect)
        {
            //Cogemos las propiedades
            SerializedProperty objProp = property.FindPropertyRelative("objeto");
            SerializedProperty compProp = property.FindPropertyRelative("componente");

            //GameObject escogido y lista con sus componentes
            object go = objProp.objectReferenceValue;
            List<Component> comps = ((GameObject)go).GetComponents<Component>().ToList<Component>();

            //Quitamos los componentes que no tienen atributos públicos
            comps.RemoveAll(x => Utils.GetProperties(x) == null);

            //Vemos cuál está seleccionado
            Component seleccionado = comps.Find((x) => x == compProp.objectReferenceValue);

            //Lista de componentes disponibles
            string[] compList = new string[comps.Count];
            for (int i = 0; i < comps.Count; i++)
                compList[i] = comps[i].GetType().ToString();

            //Hacemos el Popup
            int compIndex = Mathf.Max(EditorGUI.Popup(rect, comps.IndexOf(seleccionado), compList), 0);
            compProp.objectReferenceValue = comps[compIndex];
        }

        private void DisplayVariables(SerializedProperty property, Rect rect)
        {
            //Cogemos las propiedades
            SerializedProperty compProp = property.FindPropertyRelative("componente");
            SerializedProperty varProp = property.FindPropertyRelative("variable");

            //Componente escogido
            object comp = compProp.objectReferenceValue;

            //Lista de variables disponibles
            List<string> varNames = Utils.GetProperties(comp);
            string seleccionada = varNames.Find((x) => x == varProp.stringValue);
            string[] varList = varNames.ToArray<string>();

            //Hacemos el Popup
            int varIndex = Mathf.Max(EditorGUI.Popup(rect, varNames.IndexOf(seleccionada), varList), 0);
            varProp.stringValue = varNames[varIndex];
        }
    }
    #endregion
}

