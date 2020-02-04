using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

//TODO:
//1. Poder seleccionar componentes y variables de un dropdown
//2. Chequear las tuplas válidas (formar el sistema de reglas)
//3. Dividir en 2 scripts? El primero (este) para que el usuario se lo ponga al GameObject
// y el otro que se encargue de la ejecución en sí

//Se encarga de gestionar todo lo referente al plugin
public class MusicMaker : MonoBehaviour
{
    //Instancia del singleton
    public static MusicMaker instance = null;

    //Lista de tuplas <input, efecto, output>, que es lo único relevante que necesitamos
    [Tooltip("La lista de tuplas que condicionarán la música")]
    public List<MM.MusicTuple> tuples;

    //El estado de las tuplas en el frame anterior
    private List<object> varValues;

    //La id del cliente
    private const string ClientId = "SuperCollider";

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    // NOTA: el usuario debe inicializar sus variables en el Awake para que en la primera vuelta todo esté bajo control
    // (mejor rendimiento porque ahorra una vuelta de mensajes)
    private void Start()
    {
        //Crea:
        //a) El cliente de SuperCollider en la dirección de loopback
        //b) El servidor para recibir mensajes de SuperCollider
        OSCHandler.Instance.Init();


        //Inicializar los valores
        varValues = new List<object>();
        foreach (MM.MusicTuple t in tuples)
        {
            //Comprobamos que el usuario ha metido todos los valores de la tupla bien
            if (MM.Utils.checkCorrectTuple(t))
                varValues.Add(MM.Utils.getInputValue(t.input));
            //Tupla no correcta por lo que sea
            else
                varValues.Add(null);
        }
    }

    //Para cuando se hace algún cambio en el inspector
    private void OnValidate()
    {
        //Validar tuplas
        foreach (MM.MusicTuple t in tuples)
        {
            //Comprobamos que el usuario ha metido todos los valores de la tupla bien
            if (MM.Utils.checkCorrectTuple(t))
                Debug.Log("Tupla correcta: " + t.input.variable + "->" + t.effect.ToString() + "s " + t.output.ToString());
        }
    }

    //Manda un mensaje al servidor de SuperCollider usando el OSCHandler
    private void SendMessage(MM.MusicTuple t)
    {
        //¿Qué parámetro musical modificamos?
        object value = MM.Utils.getInputValue(t.input);
        switch (t.output)
        {
            //Volumen
            case MM.MusicOutput.Volume:
                //OSCHandler.Instance.SendMessageToClient(ClientId, "/volume", (float)varValues[i]);
                break;
            //Pitch
            case MM.MusicOutput.Pitch:
                //OSCHandler.Instance.SendMessageToClient(ClientId, "/pitch", (float)varValues[i]);
                break;
            //Tempo
            case MM.MusicOutput.Tempo:
                OSCHandler.Instance.SendMessageToClient(ClientId, "/tempo", (float)value);
                break;
            //Reverb
            case MM.MusicOutput.Reverb:
                //OSCHandler.Instance.SendMessageToClient(ClientId, "/reverb", varValues[i]);
                break;
            default:
                break;
        }
    }

    //Comprueba si se han producido cambios en las variables y avisa a SC
    private void Update()
    {
        //Recorremos las tuplas
        int i = 0;
        foreach (MM.MusicTuple t in tuples)
        {
            //Cogemos el valor actual de la variable externa
            object actualVal = MM.Utils.getInputValue(t.input);
            string type = MM.Utils.getPropertyType(t.input).Name;

            //Solo mandamos un mensaje a SuperCollider si la variable en cuestión ha cambiado desde el frame anterior
            
            if (!actualVal.Equals(varValues[i]))
            {
                //Debug
                Debug.Log(t.input.variable + " (" + type + ") : " + actualVal);
                Debug.Log(t.effect.ToString() + " " + t.output.ToString() + "(" + actualVal +")");

                //Actualizamos nuestra variable
                varValues[i] = actualVal;

                //Mandamos el mensaje a SuperCollider
                SendMessage(t);
            }
            i++;
        }
    }

    //Reproduce la música
    public void PlayMusic()
    {
        float test = 1.0f;
        OSCHandler.Instance.SendMessageToClient(ClientId, "/play", test);
    }

    //Para la música (no va)
    public void StopMusic()
    {
        float test = 1.0f;
        OSCHandler.Instance.SendMessageToClient(ClientId, "/stop", test);
    }

    //Arranca el server
    public void LaunchServer()
    {
        float test = 1.0f;
        OSCHandler.Instance.SendMessageToClient(ClientId, "/boot", test);
    }

    //Cierra el server
    public void CloseServer()
    {
        float test = 1.0f;
        OSCHandler.Instance.SendMessageToClient(ClientId, "/quit", test);
    }
}
