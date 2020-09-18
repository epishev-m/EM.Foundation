﻿using System;

namespace EM.Foundation
{
	public interface ICommand
	{
		event Action Done;

		object Data
		{
			set;
		}

		bool IsDone
		{
			get;
		}

		bool IsFailed
		{
			get;
		}

		void Execute();

		void Clear();
	}
}
