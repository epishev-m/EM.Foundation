using System;

namespace EM.Foundation
{
	public interface ICommand
	{
		object Data
		{
			set;
		}

		event Action Done;

		void Execute();
	}
}
