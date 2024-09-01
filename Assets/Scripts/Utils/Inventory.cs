using System.Collections.Generic;
using Card;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<CardData> items = new List<CardData>(); // List of cards in inventory
    public GameObject inventoryPanel; // The panel within the world space Canvas
    public GameObject itemPrefab; // A prefab with UI elements to represent a card
    
    void Start()
    {
        PopulateInventoryUI();
    }

    // Method to populate the inventory UI
    void PopulateInventoryUI()
    {
        clearInventoryUI();
        foreach (var card in items)
        {
            GameObject itemUI = Instantiate(itemPrefab, inventoryPanel.transform);
            itemUI.GetComponent<Image>().sprite = Resources.Load<Sprite>("yggdrasil_0");
        }
    }
    
    void clearInventoryUI()
    {
        foreach (Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddCard(CardData card)
    {
        items.Add(card);
        PopulateInventoryUI();
    }
}
