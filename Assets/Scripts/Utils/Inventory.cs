using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Card> items = new List<Card>(); // List of cards in inventory
    public GameObject inventoryPanel; // The panel within the world space Canvas
    public GameObject itemPrefab; // A prefab with UI elements to represent a card
    
    void Start()
    {
        PopulateInventoryUI();
        for (int i=0;i<3;i++){
            Card dummyCard = ScriptableObject.CreateInstance<Card>();
            dummyCard.cardName = "Dummy Card" + i;
            dummyCard.description = "This is a dummy card description.";
            dummyCard.cardImage = Resources.Load<Sprite>("dummy");
            items.Add(dummyCard);
        }

        PopulateInventoryUI();
    }

    // Method to populate the inventory UI
    void PopulateInventoryUI()
    {
        foreach (var card in items)
        {
            GameObject itemUI = Instantiate(itemPrefab, inventoryPanel.transform);
            itemUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("yggdrasil_0");
        }
    }
}
