using UnityEngine;
using UnityEngine.InputSystem;

public class TestBehaviour : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        Mouse m = Mouse.current;
        if (m.rightButton.wasPressedThisFrame) {
            Vector3 screenPos = m.position.value;
            float x = screenPos.x;
            float y = screenPos.y;
            string s = string.Format("Screen: x: {0} , y: {1}", x, y);
            print(s);


            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            x = worldPos.x;
            y = worldPos.y;
            s = string.Format("World: x: {0} , y: {1}", x, y);
            print(s);
            
        }
    }
}
