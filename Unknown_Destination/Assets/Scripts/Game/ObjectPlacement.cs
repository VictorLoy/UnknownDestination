using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour {

    public GameObject box, signRight, rock, cactus;

    public void PlaceObject(Vector2 cords, bool isEnemy){


        float rnd = Random.value;

        if (rnd < 0.2f)
        {
            if (isEnemy == false)
            {
                Instantiate(box, new Vector3(cords.x, cords.y + 1, 1), Quaternion.identity);
            }
            else
            {
                Instantiate(rock, new Vector3(cords.x, cords.y + 1, 1), Quaternion.identity);
            }
        }
        if (rnd >= 0.2f && rnd <0.3f)
        {
            Instantiate(signRight, new Vector3(cords.x, cords.y + 1, 1), Quaternion.identity);
        }

        if (rnd >= 0.3f && rnd < 0.7f)
        {
            Instantiate(rock, new Vector3(cords.x, cords.y + 1, 1), Quaternion.identity);
        }

        if (rnd >= 0.7f)
        {
            Instantiate(cactus, new Vector3(cords.x, cords.y + 1.45f, 1), Quaternion.identity);
        } 
    }
}
