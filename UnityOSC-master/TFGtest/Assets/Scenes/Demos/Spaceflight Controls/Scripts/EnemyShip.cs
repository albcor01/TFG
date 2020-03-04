using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    GameObject[] weapon_hardpoints;
    public GameObject bullet;
    GameObject player;
    Animator anim;
    int damage = 0;

    private int firstCannonChildIndex = 4;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = transform.GetChild(0).GetComponent<Animator>();
        
        weapon_hardpoints = new GameObject[4];

        for(int i = 0; i < 4; i++)
        {
            weapon_hardpoints[i] = transform.GetChild(i + firstCannonChildIndex).gameObject;
        }

        Invoke("fireShot", 3.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            damage += 1;
            anim.SetInteger("Damage", damage);
            if(damage > 9) Invoke("byebye", 0.55f);
        }
    }

    private void byebye() { 
        Destroy(gameObject);
        player.GetComponent<EnemyCreator>().EndBattle();
    }

    public void fireShot()
    {
        for (int i = 0; i < 4; i++)
        {
            weapon_hardpoints[i].transform.LookAt(player.transform.position);
        }

        float[] Dist = new float[4];
        for(int i = 0; i < 4; i++)
        {
            Vector3 aux = weapon_hardpoints[i].transform.position - player.transform.position;
            Dist[i] = aux.magnitude; 
        }
        float lowValue = int.MaxValue;
        int lowIndex = 0;
        for (int i = 0; i < 4; i++)
        {
            if (Dist[i] < lowValue)
            {
                lowValue = Dist[i];
                lowIndex = i;
            }
        }

        GameObject shot1 = (GameObject)GameObject.Instantiate(bullet, weapon_hardpoints[lowIndex].transform.position, Quaternion.identity);

        shot1.GetComponent<Rigidbody>().AddForce((weapon_hardpoints[lowIndex].transform.forward) * 9000f);

        Invoke("fireShot", 1.0f);
    }
}
