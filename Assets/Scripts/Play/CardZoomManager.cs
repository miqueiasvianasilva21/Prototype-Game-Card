using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


/// <summary>
/// Classe responsável por exibir as cartas no painel do lado esquerdo ao passar o mouse por cima delas.
/// É acionada no PlayCardManager para exibir as informações passadas por lá.
/// </summary>
public class CardZoomManager : MonoBehaviour
{
    public Image zoomImage; // Imagem grande da carta
    public TextMeshProUGUI zoomDescriptionText; // Texto da descrição
    public TextMeshProUGUI zoomAttackText; // Texto do ataque

    private static CardZoomManager instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false); // Começa invisível
       
    }
    private void Start()
    {
        
    }

    public static void ShowCard(Sprite cardSprite, string description, int attack) // Exibe as cartas no painel esquerdo
    {
        if (instance != null && cardSprite != null)
        {
            instance.zoomImage.sprite = cardSprite;
            instance.zoomDescriptionText.text = description;
            instance.zoomAttackText.text = "ATK: " + attack.ToString();
            instance.gameObject.SetActive(true);
        }
    }

    public static void HideCard() // Desativa o painel sempre que o mouse sai de cima da carta
    {
        if (instance != null)
        {
            instance.gameObject.SetActive(false);
        }
    }

    
}
