using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MobileHealthController : MonoBehaviour
{
    public float playerHealth;
    [SerializeField] private Text healthText;

    void Start()
    {
        UpdateHealth();
    }
    public void UpdateHealth()
    {
        healthText.text = playerHealth.ToString("0");

        if(playerHealth <= 0)
        {
            
            
                SceneManager.LoadScene("GameOver_Runner");
           
        }
    }

    
}
