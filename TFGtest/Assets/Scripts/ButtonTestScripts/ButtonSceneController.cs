using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSceneController : MonoBehaviour
{
  
    public bool[] Activatedlayers { get; set; } = new bool[8];
    public GameObject[] textsLayers = new GameObject[8];

    public bool[] OneShots { get; set; } = new bool[3];
    public GameObject[] OneShotsGO = new GameObject[3];

    bool serverState { get; set; } = false;
    public GameObject serverStateText;

    string[] packageText { get; set; } = { "Desert", "Ambient", "Horror", "Fantasy" };
    public GameObject packageSelected;

    private void Start()
    {
        for (int i = 0; i < Activatedlayers.Length; i++) Activatedlayers[i] = false;
    }

    public void LayerButtonPressed(int b) {
        if (b < Activatedlayers.Length && b >= 0)
        {
            Activatedlayers[b] = !Activatedlayers[b];
            textsLayers[b].transform.GetChild(0).GetComponent<Text>().text = Activatedlayers[b] ? "True" : "False";
        }
    }

    public void PackageButtonPressed(int p)
    {
        if(p < packageText.Length && p >= 0)
            packageSelected.transform.GetChild(0).GetComponent<Text>().text = packageText[p];
    }

    public void ServerButtonPressed() {
        serverState = !serverState;
        serverStateText.transform.GetChild(0).GetComponent<Text>().text = serverState ? "On" : "Off";
    }

    public void OneShotButtonPressed(int o)
    {
        if (o < OneShots.Length && o >= 0)
        {
            OneShots[o] = !OneShots[o];
        }
    }
}
