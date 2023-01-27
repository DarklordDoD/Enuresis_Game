using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuideController : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
