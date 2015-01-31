using System;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Functional.Stamp.Duty.Tax.Calculator
{
	public class TaxCalculator
	{
		private readonly int _value;
		private readonly List<TaxBand> _taxBands = new List<TaxBand> {
			new TaxBand (1500000, int.MaxValue, 0.12),
			new TaxBand (925000, 575000, 0.10),
			new TaxBand (250000, 675000, 0.05),
			new TaxBand (125000, 125000, 0.02)
		};

		public TaxCalculator (int value)
		{
			_value = value;
		}

		private class TaxBand
		{
			public readonly int LowerBand;
			public readonly int RangeOfBand;
			public readonly double Percentage;

			public TaxBand(int lowerBand, int rangeOfBand, double percentage)
			{
				LowerBand=lowerBand;
				RangeOfBand=rangeOfBand;
				Percentage=percentage;
			}
		}

		private double CalculateTax(TaxBand taxBand)
		{
			return AmountInBand (taxBand.LowerBand, taxBand.RangeOfBand) * taxBand.Percentage;
		}

		private int AmountInBand (int lowerBand, int rangeOfBand)
		{
			return Math.Min (rangeOfBand, Math.Max (0, _value - lowerBand));
		}

		public double Calculate ()
		{
			return _taxBands.Sum(taxBand => CalculateTax(taxBand));
		}
	}

	[TestFixture]
	public class given_I_am_buying_a_house
	{
		public class when_the_purchase_price_is_within_the_first_stamp_duty_tax_bracket
		{
			[TestCase(0)]
			[TestCase(125000)]
			public void then_I_dont_pay_any_stamp_duty(int housePrice)
			{
				var taxCalculator = new TaxCalculator (housePrice);
				Assert.AreEqual(0, taxCalculator.Calculate());
			}
		}

		public class when_the_purchase_price_is_within_the_second_stamp_duty_tax_bracket
		{
			[Test]
			public void then_I_only_pay_stamp_duty_on_the_amount_within_that_tax_bracket()
			{
				var taxCalculator = new TaxCalculator (250000);
				Assert.AreEqual(2500, taxCalculator.Calculate());
			}
		}

		public class when_the_purchase_price_is_within_the_third_stamp_duty_tax_bracket
		{
			[Test]
			public void then_I_pay_the_5_percent_of_the_third_band_and_2_percent_of_the_second_band()
			{
				var taxCalculator = new TaxCalculator (925000);
				Assert.AreEqual(36250, taxCalculator.Calculate());
			}
		}

		public class when_the_purchase_price_is_within_the_fourth_stamp_duty_tax_bracket
		{
			[Test]
			public void then_I_pay_the_appropriate_percentages_for_each_band()
			{
				var taxCalculator = new TaxCalculator (1500000);
				Assert.AreEqual(93750, taxCalculator.Calculate());
			}
		}

		public class when_the_purchase_price_is_within_the_fifth_stamp_duty_tax_bracket
		{
			[Test]
			public void then_I_pay_the_appropriate_percentages_for_each_band_with_no_maximum_over_150000_pounds()
			{
				var taxCalculator = new TaxCalculator (5000000);
				Assert.AreEqual(513750, taxCalculator.Calculate());
			}
		}
	}
}