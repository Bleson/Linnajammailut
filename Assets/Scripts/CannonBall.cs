using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {

    public bool left = true;

	// Use this for initialization
	void Start () {
        if (left)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(7f, 12f), Random.Range(7f, 12f));
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-7f, -12f), Random.Range(7f, 12f));
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
