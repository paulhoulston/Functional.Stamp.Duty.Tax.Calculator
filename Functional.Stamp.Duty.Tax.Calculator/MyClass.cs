using System;
using NUnit.Framework;

namespace Functional.Stamp.Duty.Tax.Calculator
{
	public class TaxCalculator
	{
		private readonly int _value;

		public TaxCalculator (int value)
		{
			_value = value;
		}

		public double Calculate ()
		{
			if (_value > 125000)
				return (_value - 125000) * .02;
			return 0;
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
	}

}

