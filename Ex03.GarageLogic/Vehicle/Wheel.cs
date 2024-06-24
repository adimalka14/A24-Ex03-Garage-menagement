namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressureSetByTheManufacturer;

        public Wheel(float i_MaxAirPressure, string i_ManufacturerName, float i_CurrentAirPressure)
        {
            this.r_MaxAirPressureSetByTheManufacturer = i_MaxAirPressure;
            this.m_ManufacturerName = i_ManufacturerName;
            InflatingAWheel(i_CurrentAirPressure);
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressureSetByTheManufacturer; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
            set { m_CurrentAirPressure = value; }
        }

        public void InflatingAWheel(float i_AmountOfAirToAdd)
        {
            if (r_MaxAirPressureSetByTheManufacturer < i_AmountOfAirToAdd + m_CurrentAirPressure)
            {
                string messege = string.Format(@"You exceeded the manufacturer's limit,
You can only fill more {0} ", r_MaxAirPressureSetByTheManufacturer - m_CurrentAirPressure);
                throw new ValueOutOfRangeException(messege, r_MaxAirPressureSetByTheManufacturer, 0);
            }
            else if (i_AmountOfAirToAdd + m_CurrentAirPressure < 0)
            {
                throw new ValueOutOfRangeException("The air pressure must be greater than 0", r_MaxAirPressureSetByTheManufacturer, 0);
            }

            m_CurrentAirPressure += i_AmountOfAirToAdd;
        }

        public override string ToString()
        {
            return string.Format(@"Manufacturer name : {0}
-----------------------------------
Current air pressure : {1}
-----------------------------------
Max air pressure set by the Manufacturer : {2}
===================================", m_ManufacturerName, m_CurrentAirPressure, r_MaxAirPressureSetByTheManufacturer);
        }
    }
}