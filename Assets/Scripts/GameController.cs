using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public Camera cam;
    public bool mouseDown = false;
    public Block lastBlock;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) || mouseDown)
        {
            mouseDown = true;
            if (lastBlock == null)
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Rigidbody lol = hit.rigidbody;

                    if (lol.tag.ToString() == "Block")
                    {
                        lastBlock = lol.transform.gameObject.GetComponent<Block>();
                        Debug.Log("Clicked block " + lastBlock);
                    }
                }
            }
            else //Move block
            {
                Debug.Log("Yritystä 1");
                lastBlock.Move(cam.ScreenToWorldPoint(Input.mousePosition));
                lastBlock.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            lastBlock = null;
        }
	}
}
