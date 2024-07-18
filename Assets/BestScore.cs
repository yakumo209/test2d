using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{

    private TextMeshProUGUI ui;
    private void Start()
    {
        ui = GetComponent<TextMeshProUGUI>();
        UpdateText();
    }

    public void UpdateText()
    {
        ui.text = "BestScore: " + PlayerPrefs.GetFloat("time").ToString("F2");
    }
}
