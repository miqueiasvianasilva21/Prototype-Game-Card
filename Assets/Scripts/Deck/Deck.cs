using System.Collections.Generic;

public class Deck
{
    private static Deck _instance;
    private List<Card> deck;
    public int Id { get; set; }

    // Evento que a UI pode ouvir
    public delegate void DeckChanged();
    public event DeckChanged OnDeckChanged;

    private Deck()
    {
        deck = new List<Card>();
        Id = 0;
    }

    public static Deck Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Deck();
                //DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    public void AddCard(Card card)
    {
        deck.Add(card);
        OnDeckChanged?.Invoke();  // Notifica que o deck foi alterado
    }

    public void RemoveCard(Card card)
    {
        deck.Remove(card);
        OnDeckChanged?.Invoke();  // Notifica que o deck foi alterado
    }

    public List<Card> GetCards()
    {
        return new List<Card>(deck);
    }

    public int GetLength()
    {
        return deck.Count;
    }
}
