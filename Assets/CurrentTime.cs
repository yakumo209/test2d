using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentTime : MonoBehaviour
{
    public static CurrentTime instance;
    public TextMeshProUGUI ui;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        ui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText(float time)
    {
        ui.text = "CurrentScore: " + time.ToString("F2");
    }
}
