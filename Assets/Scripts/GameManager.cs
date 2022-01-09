using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    public GameObject gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showGameOverUI(){
        gameOver.SetActive(true);
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene(0);
    }

     public void ChangeToLevel2(){
        SceneManager.LoadScene(2);
    }
}
