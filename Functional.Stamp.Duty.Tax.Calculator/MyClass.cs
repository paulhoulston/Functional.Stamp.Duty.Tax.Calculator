using System;
using NUnit.Framework;

namespace Functional.Stamp.Duty.Tax.Calculator
{
	[TestFixture]
	public class given_I_am_in_the_first_stamp_duty_tax_band
	{
		public class when_I_pay_nothing_for_a_house
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

			[Test]
			public void then_I_dont_pay_any_stamp_duty()
			{
				var taxCalculator = new TaxCalculator (0);
				Assert.AreEqual(0, taxCalculator.Calculate());
			}

			public class when_I_pay_one_pound_less_than_the_next_stamp_duty_tax_band
			{
				[Test]
				public void then_I_dont_pay_any_stamp_duty()
				{
					var taxCalculator = new TaxCalculator (125000);
					Assert.AreEqual(0, taxCalculator.Calculate());
				}				
			}

		}
	}

}

