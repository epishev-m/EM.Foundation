namespace EM.Foundation
{

using System.Threading;
using Cysharp.Threading.Tasks;

public interface IExitState
{
	UniTask OnExitAsync(CancellationToken ct);
}

}