using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MusicMaker
{
    //Es un singleton
    public class MMManager : MonoBehaviour
    {
        //Instancia 
        private static MMManager instance = null;
        List<MusicTuple> tuples;

        static MMManager()
        {
        }

        MMManager()
        {
        }

        //Combina el patrón singleton con el framework de Unity; inicializa la instancia creando un gameObject con este componente
        public static MMManager getInstance()
        {
             if (instance == null)
                {
                    instance = new GameObject("MMManager").AddComponent<MMManager>();
                }

              return instance;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        //Log de las tuplas
        void Update()
        {

        }

        //Devuelve las tuplas
        public List<MusicTuple> getTuples() { return tuples; }

        //Añade las tuplas
        public void setTuples(List<MusicTuple> tuples)
        {
            tuples = new List<MusicTuple>();
            //Crear otra lista con los valores dados
            foreach (MusicTuple mt in tuples)
            {
                Debug.Log("una nueva");
                //El input
                MusicInput input = new MusicInput((Component)mt.input.component, mt.input.value); 

                //La tupla
                this.tuples.Add(new MusicTuple(input, mt.effect, mt.output));
            }
        }
    }
}

