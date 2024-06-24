namespace Ex03.GarageLogic
{
    public class Electric : SourceEnergy
    {
        private float m_BatteryTimeRemainingInHours;
        private readonly float r_MaxBatteryTime;

        public Electric(float r_MaxBatteryTime, float i_AmountOfCurrentEnergy) : base()
        {
            this.r_MaxBatteryTime = r_MaxBatteryTime;
            validateEnergyCapicity(i_AmountOfCurrentEnergy);
            this.m_BatteryTimeRemainingInHours = i_AmountOfCurrentEnergy;
        }

        public override string ToString()
        {
            return string.Format(@"Source energy details:
-----------------------------------
Battery time remaining in hours : {0}
-----------------------------------
Max battery time : {1}
===================================", m_BatteryTimeRemainingInHours, r_MaxBatteryTime);
        }

        public override void FillEnergy(float i_Capicity)
        {
            validateEnergyCapicity(i_Capicity);
            m_BatteryTimeRemainingInHours += i_Capicity;
        }

        private void validateEnergyCapicity(float i_Capicity)
        {
            if (r_MaxBatteryTime < i_Capicity + m_BatteryTimeRemainingInHours)
            {
                string messege = string.Format(@"You exceeded electric capicity limit,
You can only fill more {0} hours ", r_MaxBatteryTime - m_BatteryTimeRemainingInHours);
                throw new ValueOutOfRangeException(messege, r_MaxBatteryTime, k_MinEnergy);
            }
            else if (i_Capicity + m_BatteryTimeRemainingInHours < k_MinEnergy)
            {
                throw new ValueOutOfRangeException("The electric capicity must be greater than 0.", r_MaxBatteryTime, k_MinEnergy);
            }
        }

        public override float GetEnergyInPersentage()
        {
            float bataryPersentage = 0;

            bataryPersentage = (m_BatteryTimeRemainingInHours / r_MaxBatteryTime) * 100;

            return bataryPersentage;
        }
    }
}