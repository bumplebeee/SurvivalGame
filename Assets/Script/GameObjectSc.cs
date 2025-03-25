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
    [SerializeField]
    private GameObject winMenu;
    void Start()
    {
        curentEnery = 0;
        UpdateEnergyBar();
        boss.SetActive(false);
        winMenu.SetActive(false);
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

        // Ẩn tất cả các thành phần con trong gameUI trừ winMenu
        foreach (Transform child in gameUI.transform)
        {
            if (child.gameObject != winMenu)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateEnergyBar()
    {
        if (energuBar != null) {
            float fillAmount = Mathf.Clamp01((float)curentEnery / (float)HoldEnery);
            energuBar.fillAmount = fillAmount;
        }
    }
    public void WinGame()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
