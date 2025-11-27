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

            foreach (Rental rental in _rentals)
            {
                double rentalPrice = 0;

                //determine amounts for each line
                rentalPrice = DetermineRentalPrice(rental, rentalPrice);


                // add bonus for a two day new release rental
                frequentRenterPoints += ((rental.getMovie().getPriceCode() == Movie.NEW_RELEASE) && rental.getDaysRented() > 1) ? 2 : 1;

                // show figures for this rental
                statement.Append("\t" + rental.getMovie().getTitle() + "\t" + rentalPrice + "\n");
                totalAmount += rentalPrice;
            }

            // add footer lines
            statement.Append("Amount owed is " + totalAmount + "\n");
            statement.Append("You earned " + frequentRenterPoints + " frequent renter points");

            return statement.ToString();
        }

        private static double DetermineRentalPrice(Rental each, double thisAmount)
        {
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

            return thisAmount;
        }
    }

}
