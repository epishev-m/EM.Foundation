namespace EM.Foundation
{

using System.Threading;
using Cysharp.Threading.Tasks;

public interface IPayloadEnterState<T>
{
	UniTask OnEnterAsync(T payload, CancellationToken ct);
}

}