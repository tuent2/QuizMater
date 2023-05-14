using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    CountScore countScore;
    void Awake()
    {
        countScore = FindObjectOfType<CountScore>();
    }

    public void ShowFinalScore(){
        finalScoreText.text = "Chúc mừng!\n Bạn đạt được số điểm là: "
                         + countScore.CalculateScore() + "%";
    }
}
