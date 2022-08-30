namespace EM.Foundation
{

using System;

[Serializable]
public abstract class ConfigLink
{
	public readonly Type Type;

	public readonly string Name;

	protected ConfigLink(Type entryType,
		string name)
	{
		Requires.NotNull(entryType, nameof(entryType));
		Requires.ValidArgument(!string.IsNullOrWhiteSpace(name), "Name cannot be empty or null.");

		Type = entryType;
		Name = name;
	}
}

[Serializable]
public sealed class ConfigLink<T> :
	ConfigLink
{
	public ConfigLink(string name)
		: base(typeof(T), name)
	{
	}
}

}