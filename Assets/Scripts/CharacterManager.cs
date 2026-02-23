using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    [Header("UI Fields")]
    public TMP_InputField nameField;
    public TMP_InputField yearField;
    public TMP_Text resultText;
    public TMP_Dropdown characterDropdown;
    public TMP_Text descriptionText;

    [Header("Character Visuals")]

    public Transform characterImage; 

    public GameObject guideVisual;
    public GameObject goblinVisual;
    public GameObject anglerVisual;

    [Header("Sliders")]
    public Slider widthSlider;
    public Slider heightSlider;

    public void CalculateAge()
    {
        string name = string.IsNullOrEmpty(nameField.text) ? "Player" : nameField.text;
        if (name == "Enter name...​") name = "Player";
        int currentYear = 2026;
        int birthYear;

        if (int.TryParse(yearField.text, out birthYear))
        {
            if (birthYear < currentYear && birthYear > 0)
            {
                int age = currentYear - birthYear;

                resultText.text = $"{name} has arrived! He is {age} years old!";
            }
            else if (birthYear >= currentYear)
            {
                resultText.text = "Enter valid birth year!";
            }
            else
            {
                resultText.text = "Enter valid birth year!";
            }
        }
        else
        {
            resultText.text = "Enter valid birth year!";
        }
    }

    public void ChangeCharacter()
    {
        string name = string.IsNullOrEmpty(nameField.text) ? "Player" : nameField.text;
        if (name == "Enter name...​") name = "Player";

        if (guideVisual != null) guideVisual.SetActive(false);
        if (goblinVisual != null) goblinVisual.SetActive(false);
        if (anglerVisual != null) anglerVisual.SetActive(false);

        switch (characterDropdown.value)
        {
            case 0:
                if (guideVisual != null) guideVisual.SetActive(true);
                descriptionText.text = $"This is {name}, the Guide. Your first ally in Terraria, " +
                                       "he provides survival tips and shows every crafting recipe. " +
                                       "But remember—he's also the key to summoning the Wall of Flesh!";
                break;

            case 1:
                if (goblinVisual != null) goblinVisual.SetActive(true);
                descriptionText.text = $"This is {name}, the Goblin Tinkerer. Rescued from underground, " +
                                       "he is the only NPC who can reforge your gear with powerful modifiers. " +
                                       "He also sells essential items like Rocket Boots and the Tinkerer's Workshop.";
                break;

            case 2:
                if (anglerVisual != null) anglerVisual.SetActive(true);
                descriptionText.text = $"This is {name}, the Angler. This rude genius sends you on " +
                                       "daily quests to catch rare, exotic fish across the world. " +
                                       "Complete his tasks to earn rewards like the Golden Fishing Rod or rare accessories!";
                break;
        }
    }

    [Header("Percent Display")]
    public TMP_Text scalePercentText;

    [Header("Percent Displays")]
    public TMP_Text widthPercentText;
    public TMP_Text heightPercentText;

    public void UpdateCharacterScale()
    {
        if (characterImage != null)
        {

            float w = widthSlider.value;
            float h = heightSlider.value;

            float currentZ = characterImage.localScale.z;
            characterImage.localScale = new Vector3(w, h, currentZ);

            int widthPercent = Mathf.RoundToInt((w / 500f) * 100f);
            if (widthPercentText != null)
            {
                widthPercentText.text = widthPercent + "%";
            }

            int heightPercent = Mathf.RoundToInt((h / 500f) * 100f);
            if (heightPercentText != null)
            {
                heightPercentText.text = heightPercent + "%";
            }
        }
    }
}