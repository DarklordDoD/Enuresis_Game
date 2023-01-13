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
    [SerializeField]
    private Vector2 nextPoint;
    private float pointDistance;
    private bool venter = true;
    [SerializeField]
    private float rngTimer;
    private Vector3 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        RandomWalk();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onTask)
            KlikPoint();

        if (!venter)
            WalkTo();

        if (!valgtWalk)
            RandomWalk();
    }

    private void WalkTo()
    {
        DistanceToPoint(transform.position, nextPoint);

        if (pointDistance > pointClosness)
        {
            float walkSteps = walkSpeed * Time.deltaTime;
            rb.position = Vector2.MoveTowards(transform.position, nextPoint, walkSteps);
        }
        else
        {
            valgtWalk = false;
            venter = true;
        }
    }

    private void RandomWalk()
    {
        rngTimer -= Time.deltaTime;

        if (rngTimer <= 0)
        {
            nextPoint = new Vector2(Random.Range(minPoint.x, maxPoint.x), Random.Range(minPoint.y, maxPoint.y));
            rngTimer = Random.Range(timerRange.x, timerRange.y);
            venter = false;
        }
    }

    private void KlikPoint()
    {

        if (Input.GetMouseButtonDown(0))
            getMouseKlik(false);

        
        if (Input.touchCount > 0)
            getMouseKlik(true);

    }

    private void getMouseKlik(bool isTouch)
    {
        if (!isTouch)
        {
            Vector3 mouseIndput = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mouseIndput);
        }
        else
        {

        }

        if (mousePosition.y < maxPoint.y)
        {
            nextPoint = mousePosition;
            venter = false;
            valgtWalk = true;
        }
            
    }

    private void DistanceToPoint(Vector2 p1, Vector2 p2)
    {
        pointDistance = Mathf.Sqrt(Mathf.Pow(p1[0] - p2[0], 2) + Mathf.Pow(p1[1] - p2[1], 2));
    }
}
