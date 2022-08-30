namespace EM.Foundation
{

using System.Threading;
using Cysharp.Threading.Tasks;

public interface IStateMachine<in TState>
{
	bool IsActive<T>()
		where T : class, TState;
	
	UniTask EnterAsync<T>(CancellationToken ct = default)
		where T : class, TState, IEnterState;
	
	UniTask EnterAsync<T, TPayload>(TPayload payload,
		CancellationToken ct = default)
		where T : class, TState, IPayloadEnterState<TPayload>;
}

}