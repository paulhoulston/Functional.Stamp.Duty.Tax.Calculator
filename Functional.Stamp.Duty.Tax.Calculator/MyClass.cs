using System;
using NUnit.Framework;

namespace Functional.Stamp.Duty.Tax.Calculator
{
	[TestFixture]
	public class given_I_am_buying_a_house
	{
		public class when_the_purchase_price_is_within_the_first_stamp_duty_tax_bracket
		{
			public class TaxCalculator
			{
				private readonly int _value;

				public TaxCalculator (int value)
				{
					_value = value;
				}

				public int Calculate ()
				{
					return 0;
				}
			}

			[TestCase(0)]
			[TestCase(125000)]
			public void then_I_dont_pay_any_stamp_duty(int housePrice)
			{
				var taxCalculator = new TaxCalculator (housePrice);
				Assert.AreEqual(0, taxCalculator.Calculate());
			}
		}
	}

}

