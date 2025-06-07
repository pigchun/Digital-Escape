using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; 
    public float speed = 2f; 


    void Update(){
    if (player == null) return;

    
    Vector2 direction = (player.position - transform.position).normalized;

    
    if (direction.x > 0)
        transform.localScale = new Vector3(-1, 1, 1); 
    else 
        transform.localScale = new Vector3(1, 1, 1);  

    
    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

}
