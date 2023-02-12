using System;

[Serializable]
public class StatModifier
{
	public float Value { get; private set; }
	public StatModType Type { get; private set; }
	public int Order { get; private set; }
	public object Source { get; private set; }

	public StatModifier(float value, StatModType type, int order, object source)
	{
		Value = value;
		Type = type;
		Order = order;
		Source = source;
	}

	public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}
