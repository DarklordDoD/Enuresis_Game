using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muvePet : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    float pointClosness;
    [SerializeField]
    float petZone;

    [SerializeField]
    private Vector2 minPoint;
    [SerializeField]
    private Vector2 maxPoint;
    [SerializeField]
    private Vector2 timerRange;

    [SerializeField]
    private bool valgtWalk;
    [SerializeField]
    private bool onTask;

    private Rigidbody2D rb;
    private Vector2 nextPoint;
    private float pointDistance;
    private bool venter = true;
    private float rngTimer;
    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        RandomWalk();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame 
    // jeg bruger update til at sende beskeder til andre fungtioner
    void Update()
    {
        if (!onTask)
            KlikPoint(); //rigistrer om spilleren klikker p� sk�rmen

        if (!venter)
            WalkTo(); //f�r pettet til at g� imod et point

        if (!valgtWalk)
            RandomWalk(); //setter et tefeldigt point som pette vil g� til
    }

    private void WalkTo()
    {
        DistanceToPoint(transform.position, nextPoint); //rigistrer hvor t�t pettet er p� sin point

        if (pointDistance > pointClosness)
        {
            //flytter pette imod destinationen 
            float walkSteps = walkSpeed * Time.deltaTime;
            rb.position = Vector2.MoveTowards(transform.position, nextPoint, walkSteps);
        }
        else
        {
            //stopper med at flytte pettet og g�re klar til nyt indput
            valgtWalk = false;
            venter = true;
        }
    }

    private void RandomWalk()
    {
        rngTimer -= Time.deltaTime; //t�ller ned til n�ste tef�ldige indpit

        if (rngTimer <= 0)
        {
            //finder tef�ldig point p� sk�rmen og tef�ldig tid til n�ste tef�ldige indput
            nextPoint = new Vector2(Random.Range(minPoint.x, maxPoint.x), Random.Range(minPoint.y, maxPoint.y));
            rngTimer = Random.Range(timerRange.x, timerRange.y);
            venter = false;
        }
    }

    private void KlikPoint()
    {
        //ragistrer musseklik
        if (Input.GetMouseButtonDown(0)) 
            getMouseKlik(false);

        //registrer touch fra tablet
        if (Input.touchCount > 0)
            getMouseKlik(true);

    }

    private void getMouseKlik(bool isTouch)
    {
        if (!isTouch)
        {
            //finder lokalitionen af musen
            Vector3 mouseIndput = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mouseIndput);
        }
        else
        {
            //finder lokalitionen af touch p� tablet
        }

        //s�tter et point p� registreret klik/touch
        if (mousePosition.y < maxPoint.y)
        {
            DistanceToPoint(transform.position, mousePosition);
            if (pointDistance > petZone)
            {
                nextPoint = mousePosition;
                venter = false;
                valgtWalk = true;
            }
            else
            {
                Debug.Log("pet");
            }
        }
            
    }

    private void DistanceToPoint(Vector2 p1, Vector2 p2)
    {
        //berigner hvor t�t pettet er p� n�ste point
        pointDistance = Mathf.Sqrt(Mathf.Pow(p1[0] - p2[0], 2) + Mathf.Pow(p1[1] - p2[1], 2));
    }
}
