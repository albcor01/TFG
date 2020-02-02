using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
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
    //private List<MM.MusicInput


    //Para cuando se hace algún cambio
    private void OnValidate()
    {
        foreach (MM.MusicTuple t in tuples)
        {
            if (MM.Utils.checkCorrectTuple(t))
                Debug.Log("Tupla correcta: " + t.input.variable + "->" + t.effect.ToString() + "s " + t.output.ToString());
        }
        //Debug.Log("Cambios guardados");
    }

    private void Update()
    {
        //Mandar mensajes a SuperCollider con la información que hemos recibido
        foreach (MM.MusicTuple t in tuples)
        {
            Debug.Log(t.input.variable + ": " + MM.Utils.getInputValue(t.input));
        }
    }
}
