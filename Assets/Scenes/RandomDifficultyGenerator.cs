using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDifficultyGenerator : MonoBehaviour
{
    MonsterCounter MonsterCounter;
    MonsterCounter monsterCounter;

    public void EasyDifficulty() {
        int r = Random.Range(0, 19);
        Debug.Log("Generating Encounter : " + r);
        if (r == 0) {
            MonsterCounter.startStage(3, 0, 0, 0, 150, 150);
        }
        else if (r == 1) {
            MonsterCounter.startStage(2, 1, 0, 0, 200, 200);
        }
        else if (r == 2) {
            MonsterCounter.startStage(2, 0, 1, 0, 300, 300);
        }
        else if (r == 3) {
            MonsterCounter.startStage(2, 0, 0, 1, 250, 250);
        }
        else if (r == 3){
            MonsterCounter.startStage(1, 1, 1, 0, 350, 350);
        }
        else if (r == 4){
            MonsterCounter.startStage(1, 1, 0, 1, 300, 300);
        }
        else if (r == 5){
            MonsterCounter.startStage(1, 0, 1, 1, 400, 400);
        }
        else if (r == 6){
            MonsterCounter.startStage(0, 3, 0, 0, 300, 30);
        }
        else if (r == 7){
            MonsterCounter.startStage(1, 2, 0, 0, 250, 250);
        }
        else if (r == 8){
            MonsterCounter.startStage(0, 2, 1, 0, 400, 400);
        }
        else if (r == 9){
            MonsterCounter.startStage(0, 2, 0, 1, 350, 350);
        }
        else if (r == 10){
            MonsterCounter.startStage(0, 1, 1, 1, 450, 450);
        }
        else if (r == 11){
            MonsterCounter.startStage(0, 0, 3, 0, 600, 600);
        }
        else if (r == 12){
            MonsterCounter.startStage(1, 0, 2, 0, 450, 450);
        }
        else if (r == 13){
            MonsterCounter.startStage(0, 1, 2, 0, 500, 500);
        }
        else if (r == 14){
            MonsterCounter.startStage(0, 0, 2, 1, 550, 550);
        }
        else if (r == 15){
            MonsterCounter.startStage(0, 0, 0, 3, 450, 450);
        }
        else if (r == 16){
            MonsterCounter.startStage(1, 0, 0, 2, 350, 350);
        }
        else if (r == 17){
            MonsterCounter.startStage(0, 1, 0, 2, 400, 400);
        }
        else if (r == 18){
            MonsterCounter.startStage(0, 0, 1, 2, 500, 500);
        }
    }

    public void MediumDifficulty()
    {
        int r = Random.Range(0, 21);
        Debug.Log("Generating Encounter : " + r);
        if (r == 0)
        {
            MonsterCounter.startStage(5, 0, 0, 0, 250, 250);
        }
        else if (r == 1){
            MonsterCounter.startStage(4, 1, 0, 0, 300, 300);
        }
        else if (r == 2){
            MonsterCounter.startStage(4, 0, 1, 0, 400, 400);
        }
        else if (r == 3){
            MonsterCounter.startStage(4, 0, 0, 1, 350, 350);
        }
        else if (r == 3){
            MonsterCounter.startStage(3, 1, 1, 0, 450, 450);
        }
        else if (r == 4){
            MonsterCounter.startStage(3, 1, 0, 1, 400, 400);
        }
        else if (r == 5){
            MonsterCounter.startStage(3, 0, 1, 1, 500, 500);
        }
        else if (r == 6){
            MonsterCounter.startStage(2, 1, 1, 1, 550, 550);
        }
        else if (r == 7){
            MonsterCounter.startStage(0, 5, 0, 0, 500, 500);
        }
        else if (r == 8){
            MonsterCounter.startStage(0, 4, 1, 0, 600, 600);
        }
        else if (r == 9){
            MonsterCounter.startStage(0, 4, 0, 1, 550, 550);
        }
        else if (r == 10){
            MonsterCounter.startStage(0, 3, 1, 1, 650, 650);
        }
        else if (r == 11){
            MonsterCounter.startStage(1, 2, 1, 1, 600, 600);
        }
        else if (r == 12){
            MonsterCounter.startStage(0, 0, 5, 0, 1000, 1000);
        }
        else if (r == 13){
            MonsterCounter.startStage(0, 1, 4, 0, 900, 900);
        }
        else if (r == 14){
            MonsterCounter.startStage(0, 0, 4, 1, 950, 950);
        }
        else if (r == 15){
            MonsterCounter.startStage(0, 1, 3, 1, 850, 850);
        }
        else if (r == 16){
            MonsterCounter.startStage(1, 1, 3, 0, 750, 750);
        }
        else if (r == 17){
            MonsterCounter.startStage(1, 1, 2, 1, 700, 700);
        }
        else if (r == 18){
            MonsterCounter.startStage(2, 0, 0, 3, 550, 550);
        }
        else if (r == 19){
            MonsterCounter.startStage(0, 0, 3, 2, 900, 900);
        }
        else if (r == 20){
            MonsterCounter.startStage(1, 2, 0, 2, 550, 550);
        }
    }

    public void HardDifficulty()
    {
        int r = Random.Range(0, 9);
        Debug.Log("Generating Encounter : " + r);
        if (r == 0){
            MonsterCounter.startStage(0, 3, 4, 1, 1250, 1250);
        }
        else if (r == 1){
            MonsterCounter.startStage(0, 3, 2, 3, 1150, 1150);
        }
        else if (r == 2){
            MonsterCounter.startStage(2, 0, 5, 1, 1250, 1250);
        }
        else if (r == 3){
            MonsterCounter.startStage(1, 1, 4, 2, 1250, 1250);
        }
        else if (r == 4){
            MonsterCounter.startStage(0, 1, 2, 5, 1250, 1250);
        }
        else if (r == 5){
            MonsterCounter.startStage(0, 4, 4, 0, 1200, 1200);
        }
        else if (r == 6){
            MonsterCounter.startStage(5, 1, 1, 1, 700, 700);
        }
        else if (r == 7){
            MonsterCounter.startStage(1, 4, 2, 1, 1000, 1000);
        }
        else if (r == 8){
            MonsterCounter.startStage(0, 0, 0, 8, 1200, 1200);
        }
    }
}
