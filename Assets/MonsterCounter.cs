using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCounter : MonoBehaviour
{
    static int totalMonsters = 0;
    static int monsterLevel;
    static int skeletons;
    GameObject skelSpawner;
    static int skullheads;
    GameObject skulSpawner;
    static int pumpkins;
    GameObject pumpSpawner;
    static int eyes;
    GameObject eyeSpawner;
    static int goldreward;
    static int repreward;
    public DisplayReward rewardtext;
    private bool incremented = false;

    private void Start() {
        skelSpawner = this.gameObject.transform.GetChild(0).gameObject;
        skulSpawner = this.gameObject.transform.GetChild(1).gameObject;
        pumpSpawner = this.gameObject.transform.GetChild(2).gameObject;
        eyeSpawner = this.gameObject.transform.GetChild(3).gameObject;
        Spawn(skeletons, skullheads, pumpkins, eyes);
    }


    public static void startStage(int skeNum, int skuNum, int pumNum, int eyeNum, int goldr, int repr){
        skeletons = skeNum;
        skullheads = skuNum;
        pumpkins = pumNum;
        eyes = eyeNum;
        totalMonsters = 1 + skeletons + skullheads + pumpkins + eyes;
        goldreward = goldr;
        repreward = repr;
    }

    private void Spawn(int skeNum, int skuNum, int pumNum, int eyeNum){
        
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
        if (!incremented)
        {
            GameObject oldPlayer = GameObject.FindGameObjectsWithTag("Player")[0];
            oldPlayer.GetComponent<PlayerStats>().IncGold(goldreward);
            oldPlayer.GetComponent<PlayerStats>().IncReputation(repreward);
            oldPlayer.GetComponent<PlayerStats>().completeBounty();
            Destroy(GameObject.Find("HealthCanvas"));
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Minions");
                foreach(GameObject mon in monsters){
            Destroy(mon.GetComponent<EnemyBehavior>());
        }
            incremented = true;
        }
        rewardtext.startExit(goldreward,repreward);
    }

    public void EarlyComplete(){
        float percentLeft = (float)GameObject.FindGameObjectsWithTag("Enemy").Length/(float)totalMonsters;
        GameObject oldPlayer = GameObject.FindGameObjectsWithTag("Player")[0];
        oldPlayer.GetComponent<PlayerStats>().IncGold((int)(goldreward*(1-percentLeft)));
        Debug.Log(percentLeft);
        oldPlayer.GetComponent<PlayerStats>().IncReputation(-(int)(repreward*(1-percentLeft)));
        oldPlayer.GetComponent<PlayerStats>().healToFull();
        cripple();
        Destroy(GameObject.Find("HealthCanvas"));
        
        rewardtext.startExit((int)(goldreward*(1-percentLeft)),-(int)(repreward*(1-percentLeft)));
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
