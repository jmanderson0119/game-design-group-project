using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EasyRequest : MonoBehaviour
{
    
    public void EasyGame()
    {
    
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
