using UnityEngine;
using System.Collections;

public class BallSpawner : MonoBehaviour {

    //Camera cam;
    public GameObject BallToSpawn;
    float time;
    float offset = 0f;
    public float spawnFrequency = 4;

    void Start()
    {
        //cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        offset = Random.Range(-1f, 1f);
        time += offset;
        spawnFrequency += offset;
    }

	void Update () {
        time += Time.deltaTime;
        if (time >= spawnFrequency)
        {
            time = 0f;
            Instantiate(BallToSpawn, gameObject.transform.position, gameObject.transform.rotation);
        }
	}
}
