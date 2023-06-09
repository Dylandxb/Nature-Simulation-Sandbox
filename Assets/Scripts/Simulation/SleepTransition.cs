using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepTransition : MonoBehaviour
{
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject dateUI;
    [SerializeField] private GameObject saveText;
    [SerializeField] private GameObject waypointMarker;
    [SerializeField] private CanvasGroup canvasGroup;


    void Start()
    {
        canvasGroup.alpha = 0f;
        //Subscribe function calling coroutine to the sleep event
        EventManager.instance.StartSleep += SleepFade;

    }

    private void OnDisable()
    {
        EventManager.instance.StartSleep -= SleepFade;
    }
  
    public void SleepFade()
    {
        StartCoroutine(FadeTransition());
    }
    private IEnumerator FadeTransition()
    {
        //Fade in the panel if its current alpha is less than 1
        if(canvasGroup.alpha < 1)
        {
            //Increase alpha over time
            canvasGroup.alpha += Time.deltaTime * 1.05f;
        }
        yield return new WaitForSeconds(2.0f);
        dateUI.SetActive(true);
        saveText.SetActive(true);
        yield return new WaitForSeconds(10.0f);
        //Decrease after desired transition time
        if (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime * 1.05f;
        }
        dateUI.SetActive(false);
        saveText.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        //Load stats
        yield return null;
    }
}
