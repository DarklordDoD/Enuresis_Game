using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Snake : MonoBehaviour
{
   //Opsætning til at bevæge spilleren
   private Vector2 _direction = Vector2.up;
   
   private Vector2 input;
   
   //Opsætning til de segmenter, som skal på for at gøre slangen større
   private List<Transform> _segments = new List<Transform>();
   public Transform segmentPrefab;
   
   public int initialSize = 3;
   
   //Opsætning til at kunne bestemme farten på slangen/spillet
   public float speed = 20f;
   public float speedMultiplier = 1f;
   
   private float nextUpdate;
   
   //Opsætning til at kunne bruge touch knapper i spillet
   public Button upButton;
   public Button downButton;
   public Button leftButton;
   public Button rightButton;
	
	
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
   		if(_direction.x != 0f){
			return;
		} else {
			input = Vector2.right;
		}
   }
   
   public void OnClickLeft()
   {
   		if(_direction.x != 0f){
			return;
		} else {
			input = Vector2.left;
		}
   }
   
   public void OnClickUp()
   {
   		if(_direction.y != 0f){
			return;
		} else {
			input = Vector2.up;
		}
   }
   
   public void OnClickDown()
   {
   		if(_direction.y != 0f){
			return;
		} else {
			input = Vector2.down;
		}
   }
   
   //En konstant opdatering af kode
   private void Update()
   {
    	// Only allow turning up or down while moving in the x-axis
		if (_direction.x != 0f){
        	if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
				input = Vector2.up;
				} else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
					input = Vector2.down;
        		}
    	}
	
    	// Only allow turning left or right while moving in the y-axis
    	else if (_direction.y != 0f){
        	if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            	input = Vector2.right;
        	} else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            	input = Vector2.left;
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
   
   
   		for(int i = _segments.Count -1; i > 0; i--)
		{
			_segments[i].position = _segments[i - 1].position;
		}
   
   		// Move the snake in the direction it is facing
    	// Round the values to ensure it aligns to the grid
		float x = Mathf.Round(transform.position.x) + _direction.x;
    	float y = Mathf.Round(transform.position.y) + _direction.y;

    	transform.position = new Vector2(x, y);
    	nextUpdate = Time.time + (1f / (speed * speedMultiplier));
   }
   
   //Opsætning til at vokse spilleren
   private void Grow()
   {
   		Transform segment = Instantiate(this.segmentPrefab);
		segment.position = _segments[_segments.Count -1].position;
		
		_segments.Add(segment);
   }
   
   //Kode til at angive, hvordan spilleren start, blandt andet hvor lang de er
   private void StartState()
   {
   		for(int i = 1; i < _segments.Count; i++){
			Destroy(_segments[i].gameObject);
		}
		
		_segments.Clear();
		_segments.Add(this.transform);
		
		for(int i = 1; i < this.initialSize; i++){
			_segments.Add(Instantiate(this.segmentPrefab));
		}
		
		this.transform.position = Vector3.zero;
   }
   
   //Kode til at loade slut skærmmen når spilleren rammer væggene/sig selv
   public int sceneNum;
   
   private void FailState()
   {
   		for(int i = 1; i < _segments.Count; i++){
			Destroy(_segments[i].gameObject);
		}
		
		_segments.Clear();
   
   
   		SceneManager.LoadScene(sceneNum);
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
    	foreach (Transform segment in _segments)
    	{
        	if (segment.position.x == x && segment.position.y == y) {
            	return true;
        	}
    	}

    	return false;
   }
}
