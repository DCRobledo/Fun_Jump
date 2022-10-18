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
    [SerializeField]
    private TextMeshProUGUI heightText;

    private int jumpForceLevel = 1;

    [SerializeField]
    private GameObject experienceTextPrefab;
    [SerializeField]
    private GameObject levelUpTextPrefab;

    private Transform player;

    private float experienceBarMaxSize;
    private float experience; 

    void Start()
    {
        instance = this;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        experienceBarMaxSize = experienceBar.GetComponent<RectTransform>().sizeDelta.x;

        UpdateExperienceBar();
    }

    private void Update() {
        UpdateHeightText();
    }

    private void UpdateHeightText() {
        int normalizedHeight = Mathf.RoundToInt((player.position.y + 1.5f) * 5f);

        if(normalizedHeight < 0) normalizedHeight = 0;

        heightText.text = $"Height: {normalizedHeight}m";
    }

    public void UpdateExperience(float modifier, bool isPickUp = false) {
        this.experience += modifier;

        if(experience >= 100) {
            Player.instance.LevelUp();

            experience -= 100;
        }
            

        SpawnExperienceText((int) modifier, isPickUp);

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

    private void SpawnExperienceText(int experienceModifier, bool isPickUp = false) {
        float rotation = Random.Range(-20, 20);

        GameObject experienceText = GameObject.Instantiate(experienceTextPrefab, Vector3.zero, Quaternion.Euler(0f, 0f, rotation), this.transform);

        experienceText.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-400, 400), Random.Range(-280, 280));

        experienceText.GetComponent<TextMeshProUGUI>().text = $"+{experienceModifier}";

        if(isPickUp)
            experienceText.GetComponent<TextMeshProUGUI>().color = new Color32(255, 105, 128, 255);

        experienceText.transform.SetAsFirstSibling();
    }

    private void SpawnLevelUpText() {
        GameObject levelUpText = GameObject.Instantiate(levelUpTextPrefab, Vector3.zero, Quaternion.identity, this.transform);

        levelUpText.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
    } 

    private void UpdateJumpForceText() {
        jumpForceLevel++;

        jumpForceText.text = jumpForceLevel < 10 ? $"Jump Force: 0{jumpForceLevel}" : $"Jump Force: {jumpForceLevel}";

        jumpForceText.GetComponent<Animator>().SetTrigger("pop");
    }

    public int GetJumpForceLevel() { return this.jumpForceLevel; }
}
