using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector2 myMaxPos = new Vector2(20, 1);
    public Vector2 myStartPos = new Vector2(-20, 1);
    public float speed = 0.1f;
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed);
        if (transform.position == (Vector3)myMaxPos) transform.position = myStartPos;
    }
}
