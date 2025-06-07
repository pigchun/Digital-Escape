using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SceneStateManager : MonoBehaviour
{
    private AnswerChecker answerCheckerInstance;    // Holds AnswerChecker Instance
    private Player playerInstance;                  // Holds Player Instance

    public bool answerCheckerInstanceSet = false;   // Flag indicating AnswerChecker Instance has been instantiated
    public bool playerInstanceSet = false;          // Flag indicating Player Instance has been instantiated
    public GameObject mazeGrid;                     // Holds BLUEMAZE Grid Game Object
    public GameObject mazeGrid2;                    // Holds PURPLEMAZE Grid Game Object
    public GameObject questionCanvas;               // Holds Question Canvas Game Object

    // Function to transition View to the Maze
    public void transitionToMaze(string reason) {
        // Player wants to quit answering the Question
        if(reason == "Quit")
        {
            // Player quit Question for Door 1
            if(answerCheckerInstance.curDoor == "Door 1")
            {
                mazeGrid.SetActive(true);
                playerInstance.unlockedDoor1 = false;
            }

            // Player quit Question for Door 2
            else if(answerCheckerInstance.curDoor == "Door 2")
            {
                mazeGrid.SetActive(true);
                playerInstance.unlockedDoor2 = false;
            }

            // Player quit Question for Door 3
            else if(answerCheckerInstance.curDoor == "Door 3")
            {
                mazeGrid.SetActive(true);
                playerInstance.unlockedDoor3 = false;
            }

            // Player quit Question for Door 4
            else if(answerCheckerInstance.curDoor == "Door 4")
            {
                mazeGrid.SetActive(true);
                playerInstance.unlockedDoor4 = false;
            }

            // Player quit Question for Door 5
            else if(answerCheckerInstance.curDoor == "Door 5")
            {
                mazeGrid2.SetActive(true);
                playerInstance.unlockedDoor5 = false;
            }

            // Player quit Question for Door 6
            else if(answerCheckerInstance.curDoor == "Door 6")
            {
                mazeGrid2.SetActive(true);
                playerInstance.unlockedDoor6 = false;
            }

            // Player quit Question for Door 7
            else if(answerCheckerInstance.curDoor == "Door 7")
            {
                mazeGrid2.SetActive(true);
                playerInstance.unlockedDoor7 = false;
            }

            // Player quit Question for Door 8
            else if(answerCheckerInstance.curDoor == "Door 8")
            {
                mazeGrid2.SetActive(true);
                playerInstance.unlockedDoor8 = false;
            }

            // Player quit Question for Door 9
            else if(answerCheckerInstance.curDoor == "Door 9")
            {
                mazeGrid2.SetActive(true);
                playerInstance.unlockedDoor9 = false;
            }
            
            answerCheckerInstance.quit = false;
        }

        // Player entered the Correct Answer to the Question
        else if(reason == "Correct")
        {
            // Player answered Question for Door 1
            if(answerCheckerInstance.curDoor == "Door 1")
            {
                mazeGrid.SetActive(true);
                playerInstance.answeredQuestion1 = true;
            }

            // Player answered Question for Door 2
            else if(answerCheckerInstance.curDoor == "Door 2")
            {
                mazeGrid.SetActive(true);
                playerInstance.answeredQuestion2 = true;
            }

            // Player answered Question for Door 3
            else if(answerCheckerInstance.curDoor == "Door 3")
            {
                mazeGrid.SetActive(true);
                playerInstance.answeredQuestion3 = true;
            }

            // Player answered Question for Door 4
            else if(answerCheckerInstance.curDoor == "Door 4")
            {
                mazeGrid.SetActive(true);
                playerInstance.answeredQuestion4 = true;
            }

            // Player answered Question for Door 5
            else if(answerCheckerInstance.curDoor == "Door 5")
            {
                mazeGrid2.SetActive(true);
                playerInstance.answeredQuestion5 = true;
            }

            // Player answered Question for Door 6
            else if(answerCheckerInstance.curDoor == "Door 6")
            {
                mazeGrid2.SetActive(true);
                playerInstance.answeredQuestion6 = true;
            }

            // Player answered Question for Door 7
            else if(answerCheckerInstance.curDoor == "Door 7")
            {
                mazeGrid2.SetActive(true);
                playerInstance.answeredQuestion7 = true;
            }

            // Player answered Question for Door 8
            else if(answerCheckerInstance.curDoor == "Door 8")
            {
                mazeGrid2.SetActive(true);
                playerInstance.answeredQuestion8 = true;
            }

            // Player answered Question for Door 9
            else if(answerCheckerInstance.curDoor == "Door 9")
            {
                mazeGrid2.SetActive(true);
                playerInstance.answeredQuestion9 = true;
            }

            answerCheckerInstance.correct = false;
        }

        questionCanvas.SetActive(false);
    }

    // Function to transition View to the Question Answering Canvas
    public void transitionToQuestionCanvas(string doorNumber) {
        // Player unlocked Door 1
        if(doorNumber == "Door 1")
        {
            mazeGrid.SetActive(false);
            answerCheckerInstance.curDoor = "Door 1";
        }

        // Player unlocked Door 2
        else if(doorNumber == "Door 2")
        {
            mazeGrid.SetActive(false);
            answerCheckerInstance.curDoor = "Door 2";
        }

        // Player unlocked Door 3
        else if(doorNumber == "Door 3")
        {
            mazeGrid.SetActive(false);
            answerCheckerInstance.curDoor = "Door 3";
        }

        // Player unlocked Door 4
        else if(doorNumber == "Door 4")
        {
            mazeGrid.SetActive(false);
            answerCheckerInstance.curDoor = "Door 4";
        }

        // Player unlocked Door 5
        else if(doorNumber == "Door 5")
        {
            mazeGrid2.SetActive(false);
            answerCheckerInstance.curDoor = "Door 5";
        }

        // Player unlocked Door 6
        else if(doorNumber == "Door 6")
        {
            mazeGrid2.SetActive(false);
            answerCheckerInstance.curDoor = "Door 6";
        }

        // Player unlocked Door 7
        else if(doorNumber == "Door 7")
        {
            mazeGrid2.SetActive(false);
            answerCheckerInstance.curDoor = "Door 7";
        }

        // Player unlocked Door 8
        else if(doorNumber == "Door 8")
        {
            mazeGrid2.SetActive(false);
            answerCheckerInstance.curDoor = "Door 8";
        }

        // Player unlocked Door 9
        else if(doorNumber == "Door 9")
        {
            mazeGrid2.SetActive(false);
            answerCheckerInstance.curDoor = "Door 9";
        }

        questionCanvas.SetActive(true);
    }

    // Function to setup answerCheckerInstace, playerInstance,
    // and questionCanvas
    void Start()
    {
        answerCheckerInstance = AnswerChecker.Instance;
        playerInstance = Player.Instance;
        questionCanvas.SetActive(false);
    }

    // Function to update the State of the Scene based on
    // what Events have occured
    void Update()
    {
        if (Player.Instance != null && playerInstanceSet == false)
        {
            Debug.Log("playerInstance set");
            playerInstance = Player.Instance;
            playerInstanceSet = true;
        }

        if (AnswerChecker.Instance != null && answerCheckerInstanceSet == false)
        {
            Debug.Log("answerCheckerInstance set");
            answerCheckerInstance = AnswerChecker.Instance;
            answerCheckerInstanceSet = true;
        }

        // Player unlocked Door 1,
        // answer a Question
        if (playerInstance != null && playerInstance.unlockedDoor1 == true && playerInstance.answeredQuestion1 == false)
        {
            Debug.Log("Door is unlocked");
            transitionToQuestionCanvas("Door 1");
        }

        // Player unlocked Door 2,
        // answer a Question
        if (playerInstance != null && playerInstance.unlockedDoor2 == true && playerInstance.answeredQuestion2 == false)
        {
            Debug.Log("Door is unlocked");
            transitionToQuestionCanvas("Door 2");
        }

        // Player unlocked Door 3,
        // answer a Question
        if (playerInstance != null && playerInstance.unlockedDoor3 == true && playerInstance.answeredQuestion3 == false)
        {
            Debug.Log("Door is unlocked");
            transitionToQuestionCanvas("Door 3");
        }

        // Player unlocked Door 4,
        // answer a Question
        if (playerInstance != null && playerInstance.unlockedDoor4 == true && playerInstance.answeredQuestion4 == false)
        {
            Debug.Log("Door is unlocked");
            transitionToQuestionCanvas("Door 4");
        }
        // Player unlocked Door 5,
        // answer a Question
        if (playerInstance != null && playerInstance.unlockedDoor5 == true && playerInstance.answeredQuestion5 == false)
        {
            Debug.Log("Door is unlocked");
            transitionToQuestionCanvas("Door 5");
        }

        // Player unlocked Door 6,
        // answer a Question
        if (playerInstance != null && playerInstance.unlockedDoor6 == true && playerInstance.answeredQuestion6 == false)
        {
            Debug.Log("Door is unlocked");
            transitionToQuestionCanvas("Door 6");
        }

        // Player unlocked Door 7,
        // answer a Question
        if (playerInstance != null && playerInstance.unlockedDoor7 == true && playerInstance.answeredQuestion7 == false)
        {
            Debug.Log("Door is unlocked");
            transitionToQuestionCanvas("Door 7");
        }

        // Player unlocked Door 8,
        // answer a Question
        if (playerInstance != null && playerInstance.unlockedDoor8 == true && playerInstance.answeredQuestion8 == false)
        {
            Debug.Log("Door is unlocked");
            transitionToQuestionCanvas("Door 8");
        }
        // Player unlocked Door 9,
        // answer a Question
        if (playerInstance != null && playerInstance.unlockedDoor9 == true && playerInstance.answeredQuestion9 == false)
        {
            Debug.Log("Door is unlocked");
            transitionToQuestionCanvas("Door 9");
        }

        // Player wants to quit the Question, go back to Maze
        if (answerCheckerInstanceSet && answerCheckerInstance.quit == true)
        {
            Debug.Log("Player wants to quit");
            transitionToMaze("Quit");
        }

        // Player answered the Question,
        // go back to Maze
        if (answerCheckerInstance != null && answerCheckerInstance.correct == true)
        {
            Debug.Log("Answer is correct");
            transitionToMaze("Correct");
        }
    }
}
