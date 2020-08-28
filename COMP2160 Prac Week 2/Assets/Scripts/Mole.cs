using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CircleCollider2D))]
public class Mole : MonoBehaviour
{
    public Color colour;
    public float timeClickedDelay = 2.0f;
    public float timeRandomDelay = 5.0f;
    public float timeUpDelay = 2.0f;
    private SpriteRenderer sprite;
    private float timer;
    private enum States {down, up, missed, clicked};
    States state = States.down;
    private bool randomAssigned = false;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        colour = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        sprite.color = colour;
        timer -= Time.deltaTime;

        switch (state)
        {
            case States.down:
                colour = Color.white;
                if(randomAssigned == false)
                {
                    timer = Random.value * timeRandomDelay;
                    Debug.Log(timer);
                    randomAssigned = true;
                }
                else if(timer <= 0)
                {
                    state = States.up;
                    randomAssigned = false;
                    timer= timeUpDelay;
                }
                break;
            case States.up:
                colour = Color.yellow;
                if(timer <= 0)
                {
                    state = States.missed;
                    timer = timeClickedDelay;
                }
                break;
            case States.missed:
                colour = Color.red;
                if(timer <= 0)
                {
                    state = States.down;
                }
                break;
            case States.clicked:
                if(timer <= 0)
                {
                    state = States.down;
                }
                break;
        }
        //Debug.Log(state);
    }

    void OnMouseDown()
    {
        if(state == States.up)
        {
            colour = Color.green;
            timer = timeClickedDelay;
            state = States.clicked;
        }
    }
}
