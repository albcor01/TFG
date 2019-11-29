using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMakerTest : MonoBehaviour
{

    public float parametro { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        parametro = 0;
    }

    // Update is called once per frame
    void Update()
    {
        parametro+=Time.deltaTime;
    }
}
