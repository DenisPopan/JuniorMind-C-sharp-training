
using Xunit;
using static Json.JsonNumber;

namespace Json.Facts
{
    public class JsonNumberFacts
    {
        [Fact]
        public void CanBeZero()
        {
            Assert.True(IsJsonNumber("0"));
        }

        [Fact]
        public void DoesNotContainLetters()
        {
            Assert.False(IsJsonNumber("a"));
        }

        [Fact]
        public void CanHaveASingleDigit()
        {
            Assert.True(IsJsonNumber("7"));
        }

        [Fact]
        public void CanHaveMultipleDigits()
        {
            Assert.True(IsJsonNumber("70"));
        }

        [Fact]
        public void DoesNotStartWithALetter()
        {
            Assert.False(IsJsonNumber("a820"));
        }

        [Fact]
        public void IsNotNull()
        {
            Assert.False(IsJsonNumber(null));
        }

        [Fact]
        public void IsNotAnEmptyString()
        {
            Assert.False(IsJsonNumber(string.Empty));
        }

        [Fact]
        public void DoesNotStartWithZero()
        {
            Assert.False(IsJsonNumber("07"));
        }

        [Fact]
        public void CanBeNegative()
        {
            Assert.True(IsJsonNumber("-26"));
        }

        [Fact]
        public void CanBeMinusZero()
        {
            Assert.True(IsJsonNumber("-0"));
        }

        [Fact]
        public void CannotBeMinusZeroZero()
        {
            Assert.False(IsJsonNumber("-00"));
        }

        [Fact]
        public void CanBeFractional()
        {
            Assert.True(IsJsonNumber("12.34"));
        }

        [Fact]
        public void TheFractionCanHaveLeadingZeros()
        {
            Assert.True(IsJsonNumber("0.00000001"));
            Assert.True(IsJsonNumber("10.00000001"));
        }

        [Fact]
        public void DoesNotEndWithADot()
        {
            Assert.False(IsJsonNumber("12."));
        }

        [Fact]
        public void DoesNotHaveTwoFractionParts()
        {
            Assert.False(IsJsonNumber("12.34.56"));
        }

        [Fact]
        public void TheDecimalPartDoesNotAllowLetters()
        {
            Assert.False(IsJsonNumber("12.3x"));
        }

        [Fact]
        public void CanHaveAnExponent()
        {
            Assert.True(IsJsonNumber("12e3"));
        }

        [Fact]
        public void TheExponentCanStartWithCapitalE()
        {
            Assert.True(IsJsonNumber("12E3"));
        }

        [Fact]
        public void TheExponentCanHavePositive()
        {
            Assert.True(IsJsonNumber("12e+3"));
        }

        [Fact]
        public void TheExponentCanBeNegative()
        {
            Assert.True(IsJsonNumber("61e-9"));
        }

        [Fact]
        public void CanHaveFractionAndExponent()
        {
            Assert.True(IsJsonNumber("12.34E3"));
        }

        [Fact]
        public void TheExponentDoesNotAllowLetters()
        {
            Assert.False(IsJsonNumber("22e3x3"));
        }

        [Fact]
        public void DoesNotHaveTwoExponents()
        {
            Assert.False(IsJsonNumber("22e323e33"));
        }

        [Fact]
        public void TheExponentIsAlwaysComplete()
        {
            Assert.False(IsJsonNumber("22e"));
            Assert.False(IsJsonNumber("22e+"));
            Assert.False(IsJsonNumber("23E-"));
        }

        [Fact]
        public void TheExponentIsAfterTheFraction()
        {
            Assert.False(IsJsonNumber("22e3.3"));
        }

        [Fact]
        public void FractionalPartStartsOnlyWithADigit()
        {
            Assert.False(IsJsonNumber("235.a3e12"));
            Assert.False(IsJsonNumber("665.e-8"));
            Assert.False(IsJsonNumber("67.-9"));
        }

        [Fact]
        public void ExponentCanBeZero()
        {
            Assert.True(IsJsonNumber("235.3e0"));
            Assert.True(IsJsonNumber("235.3e+0"));
            Assert.True(IsJsonNumber("235.3e-0"));
            Assert.True(IsJsonNumber("456.28e07"));
        }

        [Fact]
        public void ExponentCannotHaveMultipleSymbols()
        {
            Assert.False(IsJsonNumber("-4986.28e--7"));
            Assert.False(IsJsonNumber("135.28e++12"));
            Assert.False(IsJsonNumber("9.28ee12"));
            Assert.False(IsJsonNumber("96.28ee--901"));
            Assert.False(IsJsonNumber("58787.28e+-22"));
            Assert.False(IsJsonNumber("36.28e-+68"));
            Assert.False(IsJsonNumber("6.28e+12e"));
        }
    }
}
