using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// cherry, grape, apple

public class PlayerBehaviour : MonoBehaviour {
    public float speed;
    private GameObject currentFruit;
    public GameObject[] fruits;
    
    public float offY = -0.6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // fruit position below player
        if (currentFruit != null ) {
            Vector3 playerPos = transform.position;
            Vector3 fruitOffset = new Vector3(0.0f, offY, 0.0f);
            currentFruit.transform.position = playerPos + fruitOffset;
        }
        else {
            int choice = Random.Range(0, fruits.Length);
            currentFruit = Instantiate(fruits[choice], new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
        }
        // drop fruit
        if (Keyboard.current.spaceKey.wasPressedThisFrame ) {
            Rigidbody2D body = currentFruit.GetComponent<Rigidbody2D>();
            body.gravityScale = 1.0f;
            
            Collider2D collider = currentFruit.GetComponent<Collider2D>();
            collider.enabled = true;
            
            currentFruit = null;
        }
        // keyboard movement of player
        float offset = 0.0f;
        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed) {
            offset = -speed;
        }
        if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed) {
            offset = speed;
        }
        
        Vector3 newPos = transform.position;
        newPos.x = newPos.x + offset;
        transform.position = newPos;
    }
    
    
    
    
}
