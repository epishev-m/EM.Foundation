namespace EM.Foundation
{

public interface IInstanceProvider
{
	Result<object> GetInstance();
}

public interface IInstanceProvider<T>
{
	Result<T> GetInstance();
}

}
