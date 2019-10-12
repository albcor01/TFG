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
        public MusicInput(Component component, string value)
        {
            this.owner = component.gameObject;
            this.component = component;
            this.value = value;
        }


        public MusicInput(Object owner, Component component, string value)
        {
            this.owner = owner;
            this.component = component;
            this.value = value;
        }


        public string value { get; set; }
        public object component { get; set; }
        public Object owner { get; set; }
    }

    //Tupla que consta de input, output y efecto del primero sobre el segundo
    public class MusicTuple
    {
        public MusicTuple(MusicInput input, MusicEffect effect, MusicOutput output)
        {
            this.input = input;
            this.effect = effect;
            this.output = output;
        }

        public MusicInput input { get; set; }
        public MusicEffect effect { get; set; }
        public MusicOutput output { get; set; }
        //TODO: efecto del input sobre el output
    }


    //Métodos auxiliares
    public static class Utils
    {
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