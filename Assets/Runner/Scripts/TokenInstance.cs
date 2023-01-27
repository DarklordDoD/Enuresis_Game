using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenInstance : MonoBehaviour
{  
    void OnTriggerEnter2D(Collider2D Coin)
    {
        if (Coin.tag == "MyCoin")
        {
            ScoreManager.instance.AddPoint();
            Destroy(Coin.gameObject);
        }

        if (Coin.tag == "Food")
        {
            ScoreManager.instance.AddPoint();
        }
    }
}
