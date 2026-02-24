using System;
using UnityEngine;
public class FruitBehaviour : MonoBehaviour {
    public float timeout;
    private float timeStart;
    public GameObject[] fruits;
    public int fruitType;
    private AudioSource mergeSource;
    void Start() {
        fruits = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().fruits; 
        mergeSource = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>()[0];
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Fruit")) {
            int otherType = other.gameObject.GetComponent<FruitBehaviour>().fruitType;
            if (otherType == fruitType && fruitType < fruits.Length-1) {
                if (gameObject.transform.position.x < other.transform.position.x
                    || (gameObject.transform.position.x == other.transform.position.x 
                        && gameObject.transform.position.y >= other.transform.position.y)) {
                    // Create the merged one
                    int choice = fruitType + 1;
                    GameObject currentFruit = Instantiate(fruits[choice], 
                        Vector3.Lerp(gameObject.transform.position,other.gameObject.transform.position, 0.5f), 
                        Quaternion.identity);
                    currentFruit.GetComponent<Collider2D>().enabled = true;
                    currentFruit.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                    mergeSource.Play();
                    GameObject.FindGameObjectWithTag("Player").
                        GetComponent<PlayerBehaviour>().updateScore(fruitType);
                    // Destroy both things (fruits)
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Top")) {
            timeStart = Time.time;
        }
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.CompareTag("Top")) {
            float currentTime = Time.time;
            float timeThusFar = currentTime - timeStart;
            if (timeThusFar > timeout) {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>().GameOver();
                print("game over dude");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Top")) {
            timeStart = 0.0f;
        }
    }
}
