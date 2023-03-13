using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    [SerializeField]
    private GameObject desplayScene;
    [SerializeField]
    private List<Sprite> fraim;
    [SerializeField]
    private float delayTimer;

    private float timer;
    private int theAktivFrame;
    private bool isAktiv;

    public void StartCutScene()
    {
        isAktiv = true;

        desplayScene.SetActive(true);
    }

    private void Update()
    {
        if (!isAktiv)
            return;

        if (timer <= 0)
        {
            timer = delayTimer;

            if (theAktivFrame <= fraim.Count - 1)
            {
                desplayScene.GetComponent<Image>().sprite = fraim[theAktivFrame];
                theAktivFrame++;
            }
            else
            {
                desplayScene.SetActive(false);
                isAktiv = false;
                theAktivFrame = 0;
            }
        }
        else
            timer -= 1 * Time.deltaTime;
    }
}
