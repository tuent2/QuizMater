using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountScore : MonoBehaviour
{
    int correctAnswers = 0;
    int questionSeen = 0;

    public int GetCorrentAnswers(){
        return correctAnswers;
    }

    public void IncrementCorrectAnswers(){
        correctAnswers ++ ;
    }

    public int getQuestionSeen(){
        return questionSeen;
    }

    public void IncrementquestionSeen(){
        questionSeen ++ ;
    }

    public int CalculateScore(){
        return Mathf.RoundToInt(correctAnswers /(float) questionSeen * 100) ;
    }
}
