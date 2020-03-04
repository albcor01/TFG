using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public GameObject explo;
	

	// Use this for initialization
	void Start () {
        Invoke("DestroyBullet", 10.0f);
	}
	
	
    void DestroyBullet()
    {
        Destroy(gameObject);
    }
	
	void OnCollisionEnter(Collision col) {
	
		GameObject.Instantiate(explo, col.contacts[0].point, Quaternion.identity);
	
		Destroy(gameObject);
	}
	
	
}
