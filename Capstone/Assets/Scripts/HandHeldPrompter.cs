using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HandHeldPrompter : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] TextMeshProUGUI CriticalText;
    [SerializeField] GameObject CriticalPopUp;

    float criticalAlertTimer = 0.0f;
    float tipTimer = 10.0f;
    string[] tips =
    {
        "Left Click to Interact",
        "Press E to Pick Up Object",
        "Press E to Drop Object",
        "Scroll to turn held object",
        "Right Click to Use Held Object"
    };


    // Start is called before the first frame update
    void Start()
    {
        CriticalPopUp.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (criticalAlertTimer > 0.0f)
        {
            criticalAlertTimer -= Time.deltaTime;
            CriticalPopUp.SetActive(true);
        } else
        {
            CriticalPopUp.SetActive(false);
        }

        if (tipTimer > 0.0f)
        {
            tipTimer -= Time.deltaTime;
        }
        else
        {
            UpdateText2(tips[Random.Range(0, tips.Length)]);
            tipTimer = 10.0f;
        }
    }


    public void UpdateText1(string text)
    {
        text1.text = text;
    }

    public void UpdateText2(string text)
    {
        text2.text = text;
    }


    public void CriticalAlert(string text)
    {
        CriticalText.text = text;
        criticalAlertTimer = 30.0f;
    }
}
