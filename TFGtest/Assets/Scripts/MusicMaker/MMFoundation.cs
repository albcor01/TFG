using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;

//Espacio de nombres del Music Maker
namespace MM
{
    #region Tuplas
    //Los distintos paquetes que el usuario va a poder utilizar para la creación de musica
    public enum Package { None, Ambient, Desert, Horror, Asian, Aquatic, War, Electronic };

    //Parámetros modificables de la música
    public enum MusicOutput { None, Tempo, Volume, Pitch, Reverb, Percussions, BackgroundMusic, IntenseFX, AmbienceFX, OneShotFX }

    //Efecto del input sobre el output
    public enum MusicEffect { None, Increase, Decrease, Activate, Deactivate }

    //Inputs
    [System.Serializable]
    public class MusicInput
    {
        public Object objeto;
        public Component componente;
        public string variable;
        public float min;
        public float max;

        #region Constructoras
        public MusicInput()
        {
            this.objeto = null;
            this.componente = null;
            this.variable = "";
        }

        public MusicInput(Component component, string value)
        {
            this.objeto = component.gameObject;
            this.componente = component;
            this.variable = value;
        }


        public MusicInput(UnityEngine.Object objeto, Component component, string value)
        {
            this.objeto = objeto;
            this.componente = component;
            this.variable = value;
        }
        #endregion

        #region Equals y Hash
        public override bool Equals(object obj)
        {
            var input = obj as MusicInput;
            return input != null &&
                   variable == input.variable &&
                   EqualityComparer<object>.Default.Equals(componente, input.componente) &&
                   EqualityComparer<Object>.Default.Equals(objeto, input.objeto);
        }

        public override int GetHashCode()
        {
            var hashCode = -1495079906;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(variable);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(componente);
            hashCode = hashCode * -1521134295 + EqualityComparer<Object>.Default.GetHashCode(objeto);
            return hashCode;
        }
        #endregion
    }

    //Tupla que consta de input, output y efecto del primero sobre el segundo
    [System.Serializable]
    public class MusicTuple
    {
        [SerializeField]
        public MusicInput input;
        [SerializeField]
        public MusicEffect effect;
        [SerializeField]
        public MusicOutput output;

        public MusicTuple(MusicInput input, MusicEffect effect, MusicOutput output)
        {
            this.input = input;
            this.effect = effect;
            this.output = output;
        }

        #region Equals y Hash

        public override bool Equals(object obj)
        {
            var tuple = obj as MusicTuple;
            return tuple != null &&
                   EqualityComparer<MusicInput>.Default.Equals(input, tuple.input) &&
                   effect == tuple.effect &&
                   output == tuple.output;
        }

        public override int GetHashCode()
        {
            var hashCode = 1440660003;
            hashCode = hashCode * -1521134295 + EqualityComparer<MusicInput>.Default.GetHashCode(input);
            hashCode = hashCode * -1521134295 + effect.GetHashCode();
            hashCode = hashCode * -1521134295 + output.GetHashCode();
            return hashCode;
        }
        #endregion
    }
    #endregion

    #region Utilidades
    //Métodos auxiliares
    public static class Utils
    {
        #region Reflection
        //Devuelve el valor del input
        public static object getInputValue(MusicInput input)
        {
            //Obtenemos la propiedad del componente
            object obj = input.componente;
            System.Type t = obj.GetType();
            PropertyInfo prop = t.GetProperty(input.variable);
            //List<PropertyInfo> props = t.GetProperties().ToList();
            //PropertyInfo prop = props.Find((x) => x.Name == input.variable);
            if (prop == null)
                return null;

            //Devolvemos el valor de la propiedad
            return prop.GetValue(obj);
        }

        //Devuelve el valor del input
        public static object GetValue(string propName, object instance)
        {
            //Obtenemos la propiedad del componente
            System.Type t = instance.GetType();
            PropertyInfo prop = t.GetProperty(propName);
            //List<PropertyInfo> props = t.GetProperties().ToList();
            //PropertyInfo prop = props.Find((x) => x.Name == input.variable);
            if (prop == null)
                return null;

