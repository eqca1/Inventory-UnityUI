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
        string name = string.IsNullOrEmpty(nameField.text) ? "Varonis" : nameField.text;
        int year;

        if (int.TryParse(yearField.text, out year))
        {
            int age = 2026 - year;

            resultText.text = $"Supervaronis {name} ir {age} gadus vecs!";
        }
        else
        {
            resultText.text = "Lūdzu, ievadiet derīgu gadu!";
        }
    }

    public void ChangeCharacter()
    {
        string userName = string.IsNullOrEmpty(nameField.text) ? "Varonis" : nameField.text;


        if (guideVisual != null) guideVisual.SetActive(false);
        if (goblinVisual != null) goblinVisual.SetActive(false);
        if (anglerVisual != null) anglerVisual.SetActive(false);


        switch (characterDropdown.value)
        {
            case 0:
                if (guideVisual != null) guideVisual.SetActive(true);
                descriptionText.text = $"Tas ir {userName}. Viņš izskatās pēc Guide. Viņš ir pirmais NPC, ko spēlētājs satiek Terraria pasaulē, un sniedz noderīgus padomus iesācējiem.";
                break;

            case 1:
                if (goblinVisual != null) goblinVisual.SetActive(true);
                descriptionText.text = $"Tas ir {userName}. Viņš ir Goblin Tinkerer. Viņš var pārkalt jūsu ieročus un pārdod ļoti noderīgas lietas, piemēram, raķešu zābakus un darbnīcu.";
                break;

            case 2:
                if (anglerVisual != null) anglerVisual.SetActive(true);
                descriptionText.text = $"Tas ir {userName}. Viņš ir Angler. Tas ir mazs zēns, kurš sūta spēlētāju bīstamos makšķerēšanas uzdevumos, lai iegūtu retas zivis un balvas.";
                break;
        }
    }

    public void UpdateCharacterScale()
    {
        if (characterImage != null)
        {

            float w = Mathf.Max(widthSlider.value, 0.1f);
            float h = Mathf.Max(heightSlider.value, 0.1f);

            characterImage.localScale = new Vector3(w, h, 1f);
        }
    }
}