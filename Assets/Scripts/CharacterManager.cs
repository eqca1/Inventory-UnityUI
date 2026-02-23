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
                descriptionText.text = $"This is {name}, appearing as the Guide. He is the first person you meet in the world of Terraria. " +
                                       "He provides essential survival tips and shows you every recipe you can craft with the items in your inventory. " +
                                       "Watch out for him—he’s your key to summoning the Wall of Flesh!";
                break;

            case 1:
                if (goblinVisual != null) goblinVisual.SetActive(true);
                descriptionText.text = $"This is {name}, taking the form of the Goblin Tinkerer. Once rescued from the underground caves, " +
                                       "he becomes your most valuable ally for upgrading gear. He is the only one who can reforge your weapons and accessories " +
                                       "to give them powerful modifiers, and he sells the legendary Rocket Boots.";
                break;

            case 2:
                if (anglerVisual != null) anglerVisual.SetActive(true);
                descriptionText.text = $"This is {name}, embodying the Angler. Don't let his small size fool you—he’s a rude little genius who sends you on " +
                                       "dangerous daily fishing quests to find rare and exotic fish across the world. " +
                                       "If you complete his tasks, he might reward you with a Golden Fishing Rod or a bottomless water bucket!";
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