using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{

    public GameObject gameOver;
    public Canvas health;

    public Canvas inputCanvas;
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

    public void UpdateHealth(int number){
        Debug.Log(health.transform.GetChild(number).gameObject.name);
        health.transform.GetChild(number).gameObject.SetActive(false);
    }

    public void UpdateInputCanvas(string input){
        if(input == "left"){
            Debug.Log(inputCanvas.transform.GetChild(0).gameObject.name);
            inputCanvas.transform.GetChild(0).gameObject.SetActive(true);
            inputCanvas.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if(input == "right"){
            Debug.Log(inputCanvas.transform.GetChild(1).gameObject.name);
            inputCanvas.transform.GetChild(1).gameObject.SetActive(true);
            inputCanvas.transform.GetChild(0).gameObject.SetActive(false);
        }
        else{
            inputCanvas.gameObject.SetActive(false);
        }
    }

     public void NextLevel(){
        if(SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
        else{
            SceneManager.LoadScene(0);
        }
    }
}
