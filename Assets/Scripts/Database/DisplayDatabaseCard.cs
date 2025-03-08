using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Vinculada a todas os prefabs de CardDatabase
/// Responsável por exibir uma única carta do banco de dados na interface do usuário.
/// Atualiza os textos, imagem e botão de adicionar ao deck.
/// </summary>

public class DisplayDatabaseCard : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ataqueText;
    public TextMeshProUGUI descriptionText;
    public Image cardImage;
    public Button addButton;

    private Card card;

    public void SetCard(Card newCard)
    {
        card = newCard;
        UpdateCardDisplay();

        if (addButton != null)
        {
            addButton.onClick.RemoveAllListeners();
            addButton.onClick.AddListener(AddCardToDeck);
        }
        else
        {
            Debug.LogError("Botão de adicionar não foi encontrado no prefab do banco de dados!");
        }
    }

    private void UpdateCardDisplay()
    {
        if (card == null)
        {
            Debug.LogError("Card não foi definido!");
            return;
        }

        nameText.text = card.Name;
        ataqueText.text = card.Attack.ToString();
        descriptionText.text = card.Description;
        cardImage.sprite = card.GetSprite();
    }

    private void AddCardToDeck()
    {
        if (card != null)
        {
            if (Deck.Instance.GetLength() < 18)// O deck só pode ter 18 cartas
            {
                Deck.Instance.AddCard(card);
                Debug.Log("Carta adicionada ao deck: " + card.Name);
                Debug.Log("Cartas no deck = " + Deck.Instance.GetLength());
            }
            
        }
    }
}
