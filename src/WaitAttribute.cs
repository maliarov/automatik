using System;

namespace Automatik
{
   [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public abstract class WaitAttribute : Attribute
    {
        public uint TimeoutInterval { get; set; }
        public TimeoutDimension TimeoutDimension { get; set; } = TimeoutDimension.Seconds;

        public TimeSpan? GetTimeout()
        {
            if (TimeoutInterval == 0)
                return null;

            switch (TimeoutDimension)
            {
                case TimeoutDimension.Minutes:
                    return TimeSpan.FromMinutes(TimeoutInterval);
                case TimeoutDimension.Seconds:
                    return TimeSpan.FromSeconds(TimeoutInterval);
                case TimeoutDimension.Miliseconds:
                    return TimeSpan.FromMilliseconds(TimeoutInterval);

                default:
                    return null;
            }
        }
    }

}