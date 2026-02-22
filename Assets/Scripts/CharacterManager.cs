using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public TMP_InputField nameField;
    public TMP_InputField yearField;
    public TMP_Text resultText;
    public TMP_Dropdown characterDropdown;
    public TMP_Text descriptionText;
    public Image characterImage;

    public Sprite playerSprite;
    public Sprite guideSprite;

    public Slider widthSlider;
    public Slider heightSlider;

    public void CalculateAge()
    {
        string name = nameField.text;
        int year;

        if (int.TryParse(yearField.text, out year))
        {
            int age = 2026 - year;
            resultText.text = $"Character {name} is {age} years old!";
        }
    }

    public void ChangeCharacter()
    {
        string userName = nameField.text;
        if (string.IsNullOrEmpty(userName)) userName = "Player";

        if (characterDropdown.value == 0)
        {
            characterImage.sprite = guideSprite;
            descriptionText.text = $"This is {userName}. He looks like the Guide, who is the first NPC a player encounters.";
        }
        else
        {
            characterImage.sprite = playerSprite;
            descriptionText.text = $"This is {userName}. He is a brave explorer of the Terraria world, ready for adventure!";
        }
    }
    public void UpdateCharacterScale()
    {
        characterImage.transform.localScale = new Vector3(widthSlider.value, heightSlider.value, 1f);
    }
}