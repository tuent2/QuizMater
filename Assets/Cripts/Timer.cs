using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float TimeToCompleteQuestion = 30f;
    [SerializeField] float TimeToShowCorectAnswer = 10f;
    public bool isAnsweringQuestion = false;
    public float fillFraction ;
    public bool loadNextQuestion;
    float timerValue ;
    void Update()
    {
        UpdateTimer();
    }

    public void CancerTimer(){
        timerValue = 0;
    }
    void UpdateTimer(){
        timerValue -= Time.deltaTime;
        if (isAnsweringQuestion){
            if (timerValue > 0 ){
                fillFraction = timerValue / TimeToCompleteQuestion;
            }
            else {
                isAnsweringQuestion = false;
                timerValue = TimeToShowCorectAnswer;
            }
        } else {
            if (timerValue > 0 ){
                fillFraction = timerValue / TimeToShowCorectAnswer;
            }
            else {
                isAnsweringQuestion = true;
                timerValue = TimeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
        // if(timerValue  <=0){
        //     timerValue = TimeToCompleteQuestion;
        // }
        
    }
}
