namespace EM.Foundation
{

using System;

public interface IStateFactory<TState>
{
	Result<TState> Create(Type stateType);
}

}