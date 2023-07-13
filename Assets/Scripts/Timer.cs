using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]private TMP_Text Text; // Timer_text

    public float timeRemaining =0;          
    public bool timeIsRunning = true;

    private void Start()
    {
        timeIsRunning = true;
    }
  
    void Update()
    {
       if (timeIsRunning)
       {
          if (timeRemaining >= 0)
          {
              timeRemaining += Time.deltaTime;
              DisplayTime(timeRemaining);
              if(timeRemaining >= 119) 
              {
              timeIsRunning=false;
              OnDisPlayTimer();
              }
          }
       }
    }

    private void DisplayTime(float timetoDisplay)
    {
        timetoDisplay += 1;
        float minutes = Mathf.FloorToInt(timetoDisplay / 60);
        float seconds = Mathf.FloorToInt(timetoDisplay % 60);
        Text.text = string.Format("{0:00}:{1:00}",minutes,seconds);
    }

    private void OnDisPlayTimer()
    {
        if (!timeIsRunning)
        {
            Exo_Gray.instance.Panel.SetActive(true);
            UI.instance.GameOverTxt("GAME OVER");
            Exo_Gray.instance.CheckCollected();
        }
    }
}
