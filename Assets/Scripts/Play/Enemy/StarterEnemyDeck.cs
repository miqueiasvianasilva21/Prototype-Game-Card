using UnityEngine;

public class StarterEnemyDeck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public CardDatabase cardDatabase;
    public void StartEnemyDeck()
    {


        for (int i = 0; i < 12; i++)
        {
            int rand = Random.Range(0, 6);
            EnemyDeck.Instance.AddCard(cardDatabase.CardCollection[rand]);
        }



    }

    // Update is called once per frame
    
}
