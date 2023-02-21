using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
   //Opsætning til at bevæge spilleren
   private Vector2 _direction = Vector2.up;
   
   private Vector2 input;
   
   //Opsætning til de segmenter, som skal på for at gøre slangen større
   private List<SnakeSegment> _segments = new List<SnakeSegment>();
   private SnakeSegment head;
   public SnakeSegment segmentPrefab;
   public int initialSize = 4;
   
   //Opsætning til at kunne bestemme farten på slangen/spillet
   public float speed = 20f;
   public float speedMultiplier = 1f;
   
   private float nextUpdate;
   
   //Opsætning til at kunne bruge touch knapper i spillet
   public Button upButton;
   public Button downButton;
   public Button leftButton;
   public Button rightButton;
	
   private void Awake()
    {
        head = GetComponent<SnakeSegment>();

        if (head == null)
        {
            head = gameObject.AddComponent<SnakeSegment>();
            head.hideFlags = HideFlags.HideInInspector;
        }
    }
	
   //Det som spillet skal gøre, når det starter
   private void Start()
   {
   		//Loade vores Start State
   		StartState();
		
		//Lave vores knapper så vi kan bruge dem, her op, ned, venstre, højre
		Button btnUp = upButton.GetComponent<Button>();
		btnUp.onClick.AddListener(OnClickUp);
		
		Button btnDown = downButton.GetComponent<Button>();
		btnDown.onClick.AddListener(OnClickDown);
		
		Button btnLeft = leftButton.GetComponent<Button>();
		btnLeft.onClick.AddListener(OnClickLeft);
		
		Button btnRight = rightButton.GetComponent<Button>();
		btnRight.onClick.AddListener(OnClickRight);
   }
   
   //Fortælle hvad knapperne skal gøre når de bliver trykket på. Her at lave spilleren/slanges retning op, ned, venstre eller højre
   public void OnClickRight()
   {
   		if(head.direction.x != 0f){
			return;
		} else {
			head.SetDirection(Vector2.right, Vector2.zero);
		}
   }
   
   public void OnClickLeft()
   {
   		if(head.direction.x != 0f){
			return;
		} else {
			head.SetDirection(Vector2.left, Vector2.zero);
		}
   }
   
   public void OnClickUp()
   {
   		if(head.direction.y != 0f){
			return;
		} else {
			head.SetDirection(Vector2.up, Vector2.zero);
		}
   }
   
   public void OnClickDown()
   {
   		if(head.direction.y != 0f){
			return;
		} else {
			head.SetDirection(Vector2.down, Vector2.zero);
		}
   }
   
   //En konstant opdatering af kode
   private void Update()
   {
    	// Only allow turning up or down while moving in the x-axis
		if (head.direction.x != 0f){
        	if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
				head.SetDirection(Vector2.up, Vector2.zero);
				} else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
					head.SetDirection(Vector2.down, Vector2.zero);
        		}
    	}
	
    	// Only allow turning left or right while moving in the y-axis
    	else if (head.direction.y != 0f){
        	if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            	head.SetDirection(Vector2.right, Vector2.zero);
        	} else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            	head.SetDirection(Vector2.left, Vector2.zero);
        	}
    	}
   }
   
   //En sat opdatering af kode i takt med spillets frames
   private void FixedUpdate()
   {
   		// Wait until the next update before proceeding
    	if (Time.time < nextUpdate) {
        return;
    }
   
   		if (input != Vector2.zero) {
        _direction = input;
    }
   
   
   		 for (int i = _segments.Count - 1; i > 0; i--) {
            _segments[i].Follow(_segments[i - 1], i, _segments.Count);
        }
   
   		// Move the snake in the direction it is facing
    	// Round the values to ensure it aligns to the grid
		float x = Mathf.Round(head.transform.position.x) + head.direction.x;
    	float y = Mathf.Round(head.transform.position.y) + head.direction.y;

    	head.transform.position = new Vector2(x, y);
    	nextUpdate = Time.time + (1f / (speed * speedMultiplier));
   }
   
   //Opsætning til at vokse spilleren
   private void Grow()
   {
   		SnakeSegment _segment = Instantiate(segmentPrefab);
        _segment.Follow(_segments[_segments.Count - 1], _segments.Count, _segments.Count + 1);
        _segments.Add(_segment);
   }
   
   //Kode til at angive, hvordan spilleren start, blandt andet hvor lang de er
   private void StartState()
   {
   		 // Set the initial direction of the snake, starting at the origin
        // (center of the grid)
        head.SetDirection(Vector2.right, Vector2.zero);
        head.transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < _segments.Count; i++) {
            Destroy(_segments[i].gameObject);
        }

        // Clear the list then add the head as the first segment
        _segments.Clear();
        _segments.Add(head);

        // -1 since the head is already in the list
        for (int i = 0; i < initialSize - 1; i++) {
            Grow();
   		}
   }
   
   //Kode til at loade slut skærmmen når spilleren rammer væggene/sig selv
   private void FailState()
   {
   		for(int i = 1; i < _segments.Count; i++){
			Destroy(_segments[i].gameObject);
		}
		
		_segments.Clear();

        SceneManager.LoadScene("SnakeEnd");
   }
   
   //Kode til at køre tidligere opsat kode, når spilleren enden samler mad op, eller rammer væggene/sig selv
   private void OnTriggerEnter2D(Collider2D other)
   {
   		if(other.tag == "Food"){
			Grow();
		} else if(other.tag == "Obstacle"){
			FailState();
		}
   }
   
   //Opsætning til hvad tæller som en væg eller spillerens krop
   public bool Occupies(float x, float y)
   {
    	foreach (SnakeSegment segment in _segments)
    	{
        	if (segment.segmentPosition.position.x == x && segment.segmentPosition.position.y == y) {
            	return true;
        	}
    	}

    	return false;
   }
}

