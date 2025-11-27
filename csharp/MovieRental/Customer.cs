using System;
using System.Collections.Generic;
using System.Text;

namespace MovieRental
{
    public class Customer
    {

        private readonly string _name;
        private  int frequentRenterPoints = 0;
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
            var statement = new StringBuilder("Rental Record for " + _name + "\n");

            foreach (Rental rental in _rentals)
            {
                //determine amounts for each line
               double rentalPrice = DetermineRentalPrice(rental);
                
                // add bonus for a two day new release rental
                frequentRenterPoints += ComputeRenterPoint(rental);

                // show figures for this rental
                statement.Append("\t" + rental.getMovie().getTitle() + "\t" + rentalPrice + "\n");
                totalAmount += rentalPrice;
            }

            // add footer lines
            statement.Append("Amount owed is " + totalAmount + "\n");
            statement.Append("You earned " + frequentRenterPoints + " frequent renter points");

            return statement.ToString();

            
        }

        private static double DetermineRentalPrice(Rental each)
        {
            double currentAmount = 0;
            switch (each.getMovie().getPriceCode())
            {
                case Movie.REGULAR:
                    currentAmount += 2;
                    if (each.getDaysRented() > 2)
                        currentAmount += (each.getDaysRented() - 2) * 1.5;
                    break;
                case Movie.NEW_RELEASE:
                    currentAmount += each.getDaysRented() * 3;
                    break;
                case Movie.CHILDRENS:
                    currentAmount += 1.5;
                    if (each.getDaysRented() > 3)
                        currentAmount += (each.getDaysRented() - 3) * 1.5;
                    break;
            }

            return currentAmount;
        }
        
        int ComputeRenterPoint(Rental rental) 
            => ((rental.getMovie().getPriceCode() == Movie.NEW_RELEASE) && rental.getDaysRented() > 1) ? 2 : 1;
        
    }

}
