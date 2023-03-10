using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FrindSamtale : MonoBehaviour
{

    [SerializeField]
    private List<TextMeshProUGUI> taleBoxe; 
    [SerializeField]
    private List<SamtaleLinjer> samtaler;
    [SerializeField]
    private float delayMellemTale = 0.5f;
    [SerializeField]
    private Button theButten;
    [SerializeField]
    private GameObject textMenu;

    [SerializeField]
    private int tisEventType;
    private List<string> samtaleList;

    [Header("ObjektDesplay")]
    [SerializeField]
    private List<Vector2> objektPositions = new List<Vector2> {new Vector2(), new Vector2(), new Vector2(), new Vector2(), new Vector2() };
    [SerializeField]
    private float petSice = 1;
    [SerializeField]
    private GameObject petBed;
    [SerializeField]
    private GameObject frindBed;

    private GameObject petSkin;
    private GameObject frindSkin;
    private bool aktivFrindStain;

    public void StartSamtale()
    {
        SetKlik(false);

        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");

        petSkin = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
        frindSkin = GameObject.FindGameObjectWithTag("PetFrind").transform.GetChild(0).gameObject;

        GetComponent<SceneSkift>().SkiftTil();

        Instantiate(petSkin, transform.GetChild(0));
        Instantiate(frindSkin, transform.GetChild(0));

        GameObject[] showPet = GameObject.FindGameObjectsWithTag("ShowPet");

        for (int i = 0; i < showPet.Length; i++)
        {
            showPet[i].transform.localScale = new Vector2(petSice, petSice);

            showPet[i].transform.position = objektPositions[i];

            foreach (SpriteRenderer petLims in showPet[i].GetComponentsInChildren<SpriteRenderer>())
                petLims.sortingLayerName = "Front";
        }

        if (gameController.GetComponent<Ressourcer>().aktivStain)
            tisEventType++;

        if (aktivFrindStain)
            tisEventType += 2;

        samtaleList = samtaler[gameController.GetComponent<Instilinger>().sprog].lines;

        RunSamtale(false);
    }

    public void RunSamtale(bool buttenPress)
    {
        switch (tisEventType)
        {
            case 1: //pet har tisset i sengen
                if (buttenPress)
                {
                    taleBoxe[0].text = samtaleList[3];
                    Invoke("DelayAwnser", delayMellemTale);
                    break;
                }

                frindBed.transform.position = objektPositions[2];
                Instantiate(petBed, transform.GetChild(0));

                taleBoxe[2].text = samtaleList[5];
                break;

            case 2: //frind har tisset i sengen
                if (buttenPress)
                {
                    taleBoxe[0].text = samtaleList[6];
                    Invoke("LukButtenAktiv", delayMellemTale);
                    break;
                }

                frindBed.transform.position = objektPositions[2];
                Instantiate(frindBed, transform.GetChild(0));

                taleBoxe[1].text = samtaleList[7];
                Invoke("DelayAwnser", delayMellemTale);
                break;

            case 3: //frind og pet har tisset
                if (buttenPress)
                {
                    taleBoxe[0].text = samtaleList[9];
                    Invoke("LukButtenAktiv", delayMellemTale);
                    break;
                }

                petBed.transform.position = objektPositions[3];
                Instantiate(petBed, transform.GetChild(0));

                frindBed.transform.position = objektPositions[4];
                Instantiate(frindBed, transform.GetChild(0));

                taleBoxe[1].text = samtaleList[10];
                Invoke("DelayAwnser", delayMellemTale);
                break;

            default: //ingen tisser i sengen
                if (buttenPress)
                {
                    taleBoxe[0].text = samtaleList[0];
                    Invoke("DelayAwnser", delayMellemTale);
                    break;
                }

                taleBoxe[2].text = samtaleList[2];
                print(samtaleList[2]);
                break;
        }
    }

    private void DelayAwnser()
    {
        switch (tisEventType)
        {
            case 1:
                taleBoxe[1].text = samtaleList[4];
                Invoke("LukButtenAktiv", delayMellemTale);
                break;

            case 2:
                taleBoxe[2].text = samtaleList[8];
                break;

            case 3:
                taleBoxe[2].text = samtaleList[11];
                break;

            default:
                taleBoxe[1].text = samtaleList[1];
                Invoke("LukButtenAktiv", delayMellemTale);
                break;
        }
    }

    private void LukButtenAktiv()
    {
        theButten.GetComponentInChildren<TextMeshProUGUI>().text = samtaleList[12];
        theButten.GetComponent<TaleButten>().lukAktiv = true;
    }

    public void SetKlik(bool setTil)
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("SceneSkift"))
            go.GetComponent<Collider2D>().enabled = setTil;
    }
}
