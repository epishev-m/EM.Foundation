namespace EM.Foundation
{
public interface IFactory
{
	bool TryCreate(
		out object instance);
}

}