using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;


/// <summary>
/// Classe respons�vel por exibir as cartas no painel do lado esquerdo ao passar o mouse por cima delas.
/// � acionada no PlayCardManager para exibir as informa��es passadas por l�.
/// </summary>
public class CardZoomManager : MonoBehaviour
{
    public Image zoomImage; // Imagem grande da carta
    public TextMeshProUGUI zoomDescriptionText; // Texto da descri��o
    public TextMeshProUGUI zoomAttackText; // Texto do ataque

    private static CardZoomManager instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false); // Come�a invis�vel
       
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
