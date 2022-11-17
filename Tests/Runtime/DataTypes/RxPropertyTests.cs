using EM.Foundation;
using NUnit.Framework;

public sealed class RxPropertyTests
{
	[Test]
	public void RxProperty_GetAndSetValue()
	{
		// Arrange
		const int expected = 10;

		// Act
		var rxProperty = new RxProperty<int>
		{
			Value = expected
		};

		var actual = rxProperty.Value;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void RxProperty_SetValue_Subscribe()
	{
		// Arrange
		var actual = 0;

		// Act
		var rxProperty = new RxProperty<int>();

		rxProperty.Subscribe(() =>
		{
			actual++;
		});

		rxProperty.Value = 10;

		//Assert
		Assert.AreEqual(2, actual);
	}

	[Test]
	public void RxProperty_Subscribe_NoChange()
	{
		// Arrange
		const int value = 10;
		var actual = 0;

		// Act
		var rxProperty = new RxProperty<int>
		{
			Value = value
		};

		rxProperty.Subscribe(() =>
		{
			actual++;
		});

		rxProperty.Value = value;

		//Assert
		Assert.AreEqual(1, actual);
	}

	[Test]
	public void RxProperty_UnSubscribe()
	{
		// Arrange
		const int value = 10;
		var actual = 0;

		void Test() => actual++;

		// Act
		var rxProperty = new RxProperty<int>();
		rxProperty.Subscribe(Test);
		rxProperty.UnSubscribe(Test);
		rxProperty.Value = value;

		//Assert
		Assert.AreEqual(1, actual);
	}
}