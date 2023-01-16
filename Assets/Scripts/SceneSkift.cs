using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSkift : MonoBehaviour
{
    [SerializeField]
    private string skiftSceneTil;

    SceneManeger scM;

    private void Start()
    {
        scM = FindObjectOfType<Canvas>().GetComponent<SceneManeger>();
    }

    public void SkiftTil()
    {
        scM.NewScene(skiftSceneTil);
    }
}
