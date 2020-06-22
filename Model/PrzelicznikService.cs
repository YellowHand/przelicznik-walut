using System;
using Microsoft.AspNetCore.Mvc;

namespace PrzelicznikWalut.Model
{
    public class PrzelicznikService
    {

       string[,] przeliczniki = new string[,] {
            {"PLN", "USD", "0.25"},
            { "PLN", "EUR", "0.22"},
            { "USD", "PLN", "3.99"},
            { "EUR", "PLN", "4.46"}
        };

        public PrzelicznikService()
        {
        }

        public string Przelicz(PrzelicznikItem przelicznikItem)
        {
            if (przelicznikItem.walutaNa == null || "".Equals(przelicznikItem.walutaNa.Trim()))
            {
                throw new ArgumentException("WalutaNa nie ma wartosci");
            }
            if (przelicznikItem.walutaZ == null || "".Equals(przelicznikItem.walutaZ.Trim()))
            {
                throw new ArgumentException("WalutaZ nie ma wartosci");
            }
            if (przelicznikItem.wartosc <= 0)
            {
                throw new ArgumentException("Wartość musi byc wieksza od 0");
            }
            for (int i = 0; i < przeliczniki.GetLength(0); i++)
            {
                if (przeliczniki[i, 0].ToUpper().Equals(przelicznikItem.walutaZ.ToUpper()) &&
                    przeliczniki[i, 1].ToUpper().Equals(przelicznikItem.walutaNa.ToUpper())) {
                    double przelicznikDouble = Double.Parse(przeliczniki[i, 2]);
                    double wynik = przelicznikItem.wartosc * przelicznikDouble;
                    double wynikRounded = Math.Round(wynik, 2);
                    return wynikRounded.ToString("0.##");
                }
            }
            throw new ArgumentException("Nieobsługiwana para walut");
        }

    }
}
