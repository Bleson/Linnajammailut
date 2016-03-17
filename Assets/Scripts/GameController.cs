using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    bool gameOver = false;

    public bool mouseDown = false;
    public bool mouse2Down = false;
    bool rotateToHorizontalNext = true;

    public Block lastBlock;
    Block lastBlockScoreCheck;
    public float scoreCheckDelay = 2f;
    public float highscore;

    //camera stuff
    float lerpTime = 1f;
    float currentLerpTime;
    Vector3 cameraStartPos;
    Vector3 cameraEndPos;

    public Camera cam;
    bool moveCamera;
    float cameraOffset;
    public float cameraMovementTime = 2f;

    // Use this for initialization
    void Start()
    {
        cameraOffset = cam.transform.position.y;
        cameraStartPos = cam.transform.position;
        cameraEndPos = cam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        #region Mouse1 Input
        //Mouse click action
        if (Input.GetMouseButtonDown(0) || mouseDown && !gameOver)
        {           
            mouseDown = true;
            if (lastBlock == null)
            {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Rigidbody lol = hit.rigidbody;

                    //Testaa klikatun objektin
                    if (lol.tag.ToString() == "Block")
                    {
                        lastBlock = lol.transform.gameObject.GetComponent<Block>();
                        lastBlock.StopTimer();

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

        //Mouse release
        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
            if (lastBlock != null)
            {
                lastBlockScoreCheck = lastBlock;
                if (!lastBlockScoreCheck.countLastTouched)
                {
                    lastBlockScoreCheck.StartTimer();
                }
                lastBlock = null;
            }
        }
        #endregion

        #region Mouse2 Input
        if (Input.GetMouseButtonDown(1) && mouseDown && !gameOver && lastBlock != null && !mouse2Down)
        {
            if (rotateToHorizontalNext)
            {
                lastBlock.transform.rotation = Quaternion.Euler(Vector3.zero);
            }
            else
            {
                lastBlock.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            }
            lastBlock.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            rotateToHorizontalNext = !rotateToHorizontalNext;
            mouse2Down = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            mouse2Down = false;
        }
        #endregion

        if (moveCamera)
        {
            MoveCamera();
        }
    }

    void MoveCamera() //Move camera according to the high score
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float perc = currentLerpTime / lerpTime;
        Vector3 newCamPos = Vector3.Lerp(cameraStartPos, cameraEndPos, perc);

        cam.transform.position = newCamPos;
    }

    void StopCameraMovement()
    {
        moveCamera = false;
        currentLerpTime = 0f;
    }

    public void UpdateHighscore(float newScore)
    {
        if (newScore > highscore)
        {
            highscore = newScore;
            cameraStartPos = cam.transform.position;
            if (highscore > cameraOffset)
            {
                cameraEndPos = new Vector3(0f, highscore, cam.transform.position.z);
                moveCamera = true;
                Invoke("StopCameraMovement", cameraMovementTime);
            }
        }
    }
    public void Lose()
    {
        Debug.Log("You lose!");//Lose game
    }
}
