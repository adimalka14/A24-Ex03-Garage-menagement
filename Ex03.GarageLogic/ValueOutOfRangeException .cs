using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        public readonly float r_MaxValue;
        public readonly float r_MinValue;

        public ValueOutOfRangeException(string i_Message, float i_MaxValue, float i_MinValue)
            : base(i_Message)
        {
            r_MaxValue = i_MaxValue;
            r_MinValue = i_MinValue;
        }
    }
}