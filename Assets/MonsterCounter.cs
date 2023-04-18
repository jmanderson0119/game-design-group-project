using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCounter : MonoBehaviour
{
    int totalMonsters = 0;
    int skeletons;
    GameObject skelSpawner;
    int skullheads;
    GameObject skulSpawner;
    int pumpkins;
    GameObject pumpSpawner;
    int eyes;
    GameObject eyeSpawner;
    int goldreward;
    int repreward;
    public DisplayReward rewardtext;

    private void Start() {
        skelSpawner = this.gameObject.transform.GetChild(0).gameObject;
        skulSpawner = this.gameObject.transform.GetChild(1).gameObject;
        pumpSpawner = this.gameObject.transform.GetChild(2).gameObject;
        eyeSpawner = this.gameObject.transform.GetChild(3).gameObject;
        startStage(1,1,1,1,1000,1000);
    }


    public void startStage(int skeNum, int skuNum, int pumNum, int eyeNum, int goldr, int repr){
        Spawn(skeNum, skuNum, pumNum, eyeNum);
        goldreward = goldr;
        repreward = repr;
    }

    private void Spawn(int skeNum, int skuNum, int pumNum, int eyeNum){
        skeletons = skeNum;
        skullheads = skuNum;
        pumpkins = pumNum;
        eyes = eyeNum;
        totalMonsters = totalMonsters + skeletons + skullheads + pumpkins + eyes;
        skelSpawner.GetComponent<MonsterSpawnerV1>().Spawn(skeletons);
        skulSpawner.GetComponent<MonsterSpawnerV1>().Spawn(skullheads);
        pumpSpawner.GetComponent<MonsterSpawnerV1>().Spawn(pumpkins);
        eyeSpawner.GetComponent<MonsterSpawnerV1>().Spawn(eyes);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
            CompleteStage();
        }
    }
    public void CompleteStage(){
        GameObject oldPlayer = GameObject.FindGameObjectsWithTag("Player")[0];
        oldPlayer.GetComponent<PlayerStats>().IncGold(goldreward);
        oldPlayer.GetComponent<PlayerStats>().IncReputation(repreward);
        rewardtext.startExit(goldreward,repreward);
    }

    public void EarlyComplete(){
        float percent = GameObject.FindGameObjectsWithTag("Enemy").Length/totalMonsters;
        GameObject oldPlayer = GameObject.FindGameObjectsWithTag("Player")[0];
        oldPlayer.GetComponent<PlayerStats>().IncGold((int)(goldreward*percent));
        oldPlayer.GetComponent<PlayerStats>().IncReputation(-(int)(repreward*(1-percent)));
        cripple();
        rewardtext.startExit((int)(goldreward*percent),-(int)(repreward*(1-percent)));
    }

    private void cripple(){
        GameObject oldPlayer = GameObject.FindGameObjectsWithTag("Player")[0];
        Destroy(oldPlayer.GetComponent<PlayerMovement>());
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject mon in monsters){
            Destroy(mon.GetComponent<EnemyBehavior>());
        }
    }
}
