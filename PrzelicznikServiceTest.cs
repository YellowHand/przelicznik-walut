using System;
using PrzelicznikWalut.Model;
using NUnit.Framework;

namespace PrzelicznikWalut
{
    [TestFixture]
    public class PrzelicznikServiceTest
    {

        PrzelicznikService service;

       
        [OneTimeSetUp]
        public void Init()
        {
            service = new PrzelicznikService();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            service = null;
        }

        [TestCase("PLN", "USD", 1013.11, "253.28")]
        [TestCase("PLN", "USD", 2500, "625")]
        [TestCase("PLN", "USD", 100, "25")]
        [TestCase("USD", "PLN", 548, "2186.52")]
        [TestCase("USD", "PLN", 22.50, "89.78")]
        [TestCase("USD", "PLN", 15000, "59850")]
        [TestCase("PLN", "EUR", 100, "22")]
        [TestCase("PLN", "EUR", 2041.42, "449.11")]
        [TestCase("PLN", "EUR", 525, "115.5")]
        [TestCase("EUR", "PLN", 750.450001, "3347.01")]
        [TestCase("EUR", "PLN", 3000, "13380")]
        [TestCase("EUR", "PLN", 7500, "33450")]
        public void przeliczWalute_RozneWartosci_Ok(string walutaZ, string walutaNa, double wartosc, string wynik)
        {
            //Arrange
            PrzelicznikItem item = new PrzelicznikItem();
            item.walutaZ = walutaZ;
            item.walutaNa = walutaNa;
            item.wartosc = wartosc;

            //Act
            string result = service.Przelicz(item);

            //Assert
            Assert.AreEqual(wynik, result);
        }

        [TestCase("NOK", "PLN", 100)]
        [TestCase("PLN", "NOK", 100)]
        [TestCase("", "PLN", 100)]
        [TestCase("NOK", "", 100)]
        [TestCase("", "", null)]
        [TestCase("  ", "PLN", 100)]
        [TestCase("NOK", " ", 100)]
        [TestCase(null, "PLN", 100)]
        [TestCase("NOK", null, 100)]
        [TestCase("NOK", null, null)]
        [TestCase("dupa", null, null)]
        [TestCase(null, null, null)]
        [TestCase("NOK", "PLN", -100)]
        [TestCase("EUR", "PLN", 0)]
        public void przeliczWalute_NiepoprawneWartosci_Wyjatek(string walutaZ, string walutaNa, double wartosc)
        {
            //Arrange
            PrzelicznikItem item = new PrzelicznikItem();
            item.walutaZ = walutaZ;
            item.walutaNa = walutaNa;
            item.wartosc = wartosc;

            //Act, Assert
            Assert.That(() => service.Przelicz(item), Throws.TypeOf<ArgumentException>());
        }
    }
}

