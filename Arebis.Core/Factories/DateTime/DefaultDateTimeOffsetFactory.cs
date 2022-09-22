using System;
using System.Collections.Generic;
using System.Text;

namespace Arebis.Core.Factories.DateTime
{
	/// <summary>
	/// Default implementation of IDateTimeOffsetFactory.
	/// </summary>
	class DefaultDateTimeOffsetFactory : BaseDateTimeOffsetFactory
	{
		public override System.DateTimeOffset Now
		{
			get { return global::System.DateTimeOffset.Now; }
		}

		public override TimeZoneInfo LocalTimeZone
		{
			get { return global::System.TimeZoneInfo.Local; }
		}
	}
}
