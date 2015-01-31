using System;
using Xunit;

namespace Functional.Stamp.Duty.Tax.Calculator
{
	public class given_I_am_in_the_first_stamp_duty_tax_band
	{
		public class when_I_pay_nothing_for_a_house
		{
			[Fact]
			public void then_I_dont_pay_any_stamp_duty()
			{
				Assert.True(false);
			}
		}
	}

}

