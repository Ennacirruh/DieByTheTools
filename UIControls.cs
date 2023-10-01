using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIControls : MonoBehaviour
{
    GameManager manager;
    [SerializeField] GameObject homeScreen;
    [SerializeField] GameObject tutorialScreen;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject runningScreen;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI difficultyDisplay;
    [SerializeField] SpellTrinket[] trinkets;
    [SerializeField] Weapon[] weapons;
    [SerializeField] Boots[] boots;
    [SerializeField] Helmet[] helmets;
    [SerializeField] Slider left;
    [SerializeField] Slider right;
    [SerializeField] Slider a;
    [SerializeField] Slider b;
    [SerializeField] Slider boot;
    [SerializeField] Slider helm;
    [SerializeField] TextMeshProUGUI lW;
    [SerializeField] TextMeshProUGUI rW;
    [SerializeField] TextMeshProUGUI aT;
    [SerializeField] TextMeshProUGUI bT;
    [SerializeField] TextMeshProUGUI boo;
    [SerializeField] TextMeshProUGUI hel;
    [SerializeField] TextMeshProUGUI survivalTime;
    private void Start()
    {
        manager = GetComponent<GameManager>();
        left.maxValue = weapons.Length - 1;
        right.maxValue = weapons.Length - 1;
        a.maxValue = trinkets.Length - 1;
        b.maxValue = trinkets.Length - 1;
        boot.maxValue = boots.Length - 1;
        helm.maxValue = helmets.Length - 1;
        selectLeft();
        selectRight();
        selectA();
        selectB();
        selectHelm();
        selectBoot();
    }
    public void GoToHome()
    {
        homeScreen.SetActive(true);
        tutorialScreen.SetActive(false);
        deathScreen.SetActive(false);
        runningScreen.SetActive(false);
    }
    public void GoToTutorial()
    {
        homeScreen.SetActive(false);
        tutorialScreen.SetActive(true);
        deathScreen.SetActive(false);
        runningScreen.SetActive(false);
    }
    public void GoToDeath()
    {
        homeScreen.SetActive(false);
        tutorialScreen.SetActive(false);
        deathScreen.SetActive(true);
        runningScreen.SetActive(false);
    }

    public void GoToRunning()
    {
        homeScreen.SetActive(false);
        tutorialScreen.SetActive(false);
        deathScreen.SetActive(false);
        runningScreen.SetActive(true);
    }

    public void ChangeDifficulty()
    {
        manager.difficulty = (int)slider.value;
        switch(manager.difficulty)
        {
            case 0:
                difficultyDisplay.text = "Easy";
                break;
            case 1:
                difficultyDisplay.text = "Medium";
                break;
            case 2:
                difficultyDisplay.text = "Hard";
                break;
            case 3:
                difficultyDisplay.text = "Onslaught";
                break;
            default:
                Debug.LogError("Slider is broken");
                break;
        }
    }

    public void StartGame()
    {
        GoToRunning();
        manager.startGame();
    }

    public void Die()
    {
        manager.onDeath();
        survivalTime.text = "Time Survived: " + Mathf.Round(manager.timeSurvived) + " seconds";
        GoToDeath();
    }

    public void selectLeft()
    {
        if ((int)left.value == (int)right.value)
        {
            if((int)left.value == left. maxValue)
            {
                left.value -= 1;
            }
            else
            {
                left.value += 1;
            }
        }
        lW.text = "Weapon: " + weapons[(int)left.value].name;
        manager.left = weapons[(int)left.value];
    }
    public void selectRight()
    {
        if ((int)left.value == (int)right.value)
        {
            if ((int)right.value == right.maxValue)
            {
                right.value -= 1;
            }
            else
            {
                right.value += 1;
            }
        }
        rW.text = "Weapon: " + weapons[(int)right.value].name;
        manager.right = weapons[(int)right.value];
    }
    public void selectA()
    {
        if ((int)a.value == (int)b.value)
        {
            if ((int)a.value == a.maxValue)
            {
                a.value -= 1;
            }
            else
            {
                a.value += 1;
            }
        }
        aT.text = "Trinket: " + trinkets[(int)a.value].name;
        manager.a = trinkets[(int)a.value];
    }
    public void selectB()
    {
        if ((int)a.value == (int)b.value)
        {
            if ((int)b.value == b.maxValue)
            {
                b.value -= 1;
            }
            else
            {
                b.value += 1;
            }
        }
        bT.text = "Trinket: " + trinkets[(int)b.value].name;
        manager.b = trinkets[(int)b.value];
    }
    public void selectBoot()
    {
        boo.text = "Boots: " + boots[(int)boot.value].name;
        manager.boot = boots[(int)boot.value];
    }
    public void selectHelm()
    {
        hel.text = "Helmet: " + helmets[(int)helm.value].name;
        manager.helm = helmets[(int)helm.value];
    }
    public void Exit()
    {
        Application.Quit();
    }
}
