namespace EM.Foundation.Editor
{

using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public sealed class UnionConverter : JsonConverter
{
	public override bool CanWrite => false;

	public override bool CanConvert(Type objectType)
	{
		return Attribute.GetCustomAttributes(objectType).Any(v => v is UnionAttribute);
	}

	public override object ReadJson(JsonReader reader,
		Type objectType,
		object existingValue,
		JsonSerializer serializer)
	{
		var jObject = JObject.Load(reader);
		var attrs = Attribute.GetCustomAttributes(objectType);

		foreach (var attribute in attrs)
		{
			if (attribute is not UnionAttribute unionAttribute)
			{
				continue;
			}

			var fields = unionAttribute.Type.GetFields();
			var found = true;

			foreach (var obj in jObject)
			{
				if (fields.Any(z => z.Name == obj.Key))
				{
					continue;
				}

				found = false;

				break;
			}

			if (!found)
			{
				continue;
			}

			var target = Activator.CreateInstance(unionAttribute.Type);
			serializer.Populate(jObject.CreateReader(), target);

			return target;
		}

		throw new InvalidOperationException();
	}

	public override void WriteJson(JsonWriter writer,
		object value,
		JsonSerializer serializer)
	{
		throw new NotImplementedException();
	}
}

}