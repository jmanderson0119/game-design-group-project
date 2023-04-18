using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RandomDifficultyGenerator : MonoBehaviour
{
    public GameObject EnemySpawnwers;
    public MonsterCounter MonsterCounter;
    
    [SerializeField] private TMP_Text request;
    public void Start() {
        MonsterCounter = EnemySpawnwers.GetComponent<MonsterCounter>();
    }

    public void EasyDifficulty() {
        int r = Random.Range(0, 19);
        Debug.Log("Generating Encounter : " + r);
        if (r == 0) {
            request.text = "Defeat 3 Skeletons";
            MonsterCounter.startStage(3, 0, 0, 0, 150, 150);
        }
        else if (r == 1) {
            request.text = "Defeat 2 Skeletons and 1 Skeleton Head";
            MonsterCounter.startStage(2, 1, 0, 0, 200, 200);
        }
        else if (r == 2) {
            request.text = "Defeat 2 Skeletons and 1 Pumpkin";
            MonsterCounter.startStage(2, 0, 1, 0, 300, 300);
        }
        else if (r == 3) {
            request.text = "Defeat 2 Skeletons and 1 Mimic";
            MonsterCounter.startStage(2, 0, 0, 1, 250, 250);
        }
        else if (r == 3){
            request.text = "Defeat 1 Skeleton, 1 Skeleton Head, and 1 Pumpkin";
            MonsterCounter.startStage(1, 1, 1, 0, 350, 350);
        }
        else if (r == 4){
            request.text = "Defeat 1 Skeleton, 1 Skeleton Head, and 1 Mimic";
            MonsterCounter.startStage(1, 1, 0, 1, 300, 300);
        }
        else if (r == 5){
            request.text = "Defeat 1 Skeleton, 1 Pumpkin, and 1 Mimic";
            MonsterCounter.startStage(1, 0, 1, 1, 400, 400);
        }
        else if (r == 6){
            request.text = "Defeat 3 Skeleton Heads";
            MonsterCounter.startStage(0, 3, 0, 0, 300, 30);
        }
        else if (r == 7){
            request.text = "Defeat 2 Skeleton Heads and 1 Skeleton";
            MonsterCounter.startStage(1, 2, 0, 0, 250, 250);
        }
        else if (r == 8){
            request.text = "Defeat 2 Skeleton Heads and 1 Pumpkin";
            MonsterCounter.startStage(0, 2, 1, 0, 400, 400);
        }
        else if (r == 9){
            request.text = "Defeat 2 Skeleton Heads and 1 Mimic";
            MonsterCounter.startStage(0, 2, 0, 1, 350, 350);
        }
        else if (r == 10){
            request.text = "Defeat 1 Skeleton Head, 1 Pumpkin, and 1 Mimic";
            MonsterCounter.startStage(0, 1, 1, 1, 450, 450);
        }
        else if (r == 11){
            request.text = "Defeat 3 Pumpkins";
            MonsterCounter.startStage(0, 0, 3, 0, 600, 600);
        }
        else if (r == 12){
            request.text = "Defeat 2 Pumpkins and 1 Skeleton";
            MonsterCounter.startStage(1, 0, 2, 0, 450, 450);
        }
        else if (r == 13){
            request.text = "Defeat 2 Pumpkins and 1 Skeleton Head";
            MonsterCounter.startStage(0, 1, 2, 0, 500, 500);
        }
        else if (r == 14){
            request.text = "Defeat 2 Pumpkins and 1 Mimic";
            MonsterCounter.startStage(0, 0, 2, 1, 550, 550);
        }
        else if (r == 15){
            request.text = "Defeat 3 Mimics";
            MonsterCounter.startStage(0, 0, 0, 3, 450, 450);
        }
        else if (r == 16){
            request.text = "Defeat 2 Mimics and 1 Skeleton";
            MonsterCounter.startStage(1, 0, 0, 2, 350, 350);
        }
        else if (r == 17){
            request.text = "Defeat 2 Mimics and 1 Skeleton Head";
            MonsterCounter.startStage(0, 1, 0, 2, 400, 400);
        }
        else if (r == 18){
            request.text = "Defeat 2 Mimics and 1 Pumpkin";
            MonsterCounter.startStage(0, 0, 1, 2, 500, 500);
        }
        SceneManager.LoadScene(3);

    }

    public void MediumDifficulty()
    {
        int r = Random.Range(0, 21);
        Debug.Log("Generating Encounter : " + r);
        if (r == 0)
        {
            request.text = "Defeat 5 Skeletons";
            MonsterCounter.startStage(5, 0, 0, 0, 250, 250);
        }
        else if (r == 1){
            request.text = "Defeat 4 Skeletons and 1 Skeleton Head";
            MonsterCounter.startStage(4, 1, 0, 0, 300, 300);
        }
        else if (r == 2){
            request.text = "Defeat 4 Skeletons and 1 Pumpkin";
            MonsterCounter.startStage(4, 0, 1, 0, 400, 400);
        }
        else if (r == 3){
            request.text = "Defeat 4 Skeletons and 1 Mimic";
            MonsterCounter.startStage(4, 0, 0, 1, 350, 350);
        }
        else if (r == 3){
            request.text = "Defeat 3 Skeletons, 1 Skeleton Head, and 1 Pumpkin";
            MonsterCounter.startStage(3, 1, 1, 0, 450, 450);
        }
        else if (r == 4){
            request.text = "Defeat 3 Skeletons, 1 Skeleton Head, and 1 Mimic";
            MonsterCounter.startStage(3, 1, 0, 1, 400, 400);
        }
        else if (r == 5){
            request.text = "Defeat 3 Skeletons, 1 Pumpkin, and 1 Mimic";
            MonsterCounter.startStage(3, 0, 1, 1, 500, 500);
        }
        else if (r == 6){
            request.text = "Defeat 2 Skeletons, 1 Skeleton Head, 1 Pumpkin, and 1 Mimic";
            MonsterCounter.startStage(2, 1, 1, 1, 550, 550);
        }
        else if (r == 7){
            request.text = "5 Skeleton Heads";
            MonsterCounter.startStage(0, 5, 0, 0, 500, 500);
        }
        else if (r == 8){
            request.text = "Defeat 4 Skeleton Heads and 1 Pumpkin";
            MonsterCounter.startStage(0, 4, 1, 0, 600, 600);
        }
        else if (r == 9){
            request.text = "Defeat 4 Skeleton Heads and 1 Mimic";
            MonsterCounter.startStage(0, 4, 0, 1, 550, 550);
        }
        else if (r == 10){
            request.text = "Defeat 3 Skeleton Heads, 1 Pumpkin, and 1 Mimic";
            MonsterCounter.startStage(0, 3, 1, 1, 650, 650);
        }
        else if (r == 11){
            request.text = "Defeat 2 Skeleton Heads, 1 Skeleton, 1 Pumpkin, and 1 Mimic";
            MonsterCounter.startStage(1, 2, 1, 1, 600, 600);
        }
        else if (r == 12){
            request.text = "Defeat 5 Pumpkins";
            MonsterCounter.startStage(0, 0, 5, 0, 1000, 1000);
        }
        else if (r == 13){
            request.text = "Defeat 4 Pumpkins and 1 Skeleton Head";
            MonsterCounter.startStage(0, 1, 4, 0, 900, 900);
        }
        else if (r == 14){
            request.text = "Defeat 4 Pumpkins and 1 Mimic";
            MonsterCounter.startStage(0, 0, 4, 1, 950, 950);
        }
        else if (r == 15){
            request.text = "Defeat 3 Pumpkins, 1 Skeleton Head, and 1 Mimic";
            MonsterCounter.startStage(0, 1, 3, 1, 850, 850);
        }
        else if (r == 16){
            request.text = "Defeat 3 Pumpkins, 1 Skeleton, and 1 Skeleton Head";
            MonsterCounter.startStage(1, 1, 3, 0, 750, 750);
        }
        else if (r == 17){
            request.text = "Defeat 2 Pumpkins, 1 Skeleton, 1 Skeleton Head, and 1 Mimic";
            MonsterCounter.startStage(1, 1, 2, 1, 700, 700);
        }
        else if (r == 18){
            request.text = "Defeat 2 Skeletons, and 3 Mimics";
            MonsterCounter.startStage(2, 0, 0, 3, 550, 550);
        }
        else if (r == 19){
            request.text = "Defeat 3 Pumpkins and 2 Mimics";
            MonsterCounter.startStage(0, 0, 3, 2, 900, 900);
        }
        else if (r == 20){
            request.text = "Defeat 1 Skeletons, 2 Skeleton Heads, and 2 Mimics";
            MonsterCounter.startStage(1, 2, 0, 2, 550, 550);
        }
        SceneManager.LoadScene(4);
    }

    public void HardDifficulty()
    {
        int r = Random.Range(0, 9);
        Debug.Log("Generating Encounter : " + r);
        if (r == 0){
            request.text = "Defeat 3 Skeleton Heads, 4 Pumpkins, and 1 Mimic";
            MonsterCounter.startStage(0, 3, 4, 1, 1250, 1250);
        }
        else if (r == 1){
            request.text = "Defeat 3 Skeleton Heads, 2 Pumpkins, and 3 Mimics";
            MonsterCounter.startStage(0, 3, 2, 3, 1150, 1150);
        }
        else if (r == 2){
            request.text = "Defeat 2 Skeletons, 5 Pumpkins, and 1 Mimic";
            MonsterCounter.startStage(2, 0, 5, 1, 1250, 1250);
        }
        else if (r == 3){
            request.text = "Defeat 1 Skeletons, 1 Skeleton Head, 4 Pumpkins, and 2 Mimics";
            MonsterCounter.startStage(1, 1, 4, 2, 1250, 1250);
        }
        else if (r == 4){
            request.text = "Defeat 1 Skeleton Head, 2 Pumpkins, and 5 Mimics";
            MonsterCounter.startStage(0, 1, 2, 5, 1250, 1250);
        }
        else if (r == 5){
            request.text = "Defeat 4 Skeleton Heads and 4 Pumpkins";
            MonsterCounter.startStage(0, 4, 4, 0, 1200, 1200);
        }
        else if (r == 6){
            request.text = "Defeat 5 Skeletons, 1 Skeleton Head, 1 Pumpkin, and 1 Mimic";
            MonsterCounter.startStage(5, 1, 1, 1, 700, 700);
        }
        else if (r == 7){
            request.text = "Defeat 1 Skeleton, 4 Skeleton Heads, 2 Pumpkins, and 1 Mimic";
            MonsterCounter.startStage(1, 4, 2, 1, 1000, 1000);
        }
        else if (r == 8){
            request.text = "Defeat 8 Mimics";
            MonsterCounter.startStage(0, 0, 0, 8, 1200, 1200);
        }
        SceneManager.LoadScene(5);
    }
}
