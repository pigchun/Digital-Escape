using Fractions;

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AnswerChecker : MonoBehaviour
{
    private int numAlgProblems = 5;                             // Number of Algebra Problems in List
    private int numBasicProblems = 5;                           // Number of Basic Math Problems in List
    private int questionIndex;                                  // Index of Current Question
    private List<Question> questions = new List<Question>();    // List of Question Objects
    private string[] algAnswers;                                // Array of Answers to Algebra Problems
    private string[] algProblems;                               // Array of Algebra Problems
    private string[] basicAnswers;                              // Array of Answers to Basic Math Problems
    private string[] basicProblems;                             // Array of Basic Math Problems

    public bool correct = false;            // Flag indicating whether the Player answered the Current Question correctly
    public bool quit = false;               // Flag indicating whether the Player wants to go back to Maze
    public Button quitButton;               // UI Button used to quit Question Answering Scene
    public Button submitButton;             // UI Button used to submit Answer to Current Question
    public InputField responseField;        // UI Input Field used to type in Answer to Current Question
    public string curDoor = "";             // String specifying Door to be opened upon correctly answering Question
    public Text questionText;               // UI Text used to display the Current Question
    public static AnswerChecker Instance;   // Stores AnswerChecker Instance for other Scripts to access

    [System.Serializable]
    public class Question
    {
        public string question; // Stores Question Text
        public string answer;   // Stores Answer to Question
    }

    // Function to initialize AnswerChecker Instance, Generate Trivia, Basic Math,
    // and Algebra Problems, and setup UI Elements
    void Awake()
    {
        Instance = this;
        basicProblems = new string[numBasicProblems];
        basicAnswers = new string[numBasicProblems];
        algProblems = new string[numAlgProblems];
        algAnswers = new string[numAlgProblems];

        // Generate Basic Math Problems and their Answers
        for (int i = 0; i < numBasicProblems; i++)
        {
            var (problem, answer) = GenerateBasicProblem();
            basicProblems[i] = problem;
            basicAnswers[i] = answer;
            Debug.Log("Problem: " + basicProblems[i] + ", Answer: " + basicAnswers[i]);
        }

        // Generate Algebra Problems and their Answers
        for (int i = 0; i < numAlgProblems; i++)
        {
            var (problem, answer) = GenerateAlgebraProblem();
            algProblems[i] = problem;
            algAnswers[i] = answer;
            Debug.Log("Problem: " + algProblems[i] + ", Answer: " + algAnswers[i]);
        }

        questionIndex = Random.Range(0, 15);                                // Choose a Random Index for Current Question
        addQuestions(basicProblems, basicAnswers, algProblems, algAnswers); // Add 15 Questions
        ShowQuestion();                                                     // Show Current Question
        quitButton.onClick.AddListener(onQuitButtonClick);                  // Set quitButton's onClick Listener
        submitButton.onClick.AddListener(OnSubmitButtonClick);              // Set submitButton's onClick Listener
    }

    // Function to compute the Correct Answer for a
    // Basic Math Problem
    private Fraction computeCorrectAnswer(int num1, int num2, char op, Fraction correctAnswer)
    {
        // Operator is '+'
        if (op == '+')
        {
            correctAnswer = new Fraction(num1 + num2, 1);
        }

        // Operator is '-'
        else if (op == '-')
        {
            correctAnswer = new Fraction(num1 - num2, 1);
        }

        // Operator is '*'
        else if (op == '*')
        {
            correctAnswer = new Fraction(num1 * num2, 1);
        }

        // Operator is '/'
        else if (op == '/')
        {
            correctAnswer = new Fraction(num1, num2);
        }

        return correctAnswer;
    }

    // Function to compute the Numerator used to compose 
    // the Answer to an Algebra Problem
    private int computeNumerator(int num2, int num3, char op)
    {
        int numerator = 0;

        // Operator is '+'
        if (op == '+')
        {
            numerator = num3 - num2;
        }

        // Operator is '-'
        else if (op == '-')
        {
            numerator = num3 + num2;
        }

        // Operator is '*'
        else if (op == '*')
        {
            numerator = num3 / num2;
        }

        // Operator is '/'
        else if (op == '/')
        {
            numerator = num3 * num2;
        }

        return numerator;
    }

    // Function that gets called whenever Player clicks Quit Button
    void onQuitButtonClick()
    {
        Text placeholderText = responseField.placeholder.GetComponent<Text>();
        placeholderText.text = "Enter answer";
        responseField.text = "";
        quit = true;
    }

    // Function that gets called whenever Player clicks Submit Button
    void OnSubmitButtonClick()
    {
        CheckAnswer();
    }

    // Function to add Questions to List
    public void addQuestions(string[] basicProblems, string[] basicAnswers, string[] algProblems, string[] algAnswers)
    {
        questions.Add(new Question { question = "What is the capital of France?", answer = "Paris" });
        questions.Add(new Question { question = basicProblems[0], answer = basicAnswers[0] });
        questions.Add(new Question { question = algProblems[0], answer = algAnswers[0] });
        questions.Add(new Question { question = "What is the largest planet?", answer = "Jupiter" });
        questions.Add(new Question { question = basicProblems[1], answer = basicAnswers[1] });
        questions.Add(new Question { question = algProblems[1], answer = algAnswers[1] });
        questions.Add(new Question { question = "What is the third largest city in the US?", answer = "Chicago" });
        questions.Add(new Question { question = basicProblems[2], answer = basicAnswers[2] });
        questions.Add(new Question { question = algProblems[2], answer = algAnswers[2] });
        questions.Add(new Question { question = "What is the longest river in the world?", answer = "Nile River" });
        questions.Add(new Question { question = basicProblems[3], answer = basicAnswers[3] });
        questions.Add(new Question { question = algProblems[3], answer = algAnswers[3] });
        questions.Add(new Question { question = "What ocean is between North America and Asia?", answer = "Pacific Ocean" });
        questions.Add(new Question { question = basicProblems[4], answer = basicAnswers[4] });
        questions.Add(new Question { question = algProblems[4], answer = algAnswers[4] });
    }

    // Function to check Answer Player gave to Current Question
    public void CheckAnswer()
    {
        string userAnswer = responseField.text.ToLower();
        string expectedAnswer = questions[questionIndex].answer.ToLower();

        if (userAnswer == expectedAnswer)
        {
            Text placeholderText = responseField.placeholder.GetComponent<Text>();
            placeholderText.text = "Enter answer";
            responseField.text = "";
            questionIndex = Random.Range(0, 15);
            questionText.text = questions[questionIndex].question;
            correct = true;
        }
        else
        {
            Text placeholderText = responseField.placeholder.GetComponent<Text>();
            placeholderText.text = "Try again";
            responseField.text = "";
        }
    }

    // Function to generate an Algebra Problem and Answer to the Problem
    public (string, string) GenerateAlgebraProblem()
    {
        int num1 = Random.Range(1, 11);                               // Random number between 1 and 10
        int num2 = Random.Range(1, 11);                               // Random number between 1 and 10
        int num3 = Random.Range(1, 11);                               // Random number between 1 and 10
        char[] operators = { '+', '-', '*', '/' };                    // Operators Array
        char op = operators[Random.Range(0, operators.Length)];       // Randomly assign an Operator
        char[] variables = { 'x', 'y', 'z' };                         // Variables Array
        char variable = variables[Random.Range(0, variables.Length)]; // Randomly assign a Variable
        int numerator;                                                // Numerator for Fraction
        int denominator;                                              // Denominator for Fraction
        Fraction correctAnswer;                                       // Correct Answer to Problem

        // Ensure Multiplication Problems are Valid (Numerator and Denominator are Whole Numbers)
        Fraction fraction = new Fraction(num3, num2);
        while (fraction.Denominator != 1)
        {
            num2 = Random.Range(1, 11);
            num3 = Random.Range(1, 11);
            fraction = new Fraction(num3, num2);
        }

        // Ensure Division Problems are Valid (no Division by Zero)
        if (op == '/' && num2 == 0)
        {
            num2 = 1;
        }

        // Create the Algebra Problem String
        string problem = $"{num1}{variable} {op} {num2} = {num3} solve for {variable}";

        // Create Fraction for Answer
        numerator = computeNumerator(num2, num3, op);
        denominator = num1;
        correctAnswer = new Fraction(numerator, denominator);

        return (problem, correctAnswer.ToString());
    }

    // Function to generate a Basic Math Problem and Answer to the Problem
    public (string, string) GenerateBasicProblem()
    {
        int num1 = Random.Range(1, 11);                         // Random number between 1 and 10
        int num2 = Random.Range(1, 11);                         // Random number between 1 and 10
        char[] operators = { '+', '-', '*', '/' };              // Operators Array
        char op = operators[Random.Range(0, operators.Length)]; // Randomly assign an Operator
        Fraction correctAnswer;                                 // Correct Answer to Problem

        // Ensure Division Problems are Valid (no Division by Zero)
        if (op == '/' && num2 == 0)
        {
            num2 = 1;
        }

        // Create the Math Problem String
        string problem = $"{num1} {op} {num2} = ?";

        // Create Fraction for Correct Answer
        correctAnswer = new Fraction(0, 1);
        correctAnswer = computeCorrectAnswer(num1, num2, op, correctAnswer);

        return (problem, correctAnswer.ToString());
    }

    // Function to display the Question at questionIndex in the List
    void ShowQuestion()
    {
        questionText.text = questions[questionIndex].question;
    }
}