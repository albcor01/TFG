using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System.Linq;

namespace MusicMaker
{
    public static class Utils
    {
        //Comprueba que la variable es correcta (es decir, que el componente tiene una propiedad que se llama como se indica)
        public static bool checkVariable(Variable v)
        {
            object obj = v.component;
            System.Type t = obj.GetType();
            List<PropertyInfo>props = t.GetProperties().ToList();

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
    }
    public class Variable
    {

        public Variable(Component component, string value)
        {
            this.owner = component.gameObject;
            this.component = component;
            this.value = value;
        }


        public Variable(Object owner, Component component, string value)
        {
            this.owner = owner;
            this.component = component;
            this.value = value;
        }


        public string value { get; set; }
        public object component { get; set; }
        public Object owner { get; set; }
    }


    public class ListaVariables
    {
        List<Variable> variables;


        public ListaVariables()
        {

        }

        public ListaVariables(List<Variable> variables)
        {
            this.variables = variables;
        }


        public void addVariable(Variable v)
        {
            //Metemos la variable
            if (!variables.Contains(v))
                variables.Add(v);
        }

    };
}