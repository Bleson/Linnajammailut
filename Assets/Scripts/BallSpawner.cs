using UnityEngine;
using System.Collections;

public class BallSpawner : MonoBehaviour {

    public bool enableOffset = true;
    public GameObject ObjectToSpawn;
    float time;
    float offset = 0f;
    public float spawnFrequency = 4;

    void Start()
    {
        Spawn();
        if (enableOffset)
        {
            offset = Random.Range(-1f, 1f);
            time += offset;
            spawnFrequency += offset;
        }
    }

	void Update () {
        time += Time.deltaTime;
        if (time >= spawnFrequency)
        {
            time = 0f;
            Spawn();
        }
	}

    void Spawn()
    {
        Instantiate(ObjectToSpawn, gameObject.transform.position, gameObject.transform.rotation);
    }
}
