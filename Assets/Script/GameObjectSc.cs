using System;
using UnityEngine;
using UnityEngine.UI;
public class GameObjectSc : MonoBehaviour
{
    private int curentEnery;
    [SerializeField] int HoldEnery = 3;

    [SerializeField]
    private GameObject boss;
    private bool bossCall = false;

    [SerializeField]
    private Image energuBar;
    [SerializeField]
    private GameObject gameUI;
    void Start()
    {
        curentEnery = 0;
        UpdateEnergyBar();
        boss.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddEneyy()
    {
        curentEnery += 1;
        UpdateEnergyBar();
        if (bossCall)
        {
            return;
        }
        if (curentEnery == HoldEnery)
        {
            CallBoss();
        }
    }

    public void CallBoss()
    {
        bossCall = true;
        boss.SetActive(true);
        gameUI.SetActive(false);

    }
    public void UpdateEnergyBar()
    {
        if (energuBar != null) {
            float fillAmount = Mathf.Clamp01((float)curentEnery / (float)HoldEnery);
            energuBar.fillAmount = fillAmount;
        }
    }
}
