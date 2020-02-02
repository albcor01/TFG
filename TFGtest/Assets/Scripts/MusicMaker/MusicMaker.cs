using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using System.IO;


//TODO:
//1. Optimizar el código quitando las listas
//2. Chequear las tuplas válidas
//3. Guardado y cargado de datos
//4. Estructura de la ventana para hacerla como un árbol
//5. Enganchar y probar con SuperCollider
//6. Toggle para usar los atributos del padre / no
//7. Poder seleccionar componentes igual que se hace con las variables

//Se encarga de gestionar la ventana del editor
public class MusicMaker : MonoBehaviour
{
    //Lista de tuplas <input, efecto, output>, que es lo único relevante que necesitamos
    public List<MM.MusicTuple> tuples;


    //Para cuando se hace algún cambio
    private void OnValidate()
    {
        //string jsonData = JsonUtility.ToJson(tuples);
        //File.WriteAllText(filePath + "Tuples.json", jsonData);
        foreach (MM.MusicTuple t in tuples)
        {
            if (MM.Utils.checkCorrectInput(t.input))
                Debug.Log("Tupla correcta");
        }
        
        //Debug.Log("Cambios guardados");
    }
}
