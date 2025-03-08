using UnityEngine;

[System.Serializable]
public class Card
{
    public int Id; // Unique identifier for the card
    public string Name;
    public int Attack;
    public string Description;
    public string ImageName; // Name of the image file (without extension)

    public Card(int id, string name, int attack, string description, string imageName)
    {
        Id = id;
        Name = name;
        Attack = attack;
        Description = description;
        ImageName = imageName;
    }

    // Method to load the image dynamically from Resources
    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>(ImageName);
    }
}
