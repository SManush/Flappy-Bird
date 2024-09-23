using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 1;
    private float leftBound = -4;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * (speed * 2));
       
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);

        }
        else if (transform.position.x < leftBound && gameObject.CompareTag("Score"))
        {
            Destroy(gameObject);
        }
    }
}
