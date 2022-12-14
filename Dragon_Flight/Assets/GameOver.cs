using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text score1;
    public Text score2;


    private void OnEnable()
    {
        //score1에 distance 넣기
        score1.text = Timer.scoreDistance.ToString("F0");
        score2.text = gold.score.ToString("F0");
    }

    //버튼 클릭시 씬 재시작
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        //일시정지 해제
        Time.timeScale = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
