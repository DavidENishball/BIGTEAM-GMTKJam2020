using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShufflePosition : MonoBehaviour
{
    public  Vector2 Maximum = new Vector2(5,3);
    public  Vector2 Minimum = new Vector2(-5,-2);

    // Start is called before the first frame update
    void Start()
    {
        // Shuffle


        // Check each.
        GridMovementComponent movement = GetComponent<GridMovementComponent>();

        List<Vector2> AllVectors = new List<Vector2>();

        for (float x = Minimum.x; x <= Maximum.x; ++x)
        {
            for (float y = Minimum.y; y <= Maximum.y; ++y)
            {
                AllVectors.Add(new Vector2(x, y));
            }
        }

        // Shuffle
        AllVectors.Shuffle();


        if (movement != null)
        {
            foreach (Vector2 possibleChoice in AllVectors)
            {
                if (!movement.IsLocationOccupied(possibleChoice))
                {
                    transform.position = new Vector3(possibleChoice.x, possibleChoice.y, 0);
                    break;
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
