using EM.Foundation;
using NUnit.Framework;

public sealed class ObservableFieldTests
{
	[Test]
	public void ObservableField_GetAndSetValue()
	{
		// Arrange
		const int expected = 10;

		// Act
		var observableField = new ObservableField<int>();
		observableField.SetValue(expected);
		var actual = observableField.Value;

		//Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void ObservableField_Subscribe()
	{
		// Arrange
		var actual = 0;

		// Act
		var observableField = new ObservableField<int>();
		observableField.SetValue(1);
		observableField.OnChanged += value => actual = value;

		//Assert
		Assert.AreEqual(0, actual);
	}

	[Test]
	public void ObservableField_SetValue_Subscribe()
	{
		// Arrange
		var actual = 0;

		// Act
		var observableField = new ObservableField<int>();
		observableField.OnChanged += value => actual = value;
		observableField.SetValue(1);

		//Assert
		Assert.AreEqual(1, actual);
	}

	[Test]
	public void ObservableField_Subscribe_SetValue()
	{
		// Arrange
		const int value = 1;
		var actual = 0;

		// Act
		var observableField = new ObservableField<int>();
		observableField.SetValue(0);
		observableField.OnChanged += v => actual = v;
		observableField.SetValue(value);

		//Assert
		Assert.AreEqual(1, actual);
	}

	[Test]
	public void ObservableField_UnSubscribe()
	{
		// Arrange
		const int value = 1;
		var actual = 0;

		// Act
		var rxProperty = new ObservableField<int>();
		rxProperty.OnChanged += Test;
		rxProperty.OnChanged -= Test;
		rxProperty.SetValue(value);

		void Test(int v) => actual = v;

		//Assert
		Assert.AreEqual(0, actual);
	}
}