using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class muvePet : MonoBehaviour
{
    [Header("Pet Muvment")]
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
    private Vector2 siceControle;

    private bool valgtWalk;
    private bool onTask;

    private Rigidbody2D rb;
    private Vector2 nextPoint;
    private float pointDistance;
    private bool venter = true;
    private float rngTimer;
    private Vector3 mousePosition;

    public static GameObject instance;

    [Header("Interakt med Pet")]
    [SerializeField]
    private float timeMellemPets;
    [SerializeField]
    private float petEffekt;

    private float petTimer;

    [Header("Animation")]
    [SerializeField]
    private Animator animator;
    

    // Start is called before the first frame update
    void Awake()
    {
        //dette objekt bliver ikke fjernet når en ny scenemaneger
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this.gameObject;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        RandomWalk();
        rb = GetComponent<Rigidbody2D>();

        Invoke("lateStart", 0.5f);
    }

    private void lateStart()
    {
        //get animator
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame 
    // jeg bruger update til at sende beskeder til andre fungtioner
    void Update()
    {
        if (!onTask)
            KlikPoint(); //rigistrer om spilleren klikker på skærmen

        if (!venter)
            WalkTo(); //får pettet til at gå imod et point

        if (!valgtWalk)
            RandomWalk(); //setter et tefeldigt point som pette vil gå til

        if (petTimer <= timeMellemPets)
            petTimer += Time.deltaTime;
    }

    private void WalkTo()
    {
        DistanceToPoint(transform.position, nextPoint); //rigistrer hvor tæt pettet er på sin point

        if (pointDistance > pointClosness)
        {
            //start walking (Animation)
            animator.SetBool("Run-element", true);

            //flytter pette imod destinationen 
            float walkSteps = walkSpeed * Time.deltaTime;
            rb.position = Vector2.MoveTowards(transform.position, nextPoint, walkSteps);

            //scalere pettet efter y position
            float location = gameObject.GetComponent<Transform>().position.y / siceControle.x;
            float locationS = siceControle.y - location;
            gameObject.GetComponent<Transform>().localScale = new Vector2(locationS, locationS);
        }
        else
        {
            //stopper med at flytte pettet og gøre klar til nyt indput
            valgtWalk = false;
            venter = true;

            //stop walking (Animation)
            animator.SetBool("Run-element", false);
        }
    }

    private void RandomWalk()
    {
        rngTimer -= Time.deltaTime; //tæller ned til næste tefældige indpit

        if (rngTimer <= 0)
        {
            //finder tefældig point på skærmen og tefældig tid til næste tefældige indput
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
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            //finder lokalitionen af touch på tablet
            mousePosition = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
        }

        //sørger for at pettet ikke kravler op af vægene
        if (mousePosition.y > maxPoint.y)
            mousePosition.y = maxPoint.y;

        if (mousePosition.y < minPoint.y)
            mousePosition.y = minPoint.y;

        if (mousePosition.x > maxPoint.x)
            mousePosition.x = maxPoint.x;

        if (mousePosition.x < minPoint.x)
            mousePosition.x = minPoint.x;

        //sætter et point på registreret klik/touch
        DistanceToPoint(transform.position, mousePosition);
        if (pointDistance > petZone) //sørger for at klikket ikke er for tet på pet
        {
            nextPoint = mousePosition;
            venter = false;
            valgtWalk = true;
        }
    }

    private void DistanceToPoint(Vector2 p1, Vector2 p2)
    {
        //berigner hvor tæt pettet er på næste point
        pointDistance = Mathf.Sqrt(Mathf.Pow(p1[0] - p2[0], 2) + Mathf.Pow(p1[1] - p2[1], 2));
    }

    public void PetPet()
    {
        if (petTimer >= timeMellemPets)
        {
            //Start vink (Animation)
            animator.SetBool("Wave-element", true);
            Invoke("SropVink", 0.3f);

            petTimer = 0;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Ressourcer>().AddGlad(petEffekt);         
        }
            
    }

    private void SropVink()
    {
        //stop vink (Animation)
        animator.SetBool("Wave-element", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        nextPoint = transform.position;
    }
}
