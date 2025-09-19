using MGMG.Magic;
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/CardSO")]
public class CardSO : ScriptableObject
{
    public string cardName;
    [SerializeReference] public Card card;

    [Space]
    public string displayName;
    public string description;
    public Sprite icon;

    private void OnValidate()
    {
        if (card != null) return;

        try
        {
            string cardStr = $"{cardName}";

            Type cardType = Type.GetType(cardStr);
            if(card == null || card.GetType() != cardType)
                card = Activator.CreateInstance(cardType) as Card;
        }
        catch (Exception e)
        {
            card = null;
            Debug.LogWarning(e);
        }
    }
}
