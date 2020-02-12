using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    bool EnCombate = false;
    public GameObject Enemgo;
    public GameObject EnemyShipRespawn;

    // Update is called once per frame
    void Update()
    {
        if (!EnCombate) {
            Invoke("InstanciaEnemigo", Random.Range(10.0f, 12.0f));
            EnCombate = true;
        }
    }

    public void EndBattle()
    {
        EnCombate = false;
    }

    void InstanciaEnemigo()
    {
        Vector3 pos;
        if (EnemyShipRespawn.transform.position.y < gameObject.transform.position.y)
            pos = new Vector3(EnemyShipRespawn.transform.position.x, EnemyShipRespawn.transform.position.y - 50, EnemyShipRespawn.transform.position.z); 
        else pos = new Vector3(EnemyShipRespawn.transform.position.x, EnemyShipRespawn.transform.position.y + 50, EnemyShipRespawn.transform.position.z);
        Instantiate(Enemgo, pos, Quaternion.identity);
    }
}
