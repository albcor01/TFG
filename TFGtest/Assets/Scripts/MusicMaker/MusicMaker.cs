using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEditor;

//TODO:
//1. Chequear las tuplas válidas (formar el sistema de reglas)
//2. Dividir en 2 scripts? El primero (este) para que el usuario se lo ponga al GameObject
// y el otro que se encargue de la ejecución en sí
//3. QUITAR LAS TUPLAS QUE SEAN NULL

//Se encarga de gestionar todo lo referente al plugin
[ExecuteInEditMode]
public class MusicMaker : MonoBehaviour
{
    #region Variables

    //Paquete elegido para la música 
    private MM.Package package;
    
    //Instancia del singleton
    private static MusicMaker instance_ = null;

    //Lista de tuplas <input, efecto, output>, que es lo único relevante que necesitamos
    [Tooltip("La lista de tuplas que condicionarán la música")]
    public List<MM.MusicTuple> tuples;

    //El estado de las tuplas en el frame anterior
    private List<object> varValues;

    //La id del cliente
    private const string ClientId = "SuperCollider";

    #endregion

    //Singleton
    public static MusicMaker Instance
    {
        get
        {
            if (instance_ == null)
            {
                instance_ = new GameObject("MusicMaker").AddComponent<MusicMaker>();
            }

            return instance_;
        }
    }


    #region Callbacks de Unity
    private void Awake()
    {
        if (instance_ == null)
            instance_ = this;
    }

    // NOTA: el usuario debe inicializar sus variables en el Awake para que en la primera vuelta todo esté bajo control
    // (mejor rendimiento porque ahorra una vuelta de mensajes)
    // Aquí se hace que solo queden tuplas válidas para no tener que comprobarlo en cada tick
    private void Start()
    {
        if (EditorApplication.isPlaying)
        {

            //Recuperamos la info del paquete (que se pierde por arte de magia)
            string data = File.ReadAllText(Application.persistentDataPath + "/save.json");
            package = (MM.Package)Enum.Parse(typeof(MM.Package), data);

            //Iniciamos SuperCollider:
            //a) Crea el cliente de SuperCollider en la dirección de loopback
            //b) Crea el servidor para recibir mensajes de SuperCollider
            if (package != MM.Package.None)
                OSCHandler.Instance.Init();

            //Eliminar tuplas no válidas 
            tuples.RemoveAll(x => !MM.Utils.CheckCorrectTuple(x));

            //Inicializar los valores auxiliares copiando de los originales
            varValues = new List<object>();
            foreach (MM.MusicTuple t in tuples)
                varValues.Add(MM.Utils.GetInputValue(t.input));

            //Le mandamos el tipo de paquete al cliente de SuperCollider
            float test = 1.0f;
            if (package != MM.Package.None)
                OSCHandler.Instance.SendMessageToClient(ClientId, "/" + package.ToString(), test);
        }
    }

    //Para cuando se hace algún cambio en el inspector
    private void OnValidate()
    {
        if(tuples != null)
        {
            //Validar tuplas
            foreach (MM.MusicTuple t in tuples)
            {
                //Comprobamos que el usuario ha metido todos los valores de la tupla bien
                if (MM.Utils.CheckCorrectTuple(t))
                    Debug.Log("Tupla correcta: " + t.input.variable + "->" + t.effect.ToString() + "s " + t.output.ToString());
            }
        }
    }

    //Comprueba si se han producido cambios en las variables y avisa a SC
    private void Update()
    {
        if (EditorApplication.isPlaying)
        {
            //Recorremos las tuplas
            int i = 0;
            foreach (MM.MusicTuple t in tuples)
            {
                //Cogemos el valor actual de la variable externa
                object actualVal = MM.Utils.GetInputValue(t.input);
                string type = MM.Utils.GetPropertyType(t.input).Name;

                //Solo mandamos un mensaje a SuperCollider si la variable en cuestión ha cambiado desde el frame anterior
                if (!actualVal.Equals(varValues[i]))
                {
                    //Debug
                    //Debug.Log(t.input.variable + " (" + type + ") : " + actualVal);
                    //Debug.Log(t.effect.ToString() + " " + t.output.ToString() + "(" + actualVal +")");

                    //Actualizamos nuestra variable
                    varValues[i] = actualVal;

                    //Procesamos el mensaje y lo mandamos
                    ProcessMessage(t);
                }
                i++;
            }
        }
    }

    #endregion

    #region Mensajes

    //Saca la información de la tupla para mandar el mensaje a SuperCollider
    private void ProcessMessage(MM.MusicTuple t)
    {
        //Para los valores numéricos; oscila entre 0 y 1
        float numValue = 0;

        //1. SACAR LA INFORMACIÓN SOBRE EL INPUT Y AJUSTARLO AL FORMATO QUE RECIBE SUPERCOLLIDER
        //Si es un número, hay que normalizar:
        object variable = MM.Utils.GetInputValue(t.input);
        if (variable.GetType().Name != "Boolean")
        {
            float range = t.input.max - t.input.min;
            float value = Mathf.Clamp((float)variable, t.input.min, t.input.max);
            numValue = (value - t.input.min) / range;

            //Caso especial: el parámetro es inversamente proporcional
            if (t.effect == MM.MusicEffect.Decrease)
                numValue = 1 - numValue;
        }
        //Si es un booleano:
        else
        {
            numValue = (bool)variable ? 1 : 0; // 1 / 0
            //Caso especial: el booleano va al revés (false->true, true->false)
            if (t.effect == MM.MusicEffect.Deactivate)
                numValue = 1 - numValue;
        }

        //2. DISTINGUIR EL OUTPUT Y MANDAR EL MENSAJE
        string msg = "/" + t.output.ToString().ToLower();
        OSCHandler.Instance.SendMessageToClient(ClientId, msg, numValue);
    }

    //Reproduce la música
    public void StartMusic()
    {
        float test = 1.0f;
        OSCHandler.Instance.SendMessageToClient(ClientId, "/play", test);
    }

    //Establece el paquete
    public void SetPackage(MM.Package newPackage)
    {
        package = newPackage;
    }

    #endregion
}
