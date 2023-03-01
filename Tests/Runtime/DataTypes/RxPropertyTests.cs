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
	public void RxProperty_Subscribe()
	{
		// Arrange
		var actual = 0;

		// Act
		var rxProperty = new RxProperty<int>
		{
			Value = 1
		};

		rxProperty.Subscribe(i =>
		{
			actual = i;
		});

		//Assert
		Assert.AreEqual(1, actual);
	}

	[Test]
	public void RxProperty_SetValue_Subscribe()
	{
		// Arrange
		var actual = 0;

		// Act
		var rxProperty = new RxProperty<int>();
		rxProperty.Subscribe(i =>actual = i);
		rxProperty.Value = 1;

		//Assert
		Assert.AreEqual(1, actual);
	}

	[Test]
	public void RxProperty_Subscribe_SetValue()
	{
		// Arrange
		const int value = 1;
		var actual = 0;

		// Act
		var rxProperty = new RxProperty<int>
		{
			Value = 0
		};

		rxProperty.Subscribe(i =>
		{
			actual = i;
		});

		rxProperty.Value = value;

		//Assert
		Assert.AreEqual(1, actual);
	}

	[Test]
	public void RxProperty_UnSubscribe()
	{
		// Arrange
		const int value = 1;
		var actual = 0;

		void Test(int i) => actual = i;

		// Act
		var rxProperty = new RxProperty<int>();
		rxProperty.Subscribe(Test);
		rxProperty.UnSubscribe(Test);
		rxProperty.Value = value;

		//Assert
		Assert.AreEqual(0, actual);
	}
}