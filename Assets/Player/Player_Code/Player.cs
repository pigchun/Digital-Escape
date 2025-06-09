using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Player : MonoBehaviour
{
    /*
    public bool hasKey = false; // Track if the player has picked up the key
    public bool open = false;
    */
    public AudioClip coinSound;
    public AudioClip pickupSound;
    public AudioClip exitSound; // ★NEW★ 新增出口

    public bool answeredQuestion1 = false;  // Flag indicating Player has answered a Question after unlocking Door 1
    public bool answeredQuestion2 = false;  // Flag indicating Player has answered a Question after unlocking Door 2
    public bool answeredQuestion3 = false;  // Flag indicating Player has answered a Question after unlocking Door 3
    public bool answeredQuestion4 = false;  // Flag indicating Player has answered a Question after unlocking Door 4
    public bool answeredQuestion5 = false;
    public bool answeredQuestion6 = false;
    public bool answeredQuestion7 = false;
    public bool answeredQuestion8 = false;
    public bool answeredQuestion9 = false;
    public bool hasKey1 = false;            // Flag indicating Player has picked up Key 1
    public bool hasKey2 = false;            // Flag indicating Player has picked up Key 2
    public bool hasKey3 = false;            // Flag indicating Player has picked up Key 3
    public bool hasKey4 = false;            // Flag indicating Player has picked up Key 4
    public bool hasKey5 = false;
    public bool hasKey6 = false;
    public bool hasKey7 = false;
    public bool hasKey8 = false;
    public bool hasKey9 = false;
    public bool unlockedDoor1 = false;      // Flag indicating Player has unlocked Door 1
    public bool unlockedDoor2 = false;      // Flag indicating Player has unlocked Door 2
    public bool unlockedDoor3 = false;      // Flag indicating Player has unlocked Door 3
    public bool unlockedDoor4 = false;      // Flag indicating Player has unlocked Door 4
    public bool unlockedDoor5 = false;
    public bool unlockedDoor6 = false;
    public bool unlockedDoor7 = false;
    public bool unlockedDoor8 = false;
    public bool unlockedDoor9 = false;
    public GameObject heartClone;
    public GameObject timeClone;
    public static Player Instance;          // Stores Player Instance for other Scripts to access
    public Transform exitPoint;

    // Function to initialize Player Instance
    void Awake()
    {
        Instance = this;
    }

    //Function for advancing to Next Level
    private void exitLevel(GameObject exitDoor, string levelType){
        if (exitSound != null)
        {
            AudioSource.PlayClipAtPoint(exitSound, transform.position);
        }
        GameObject bluemaze = GameObject.Find("BLUEMAZE");
        GameObject purplemaze = GameObject.Find("PURPLEMAZE");
        GameObject canvas = GameObject.Find("Canvas-always");
        

        
        //If player completes blue maze
        if (levelType == "ExitBlue" || levelType == "PurpleExit")
        {
            
            if (exitPoint != null)
                transform.position = exitPoint.position;

            canvas.GetComponent<timerScript>().finishLevel();
            Destroy(exitDoor);
        }
        if (levelType == "ShopExit"){
            purplemaze.SetActive(true);
            transform.position = new Vector3 (64,-17,0);
            canvas.GetComponent<timerScript>().startLevel();
        }
    }

    // Function to determine if Player has collided with a Key Object
    // and if so, pick up the Key that the Player has collided with
    private bool keyPickedUp(Collider2D other)
    {
        // Player has collided with Key 1
        if (other.CompareTag("Key 1"))
        {
            PickUpKey(other.gameObject, "Door 1");
            hasKey1 = true;
            return true;
        }

        // Player has collided with Key 2
        else if (other.CompareTag("Key 2"))
        {
            PickUpKey(other.gameObject, "Door 2");
            hasKey2 = true;
            return true;
        }

        // Player has collided with Key 3
        else if (other.CompareTag("Key 3"))
        {
            PickUpKey(other.gameObject, "Door 3");
            hasKey3 = true;
            return true;
        }

        // Player has collided with Key 4
        else if (other.CompareTag("Key 4"))
        {
            PickUpKey(other.gameObject, "Door 4");
            hasKey4 = true;
            return true;
        }

        // Player has collided with Key 5
        else if (other.CompareTag("Key 5"))
        {
            PickUpKey(other.gameObject, "Door 5");
            hasKey5 = true;
            return true;
        }

        // Player has collided with Key 6
        else if (other.CompareTag("Key 6"))
        {
            PickUpKey(other.gameObject, "Door 6");
            hasKey6 = true;
            return true;
        }

        // Player has collided with Key 7
        else if (other.CompareTag("Key 7"))
        {
            PickUpKey(other.gameObject, "Door 7");
            hasKey7 = true;
            return true;
        }

        // Player has collided with Key 8
        else if (other.CompareTag("Key 8"))
        {
            PickUpKey(other.gameObject, "Door 8");
            hasKey8 = true;
            return true;
        }

        // Player has collided with Key 9
        else if (other.CompareTag("Key 9"))
        {
            PickUpKey(other.gameObject, "Door 9");
            hasKey9 = true;
            return true;
        }
        
        return false;
    }

    // Function that gets called when Player collides with a Game Object
    // and determines what Game Object the Player collided with
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject canvas = GameObject.Find("Canvas-always");
        //WHEN PLAYER REACHES PORTAL/LEVEL EXIT
        if (other.CompareTag("Exit")){
            exitLevel(other.gameObject, other.gameObject.name);
        }
        
        else if (other.CompareTag("Coin"))
        {
            if (coinSound != null)
            {
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
            }
            Destroy(other.gameObject);
            canvas.GetComponent<timerScript>().getCoin();
        }

        //SHOP ITEMS
        else if (other.CompareTag("Heart"))
        {
            buyHeart(other.gameObject);
        }
        else if (other.CompareTag("Hourglass"))
        {
            buyTime(other.gameObject);
        }

        // Detect if Player collided with Key Object
        else if(keyPickedUp(other))
        {
            return;
        }

    }

    private void buyHeart(GameObject heart)
    {
        GameObject canvas = GameObject.Find("Canvas-always");
        PlayerHealth playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        //If player has enough money and their health isn't max, buy heart
        if(canvas.GetComponent<timerScript>().coins >= 5 && playerHealth.currentHealth < playerHealth.maxHealth){

            //Make heart clone
            heartClone = Instantiate(heart, heart.transform.position, Quaternion.identity);
            heartClone.SetActive(false);
            
            Destroy(heart);
            
            canvas.GetComponent<timerScript>().buyItem(5);
            playerHealth.currentHealth += 1;
            playerHealth.healthUI.UpdateHearts(playerHealth.currentHealth);

            //Wait for heart item to respawn
            StartCoroutine(Wait(heartClone, 3));
        }
    }

    private void buyTime(GameObject hourglass)
    {
        GameObject canvas = GameObject.Find("Canvas-always");
        
        //If player has enough money buy item
        if(canvas.GetComponent<timerScript>().coins >= 10){

            //Make item clone
            timeClone = Instantiate(hourglass, hourglass.transform.position, Quaternion.identity);
            timeClone.SetActive(false);
            
            Destroy(hourglass);
            
            canvas.GetComponent<timerScript>().buyItem(10);
            canvas.GetComponent<timerScript>().extraTime += 30;
            

            //Wait for time item to respawn
            StartCoroutine(Wait(timeClone, 3));
        }
    }

    // Function to handle Key Pickup
    private void PickUpKey(GameObject key, string doorNumber)
    {
        //hasKey = true; // Set the hasKey flag to true
        //open = true;
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
        Destroy(key); // Destroy the key GameObject

    }

    IEnumerator Wait(GameObject clone, int time)
    {
      

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(time);

        if(clone == null){
            yield break;
        }
        clone.SetActive(true);
    }
}