using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Test Skill")]
public class TestSkill : Skill
{
	public List<Sprite> possibleSprites = new List<Sprite>();

	protected Skill _skillCache;

	public override Skill GetCopy()
	{
		_skillCache = Instantiate(this);
		_skillCache.SkillSprite = possibleSprites[Random.Range(0, possibleSprites.Count)];
		return _skillCache;
	}
}
