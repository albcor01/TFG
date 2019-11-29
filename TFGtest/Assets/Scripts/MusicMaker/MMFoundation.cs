using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace MusicMaker
{
    //Parámetros modificables de la música
    public enum MusicOutput { None, Tempo, Volume }

    //Efecto del input sobre el output
    public enum MusicEffect { None, Increase, Decrease, Activate, Deactivate }


    //Inputs
    public class MusicInput
    {

        public string value { get; set; }
        public object component { get; set; }
        public UnityEngine.Object owner { get; set; }

        public MusicInput()
        {
            this.owner = null;
            this.component = null;
            this.value = "";
        }

        public MusicInput(Component component, string value)
        {
            this.owner = component.gameObject;
            this.component = component;
            this.value = value;
        }


        public MusicInput(UnityEngine.Object owner, Component component, string value)
        {
            this.owner = owner;
            this.component = component;
            this.value = value;
        }

        ////Operadores
        //public static bool operator ==(MusicInput mi1, MusicInput mi2)
        //{
        //    if (mi1.owner == mi2.owner && mi1.component == mi2.component && System.String.Equals(mi1.value, mi2.value))
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
                   value == input.value &&
                   EqualityComparer<object>.Default.Equals(component, input.component) &&
                   EqualityComparer<Object>.Default.Equals(owner, input.owner);
        }

        public override int GetHashCode()
        {
            var hashCode = -1495079906;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(value);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(component);
            hashCode = hashCode * -1521134295 + EqualityComparer<Object>.Default.GetHashCode(owner);
            return hashCode;
        }
    }

    //Tupla que consta de input, output y efecto del primero sobre el segundo
    public class MusicTuple
    {

        public MusicInput input { get; set; }
        public MusicEffect effect { get; set; }
        public MusicOutput output { get; set; }

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
        static List<MusicTuple> tuples;
        //Comprueba que la variable es correcta (es decir, que el componente tiene una propiedad que se llama como se indica)
        public static bool checkCorrectInput(MusicInput v)
        {
            object obj = v.component;
            System.Type t = obj.GetType();
            List<PropertyInfo> props = t.GetProperties().ToList();

            //Propiedades del objeto (mirar C# reflection)
            foreach (var prop in props)
            {
                //string s = "";
                //if (prop.DeclaringType.Name == "MonoBehaviour")
                //{
                //    s = prop.Name + " (" + prop.PropertyType.Name + ") :" + prop.GetValue(obj);
                //    Debug.Log(prop.DeclaringType.Name);
                //}
                //Debug.Log(prop.DeclaringType.Name);
                //if (prop.Name == v.value)
                // return true;
                if (prop.Name == v.value)
                    return true;
            }
            return false;
        }

        //Devuelve el valor del inpur
        public static object getInputValue(MusicInput input)
        {
            object obj = input.component;
            System.Type t = obj.GetType();
            List<PropertyInfo> props = t.GetProperties().ToList();

            //Propiedades del objeto (mirar C# reflection)
            foreach (var prop in props)
            {
                //string s = "";
                //if (prop.DeclaringType.Name == "MonoBehaviour")
                //{
                //    s = prop.Name + " (" + prop.PropertyType.Name + ") :" + prop.GetValue(obj);
                //    Debug.Log(prop.DeclaringType.Name);
                //}
                //Debug.Log(prop.DeclaringType.Name);ç

                if (prop.Name == input.value)
                    return prop.GetValue(obj);
            }
            return null;
        }

        //Devuelve las propiedades de un objeto
        public static List<string> getProperties(MusicInput input, bool showInherited = false)
        {
            object obj = input.component;
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

        //Comprueba que la tupla es correcta
        public static bool checkCorrectTuple(MusicTuple t)
        {
            return false;
        }
    }
}