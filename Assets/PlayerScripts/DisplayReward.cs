using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplayReward : MonoBehaviour
{
    private float currentTime = 5f;
    [SerializeField] TMP_Text reward; // dash cooldown UI
    [SerializeField] bool rewardDisplayActive = false;
    public int gold;
    public int rep;

    // Start is called before the first frame update
    void Start()
    {
        reward.text = "";

    }

    // Update is called once per frame
    void Update()
    {
        if (rewardDisplayActive)
        {
            if (currentTime >= 0)
            {
                reward.text = "Gold Earned: "+gold.ToString()+"\nReputation Earned: "+rep.ToString()+"\nReturning Home: "+currentTime.ToString("0") + "s";
            } 
            currentTime -= 1 * Time.deltaTime;
        }
        if(currentTime<=0){
            SceneManager.LoadScene (0);
        }
        
    }

    public void startExit(int goldv, int repv){
        gold = goldv;
        rep = repv;
        rewardDisplayActive = true;
    }
}
