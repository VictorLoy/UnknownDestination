using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class map : MonoBehaviour
{

    public GameObject groundtop;
    public GameObject groundmid;
    public GameObject bridge;
    public GameObject spikes;


    public int minplateformsize = 1;
    public int maxplateformsize = 10;
    public int maxHazrdsize = 3;
    public int maxheight = 3;
    public int maxDrop = -3;

    public int platforms = 100;
    [Range(0.0f, 1f)]
    public float hazardchange = .5f;
    [Range(0.0f, 1f)]
    public float bridgechange = .1f;
    [Range(0.0f, 1f)]

    public float spikeChance = .5f;


    private int blocknum = 1;
    private int blockheight;
    private bool isHazard;

    // Use this for initialization
    void Start()
    {

        Instantiate(groundtop, new Vector2(0, 0), Quaternion.identity);

        for (int plat = 1; plat < platforms; plat++)
        {
            if (isHazard == true)
            {
                isHazard = false;
            }
            else
            {
                if (Random.value < hazardchange)
                {
                    isHazard = true;
                }
                else
                    isHazard = false;
            }


            if (isHazard == true)
            {
                int hazardsize = Mathf.RoundToInt(Random.Range(1, maxHazrdsize));
                if (Random.value < spikeChance)
                {
                    for (int tiles = 0; tiles < hazardsize; tiles++)
                    {
                        Instantiate(spikes, new Vector2(blocknum, blockheight - 2 ), Quaternion.identity);

                        for (int grdmid = 1; grdmid < 5; grdmid++)
                        {
                            Instantiate(groundmid, new Vector2(blocknum, (blockheight - grdmid)-2), Quaternion.identity);

                        }
                        blocknum++;
                    }
                }
                else
                {



                   
                    blocknum += hazardsize;
                }
            }
            else
            {

                if (Random.value < bridgechange)
                {
                    int platformsize = Mathf.RoundToInt(Random.Range(minplateformsize, maxplateformsize));
                    blockheight = blockheight + Random.Range(maxDrop, maxheight);


                    for (int tiles = 0; tiles < platformsize; tiles++)
                    {
                        if (tiles == 0 || tiles == platformsize - 1)
                        {
                            Instantiate(groundtop, new Vector2(blocknum, blockheight), Quaternion.identity);

                            for (int grdmid = 1; grdmid < 5; grdmid++)
                            {
                                Instantiate(groundmid, new Vector2(blocknum,blockheight -grdmid), Quaternion.identity);

                            }
                            blocknum++;

                        }
                        else
                        {
                            Instantiate(bridge, new Vector2(blocknum, blockheight), Quaternion.identity);
                            blocknum++;
                        }

                    }

                }
                else
                {
                    bool isEnemyPlateform = false;

                     int platformsize = Mathf.RoundToInt(Random.Range(minplateformsize, maxplateformsize));
                    blockheight = blockheight + Random.Range(maxDrop, maxheight);

                    if(platforms >= 3){
                        if(Random.value<0.3f){
                            GetComponent<EnemyPlacement>().PlaceEnemy(new Vector2(blocknum+1 , blockheight));
                            isEnemyPlateform = true;
                        }
                    }

                    for (int tiles = 0; tiles < platformsize; tiles++)
                    {
                        Instantiate(groundtop, new Vector2(blocknum, blockheight), Quaternion.identity);

                        for (int grdmid = 1; grdmid < 5; grdmid++)
                        {
                            Instantiate(groundmid, new Vector2(blocknum,blockheight -grdmid), Quaternion.identity);

                        }
                        if (tiles > 0 && tiles < platforms)
                        {


                            if (Random.value < 0.2f)
                            {
                                GetComponent<ObjectPlacement>().PlaceObject(new Vector2(blocknum, blockheight),isEnemyPlateform);
                            }
                        }

                        blocknum++;
                    }
                }
            }
        }
    }



    void Update()
    {

    }

}