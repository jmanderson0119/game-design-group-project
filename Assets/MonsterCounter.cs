using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCounter : MonoBehaviour
{
    int skeletons;
    int skullheads;
    int pumpkins;
    int eyes;


    void Spawn(int skeNum, int skuNum, int pumNum, int eyeNum){
        skeletons = skeNum;
        skullheads = skuNum;
        pumpkins = pumNum;
        eyes = eyeNum;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
