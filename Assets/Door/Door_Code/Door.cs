using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    
    private bool hasTriggeredGPT = false;  // 防止多次触发
    public GPTDialogPool gptPool; // 拖入 GPTDialogPool 脚本对象



    private Collider2D doorCollider;        // Holds Collider Component for this Door Instance
    private Collider2D playerCollider;      // Holds Collider Component for Player Instance
    private GameObject playerObject;        // Holds Player GameObject
    private float actualDistance;           // Holds Actual Distance between Player and Door
    private float minTriggerDistance;       // Holds Minimum Distance needed between Player and Door to trigger Question Answering Canvas
    private Player playerInstance;          // Holds Player Instance

    public AudioClip openDoorSound;
    public bool doorUnlocked = false;       // Flag indicating whether the Door is Locked or Unlocked
    public bool isOpen = false;             // Flag indicating whether the Door is Open or Closed
    public bool playerInstanceSet = false;  // Flag indicating Player Instance has been instantiated

    IEnumerator unlockDoor()
    {
        yield return new WaitForSeconds(2f); // 2 second delay
        // Calculate minimum distance needed for trigger
        minTriggerDistance = CalculateMinTriggerDistance(doorCollider, playerCollider);
        
        // Get actual distance between objects
        actualDistance = Vector2.Distance(transform.position, playerObject.transform.position);

        if (actualDistance <= minTriggerDistance)
        {
            // Unlock Door 1-9
            if      (gameObject.tag == "Door 1") playerInstance.unlockedDoor1 = true;
            else if (gameObject.tag == "Door 2") playerInstance.unlockedDoor2 = true;
            else if (gameObject.tag == "Door 3") playerInstance.unlockedDoor3 = true;
            else if (gameObject.tag == "Door 4") playerInstance.unlockedDoor4 = true;
            else if (gameObject.tag == "Door 5") playerInstance.unlockedDoor5 = true;
            else if (gameObject.tag == "Door 6") playerInstance.unlockedDoor6 = true;
            else if (gameObject.tag == "Door 7") playerInstance.unlockedDoor7 = true;
            else if (gameObject.tag == "Door 8") playerInstance.unlockedDoor8 = true;
            else if (gameObject.tag == "Door 9") playerInstance.unlockedDoor9 = true;
        }

        doorUnlocked = true;
    }

    void Start()
    {
        playerInstance = Player.Instance;
    }

    void Update()
    {
        // Player Instance has been set
        if (Player.Instance != null && playerInstanceSet == false)
        {
            playerInstance   = Player.Instance;
            playerObject     = GameObject.FindWithTag("Player");
            playerInstanceSet = true;
            doorCollider     = GetComponent<Collider2D>();
            playerCollider   = playerInstance.GetComponent<Collider2D>();
        }

        if (playerInstanceSet == true)
        {
            minTriggerDistance = CalculateMinTriggerDistance(doorCollider, playerCollider);
            actualDistance     = Vector2.Distance(transform.position, playerObject.transform.position);

            // Player approached this Door with the Key to unlock it
            if (actualDistance <= minTriggerDistance &&
                playerHasKeyForDoor(playerInstance) &&
                !playerAnsweredQuestionForDoor(playerInstance))
            {
                StartCoroutine(unlockDoor());
            }

            // Player answered Question associated with this Door
            if (doorUnlocked == true && playerAnsweredQuestionForDoor(playerInstance))
            {
                doorCollider.isTrigger = true;
            }
        }
    }

    private void adjustDoorPosition(float doorXPosition, float doorYPosition, float x, float y)
    {
        float doorZPosition = transform.position.z;
        float newDoorXPosition = doorXPosition + x;
        float newDoorYPosition = doorYPosition + y;
        transform.localPosition = new Vector3(newDoorXPosition, newDoorYPosition, doorZPosition);
    }

    private float CalculateMinTriggerDistance(Collider2D a, Collider2D b)
    {
        if (a is CircleCollider2D circleA && b is CircleCollider2D circleB)
        {
            return circleA.radius * GetScale(circleA.transform) +
                   circleB.radius * GetScale(circleB.transform);
        }

        if (a is BoxCollider2D boxA && b is BoxCollider2D boxB)
        {
            float sizeA = Mathf.Max(boxA.size.x, boxA.size.y) * 0.5f * GetScale(boxA.transform);
            float sizeB = Mathf.Max(boxB.size.x, boxB.size.y) * 0.5f * GetScale(boxB.transform);
            return sizeA + sizeB;
        }

        Bounds boundsA = a.bounds;
        Bounds boundsB = b.bounds;
        return Vector2.Distance(boundsA.ClosestPoint(boundsB.center),
                                boundsB.ClosestPoint(boundsA.center));
    }

    float GetScale(Transform t)
    {
        return Mathf.Max(t.lossyScale.x, t.lossyScale.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOpen)
        {
            OpenDoor();                       // *** CHANGED *** 不再传 other
            Debug.Log("Collide with door");
        }
    }
    
    private void OpenDoor()
    {
        if (Player.Instance != null &&
            playerHasKeyForDoor(Player.Instance) &&
            playerAnsweredQuestionForDoor(Player.Instance))
        {
            PlayOpenSound();
            isOpen = true;
            Destroy(gameObject);


            if (gptPool != null && !hasTriggeredGPT)
            {
                gptPool.ShowRandomLine();
                hasTriggeredGPT = true;
            }

        }
        else
        {
            Debug.Log("No function played");
        }
    }


    /*---------------- 判定函数保持原样 ----------------*/
    private bool playerAnsweredQuestionForDoor(Player player)
    {
        if (CompareTag("Door 1") && player.answeredQuestion1) return true;
        else if (CompareTag("Door 2") && player.answeredQuestion2) return true;
        else if (CompareTag("Door 3") && player.answeredQuestion3) return true;
        else if (CompareTag("Door 4") && player.answeredQuestion4) return true;
        else if (CompareTag("Door 5") && player.answeredQuestion5) return true;
        else if (CompareTag("Door 6") && player.answeredQuestion6) return true;
        else if (CompareTag("Door 7") && player.answeredQuestion7) return true;
        else if (CompareTag("Door 8") && player.answeredQuestion8) return true;
        else if (CompareTag("Door 9") && player.answeredQuestion9) return true;
        return false;
    }

    private bool playerHasKeyForDoor(Player player)
    {
        if      (CompareTag("Door 1") && player.hasKey1) return true;
        else if (CompareTag("Door 2") && player.hasKey2) return true;
        else if (CompareTag("Door 3") && player.hasKey3) return true;
        else if (CompareTag("Door 4") && player.hasKey4) return true;
        else if (CompareTag("Door 5") && player.hasKey5) return true;
        else if (CompareTag("Door 6") && player.hasKey6) return true;
        else if (CompareTag("Door 7") && player.hasKey7) return true;
        else if (CompareTag("Door 8") && player.hasKey8) return true;
        else if (CompareTag("Door 9") && player.hasKey9) return true;
        return false;
    }
    /*------------------------------------------------*/

    private void PlayOpenSound()
    {
        if (openDoorSound != null)
        {
            AudioSource.PlayClipAtPoint(openDoorSound, transform.position);
        }
    }

    // rotateDoor() 现在不再被调用，但保留以免其他脚本引用
    private void rotateDoor(float startAngle, float endAngle)
    {
        Quaternion startRotation = Quaternion.Euler(0, 0, startAngle);
        Quaternion endRotation   = Quaternion.Euler(0, 0, endAngle);
        transform.rotation       = Quaternion.Slerp(startRotation, endRotation, 1);
    }
}
