
using Xunit;
using static Json.JsonNumber;

namespace Json.Facts
{
    public class JsonNumberFacts
    {
        [Fact]
        public void CanBeZero()
        {
            var number = new Number().Match("0");
            Assert.Equal((true, ""), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void DoesNotContainLetters()
        {
            var number = new Number().Match("a");
            Assert.Equal((false, "a"), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void CanHaveASingleDigit()
        {
            var number = new Number().Match("7");
            Assert.Equal((true, ""), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void CanHaveMultipleDigits()
        {
            var number = new Number().Match("70");
            Assert.Equal((true, ""), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void DoesNotStartWithALetter()
        {
            var number = new Number().Match("a820");
            Assert.Equal((false, "a820"), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void IsNotNull()
        {
            var number = new Number().Match(null);
            Assert.Equal((false, null), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void IsNotAnEmptyString()
        {
            var number = new Number().Match(string.Empty);
            Assert.Equal((false, ""), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void DoesNotStartWithZero()
        {
            var number = new Number().Match("07");
            Assert.Equal((true, "7"), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void CanBeNegative()
        {
            var number = new Number().Match("-26");
            Assert.Equal((true, ""), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void CanBeMinusZero()
        {
            var number = new Number().Match("-0");
            Assert.Equal((true, ""), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void CannotBeMinusZeroZero()
        {
            var number = new Number().Match("-00");
            Assert.Equal((true, "0"), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void CanBeFractional()
        {
            var number = new Number().Match("12.34");
            Assert.Equal((true, ""), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void TheFractionCanHaveLeadingZeros()
        {
            var number = new Number().Match("0.00000001");
            var number1 = new Number().Match("10.00000001");
            Assert.Equal((true, ""), (number.Success(), number.RemainingText()));
            Assert.Equal((true, ""), (number.Success(), number1.RemainingText()));
        }

        [Fact]
        public void DoesNotEndWithADot()
        {
            var number = new Number().Match("12.");
            Assert.Equal((true, "."), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void DoesNotHaveTwoFractionParts()
        {
            var number = new Number().Match("12.34.56");
            Assert.Equal((true, ".56"), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void TheDecimalPartDoesNotAllowLetters()
        {
            var number = new Number().Match("12.3x");
            Assert.Equal((true, "x"), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void CanHaveAnExponent()
        {
            var number = new Number().Match("12e3");
            Assert.Equal((true, ""), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void TheExponentCanStartWithCapitalE()
        {
            var number = new Number().Match("12E3");
            Assert.Equal((true, ""), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void TheExponentCanHavePositive()
        {
            var number = new Number().Match("12e+3");
            Assert.Equal((true, ""), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void TheExponentCanBeNegative()
        {
            var number = new Number().Match("61e-9");
            Assert.Equal((true, ""), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void CanHaveFractionAndExponent()
        {
            var number = new Number().Match("12.34E3");
            Assert.Equal((true, ""), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void TheExponentDoesNotAllowLetters()
        {
            var number = new Number().Match("22e3x3");
            Assert.Equal((true, "x3"), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void DoesNotHaveTwoExponents()
        {
            var number = new Number().Match("22e323e33");
            Assert.Equal((true, "e33"), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void TheExponentIsAlwaysComplete()
        {
            Assert.False(IsJsonNumber("22e"));
            Assert.False(IsJsonNumber("22e+"));
            Assert.False(IsJsonNumber("23E-"));

            var number1 = new Number().Match("22e");
            var number2 = new Number().Match("22e+");
            var number3 = new Number().Match("23E-");
            Assert.Equal((true, "e"), (number1.Success(), number1.RemainingText()));
            Assert.Equal((true, "e+"), (number2.Success(), number2.RemainingText()));
            Assert.Equal((true, "E-"), (number3.Success(), number3.RemainingText()));
        }

        [Fact]
        public void TheExponentIsAfterTheFraction()
        {
            var number = new Number().Match("22e3.3");
            Assert.Equal((true, ".3"), (number.Success(), number.RemainingText()));
        }

        [Fact]
        public void FractionalPartStartsOnlyWithADigit()
        {
            var number1 = new Number().Match("235.a3e12");
            var number2 = new Number().Match("665.3-8");
            var number3 = new Number().Match("67.-9");
            Assert.Equal((true, ".a3e12"), (number1.Success(), number1.RemainingText()));
            Assert.Equal((true, "-8"), (number2.Success(), number2.RemainingText()));
            Assert.Equal((true, ".-9"), (number3.Success(), number3.RemainingText()));
        }

        [Fact]
        public void ExponentCanBeZero()
        {
            var number1 = new Number().Match("235.3e0");
            var number2 = new Number().Match("235.3e+0");
            var number3 = new Number().Match("235.3e-0");
            var number4 = new Number().Match("456.28e07");

            Assert.Equal((true, ""), (number1.Success(), number1.RemainingText()));
            Assert.Equal((true, ""), (number2.Success(), number2.RemainingText()));
            Assert.Equal((true, ""), (number3.Success(), number3.RemainingText()));
            Assert.Equal((true, ""), (number4.Success(), number4.RemainingText()));
        }

        [Fact]
        public void ExponentCannotHaveMultipleSymbols()
        {
            var number1 = new Number().Match("-4986.28e--7");
            var number2 = new Number().Match("135.28e++12");
            var number3 = new Number().Match("9.28ee12");
            var number4 = new Number().Match("96.28ee--901");
            var number5 = new Number().Match("58787.28e+-22");
            var number6 = new Number().Match("36.28e-+68");
            var number7 = new Number().Match("6.28e+12e");


            Assert.Equal((true, "e--7"), (number1.Success(), number1.RemainingText()));
            Assert.Equal((true, "e++12"), (number2.Success(), number2.RemainingText()));
            Assert.Equal((true, "ee12"), (number3.Success(), number3.RemainingText()));
            Assert.Equal((true, "ee--901"), (number4.Success(), number4.RemainingText()));
            Assert.Equal((true, "e+-22"), (number5.Success(), number5.RemainingText()));
            Assert.Equal((true, "e-+68"), (number6.Success(), number6.RemainingText()));
            Assert.Equal((true, "e"), (number7.Success(), number7.RemainingText()));
        }
    }
}
