namespace EM.Foundation
{

using System.Threading;
using Cysharp.Threading.Tasks;

public interface IEnterState
{
	UniTask OnEnterAsync(CancellationToken ct);
}

}