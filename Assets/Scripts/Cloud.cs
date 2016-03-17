using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

    public float minX = -10f;
    public float maxX = 10f;
    public float speed = 2f;
    // Update is called once per frame
    void Update ()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + (speed * Time.deltaTime), gameObject.transform.position.y, gameObject.transform.position.z);
        if (gameObject.transform.position.x > maxX && speed >= 0)
        {
            gameObject.transform.position = new Vector3(minX, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if(gameObject.transform.position.x < minX && speed < 0)
        {
            gameObject.transform.position = new Vector3(maxX, gameObject.transform.position.y, gameObject.transform.position.z);
        }
    }
}
