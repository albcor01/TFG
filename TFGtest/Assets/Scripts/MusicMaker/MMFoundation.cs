using System.Collections.Generic;
using UnityEngine;
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
        /**
         * Devuelve el valor de la variable del input
         * Ejemplo de uso: object val = Utils.getInputValue(tuplas[0].input);
         */
        public static object GetInputValue(MusicInput input)
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

        /**
         * Devuelve el valor de la propiedad de "instance" con el nombre indicado en "propName"
         * Ejemplo de uso: object val = Utils.GetValue("mass", player.GetComponent<Rigidbody>());
         */
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

        /**
         * Devuelve el tipo del que es la propiedad de "instance" llamada "propName"
         * Ejemplo de uso: Type t = Utils.GetType("mass", obj.GetComponent<Rigidbody>());
         *                 Debug.Log(t.Name); //Escribe "Float"
         **/
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

        /**
         * Devuelve el tipo de la variable de un input dado
         * Ej: Type t = Utils.getPropertyType(tuplas[0].input);
         *     Debug.Log(t.Name); 
         **/
        public static System.Type GetPropertyType(MusicInput input)
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

        /**
         * Devuelve las propiedades del componente referenciado por el input en forma de lista de strings
         * Ejemplo de uso: List<string> props = Utils.getProperties(tuplas[0].input);
         *                 Debug.Log(props.Length); 
        **/
        public static List<string> GetProperties(MusicInput input, bool showInherited = false)
        {
            object obj = input.componente;
            System.Type t = obj.GetType();
            List<PropertyInfo> props = t.GetProperties().ToList();

            List<string> properties = new List<string>();
            //Propiedades del objeto (mirar C# reflection)
            foreach (var prop in props)
            {
                if (showInherited || prop.DeclaringType.Name == t.Name)
                    properties.Add(prop.Name);
            }
            return properties;
        }

        /**
         * Devuelve las propiedades de un componente en forma de lista de strings
         * Ejemplo de uso: List<string> props = Utils.getProperties(player.GetComponent<Player>());
         *                 Debug.Log(props); 
        **/
        public static List<string> GetProperties(object component, bool showInherited = false)
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
        private static bool CheckCorrectInput(MusicInput input)
        {
            //1. Comprobamos que hay un gameobject y objeto seleccionados
            object obj = input.componente;
            if (obj == null || input.objeto == null)
                return false;

            //2. Comprobamos que el componente tiene esa propiedad
            System.Type t = obj.GetType(); //Tipo
            List<PropertyInfo> props = t.GetProperties().ToList();
            PropertyInfo p = props.Find((x) => x.Name == input.variable);
            if (p == null)
                return false;

            //3. Comprobamos que el min y max están bien
            if (Utils.GetType(input.variable, obj).Name != "Boolean" && input.min >= input.max)
                return false;

            return true;
        }

        //Comprueba que la tupla es correcta (según nuestro criterio)
        public static bool CheckCorrectTuple(MusicTuple t)
        {
            return (CheckCorrectInput(t.input) && t.effect != MusicEffect.None && t.output != MusicOutput.None);
        }

        #endregion
    }

    //Clase Par
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

}