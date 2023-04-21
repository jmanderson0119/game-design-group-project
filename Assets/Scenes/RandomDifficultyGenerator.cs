using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class RandomDifficultyGenerator : MonoBehaviour
{
    public static MonsterCounter MonsterCounter;
    private static bool generated;
    private static int easy;
    private static int medium;
    private static int hard;
    private static bool selectEasy;
    private static bool selectMedium;
    private static bool selectHard;

    [SerializeField] public TMP_Text Erequest;
    [SerializeField] public TMP_Text Mrequest;
    [SerializeField] public TMP_Text Hrequest;
    public void Start() {
        if (generated == false){
            easy = Random.Range(0, 19);
            medium = Random.Range(0, 21);
            hard = Random.Range(0, 9);
            generated = true;
        }
        EasyDifficulty();
        MediumDifficulty();
        HardDifficulty();

    }

    public void EasyDifficulty() {
        Debug.Log("Generating Encounter : " + easy);
        if (easy== 0) {
            Erequest.text = "Defeat 3 Skeletons\n 150 gold\n 150 reputation";
            MonsterCounter.startStage(3, 0, 0, 0, 150, 150);
        }
        else if (easy== 1) {
            Erequest.text = "Defeat 2 Skeletons and 1 Skeleton Head\n 200 gold\n 200 reputation";
            MonsterCounter.startStage(2, 1, 0, 0, 200, 200);
        }
        else if (easy== 2) {
            Erequest.text = "Defeat 2 Skeletons and 1 Pumpkin\n 300 gold\n 300 reputation";
            MonsterCounter.startStage(2, 0, 1, 0, 300, 300);
        }
        else if (easy== 3) {
            Erequest.text = "Defeat 2 Skeletons and 1 Mimic\n 250 gold\n 250 reputation";
            MonsterCounter.startStage(2, 0, 0, 1, 250, 250);
        }
        else if (easy== 3){
            Erequest.text = "Defeat 1 Skeleton, 1 Skeleton Head, and 1 Pumpkin\n 350 gold\n 350 reputation";
            MonsterCounter.startStage(1, 1, 1, 0, 350, 350);
        }
        else if (easy== 4){
            Erequest.text = "Defeat 1 Skeleton, 1 Skeleton Head, and 1 Mimic\n 300 gold\n 300 reputation";
            MonsterCounter.startStage(1, 1, 0, 1, 300, 300);
        }
        else if (easy== 5){
            Erequest.text = "Defeat 1 Skeleton, 1 Pumpkin, and 1 Mimic\n 400 gold\n 400 reputation";
            MonsterCounter.startStage(1, 0, 1, 1, 400, 400);
        }
        else if (easy== 6){
            Erequest.text = "Defeat 3 Skeleton Heads\n 300 gold\n 30 reputation";
            MonsterCounter.startStage(0, 3, 0, 0, 300, 30);
        }
        else if (easy== 7){
            Erequest.text = "Defeat 2 Skeleton Heads and 1 Skeleton\n 250 gold\n 250 reputation";
            MonsterCounter.startStage(1, 2, 0, 0, 250, 250);
        }
        else if (easy== 8){
            Erequest.text = "Defeat 2 Skeleton Heads and 1 Pumpkin\n 400 gold\n 400 reputation";
            MonsterCounter.startStage(0, 2, 1, 0, 400, 400);
        }
        else if (easy== 9){
            Erequest.text = "Defeat 2 Skeleton Heads and 1 Mimic\n 350 gold\n 350 reputation";
            MonsterCounter.startStage(0, 2, 0, 1, 350, 350);
        }
        else if (easy== 10){
            Erequest.text = "Defeat 1 Skeleton Head, 1 Pumpkin, and 1 Mimic\n 450 gold\n 450 reputation";
            MonsterCounter.startStage(0, 1, 1, 1, 450, 450);
        }
        else if (easy== 11){
            Erequest.text = "Defeat 3 Pumpkins\n 600 gold\n 600 reputation";
            MonsterCounter.startStage(0, 0, 3, 0, 600, 600);
        }
        else if (easy== 12){
            Erequest.text = "Defeat 2 Pumpkins and 1 Skeleton\n 450 gold\n 450 reputation";
            MonsterCounter.startStage(1, 0, 2, 0, 450, 450);
        }
        else if (easy== 13){
            Erequest.text = "Defeat 2 Pumpkins and 1 Skeleton Head\n 500 gold\n 500 reputation";
            MonsterCounter.startStage(0, 1, 2, 0, 500, 500);
        }
        else if (easy== 14){
            Erequest.text = "Defeat 2 Pumpkins and 1 Mimic\n 550 gold\n 550 reputation";
            MonsterCounter.startStage(0, 0, 2, 1, 550, 550);
        }
        else if (easy== 15){
            Erequest.text = "Defeat 3 Mimics\n 450 gold\n 450 reputation";
            MonsterCounter.startStage(0, 0, 0, 3, 450, 450);
        }
        else if (easy== 16){
            Erequest.text = "Defeat 2 Mimics and 1 Skeleton\n 350 gold\n 350 reputation";
            MonsterCounter.startStage(1, 0, 0, 2, 350, 350);
        }
        else if (easy== 17){
            Erequest.text = "Defeat 2 Mimics and 1 Skeleton Head\n 400 gold\n 400 reputation";
            MonsterCounter.startStage(0, 1, 0, 2, 400, 400);
        }
        else if (easy== 18){
            Erequest.text = "Defeat 2 Mimics and 1 Pumpkin\n 500 gold\n 500 reputation";
            MonsterCounter.startStage(0, 0, 1, 2, 500, 500);
        }
    }

    public void MediumDifficulty()
    {
        
        Debug.Log("Generating Encounter : " + medium);
        if (medium== 0)
        {
            Mrequest.text = "Defeat 5 Skeletons\n 250 gold\n 250 reputation";
            MonsterCounter.startStage(5, 0, 0, 0, 250, 250);
        }
        else if (medium== 1){
            Mrequest.text = "Defeat 4 Skeletons and 1 Skeleton Head\n 300 gold\n 300 reputation";
            MonsterCounter.startStage(4, 1, 0, 0, 300, 300);
        }
        else if (medium== 2){
            Mrequest.text = "Defeat 4 Skeletons and 1 Pumpkin\n 400 gold\n 400 reputation";
            MonsterCounter.startStage(4, 0, 1, 0, 400, 400);
        }
        else if (medium== 3){
            Mrequest.text = "Defeat 4 Skeletons and 1 Mimic\n 350 gold\n 350 reputation";
            MonsterCounter.startStage(4, 0, 0, 1, 350, 350);
        }
        else if (medium== 3){
            Mrequest.text = "Defeat 3 Skeletons, 1 Skeleton Head, and 1 Pumpkin\n 450 gold\n 450 reputation";
            MonsterCounter.startStage(3, 1, 1, 0, 450, 450);
        }
        else if (medium== 4){
            Mrequest.text = "Defeat 3 Skeletons, 1 Skeleton Head, and 1 Mimic\n 400 gold\n 400 reputation";
            MonsterCounter.startStage(3, 1, 0, 1, 400, 400);
        }
        else if (medium== 5){
            Mrequest.text = "Defeat 3 Skeletons, 1 Pumpkin, and 1 Mimic\n 500 gold\n 500 reputation";
            MonsterCounter.startStage(3, 0, 1, 1, 500, 500);
        }
        else if (medium== 6){
            Mrequest.text = "Defeat 2 Skeletons, 1 Skeleton Head, 1 Pumpkin, and 1 Mimic\n 550 gold\n 550 reputation";
            MonsterCounter.startStage(2, 1, 1, 1, 550, 550);
        }
        else if (medium== 7){
            Mrequest.text = "5 Skeleton Heads\n 500 gold\n 500 reputation";
            MonsterCounter.startStage(0, 5, 0, 0, 500, 500);
        }
        else if (medium== 8){
            Mrequest.text = "Defeat 4 Skeleton Heads and 1 Pumpkin\n 600 gold\n 600 reputation";
            MonsterCounter.startStage(0, 4, 1, 0, 600, 600);
        }
        else if (medium== 9){
            Mrequest.text = "Defeat 4 Skeleton Heads and 1 Mimic\n 550 gold\n 550 reputation";
            MonsterCounter.startStage(0, 4, 0, 1, 550, 550);
        }
        else if (medium== 10){
            Mrequest.text = "Defeat 3 Skeleton Heads, 1 Pumpkin, and 1 Mimic\n 650 gold\n 650 reputation";
            MonsterCounter.startStage(0, 3, 1, 1, 650, 650);
        }
        else if (medium== 11){
            Mrequest.text = "Defeat 2 Skeleton Heads, 1 Skeleton, 1 Pumpkin, and 1 Mimic\n 600 gold\n 600 reputation";
            MonsterCounter.startStage(1, 2, 1, 1, 600, 600);
        }
        else if (medium== 12){
            Mrequest.text = "Defeat 5 Pumpkins\n 1000 gold\n 1000 reputation";
            MonsterCounter.startStage(0, 0, 5, 0, 1000, 1000);
        }
        else if (medium== 13){
            Mrequest.text = "Defeat 4 Pumpkins and 1 Skeleton Head\n 900 gold\n 900 reputation";
            MonsterCounter.startStage(0, 1, 4, 0, 900, 900);
        }
        else if (medium== 14){
            Mrequest.text = "Defeat 4 Pumpkins and 1 Mimic\n 950 gold\n 950 reputation";
            MonsterCounter.startStage(0, 0, 4, 1, 950, 950);
        }
        else if (medium== 15){
            Mrequest.text = "Defeat 3 Pumpkins, 1 Skeleton Head, and 1 Mimic\n 850 gold\n 850 reputation";
            MonsterCounter.startStage(0, 1, 3, 1, 850, 850);
        }
        else if (medium== 16){
            Mrequest.text = "Defeat 3 Pumpkins, 1 Skeleton, and 1 Skeleton Head\n 750 gold\n 750 reputation";
            MonsterCounter.startStage(1, 1, 3, 0, 750, 750);
        }
        else if (medium== 17){
            Mrequest.text = "Defeat 2 Pumpkins, 1 Skeleton, 1 Skeleton Head, and 1 Mimic\n 700 gold\n 700 reputation";
            MonsterCounter.startStage(1, 1, 2, 1, 700, 700);
        }
        else if (medium== 18){
            Mrequest.text = "Defeat 2 Skeletons, and 3 Mimics\n 550 gold\n 550 reputation";
            MonsterCounter.startStage(2, 0, 0, 3, 550, 550);
        }
        else if (medium== 19){
            Mrequest.text = "Defeat 3 Pumpkins and 2 Mimics\n 900 gold\n 900 reputation";
            MonsterCounter.startStage(0, 0, 3, 2, 900, 900);
        }
        else if (medium== 20){
            Mrequest.text = "Defeat 1 Skeletons, 2 Skeleton Heads, and 2 Mimics\n 550 gold\n 550 reputation";
            MonsterCounter.startStage(1, 2, 0, 2, 550, 550);
        }
    }

    public void HardDifficulty()
    {
        Debug.Log("Generating Encounter : " + hard);
        if (hard== 0){
            Hrequest.text = "Defeat 3 Skeleton Heads, 4 Pumpkins, and 1 Mimic\n 1250 gold\n 1250 reputation";
            MonsterCounter.startStage(0, 3, 4, 1, 1250, 1250);
        }
        else if (hard== 1){
            Hrequest.text = "Defeat 3 Skeleton Heads, 2 Pumpkins, and 3 Mimics\n 1150 gold\n 1150 reputation";
            MonsterCounter.startStage(0, 3, 2, 3, 1150, 1150);
        }
        else if (hard== 2){
            Hrequest.text = "Defeat 2 Skeletons, 5 Pumpkins, and 1 Mimic\n 1250 gold\n 1250 reputation";
            MonsterCounter.startStage(2, 0, 5, 1, 1250, 1250);
        }
        else if (hard== 3){
            Hrequest.text = "Defeat 1 Skeletons, 1 Skeleton Head, 4 Pumpkins, and 2 Mimics\n 1250 gold\n 1250 reputation";
            MonsterCounter.startStage(1, 1, 4, 2, 1250, 1250);
        }
        else if (hard== 4){
            Hrequest.text = "Defeat 1 Skeleton Head, 2 Pumpkins, and 5 Mimics\n 1250 gold\n 1250 reputation";
            MonsterCounter.startStage(0, 1, 2, 5, 1250, 1250);
        }
        else if (hard== 5){
            Hrequest.text = "Defeat 4 Skeleton Heads and 4 Pumpkins\n 1200 gold\n 1200 reputation";
            MonsterCounter.startStage(0, 4, 4, 0, 1200, 1200);
        }
        else if (hard== 6){
            Hrequest.text = "Defeat 5 Skeletons, 1 Skeleton Head, 1 Pumpkin, and 1 Mimic\n 700 gold\n 700 reputation";
            MonsterCounter.startStage(5, 1, 1, 1, 700, 700);
        }
        else if (hard== 7){
            Hrequest.text = "Defeat 1 Skeleton, 4 Skeleton Heads, 2 Pumpkins, and 1 Mimic\n 1000 gold\n 1000 reputation";
            MonsterCounter.startStage(1, 4, 2, 1, 1000, 1000);
        }
        else if (hard== 8){
            Hrequest.text = "Defeat 8 Mimics\n 1200 gold\n 1200 reputation";
            MonsterCounter.startStage(0, 0, 0, 8, 1200, 1200);
        }
    }

    private void startStage(){
        int stage = Random.Range(3,6);
        if (selectEasy){
            EasyDifficulty();
            SceneManager.LoadScene(stage);
        }
        else if(selectMedium){
            MediumDifficulty();
            SceneManager.LoadScene(stage);
        }
        else if(selectHard){
            HardDifficulty();
            SceneManager.LoadScene(stage);
        }
    }

    public void startEasy(){
        selectEasy=true;
        selectMedium=false;
        selectHard=false;
        startStage();
    }
    public void startMedium(){
        selectEasy=false;
        selectMedium=true;
        selectHard=false;
        startStage();
    }
    public void startHard(){
        selectEasy=false;
        selectMedium=false;
        selectHard=true;
        startStage();
    }
    public static void finishMission(){
        generated=false;
    }
}
