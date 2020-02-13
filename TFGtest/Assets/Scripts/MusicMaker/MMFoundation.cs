using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;

//Espacio de nombres del Music Maker
namespace MM
{
    #region Input, Output, Efecto
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
    #endregion

    #region Tuplas
    //Tupla que consta de input, output y efecto del primero sobre el segundo
    [System.Serializable]
    public class MusicTuple
    {
        [Tooltip("La variable que influye")]
        public MusicInput input;
        [Tooltip("El efecto que causa (aumenta, disminuye, etc.)")]
        public MusicEffect effect;
        [Tooltip("Parámetro de la música afectado (tempo, volumen, etc.)")]
        public MusicOutput output;

        public MusicTuple(MusicInput input, MusicEffect effect, MusicOutput output)
        {
            this.input = input;
            this.effect = effect;
            this.output = output;
        }

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
        #endregion

        #region Check Tuples
        //Comprueba que la variable es correcta (es decir, que el componente tiene una propiedad que se llama como se indica)
        private static bool checkCorrectInput(MusicInput input)
        {
            //Componente que contiene la variable
            object obj = input.componente;
            if (obj == null || input.objeto == null)
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

    #endregion

    #region Drawers
    [CustomPropertyDrawer(typeof(MusicInput))]
    public class MusicInputDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
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

            // Calculate rects
            var objRect = new Rect(position.x, position.y, 70, position.height);
            var compRect = new Rect(position.x + 75, position.y, 70, position.height);
            var varRect = new Rect(position.x + 150, position.y, 70, position.height);
            var minRect = new Rect(position.x + 225, position.y, 35, position.height);
            var maxRect = new Rect(position.x + 265, position.y, 35, position.height);


            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(objRect, property.FindPropertyRelative("objeto"), GUIContent.none);
            EditorGUI.PropertyField(compRect, property.FindPropertyRelative("componente"), GUIContent.none);
            EditorGUI.PropertyField(varRect, property.FindPropertyRelative("variable"), GUIContent.none);
            EditorGUI.PropertyField(minRect, property.FindPropertyRelative("min"), GUIContent.none);
            EditorGUI.PropertyField(maxRect, property.FindPropertyRelative("max"), GUIContent.none);


            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
    #endregion
}