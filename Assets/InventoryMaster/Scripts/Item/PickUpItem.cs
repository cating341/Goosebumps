using UnityEngine;
using System.Collections;
public class PickUpItem : MonoBehaviour
{
    public Item item;
    private Inventory _inventory;
    private GameObject _player;

    GameObject sceneManager;
    // Use this for initialization

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        sceneManager = GameObject.Find("SceneManager");
        if (_player != null)
            _inventory = _player.GetComponent<PlayerInventory>().inventory.GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
		if (_inventory != null && UserControl.PickupItem)
        {
			float distance = Vector2.Distance (new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y), new Vector2(_player.transform.position.x, _player.transform.position.y));
			print("distance: " + distance);
            if (distance <= 1.3)
            {
                sceneManager.GetComponent<MySceneManager>().removeFromGearList(this.gameObject);
                bool check = _inventory.checkIfItemAllreadyExist(item.itemID, item.itemValue);
                if (check)
                    Destroy(this.gameObject);
                else if (_inventory.ItemsInInventory.Count < (_inventory.width * _inventory.height))
                {
                    _inventory.addItemToInventory(item.itemID, item.itemValue);
                    _inventory.updateItemList();
                    _inventory.stackableSettings();
                    Destroy(this.gameObject);
                }

            }
        }
		if (_inventory != null && UserControl.GetGearsBack) {
			addItemToInv (item.itemID, item.itemValue);
		}
    }

	void addItemToInv (int id, int itemValue) {
		sceneManager.GetComponent<MySceneManager>().removeFromGearList(this.gameObject);
		bool check = _inventory.checkIfItemAllreadyExist(item.itemID, item.itemValue);
		if (check)
			Destroy (this.gameObject);
		else if (_inventory.ItemsInInventory.Count < (_inventory.width * _inventory.height)) {
			_inventory.addItemToInventory (id, itemValue);
			_inventory.updateItemList ();
			_inventory.stackableSettings ();
			Destroy (this.gameObject);
		}
	}

}