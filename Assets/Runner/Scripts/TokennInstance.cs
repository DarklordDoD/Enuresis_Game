using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokennInstance : MonoBehaviour
{



    void OnTriggerEnter2D(Collider2D Coin)
    {
        if (Coin.tag == "MyCoin")
        {
            ScoreManager.instance.AddPoint();
            Destroy(Coin.gameObject);


        }
    }
}