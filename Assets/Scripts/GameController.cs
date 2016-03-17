using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public Camera cam;
    public bool mouseDown = false;
    public Block lastBlock;
    private Block lastBlockScoreCheck;
    public float scoreCheckDelay = 2f;
    public float highestBlockY;

    //camera stuff
    float lerpTime = 1f;
    float currentLerpTime;
    Vector3 startPos;
    Vector3 endPos;

    private bool moveCamera;
    private float cameraStartPos;
    public float cameraOffset; //ei  käytössä vielä

    // Use this for initialization
    void Start()
    {
        cameraStartPos = cam.transform.position.y;
        startPos = cam.transform.position;
        endPos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        
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
            if (lastBlock != null)
            {
                lastBlockScoreCheck = lastBlock;
                if (!lastBlockScoreCheck.countLastTouched)
                {
                    lastBlockScoreCheck.lastTouched = 0f;
                    lastBlockScoreCheck.countLastTouched = true;
                }
                lastBlock = null;
            }
        }
    }
    void SaveBlockHeight(float newHeight)
    {
        if (lastBlockScoreCheck && lastBlockScoreCheck.lastTouched >= scoreCheckDelay)
        {
            if (lastBlockScoreCheck.transform.position.y > highestBlockY)
            {
                highestBlockY = (int)(lastBlockScoreCheck.transform.position.y);
                lastBlockScoreCheck.countLastTouched = false;

                if (highestBlockY > cameraStartPos)
                {
                    currentLerpTime = 0f;
                    startPos = cam.transform.position;
                    endPos = new Vector3(cam.transform.position.x, highestBlockY, cam.transform.position.z);
                }                
            }
        }        
    }
    void MoveCamera()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float perc = currentLerpTime / lerpTime;
        cam.transform.position = Vector3.Lerp(startPos, endPos, perc);
    }
}
