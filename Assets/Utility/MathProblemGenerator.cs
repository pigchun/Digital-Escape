using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathProblemGenerator : MonoBehaviour
{
    public string GenerateBasicProblem()
    {
        int num1 = Random.Range(1, 10); // Random number between 1 and 99
        int num2 = Random.Range(1, 10); // Random number between 1 and 99
        char[] operators = { '+', '-', '*', '/' };
        char op = operators[Random.Range(0, operators.Length)]; // Random operator

       
        // Create the math problem string
        string problem = $"{num1} {op} {num2} = ?";

        // Log the problem to the console (optional)
        Debug.Log("Generated Math Problem: " + problem);

        return problem;
    }
}
