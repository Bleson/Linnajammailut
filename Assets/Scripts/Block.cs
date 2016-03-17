using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
    public bool wasTouchingGround = false;

    public bool frozen = true;
    public float lastTouchedTimer = 0;
    public float heightWhenReleased = 0;
    public bool useTimer = false;
    public bool countLastTouched = false;

    public GameController gController;

    void Update()
    {
        if (useTimer)
        {
            lastTouchedTimer += Time.fixedDeltaTime;
            if (lastTouchedTimer > gController.scoreCheckDelay)
            {
                UpdateScore();
            }
        }
    }

    void Start()
    {
        gController = FindObjectOfType<GameController>();
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;

        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.transform.parent = gController.cam.transform;
    }

    public void Unfreeze()
    {
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
        gameObject.transform.parent = null;
    }

    public void Move(Vector3 targetLocation)
    {
        targetLocation = new Vector3(targetLocation.x, targetLocation.y, 0f);
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, targetLocation, 0.7f);
    }

    public void StartTimer()
    {
        useTimer = true;
        heightWhenReleased = gameObject.transform.position.y;
    }

    public void StopTimer()
    {
        useTimer = false;
        lastTouchedTimer = 0f;
    }

    public void UpdateScore()
    {
        if (gameObject.transform.position.y <= heightWhenReleased && gameObject.transform.position.y + 2 >= heightWhenReleased)
        {
            gController.UpdateHighscore(transform.position.y);
        }
        useTimer = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(GroundCheck(collision) && lastTouchedTimer < 1f)
        {
            wasTouchingGround = true;
        }
        else if(GroundCheck(collision) && !wasTouchingGround)
        {
            gController.Lose();
        }
        //else if(GroundCheck(collision))
        //{

        //}
    }
    bool GroundCheck(Collision collision)
    {
        if (collision.gameObject.tag.ToString() == "Ground")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
