using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portalScript : MonoBehaviour
{

    public GameObject portal;
    public GameObject portal2;
    public GameObject portal3;
    // Start is called before the first frame update
    void Start()
    {
        portal = GameObject.Find("ShopExit");
        portal2 = GameObject.Find("ExitBlue");
        portal3 = GameObject.Find("PurpleExit");
    }

    // Update is called once per frame
    void Update()
    {   
        if(portal != null){
            portal.transform.Rotate(0.0f, 0.0f, 2.0f, Space.Self);
        }
        
        if(portal2 != null){
            portal2.transform.Rotate(0.0f, 0.0f, 2.0f, Space.Self);
        }

        if(portal3 != null){
            portal3.transform.Rotate(0.0f, 0.0f, 2.0f, Space.Self);
        }
    }
}
