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
        //score1�� distance �ֱ�
        score1.text = Timer.scoreDistance.ToString("F0");
        score2.text = gold.score.ToString("F0");
    }

    //��ư Ŭ���� �� �����
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        //�Ͻ����� ����
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
