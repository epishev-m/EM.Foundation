namespace EM.Foundation
{

using System.Threading;
using Cysharp.Threading.Tasks;

public interface IPayloadEnterState<in T>
{
	UniTask OnEnterAsync(T payload, CancellationToken ct);
}

}