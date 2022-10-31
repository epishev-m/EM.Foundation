﻿using System;
using System.Collections.Generic;
using EM.Foundation;
using NUnit.Framework;

public sealed class NotFoundResultTests
{
	[Test]
	public void NotFoundResult_SuccessAndFailure()
	{
		// Arrange
		var result = new NotFoundResult<Test>(null, null);

		// Act
		var actualSuccess = result.Success;
		var actualFailure = result.Failure;

		//Assert
		Assert.IsFalse(actualSuccess);
		Assert.IsTrue(actualFailure);
	}

	[Test]
	public void NotFoundResult_Message()
	{
		// Arrange
		const string expected = "message";
		var result = new NotFoundResult<Test>(expected, null);

		// Act
		var actual = result.Message;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void NotFoundResult_Errors()
	{
		// Arrange
		var errors = new List<Error>()
		{
			new(null, null)
		};

		var expected = errors.AsReadOnly();
		var result = new NotFoundResult<Test>(null, expected);

		// Act
		var actual = result.Errors;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void NotFoundResult_Errors_NotNull()
	{
		// Arrange
		var result = new NotFoundResult<Test>(null, null);

		// Act
		var actual = result.Errors;

		//Assert
		Assert.IsNotNull(actual);
	}

	[Test]
	public void NotFoundResult_Data_Exception()
	{
		// Arrange
		var actual = false;
		var result = new NotFoundResult<Test>(null, null);

		// Act
		try
		{
			var unused = result.Data;
		}
		catch (InvalidOperationException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	#region Nested

	// ReSharper disable once ClassNeverInstantiated.Local
	private sealed class Test
	{
	}

	#endregion
}