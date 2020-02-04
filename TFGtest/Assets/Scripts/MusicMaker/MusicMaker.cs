using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.SceneManagement;
using System.IO;

//TODO:
//1. Enganchar y probar con SuperCollider
//2. Poder seleccionar componentes y variables de un dropdown
//3. Chequear las tuplas válidas

//Se encarga de gestionar la ventana del editor
public class MusicMaker : MonoBehaviour
{
    //Lista de tuplas <input, efecto, output>, que es lo único relevante que necesitamos
    [Tooltip("La lista de tuplas que condicionarán la música")]
    public List<MM.MusicTuple> tuples;

    //El estado de las tuplas en el frame anterior
    private List<object> varValues;


    //Para cuando se hace algún cambio
    private void OnValidate()
    {
        //La lista de valores de variables
        varValues = null;
        varValues = new List<object>();
        //Validar tuplas
        foreach (MM.MusicTuple t in tuples)
        {
            //Comprobamos que el usuario ha metido todos los valores de la tupla bien
            if (MM.Utils.checkCorrectTuple(t))
            {
                Debug.Log("Tupla correcta: " + t.input.variable + "->" + t.effect.ToString() + "s " + t.output.ToString());
                varValues.Add(MM.Utils.getInputValue(t.input));
            }
            //Tupla no correcta por lo que sea
            else
                varValues.Add(null);
        }
    }

    //Manda un mensaje al servidor de SuperCollider usando el OSCHandler
    private void SendMessage(MM.MusicTuple t)
    {
        //¿Qué parámetro musical modificamos?
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
                //OSCHandler.Instance.SendMessageToClient(ClientId, "/tempo", (float)varValues[i]);
                break;
            //Reverb
            case MM.MusicOutput.Reverb:
                //OSCHandler.Instance.SendMessageToClient(ClientId, "/reverb", varValues[i]);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        //Mandar mensajes a SuperCollider con la información que hemos recibido
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
}
