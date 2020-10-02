using Xunit;

namespace Json.Facts
{
    public class ValueFacts
    {
        [Fact]

        public void ValueCanNotBeNull()
        {
            IMatch match = new Value().Match(null);
            Assert.Equal((false, null), (match.Success(), match.RemainingText()));
        }

        [Fact]

        public void ValueCanBeNullText()
        {
            IMatch match = new Value().Match("null");
            Assert.Equal((true, ""), (match.Success(), match.RemainingText()));
        }

        [Fact]

        public void ValueCanBeABoolean()
        {
            IMatch match1 = new Value().Match("true");
            IMatch match2 = new Value().Match("false");
            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, ""), (match2.Success(), match2.RemainingText()));
        }

        [Fact]

        public void ValueCanBeANumber()
        {
            IMatch match1 = new Value().Match("654");
            IMatch match2 = new Value().Match("235.2353");
            IMatch match3 = new Value().Match("9847e23554");
            IMatch match4 = new Value().Match("234.345e66");
            IMatch match5 = new Value().Match("654.");
            IMatch match6 = new Value().Match("235ee");
            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, ""), (match2.Success(), match2.RemainingText()));
            Assert.Equal((true, ""), (match3.Success(), match3.RemainingText()));
            Assert.Equal((true, ""), (match4.Success(), match4.RemainingText()));
            Assert.Equal((true, "."), (match5.Success(), match5.RemainingText()));
            Assert.Equal((true, "ee"), (match6.Success(), match6.RemainingText()));
        }

        [Fact]

        public void ValueCanBeAString()
        {
            IMatch match1 = new Value().Match("\"ajshf\"");
            IMatch match2 = new Value().Match("\"}]@#$^%^*&$\"");
            IMatch match3 = new Value().Match("\"!][]]';/.,><\"");
            IMatch match4 = new Value().Match("\"⛅⚾\"");
            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, ""), (match2.Success(), match2.RemainingText()));
            Assert.Equal((true, ""), (match3.Success(), match3.RemainingText()));
            Assert.Equal((true, ""), (match4.Success(), match4.RemainingText()));
        }

        [Fact]

        public void ValueCanNotBeAStringWithoutQuotes()
        {
            IMatch match1 = new Value().Match("@$%#");
            Assert.Equal((false, "@$%#"), (match1.Success(), match1.RemainingText()));
        }        
        
        [Fact]

        public void ArrayShouldHaveSquareBrackets()
        {
            IMatch match1 = new Value().Match("[ \"hey\" ");
            IMatch match2 = new Value().Match(" \"hey\" ]");

            Assert.Equal((false, "[ \"hey\" "), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, "]"), (match2.Success(), match2.RemainingText()));
        }

        [Fact]

        public void ValueCanBeAnEmptyArray()
        {
            IMatch match1 = new Value().Match("[ ]");

            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
        }

        [Fact]

        public void ArrayWithOneElementShouldReturnTrue()
        {
            IMatch match1 = new Value().Match("[ \"Rainbow\" ]");

            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
        }

        [Fact]

        public void ArrayWithMultipleElementsShouldReturnTrue()
        {
            IMatch match1 = new Value().Match("[ \"Ford\", \"BMW\", \"Fiat\" ]");

            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
        }

        [Fact]

        public void IncorrectArrayElementsShouldReturnFalse()
        {
            IMatch match1 = new Value().Match("[ \"Ford\", \"BM\"W\", \"Fiat\" ]");
            IMatch match2 = new Value().Match("[ 203e, \"BMW\", \"Fiat\" ]");
            IMatch match3 = new Value().Match("[ \"Rainb\\ow\" ]");
            IMatch match4 = new Value().Match("[ * ]");

            Assert.Equal((false, "[ \"Ford\", \"BM\"W\", \"Fiat\" ]"), (match1.Success(), match1.RemainingText()));
            Assert.Equal((false, "[ 203e, \"BMW\", \"Fiat\" ]"), (match2.Success(), match2.RemainingText()));
            Assert.Equal((false, "[ \"Rainb\\ow\" ]"), (match3.Success(), match3.RemainingText()));
            Assert.Equal((false, "[ * ]"), (match4.Success(), match4.RemainingText()));
        }

        [Fact]
        public void ArrayWithoutCommasBetweenElementsShouldReturnFalse()
        {
            IMatch match1 = new Value().Match("[ \"Ford\" \"BMW\", \"Fiat\" ]");
            IMatch match2 = new Value().Match("[ \"Ford\", \"BMW\", \"Fiat\" \"Audi\" ]");

            Assert.Equal((false, "[ \"Ford\" \"BMW\", \"Fiat\" ]"), (match1.Success(), match1.RemainingText()));
            Assert.Equal((false, "[ \"Ford\", \"BMW\", \"Fiat\" \"Audi\" ]"), (match2.Success(), match2.RemainingText()));
        }

        [Fact]

        public void NestedArraysShouldReturnTrue()
        {
            IMatch match = new Value().Match("[ [ \"Fiesta\", \"Focus\", \"Mustang\" ]," +
                " [ \"320\", \"X3\", \"X5\" ], " +
                "[ \"500\", \"Panda\" ] ]");
            Assert.Equal((true, ""), (match.Success(), match.RemainingText()));
        }

        [Fact]
        public void NestedArraysWithErrorsShouldReturnFalse()
        {
            IMatch match = new Value().Match("[ [ \"Fiesta\", \"Focus\", \"Mustang\" ]," +
                " [ \"320\", \"X3\", \"X5\" ], " +
                "[ \"500\" \"Panda\" ] ]");
            IMatch match1 = new Value().Match("[ [ \"Fiesta\", \"Focus\", \"Mustang\" ]," +
                " [ \"320\", \"X3\", \"X5\" ], " +
                "[ \"5\\00\", \"Panda\" ] ]");
            IMatch match2 = new Value().Match("[ [ \"Fiesta\", \"Focus\", \"Mustang\" ]," +
                " [ \"320\", \"X3\", \"X5\" , " +
                "[ \"500\", \"Panda\" ] ]");
            Assert.Equal((false, "[ [ \"Fiesta\", \"Focus\", \"Mustang\" ]," +
                " [ \"320\", \"X3\", \"X5\" ], " +
                "[ \"500\" \"Panda\" ] ]"), (match.Success(), match.RemainingText()));
            Assert.Equal((false, "[ [ \"Fiesta\", \"Focus\", \"Mustang\" ]," +
                " [ \"320\", \"X3\", \"X5\" ], " +
                "[ \"5\\00\", \"Panda\" ] ]"), (match1.Success(), match1.RemainingText()));
            Assert.Equal((false, "[ [ \"Fiesta\", \"Focus\", \"Mustang\" ]," +
                " [ \"320\", \"X3\", \"X5\" , " +
                "[ \"500\", \"Panda\" ] ]"), (match2.Success(), match2.RemainingText()));
        }

        [Fact]

        public void ValueCanBeAnEmptyObject()
        {
            IMatch match1 = new Value().Match("{ }");

            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
        }

        [Fact]

        public void ObjectShouldHaveCurlyBrackets()
        {
            IMatch match1 = new Value().Match("{ \"name\":\"John\" ");
            IMatch match2 = new Value().Match(" \"name\":\"John\" }");

            Assert.Equal((false, "{ \"name\":\"John\" "), (match1.Success(), match1.RemainingText()));
            Assert.Equal((true, ":\"John\" }"), (match2.Success(), match2.RemainingText()));
        }

        [Fact]

        public void ObjectWithOneMmemberShouldReturnTrue()
        {
            IMatch match1 = new Value().Match("{ \"Weather\" : \"Rain\" }");

            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
        }

        [Fact]

        public void ObjectWithMultipleMembersShouldReturnTrue()
        {
            IMatch match1 = new Value().Match("{ \"name\":\"John\", \"age\":30, \"car\":null }");

            Assert.Equal((true, ""), (match1.Success(), match1.RemainingText()));
        }

        [Fact]

        public void IncorrectObjectMembersShouldReturnFalse()
        {
            IMatch match1 = new Value().Match("{ \"name\":\"John\", \"age\":30., \"car\":null }");
            IMatch match2 = new Value().Match("{ \"Weather\" : \"Ra\\in\" }");
            IMatch match3 = new Value().Match("{ * }");

            Assert.Equal((false, "{ \"name\":\"John\", \"age\":30., \"car\":null }"), (match1.Success(), match1.RemainingText()));
            Assert.Equal((false, "{ \"Weather\" : \"Ra\\in\" }"), (match2.Success(), match2.RemainingText()));
            Assert.Equal((false, "{ * }"), (match3.Success(), match3.RemainingText()));
        }

        [Fact]
        public void ObjectWithPatternErrorsShouldReturnFalse()
        {
            IMatch match1 = new Value().Match("{ \"name\":\"John\" \"age\":30, \"car\":null }");
            IMatch match2 = new Value().Match("{ \"name\":\"John\", \"age\":30, \"car\"null }");

            Assert.Equal((false, "{ \"name\":\"John\" \"age\":30, \"car\":null }"), (match1.Success(), match1.RemainingText()));
            Assert.Equal((false, "{ \"name\":\"John\", \"age\":30, \"car\"null }"), (match2.Success(), match2.RemainingText()));
        }

        [Fact]

        public void NestedObjectsShouldReturnTrue()
        {
            IMatch match = new Value().Match("{\"name\":\"Jhon\"," +
                                              "\"age\":30," +
                                              "\"cars\": {" +
                                              "\"car1\":\"Ford\"," +
                                              "\"car2\":\"BMW\"," +
                                              "\"car3\":\"Fiat\"}}");
            Assert.Equal((true, ""), (match.Success(), match.RemainingText()));
        }

        [Fact]
        public void NestedObjectsWithErrorsShouldReturnFalse()
        {
            IMatch match = new Value().Match("{\"name\":\"Jhon\"" +
                                              "\"age\":30," +
                                              "\"cars\": {" +
                                              "\"car1\":\"Ford\"," +
                                              "\"car2\":\"BMW\"," +
                                              "\"car3\":\"Fiat\"}}");

            Assert.Equal((false, "{\"name\":\"Jhon\"" +
                                  "\"age\":30," +
                                  "\"cars\": {" +
                                  "\"car1\":\"Ford\"," +
                                  "\"car2\":\"BMW\"," +
                                  "\"car3\":\"Fiat\"}}"), (match.Success(), match.RemainingText()));

            IMatch match1 = new Value().Match("{\"name\":\"Jhon\"," +
                                               "\"age\":30," +
                                               "\"cars\" {" +
                                               "\"car1\":\"Ford\"," +
                                               "\"car2\":\"BMW\"," +
                                               "\"car3\":\"Fiat\"}}");
            
            Assert.Equal((false, "{\"name\":\"Jhon\"," +
                                  "\"age\":30," +
                                  "\"cars\" {" +
                                  "\"car1\":\"Ford\"," +
                                  "\"car2\":\"BMW\"," +
                                  "\"car3\":\"Fiat\"}}"), (match1.Success(), match1.RemainingText()));

            IMatch match2 = new Value().Match("{\"name\":\"Jhon\"," +
                                               "\"age\":30," +
                                               "\"cars\": {" +
                                               "\"car1\":\"Ford\"," +
                                               "\"car2\":\"BMW\"," +
                                               "\"car3\":\"Fi\\at\"}}");

            Assert.Equal((false, "{\"name\":\"Jhon\"," +
                                  "\"age\":30," +
                                  "\"cars\": {" +
                                  "\"car1\":\"Ford\"," +
                                  "\"car2\":\"BMW\"," +
                                  "\"car3\":\"Fi\\at\"}}"), (match2.Success(), match2.RemainingText()));
        }

        [Fact]

        public void ObjectContainingAnArrayAndViceVersaShouldReturnTrue()
        {
            IMatch match = new Value().Match("{\"menu\": {" +
                                              "\"header\": \"SVG Viewer\", " +
                                              "\"items\": [" +
                                              "{ \"id\": \"Open\"}, " +
                                              "{ \"id\": \"OpenNew\", \"label\": \"Open New\"}]}}");
            Assert.Equal((true, ""), (match.Success(), match.RemainingText()));
        }
    }
}
