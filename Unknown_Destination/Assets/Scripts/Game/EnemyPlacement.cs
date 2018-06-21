using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlacement : MonoBehaviour {
    public GameObject saw, slime;

    [Range(0.0f,1)]
    public float enemyRatio = 0.5f;

    public void PlaceEnemy(Vector2 cords){
        float rnd = Random.value;

        if(rnd < enemyRatio){
            Instantiate(saw,new Vector3(cords.x,cords.y+1,-1),Quaternion.identity);
        }else{
            Instantiate(slime, new Vector3(cords.x, cords.y + 1, -1), Quaternion.identity);
        }
    }
}
