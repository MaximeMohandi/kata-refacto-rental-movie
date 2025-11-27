using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRental
{
    public class Customer
    {

        private readonly string _name;
        private readonly List<Rental> _rentals = new List<Rental>();

        public Customer(string name)
        {
            _name = name;
        }

        public void AddRental(Rental arg)
        {
            _rentals.Add(arg);
        }

        public string Statement()
        {
            double totalAmount = 0;
            int frequentRenterPoints = 0;
            var statement = new StringBuilder("Rental Record for " + _name + "\n");

            foreach (Rental each in _rentals)
            {
                double thisAmount = 0;

                //determine amounts for each line
                switch (each.getMovie().getPriceCode())
                {
                    case Movie.REGULAR:
                        thisAmount += 2;
                        if (each.getDaysRented() > 2)
                            thisAmount += (each.getDaysRented() - 2) * 1.5;
                        break;
                    case Movie.NEW_RELEASE:
                        thisAmount += each.getDaysRented() * 3;
                        break;
                    case Movie.CHILDRENS:
                        thisAmount += 1.5;
                        if (each.getDaysRented() > 3)
                            thisAmount += (each.getDaysRented() - 3) * 1.5;
                        break;
                }

                // add frequent renter points
                frequentRenterPoints++;
                // add bonus for a two day new release rental
                if ((each.getMovie().getPriceCode() == Movie.NEW_RELEASE) && each.getDaysRented() > 1)
                    frequentRenterPoints++;

                // show figures for this rental
                statement.Append("\t" + each.getMovie().getTitle() + "\t" + thisAmount + "\n");
                totalAmount += thisAmount;
            }

            // add footer lines
            statement.Append("Amount owed is " + totalAmount + "\n");
            statement.Append("You earned " + frequentRenterPoints + " frequent renter points");

            return statement.ToString();
        }
    }

}
