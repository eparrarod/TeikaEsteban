using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;
using TMPro;

// cherry, grape, apple

public class PlayerBehaviour : MonoBehaviour {
    public float speed;
    private GameObject currentFruit;
    public GameObject[] fruits;
    public float min;
    public float max;
    float startTime = 0.0f;
    public int move;
    public GameObject gameOverPanel;
    
    public int[] points;
    public int total;
    public TMP_Text textField;
    
    //private Key kk;
    
    public float offY = -0.6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        startTime = 0.0f;
        move = 0; // 0 means you can move both ways
        total = 0;
    }

    // Update is called once per frame
    void Update() {
        // Fruit position below player
        Vector3 fruitOffset = new Vector3(0.0f, offY, 0.0f);
        Vector3 playerPos = transform.position;
        Vector3 vectorP = playerPos + fruitOffset;
        if (currentFruit != null ) {
            currentFruit.transform.position = vectorP;
        } else {
            int choice = GameObject.FindGameObjectWithTag("Queue").GetComponent<QueueManager>().updateQueue();

            currentFruit = Instantiate(fruits[choice], vectorP, Quaternion.identity);

        }
        // Drop fruit
        if (Keyboard.current.spaceKey.wasPressedThisFrame ) {
            Rigidbody2D body = currentFruit.GetComponent<Rigidbody2D>();
            body.gravityScale = 1.0f;
            Collider2D collider = currentFruit.GetComponent<Collider2D>();
            collider.enabled = true;
            currentFruit = null;
        }
        // keyboard movement of player
        // Move left
        float offset = 0.0f;
        bool left = (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) && move != 1;
        if (left == true) {
            offset = -speed;
        }

        // Move Right
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) {
            offset = speed;
        }
        
        // Compute new position
        Vector3 newPos = transform.position;
        newPos.x = newPos.x + offset;
        
        // Prevent movement too far right
        if (newPos.x > max) {
            newPos.x = max; 
        }
        // Prevent movement too far left
        if (newPos.x < min) {
            newPos.x = min; 
        }
        
        transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        print("you touched " + other.gameObject.name);
        if (other.gameObject.CompareTag("LB")) {
            move = 1; // Cannot move left
        }
    }
    
    private void OnCollisionStay2D(Collision2D other) {
        print("you are touching " + other.gameObject.name);
        if (true) {
            
        }
    }
    
    private void OnCollisionExit2D(Collision2D other) {
        print("you stopped: " + other.gameObject.name);
        if (other.gameObject.CompareTag("LB")) {
            move = 0; // Cannot move left
        }
    }

    public void GameOver() {
        gameOverPanel.SetActive(true);
    }


    public void updateScore(int index) {
        total = total + points[index];
        textField.SetText("Score: " + total);
    }
}
