using Arebis.Core.Factories.DateTime;

namespace Arebis.Core
{
    /// <summary>
    /// Provides unit-testable system information.
    /// </summary>
    public static class Current
    {
        private static IDateTimeFactory? dateTimeFactory;
        private static IDateTimeOffsetFactory? dateTimeOffsetFactory;

        /// <summary>
        /// This event is triggered when a soft recycle of the current application environment is performed.
        /// Its typical use includes clearing caches.
        /// </summary>
        public static event EventHandler? SoftRecycle;

        /// <summary>
        /// Performs a soft recycle, triggering the SoftRecycle event.
        /// Call this method when you want to flush all caches and reinitialize the application without forcing
        /// a full recycle/restart.
        /// </summary>
        public static void PerformSoftRecycle()
        {
            if (SoftRecycle != null)
            {
                SoftRecycle(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// The unit-testable DateTime factory.
        /// If set to null, reverts to the default DateTime factory.
        /// </summary>
        public static IDateTimeFactory DateTime
        {
            get
            {
                if (dateTimeFactory == null)
                {
                    dateTimeFactory = new DefaultDateTimeFactory();
                }
                return dateTimeFactory;
            }
            set
            {
                dateTimeFactory = value;
            }
        }

        /// <summary>
        /// The unit-testable DateTime factory.
        /// If set to null, reverts to the default DateTime factory.
        /// </summary>
        public static IDateTimeOffsetFactory DateTimeOffset
        {
            get
            {
                if (dateTimeOffsetFactory == null)
                {
                    dateTimeOffsetFactory = new DefaultDateTimeOffsetFactory();
                }
                return dateTimeOffsetFactory;
            }
            set
            {
                dateTimeOffsetFactory = value;
            }
        }
    }
}
