using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{

    //define player game object
    public GameObject player;

    //wait for lateupdate
    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, 0f, -10f);
    }



// Start is called before the first frame update
void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
