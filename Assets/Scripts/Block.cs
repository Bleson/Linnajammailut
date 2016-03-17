using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public bool frozen = true;
    public float lastTouched = 0;
    public bool countLastTouched = false;

    void Update()
    {
        if (!frozen && countLastTouched)
        {
            lastTouched += Time.fixedDeltaTime;
            Debug.Log(lastTouched);
            if (lastTouched > 2f)
            {
                
            }
        }       
    }

    void Start()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
    }

    public void Unfreeze()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
    }

    public void Move(Vector3 targetLocation)
    {
        targetLocation = new Vector3(targetLocation.x, targetLocation.y, 0f);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetLocation, 0.7f);
    }
}
