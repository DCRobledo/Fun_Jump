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
    private Camera mainCamera;

    [SerializeField]
    private GameObject experienceTextPrefab;

    private float experienceBarMaxSize;
    public float experience; 

    void Start()
    {
        instance = this;

        experienceBarMaxSize = experienceBar.GetComponent<RectTransform>().sizeDelta.x;

        UpdateExperienceBar();
    }

    public void UpdateExperience(float modifier) {
        this.experience += modifier;

        SpawnExperienceText((int) modifier);

        UpdateExperienceBar();
    }

    private void UpdateExperienceBar() {
        float experienceBarSize = experienceBarMaxSize * Mathf.InverseLerp(0, 100, experience);

        RectTransform rectTransform = experienceBar.transform as RectTransform;
        rectTransform.sizeDelta = new Vector2(experienceBarSize, rectTransform.sizeDelta.y);
    }

    private void SpawnExperienceText(int experienceModifier) {
        Vector3 position = Camera.main.WorldToScreenPoint(new Vector3(Random.Range(-5f, 5f), Random.Range(-2f, 2f), 0f));

        float rotation = Random.Range(-20, 20);

        GameObject experienceText = GameObject.Instantiate(experienceTextPrefab, position, Quaternion.Euler(0f, 0f, rotation), this.transform);

        experienceText.GetComponent<TextMeshProUGUI>().text = $"+{experienceModifier}";
    }
}
