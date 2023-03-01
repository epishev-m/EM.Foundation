namespace EM.Foundation
{

using System;
using System.Threading;
using Cysharp.Threading.Tasks;

public abstract class StateMachine<TState> : IStateMachine<TState>
{
	private readonly IStateFactory<TState> _stateFactory;

	private Type _currentStateType;

	private TState _currentState;

	#region IStateMachine

	public bool IsActive<T>()
		where T : class, TState
	{
		return _currentStateType == typeof(T);
	}

	public async UniTask<Result> EnterAsync<T>(CancellationToken ct = default)
		where T : class, TState, IEnterState
	{
		await UniTask.DelayFrame(1, PlayerLoopTiming.Update, ct);
		var result = await EnterInternalAsync<T>(CallEnterAsync, ct);

		return result;
	}

	public async UniTask<Result> EnterAsync<T, TPayload>(TPayload payload,
		CancellationToken ct = default)
		where T : class, TState, IPayloadEnterState<TPayload>
	{
		await UniTask.DelayFrame(1, PlayerLoopTiming.Update, ct);
		var result = await EnterInternalAsync<T>(ExecuteAsync, ct);

		return result;

		UniTask ExecuteAsync(TState state,
			CancellationToken ctt)
		{
			return CallEnterAsync(state, typeof(TPayload), payload, ctt);
		}
	}

	#endregion

	#region StateMachine

	protected StateMachine(IStateFactory<TState> stateFactory)
	{

		Requires.NotNullParam(stateFactory, nameof(stateFactory));

		_stateFactory = stateFactory;
	}

	private async UniTask<Result> EnterInternalAsync<T>(Func<TState, CancellationToken, UniTask> callEnter,
			CancellationToken ct) 
		where T : class, TState
	{
		if (IsActive<T>())
		{
			return new ErrorResult(StateMachineStringResources.StateIsAlreadyActive(this));
		}

		if (_currentStateType != null)
		{
			var currentStateResult = GetStateInstance(_currentStateType);

			if (currentStateResult.Data is IExitState exitState)
			{
				await exitState.OnExitAsync(ct);
			}
		}

		var stateResult = GetStateInstance(typeof(T));

		if (stateResult.Failure)
		{
			return new ErrorResult(StateMachineStringResources.CannotFindState(this));
		}

		_currentState = stateResult.Data;
		_currentStateType = _currentState.GetType();
		await callEnter(_currentState, ct);

		return new SuccessResult();
	}

	private static async UniTask CallEnterAsync(TState state,
		CancellationToken ct)
	{
		if (state is IEnterState enterState)
		{
			await enterState.OnEnterAsync(ct);
		}
	}

	private static async UniTask CallEnterAsync(TState state,
		Type payloadType,
		object payload,
		CancellationToken ct)
	{
		var stateGenericType = typeof(IPayloadEnterState<>).MakeGenericType(payloadType);

		if (!stateGenericType.IsInstanceOfType(state))
		{
			throw new StateException(StateMachineStringResources.StateInstanceOf(
				state.GetType().FullName, payloadType.Name));
		}

		var enterMethod = stateGenericType.GetMethod("OnEnterAsync");

		if (enterMethod == null)
		{
			throw new StateException(StateMachineStringResources.CannotFindOnEnterMethod(state.GetType().FullName));
		}

		var task = (UniTask<Result>) enterMethod.Invoke(state, new[] {payload, ct});
		await task;
	}

	private Result<TState> GetStateInstance(Type stateType)
	{
		if (stateType == null)
		{
			return new ErrorResult<TState>(StateMachineStringResources.PassedTypeIsNull(this));
		}

		var result = _stateFactory.Create(stateType);

		if (result.Failure)
		{
			return new ErrorResult<TState>(StateMachineStringResources.CannotFindState(this));
		}

		return result;
	}

	#endregion
}

}