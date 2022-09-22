using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.Factories.DateTime
{
	/// <summary>
	/// Default implementation of IDateTimeFactory.
	/// </summary>
	class DefaultDateTimeFactory : BaseDateTimeFactory
	{
		public override System.DateTime Now
		{
			get { return global::System.DateTime.Now; }
		}

		public override TimeZoneInfo LocalTimeZone
		{
			get { return global::System.TimeZoneInfo.Local; }
		}
	}
}
