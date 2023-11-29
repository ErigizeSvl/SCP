using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Eyes : MonoBehaviour
{
    // Public var
    public Image blackBG, fillerImg;
    public float minTime2Blink, maxTime2Blink, blinkDuration, decreaseDurationRate;

    // Private var
    private float time2Blink, currentTime2Blink;
    private bool isBlinking;

    //Singleton
    public static Eyes Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        isBlinking = false;
        currentTime2Blink = maxTime2Blink;
        time2Blink = currentTime2Blink;
    }

    // Update is called once per frame
    void Update()
    {
        Blink();
        Time2Blink();
        UpdateUI();
    }

    // Fn update UI
    public void UpdateUI()
    {
        fillerImg.fillAmount = time2Blink / currentTime2Blink;
    }
    public void Blink()
    {
        if (!isBlinking)
        {
            if (Input.GetKeyDown(KeyCode.B) || time2Blink <= 0f)
            {
                StartCoroutine(BlinkingCoroutine());
            }
        }
    }

    public void Time2Blink()
    {
        if (!isBlinking)
        {
            time2Blink -= 1f * Time.deltaTime;
        }
    }

    public void decreaseTime2Blink()
    {
        if(currentTime2Blink > minTime2Blink)
        {
            currentTime2Blink -= decreaseDurationRate;

            if(currentTime2Blink < minTime2Blink)
            {
                currentTime2Blink = minTime2Blink;
            }
        }
    }

    public void ResetTime2Blink()
    {
        currentTime2Blink = maxTime2Blink + decreaseDurationRate;
    }

    public bool IsBlinking()
    {
        return isBlinking;
    }

    IEnumerator BlinkingCoroutine()
    {
        // Close eyes
        isBlinking = true;
        blackBG.color = new Color(0f, 0f, 0f, 1f);

        // Wait blink time
        yield return new WaitForSeconds(blinkDuration);

        // Open eyes and reduce time 4 next blink
        blackBG.color = new Color(0f, 0f, 0f, 0f);
        decreaseTime2Blink();
        time2Blink = currentTime2Blink;
        isBlinking = false;
    }
}
