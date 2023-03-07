using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileDamageController : MonoBehaviour
{
    [SerializeField] private float obstacleDamage;
    [SerializeField] private int pointMinus;
    private MobileHealthController healthController;

    private void Start()
    {
        healthController = GameObject.Find("GameManager").GetComponentInChildren<MobileHealthController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Damage();
        }
    }


    void Damage()
    {
        healthController.playerHealth = healthController.playerHealth - obstacleDamage;
        healthController.UpdateHealth();
        ScoreManager.instance.AddPoint(-pointMinus);
    }



}
