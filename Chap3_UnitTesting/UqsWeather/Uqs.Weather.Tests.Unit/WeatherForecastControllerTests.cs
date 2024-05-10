using Microsoft.Extensions.Logging.Abstractions;
using Uqs.Weather.Controllers;
using Xunit;

namespace Uqs.Weather.Tests.Unit;

public class WeatherForecastControllerTests
{
    [Fact]
    public void ConvertCToF_0Celsius_32Fahrenheit()
    {
        //Arrange
        const double expected = 32d;
        var logger = NullLogger<WeatherForecastController>.Instance;
        var controller = new WeatherForecastController(logger, null!, null!, null!, null!);
        //Act
        double actual = controller.ConvertCToF(0);
        //Assert
        Assert.Equal(expected, actual);
    }
    [Theory]
    [InlineData(-100 , -148)]
    [InlineData(-10.1, 13.8)]
    [InlineData(10   , 50)]
    public void ConvertCToF_Cel_CorrectFah(double c, double f)
    {
        //Arrange
        var logger = NullLogger<WeatherForecastController>.Instance;
        var controller = new WeatherForecastController(logger, null!, null!, null!, null!);
        //Act
        double actual = controller.ConvertCToF(c);
        //Assert
        Assert.Equal(f, actual, 1);
    }

    //Everything below is just an example and should not be present in this specific class

    //public void MethodUnderTest_Condition_Expectation
    //public void SaveData_CannotConnectToDB_InvalidOperationException
    //public void OrderShoppingBasket_EmptyBaskter_NoAction
    //Created using "aaa" code snippet
    [Fact]
    public void Method_Condition_Expectation()
    {
        // Arrange


        // Act


        // Assert

    }
    //For demonstration only, should not have write fields, if needed have readonly fields
    private int _instanceField = 0;
    private static int _staticField = 0;
    [Fact]
    public void UnitTest1()
    {
        _instanceField++;
        _staticField++;
        Assert.Equal(1, _instanceField);
        Assert.Equal(1, _staticField);
    }
    [Fact]
    public void UnitTest2()
    {
        _instanceField++;
        _staticField++;
        Assert.Equal(1, _instanceField);
        Assert.Equal(2, _staticField);
    }
    [Fact]
    public void UnitTest3_AssertShowcase() 
    {
        int expected = 2;
        int actual = 1+1;
        bool isPositive = actual > 0;
        bool isNegative = actual < 0;
        int[] collection = new int[] { 1, 2, 3 };
        int[] collectionNoTwo = new int[] { 1, 3, 4 };
        int[] collectionEmpty = new int[] { };
        Assert.Equal(expected, actual);
        Assert.True(isPositive);
        Assert.False(isNegative);
        Assert.Contains(expected, collection);
        Assert.DoesNotContain(expected, collectionNoTwo);
        Assert.Empty(collectionEmpty);
        Assert.IsType<int>(actual);

    }
    //Commented out because JsonParser class doesn't exist
    /*[Fact]
    public void Load_InvalidJson_FormatException()
    {
        // Arrange
        string input = "{not a valid JSON";

        // Act
        var exception = Record.Exception(
            () => JsonParser.Load(input)
        );

        // Assert
        Assert.IsType<FormatException>(exception);
    }*/
    [Fact]
    public void Divide_ZeroDivisor_DivideByZeroException()
    {
        // Arrange
        int dividend = 10;
        int divisor = 0;
        // Act
        Exception exception = Record.Exception(() => dividend / divisor);

        // Assert
        Assert.IsType<DivideByZeroException>(exception);

    }
}