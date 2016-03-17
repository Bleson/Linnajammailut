using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour {

    public bool left = true;
    public float velocityX = 8f;
    public float velocityY = 7f;
    public float velocityMaxOffset = 2f;
    public float lifeTime = 5f;
    public float timeLived;

    // Use this for initialization
    void Start () {
        velocityX = Random.Range(velocityX - velocityMaxOffset, velocityX + velocityMaxOffset);
        velocityY = Random.Range(velocityY - velocityMaxOffset, velocityY + velocityMaxOffset);

        if (left)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(velocityX, velocityY);
        }
        else
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-velocityX, velocityY);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeLived += Time.fixedDeltaTime;

        if (gameObject.transform.position.y <= 0f || lifeTime < timeLived)
        {
            Destroy(gameObject);
        }
	}
}
