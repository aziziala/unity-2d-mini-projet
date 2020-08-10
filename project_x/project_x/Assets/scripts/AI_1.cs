using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AI_1 : MonoBehaviour {

    private Vector2 velocity;
    public float distance = 5;
    public float Speed = 2f;
    public Vector2 PointDeDepart;
    private float DistanceParcourue;
    private bool IsGoingRight = true;


    // Use this for initialization
    void Start()
    {
        velocity = new Vector2(Speed, 0);
        PointDeDepart = gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        DistanceParcourue = transform.position.x - PointDeDepart.x;
        if (IsGoingRight)
        {
            transform.Translate(velocity.x * Time.deltaTime, 0, 0);


            if (DistanceParcourue > distance)
            {
                transform.eulerAngles = new Vector2(0, 180);
                IsGoingRight = false;

            }
        }
        else
        {
            transform.Translate(velocity.x * Time.deltaTime, 0, 0);
            if (DistanceParcourue < 0)
            {
                transform.eulerAngles = new Vector2(0, 360);
                IsGoingRight = true;
            }
        }
    }

    public void Hurt()
    {
        Destroy(this.gameObject);
    }
}
