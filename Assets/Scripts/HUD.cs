using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    [SerializeField]
    private Image experienceBar;

    [SerializeField]
    private TextMeshProUGUI jumpForceText;

    private int jumpForceLevel = 1;

    [SerializeField]
    private GameObject experienceTextPrefab;
    [SerializeField]
    private GameObject levelUpTextPrefab;

    private float experienceBarMaxSize;
    private float experience; 

    void Start()
    {
        instance = this;

        experienceBarMaxSize = experienceBar.GetComponent<RectTransform>().sizeDelta.x;

        UpdateExperienceBar();
    }

    public void UpdateExperience(float modifier) {
        this.experience += modifier;

        if(experience >= 100) {
            Player.instance.LevelUp();

            experience -= 100;
        }
            

        SpawnExperienceText((int) modifier);

        UpdateExperienceBar();
    }

    public void LevelUp() {
        SpawnLevelUpText();

        UpdateJumpForceText();
    }

    private void UpdateExperienceBar() {
        float experienceBarSize = experienceBarMaxSize * Mathf.InverseLerp(0, 100, experience);

        RectTransform rectTransform = experienceBar.transform as RectTransform;
        rectTransform.sizeDelta = new Vector2(experienceBarSize, rectTransform.sizeDelta.y);
    }

    private void SpawnExperienceText(int experienceModifier) {
        Vector3 worldPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-2f, 2f), 0f);
        Vector3 position = Camera.main.WorldToScreenPoint(worldPosition);

        float rotation = Random.Range(-20, 20);

        GameObject experienceText = GameObject.Instantiate(experienceTextPrefab, position, Quaternion.Euler(0f, 0f, rotation), this.transform);

        experienceText.GetComponent<TextMeshProUGUI>().text = $"+{experienceModifier}";
    }

    private void SpawnLevelUpText() {
        GameObject.Instantiate(levelUpTextPrefab, Camera.main.WorldToScreenPoint(Vector3.zero), Quaternion.identity, this.transform);
    } 

    private void UpdateJumpForceText() {
        jumpForceLevel++;

        jumpForceText.text = jumpForceLevel < 10 ? $"Jump Force: 0{jumpForceLevel}" : $"Jump Force: {jumpForceLevel}";

        jumpForceText.GetComponent<Animator>().SetTrigger("pop");
    }
}
