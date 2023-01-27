using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Snake : MonoBehaviour
{
   private Vector2 _direction = Vector2.up;
   
   private Vector2 input;
   
   private List<Transform> _segments = new List<Transform>();
   public Transform segmentPrefab;
   
   public int initialSize = 3;
   
   public float speed = 20f;
   public float speedMultiplier = 1f;
   
   private float nextUpdate;
   
   public Button upButton;
   public Button downButton;
   public Button leftButton;
   public Button rightButton;
	
   private void Start()
   {
   		ResetState();
		
		Button btnUp = upButton.GetComponent<Button>();
		btnUp.onClick.AddListener(OnClickUp);
		
		Button btnDown = downButton.GetComponent<Button>();
		btnDown.onClick.AddListener(OnClickDown);
		
		Button btnLeft = leftButton.GetComponent<Button>();
		btnLeft.onClick.AddListener(OnClickLeft);
		
		Button btnRight = rightButton.GetComponent<Button>();
		btnRight.onClick.AddListener(OnClickRight);
   }
   
   public void OnClickRight()
   {
   		input = Vector2.right;
   }
   
   public void OnClickLeft()
   {
   		input = Vector2.left;
   }
   
   public void OnClickUp()
   {
   		input = Vector2.up;
   }
   
   public void OnClickDown()
   {
   		input = Vector2.down;
   }
   
   
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
   
   private void Grow()
   {
   		Transform segment = Instantiate(this.segmentPrefab);
		segment.position = _segments[_segments.Count -1].position;
		
		_segments.Add(segment);
   }
   
   private void ResetState()
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
   
   private void OnTriggerEnter2D(Collider2D other)
   {
   		if(other.tag == "Food"){
			Grow();
		} else if(other.tag == "Obstacle"){
			ResetState();
		}
   }
   
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
