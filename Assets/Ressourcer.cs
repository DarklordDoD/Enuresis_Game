using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using UnityEngine.UI;

public class Ressourcer : MonoBehaviour
{
    [Header("Tis Resurce")]
    [SerializeField]
    private Scrollbar tisShow;
    private float tisMeter;
    [SerializeField]
    private float tisUp;
    [SerializeField]
    private float tisVandUp;

    [Header("Vand Resurce")]
    [SerializeField]
    private Scrollbar vandShow;
    private float vandMeter;
    [SerializeField]
    private float vandNed;

    [Header("Glad Resurce")]
    [SerializeField]
    private Scrollbar gladShow;
    private float gladMeter;
    [SerializeField]
    private float gladTisNed;
    [SerializeField]
    private float gladVandNed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void fixedUpdate()
    {
        
    }
}
