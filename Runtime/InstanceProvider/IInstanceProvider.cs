namespace EM.Foundation
{

public interface IInstanceProvider
{
	object GetInstance();
}

public interface IInstanceProvider<T>
{
	Result<T> GetInstance();
}

}
