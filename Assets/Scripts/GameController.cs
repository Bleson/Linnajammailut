using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public Camera cam;
    public bool mouseDown = false;
    public Block lastBlock;
    private Block lastBlockScoreCheck;
    public float scoreCheckDelay;
    private float highestBlockY;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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
                        if (lastBlock.frozen)
                        {
                            lastBlock.Unfreeze();
                        }
                    }
                }
            }
            else //Move block
            {
                lastBlock.Move(cam.ScreenToWorldPoint(Input.mousePosition));
                lastBlock.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            lastBlockScoreCheck = lastBlock;
            lastBlock = null;
            Invoke("SaveBlockHeight", scoreCheckDelay);
        }
    }
    void SaveBlockHeight()
    {
        if (lastBlockScoreCheck.transform.position.y > highestBlockY)
        {
            highestBlockY = (int)(lastBlockScoreCheck.transform.position.y);
        }
    }
    void MoveCamera()
    {
        /*Vector3 point = cam.WorldToViewportPoint(target.position);
        Vector3 delta = target.position - cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);*/
    }
}
