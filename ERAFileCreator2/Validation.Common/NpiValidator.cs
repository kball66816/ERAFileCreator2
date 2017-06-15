using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;

namespace EFC.BL
{
    public class NPIValidator:ValidationRule
    {
        public void ParseNpi(string npi)
        {
            //Represent NPI as an array 
            List<int> number = new List<int>();
            int sum = 0;


            foreach (char a in npi)
            {
                number.Add((int)Char.GetNumericValue(a));
            }

            if (number.Count == 10)
            {
                //Double value of alternating digits, add the sum of their resulting product's digits
                sum = SumDigits(number[8] * 2) + SumDigits(number[6] * 2) + SumDigits(number[4] * 2) + SumDigits(number[2] * 2) + SumDigits(number[0] * 2) + 24;
                sum += number[1] + number[3] + number[5] + number[7];

                //Subtract the next highest number divisible by 10 from the sum (gives us check digit)
                if (sum % 10 != 0)
                {
                    sum = (sum + (10 - (sum % 10))) - sum;
                }
                else
                {
                    sum = 0; //Check digit must be zero
                }
                //Check if our calculated check-digit matches the one originally provided by the user


                if (sum != number[9])
                {
                    InvalidNPI = true;
                }
            }

            else
            {
                InvalidNPI = true;
            }
        }


        public int SumDigits(int number)
        {
            int sum = 0;
            while (number > 0)
            {
                sum += (number % 10);
                number = number / 10;
            }
            return sum;
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ParseNpi(value.ToString());

            if(InvalidNPI==true)
            {
                return new ValidationResult(false, null);
            }

            else
            {
                return new ValidationResult(true, null);
            }
        }

        public bool InvalidNPI { get; set; }
    }
}
