using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Basic Item")]
public class Item : ScriptableObject
{
	protected string id;
	public string ID { get { return id; } }

	[SerializeField] protected string itemName = "";
	public string ItemName { get { return itemName; } }

	public string ItemDescription { get; protected set;}

	public Sprite ItemSprite;
	public int GoldCost;

	protected static readonly StringBuilder sb = new StringBuilder();

	protected Item _item;
	protected string _assetDataPath;

	public virtual Item GetCopy()
	{
		_item = Instantiate(this);

		_item.id = itemName + Random.Range(0f, 99999f);

		return _item;
	}

	public virtual void Destroy()
	{
		Destroy(this);
	}

	public virtual string GetItemType()
	{
		return "";
	}

	public virtual void GenerateItemDescription()
	{

	}

	public virtual GameObject GetWorldItem()
	{
		return null;
	}
}
