using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Vinculado a todos os CardDeckPrefabs
/// Respons�vel por exibir cada carta individualmente no DeckScene
/// Define o bot�o de remover para remover uma carta da inst�ncia de deck
/// </summary>
public class DisplayDeckCard : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ataqueText;
    public TextMeshProUGUI descriptionText;
    public Image cardImage;
    public Button removeButton;
    public GameObject backImage;

    private Card card;

    public void SetCard(Card newCard, bool isFaceUp = false)
    {
        card = newCard;
        UpdateCardDisplay();

        if (removeButton != null)
        {
            removeButton.onClick.RemoveAllListeners();
            removeButton.onClick.AddListener(() => Deck.Instance.RemoveCard(card));
        }
    }

    private void UpdateCardDisplay()
    {
        if (card == null)
        {
            Debug.LogError("Card n�o foi definido!");
            return;
        }

        nameText.text = card.Name;
        ataqueText.text = card.Attack.ToString();
        descriptionText.text = card.Description;
        cardImage.sprite = card.GetSprite();
    }

}
