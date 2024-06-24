namespace Ex03.GarageLogic
{
    public class Fuel : SourceEnergy
    {
        private float m_CurrentFuelQuantityInLiters;
        private float m_MaxFuelInLiters;
        private readonly eFuelTypes r_FuelType;

        public Fuel(float i_MaxFuelInLiters, eFuelTypes i_FuelType, float i_AmountOfCurrentEnergy) : base()
        {
            this.m_MaxFuelInLiters = i_MaxFuelInLiters;
            this.r_FuelType = i_FuelType;
            validateFuelCapacity(i_AmountOfCurrentEnergy);
            this.m_CurrentFuelQuantityInLiters = i_AmountOfCurrentEnergy;
        }

        public eFuelTypes FuelType
        {
            get { return r_FuelType; }
        }

        public override void FillEnergy(float i_Capacity)
        {
            float totalFuel = m_CurrentFuelQuantityInLiters + i_Capacity;
            validateFuelCapacity(totalFuel);
            m_CurrentFuelQuantityInLiters = totalFuel;
        }

        private void validateFuelCapacity(float i_TotalFuel)
        {
            if (i_TotalFuel > m_MaxFuelInLiters)
            {
                throw new ValueOutOfRangeException($"Exceeded capacity limit. Can fill more only{m_MaxFuelInLiters - m_CurrentFuelQuantityInLiters} liters.", m_MaxFuelInLiters, k_MinEnergy);
            }
            else if (i_TotalFuel < 0)
            {
                throw new ValueOutOfRangeException("Fuel capacity must be greater than or equal to 0.", m_MaxFuelInLiters, k_MinEnergy);
            }
        }

        public override float GetEnergyInPersentage()
        {
            float capicityPersentage = 0;

            capicityPersentage = (m_CurrentFuelQuantityInLiters / m_MaxFuelInLiters) * 100;

            return capicityPersentage;
        }

        public override string ToString()
        {
            return string.Format(@"Fuel Details
-----------------------------------
Current fuel quantity in liters : {0}
-----------------------------------
Max fuel in liters : {1}
-----------------------------------
Fuel type : {2}
===================================", m_CurrentFuelQuantityInLiters, m_MaxFuelInLiters, r_FuelType);
        }

        public enum eFuelTypes
        {
            Soler = 1,
            Octan95 = 95,
            Octan96 = 96,
            Octan98 = 98,
        }
    }
}