            //Devolvemos el valor de la propiedad
            return prop.GetValue(instance);
        }

        //Devuelve el valor del input
        public static System.Type GetType(string propName, object instance)
        {
            //Obtenemos la propiedad del componente
            System.Type t = instance.GetType();
            PropertyInfo prop = t.GetProperty(propName);
            if (prop == null)
                return null;

            //Devolvemos el valor de la propiedad
            return prop.PropertyType;
        }

        //Devuelve el tipo del objeto especificado
        public static System.Type getPropertyType(MusicInput input)
        {
            //Obtenemos la propiedad del componente
            object obj = input.componente;
            System.Type t = obj.GetType();
            PropertyInfo prop = t.GetProperty(input.variable);
            //List<PropertyInfo> props = t.GetProperties().ToList();
            //PropertyInfo prop = props.Find((x) => x.Name == input.variable);
            if (prop == null)
                return null;

            //Devolvemos el valor de la propiedad
            return prop.PropertyType;
        }

        //Devuelve las propiedades de un objeto
        public static List<string> getProperties(MusicInput input, bool showInherited = false)
        {
            object obj = input.componente;
            System.Type t = obj.GetType();
            List<PropertyInfo> props = t.GetProperties().ToList();

            List<string> properties = new List<string>();
            //Propiedades del objeto (mirar C# reflection)
            foreach (var prop in props)
            {
                if(showInherited || prop.DeclaringType.Name == t.Name)
                    properties.Add(prop.Name);
            }
            return properties;
        }

        //Devuelve las propiedades de un objeto
        public static List<string> GetVariables(object component, bool showInherited = false)
        {
            System.Type t = component.GetType();
            List<PropertyInfo> props = t.GetProperties().ToList();

            List<string> properties = new List<string>();
            //Propiedades del objeto (mirar C# reflection)
            foreach (var prop in props)
            {
                if (showInherited || prop.DeclaringType.Name == t.Name)
                    properties.Add(prop.Name);
            }

            if (properties.Count == 0)
                return null;
            return properties;
        }
        #endregion

        #region Check Tuples
        //Comprueba que la variable es correcta (es decir, que el componente tiene una propiedad que se llama como se indica)
        private static bool checkCorrectInput(MusicInput input)
        {
            //Componente que contiene la variable
            object obj = input.componente;
            if (obj == null || input.objeto == null)
                return false;

            //Minimo y maximo
            if (input.min >= input.max)
                return false;

            //Propiedades del componente
            System.Type t = obj.GetType(); //Tipo
            List<PropertyInfo> props = t.GetProperties().ToList();
            PropertyInfo p = props.Find((x) => x.Name == input.variable);
            //Tiene esa propiedad
            return (p != null);
        }

        //Comprueba que la tupla es correcta (según nuestro criterio)
        public static bool checkCorrectTuple(MusicTuple t)
        {
            return (checkCorrectInput(t.input) && t.effect != MusicEffect.None && t.output != MusicOutput.None);
        }

        #endregion
    }

    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };
    #endregion

    #region Drawers
    [CustomPropertyDrawer(typeof(MusicInput))]
    public class MusicInputDrawer : PropertyDrawer
    {
        private int height = 58;
        private int compIndex = 0;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return height;
        }

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

            // Componente
            DisplayComponents(property, compRect);

            // Variables
            DisplayVariables(property, varRect);

            // Para los booleanos
            object variable = Utils.GetValue(varProp.stringValue, compProp.objectReferenceValue);
            if(variable != null && Utils.GetType(varProp.stringValue, compProp.objectReferenceValue).Name != "Boolean")
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
            comps.RemoveAll(x => Utils.GetVariables(x) == null);

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
            List<string> varNames = Utils.GetVariables(comp);
            string seleccionada = varNames.Find((x) => x == varProp.stringValue);
            string[] varList = varNames.ToArray<string>();

            //Hacemos el Popup
            int varIndex = Mathf.Max(EditorGUI.Popup(rect, varNames.IndexOf(seleccionada), varList), 0);
            varProp.stringValue = varNames[varIndex];
        }
    }

    #endregion
}