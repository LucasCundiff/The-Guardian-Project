using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionSlot : MonoBehaviour
{
	public WeaponItem CurrentWeapon { get; private set; }
	public Skill CurrentSkill { get; private set; }

	[SerializeField] protected Image weaponImage;
	[SerializeField] protected Image skillImage;
	[SerializeField] protected Image border;

	[SerializeField] protected Sprite defaultWeaponSprite;
	[SerializeField] protected Sprite defaultSkillSprite;

	[SerializeField] protected Color skillActiveColor;
	[SerializeField] protected Color weaponActiveColor;
	[SerializeField] protected Color inactiveColor;

	public void SetWeapon(WeaponItem weaponItem)
	{
		CurrentWeapon = weaponItem;
		weaponImage.sprite = CurrentWeapon ? CurrentWeapon.ItemSprite : defaultWeaponSprite;
		weaponImage.color = CurrentWeapon ? weaponActiveColor : inactiveColor;
	}

	public void SetSkill(Skill skill)
	{
		CurrentSkill = skill;
		skillImage.sprite = CurrentSkill ? CurrentSkill.SkillSprite : defaultSkillSprite;
		skillImage.color = CurrentSkill ? skillActiveColor : inactiveColor;
	}

	public void ToggleBorder(bool state) =>	border.enabled = state;
}
