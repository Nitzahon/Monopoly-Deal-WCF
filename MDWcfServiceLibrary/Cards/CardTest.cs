using System;

namespace MDWcfServiceLibrary
{
    using NUnit.Framework;

    [TestFixture]
    public class CardTest
    {
        [Test]
        public void Card()
        {
            Card card = new Card("TestCard", "TestCardText", 1, CardType.Money);
            Assert.AreEqual(card.cardName, "TestCard");
            Assert.AreEqual(card.cardText, "TestCardText");
            Assert.AreEqual(card.cardValue, 1);
            Assert.AreEqual(card.cardType, CardType.Money);
            Assert.AreNotEqual(card.cardGuid, new Guid());
        }
    }
}