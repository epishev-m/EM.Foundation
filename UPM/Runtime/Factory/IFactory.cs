namespace EM.Foundation
{

public interface IFactory
{
	Result<object> Create();
}

public interface IFactory<T>
	where T : class
{
	Result<T> Create();
}

}