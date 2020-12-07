using System;
using BibliotekaKlas;
using NUnit.Framework;

namespace SerwerTCP.UnitTest
{
    public class TriangleTypeTest
    {
        [Test]
        public void TriangleType_ValidInput_ReturnTrue()
        {
            TriangleType triangleType = new TriangleType("21 20 20");

            Assert.That(triangleType.is_match, Is.True);
        }
        [Test]
        public void TriangleType_InvalidInput_ReturnFalse()
        {
            TriangleType triangleType = new TriangleType("212020");

            Assert.That(triangleType.is_match, Is.False);
        }
        [Test]
        public void CheckType_ValidInput_ShouldEqual()
        {
            TriangleType triangleType = new TriangleType("20 20 20");

            var result = triangleType.type;
            Console.WriteLine(result);
            Assert.AreEqual(result, "Trojkat ostrokatny\r\n");
        }
        [Test]
        public void CheckType_ValidInput2_ShouldEqual()
        {
            TriangleType triangleType = new TriangleType("30 20 20");

            var result = triangleType.type;
            Console.WriteLine(result);
            Assert.AreEqual(result, "Trojkat rozwartokatny\r\n");
        }
        [Test]
        public void CheckType_ValidInput3_ShouldEqual()
        {
            TriangleType triangleType = new TriangleType("15 20 25");

            var result = triangleType.type;
            Console.WriteLine(result);
            Assert.AreEqual(result, "Trojkat prostokatny\r\n");
        }
        [Test]
        public void CheckType_InvalidInput_ShouldBeNull()
        {
            TriangleType triangleType = new TriangleType("15 30 45");

            var result = triangleType.type;
            Console.WriteLine(result);
            Assert.AreEqual(result, null);
        }
    }
}
