using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using UnityEditor.UIElements;

namespace MM
{
    //Parámetros modificables de la música
    public enum MusicOutput { None, Tempo, Volume, Pitch, Reverb }

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

        ////Operadores
        //public static bool operator ==(MusicInput mi1, MusicInput mi2)
        //{
        //    if (mi1.objeto == mi2.objeto && mi1.component == mi2.component && System.String.Equals(mi1.value, mi2.value))
        //        return true;
        //    else
        //        return false;
        //}

        //public static bool operator !=(MusicInput mi1, MusicInput mi2)
        //{
        //    return !(mi1 == mi2);
        //}

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
    }

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


        ////Operadores
        //public static bool operator ==(MusicTuple mt1, MusicTuple mt2)
        //{
        //    if (mt1.input == mt2.input)
        //    {
        //        Debug.Log("He llegado");
        //        if (mt1.effect == mt2.effect && mt1.output == mt2.output)
        //            return true;
        //    }
                
        //    return false;
        //}

        //public static bool operator !=(MusicTuple mt1, MusicTuple mt2)
        //{
        //    return !(mt1 == mt2);
        //}
    }


    //Métodos auxiliares
    public static class Utils
    {
        //Comprueba que la variable es correcta (es decir, que el componente tiene una propiedad que se llama como se indica)
        private static bool checkCorrectInput(MusicInput input)
        {
            //Componente que contiene la variable
            object obj = input.componente;
            if (obj == null)
                return false;

            //Propiedades del componente
            System.Type t = obj.GetType(); //Tipo
            List<PropertyInfo> props = t.GetProperties().ToList();
            return (props.Find((x) => x.Name == input.variable)) != null;
        }

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

        //Comprueba que la tupla es correcta (según nuestro criterio)
        public static bool checkCorrectTuple(MusicTuple t)
        {
            return(checkCorrectInput(t.input) && t.effect != MusicEffect.None && t.output != MusicOutput.None);
        }
    }
}