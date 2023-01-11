using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SporgeSkema : MonoBehaviour
{
    [SerializeField]
    private Button yourButton;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(Sporgeskema);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Sporgeskema()
    {
        
    }
}
