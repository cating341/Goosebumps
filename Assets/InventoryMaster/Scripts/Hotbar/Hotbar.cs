using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using System.Linq;

public class Hotbar : MonoBehaviour
{

    [SerializeField]
    public KeyCode[] keyCodesForSlots = new KeyCode[999];
    [SerializeField]
    public int slotsInTotal;

	private GameObject _player;
	Transform selectedItem;

//	public delegate void ItemDelegate();
//	public static event ItemDelegate updateInventoryList;

#if UNITY_EDITOR
    [MenuItem("Master System/Create/Hotbar")]        //creating the menu item
    public static void menuItemCreateInventory()       //create the inventory at start
    {
        GameObject Canvas = null;
        if (GameObject.FindGameObjectWithTag("Canvas") == null)
        {
            GameObject inventory = new GameObject();
            inventory.name = "Inventories";
            Canvas = (GameObject)Instantiate(Resources.Load("Prefabs/Canvas - Inventory") as GameObject);
            Canvas.transform.SetParent(inventory.transform, true);
            GameObject panel = (GameObject)Instantiate(Resources.Load("Prefabs/Panel - Hotbar") as GameObject);
            panel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            panel.transform.SetParent(Canvas.transform, true);
            GameObject draggingItem = (GameObject)Instantiate(Resources.Load("Prefabs/DraggingItem") as GameObject);
            Instantiate(Resources.Load("Prefabs/EventSystem") as GameObject);
            draggingItem.transform.SetParent(Canvas.transform, true);
            Inventory inv = panel.AddComponent<Inventory>();
            panel.AddComponent<InventoryDesign>();
            panel.AddComponent<Hotbar>();
            inv.getPrefabs();
        }
        else
        {
            GameObject panel = (GameObject)Instantiate(Resources.Load("Prefabs/Panel - Hotbar") as GameObject);
            panel.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
            panel.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
            Inventory inv = panel.AddComponent<Inventory>();
            panel.AddComponent<Hotbar>();
            DestroyImmediate(GameObject.FindGameObjectWithTag("DraggingItem"));
            GameObject draggingItem = (GameObject)Instantiate(Resources.Load("Prefabs/DraggingItem") as GameObject);
            draggingItem.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, true);
            panel.AddComponent<InventoryDesign>();
            inv.getPrefabs();
        }
    }
#endif
	void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player");
	}


    void Update()
    {
        for (int i = 0; i < slotsInTotal; i++)
        {
			if (Input.GetKeyDown (keyCodesForSlots [i])) {
				if (transform.GetChild (1).GetChild (i).childCount != 0 && transform.GetChild (1).GetChild (i).GetChild (0).GetComponent<ItemOnObject> ().item.itemType != ItemType.UFPS_Ammo) {
					if (selectedItem) {
						selectedItem.GetComponentInParent<Outline> ().enabled = false;
					}
					selectedItem = transform.GetChild (1).GetChild (i).GetChild (0);
					transform.GetChild (1).GetChild (i).gameObject.GetComponent<Outline> ().enabled = true;
//                    if (transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ConsumeItem>().duplication != null && transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ItemOnObject>().item.maxStack == 1)
//                    {
//                        Destroy(transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ConsumeItem>().duplication);
//                    }
//                    transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<ConsumeItem>().consumeIt();

				}
			}
        }

		if (Input.GetKeyDown (KeyCode.Z) && selectedItem) 
		{
			float y = 0;
			string tag = selectedItem.GetComponent<ItemOnObject> ().item.itemModel.tag;
			switch (tag) {
			case "Ladder":
				y = 1.02f;
				break;
			case "Door":
				y = -1.2f;
				break;
			default:
				break;
			}
			putItem (selectedItem, y);
		}
    }

    public int getSlotsInTotal()
    {
        Inventory inv = GetComponent<Inventory>();
        return slotsInTotal = inv.width * inv.height;
    }

	void putItem(Transform selectedItem, float offSetY)
	{
		GameObject dropItem = (GameObject)Instantiate(selectedItem.GetComponent<ItemOnObject>().item.itemModel);
		dropItem.AddComponent<PickUpItem>();
		dropItem.GetComponent<PickUpItem>().item = selectedItem.GetComponent<ItemOnObject>().item;   
		dropItem.transform.localPosition = new Vector3(_player.transform.localPosition.x, _player.transform.localPosition.y + offSetY, _player.transform.localPosition.z+1.3f);
		Inventory inv = GetComponent<Inventory> ();

		Destroy (selectedItem.gameObject);
		inv.OnUpdateItemList();
	}
}
