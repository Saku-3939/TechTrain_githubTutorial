using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    public int resultScore;
    public Text resultText;
    // Start is called before the first frame update
    void Start()
    {
        resultScore = GameController.GetScore();
        resultText.text = "Your Score is " + resultScore;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
