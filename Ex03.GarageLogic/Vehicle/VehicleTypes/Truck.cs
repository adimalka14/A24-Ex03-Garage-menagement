using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Fuel;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_DoesItTransportHazardousMaterials;
        private float m_EngineVolume;

        public Truck(string i_LicenseNumber)
          : base(i_LicenseNumber) { }

        public bool DoesItTransportHazardousMaterials
        {
            get { return this.m_DoesItTransportHazardousMaterials; }
        }

        public float EngineVolume
        {
            get { return this.m_EngineVolume; }
        }

        public override void CreateWheels(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            int numbersOfWheels = 12;
            float maxAirPressure = 28f;

            base.CreateWheelList(numbersOfWheels, maxAirPressure, i_ManufacturerName, i_CurrentAirPressure);
        }

        public override void CreateSourceEnergy(float i_AmountOfCurrentEnergy, eVehicleType i_VehicleType)
        {
            float maxAmount = 110f;

            base.m_SourceEnergy = new Fuel(maxAmount, eFuelTypes.Soler, i_AmountOfCurrentEnergy);
            base.m_PercentageOfRemainingEnergy = (i_AmountOfCurrentEnergy / maxAmount) * 100;
        }

        public override void SetUniqueDetails(List<string> i_UniqueDetails)
        {
            setHazardousMaterials(i_UniqueDetails[0]);
            setEngineVolume(i_UniqueDetails[1]);
        }

        private void setHazardousMaterials(string i_Answer)
        {
            if (i_Answer == "1")
            {
                this.m_DoesItTransportHazardousMaterials = true;
            }
            else if (i_Answer == "0")
            {
                this.m_DoesItTransportHazardousMaterials = false;
            }
            else
            {
                throw new FormatException(@"Invalid input for does it transport hazardous materials.
(Enter 1 for True or 0 to False)");
            }
        }

        private void setEngineVolume(string i_EngineVolume)
        {
            if (float.TryParse(i_EngineVolume, out float engineVolume))
            {
                this.m_EngineVolume = engineVolume;
            }
            else
            {
                throw new FormatException("Enter numeric value for engine volume.");
            }
        }

        public override string ToString()
        {
            return string.Format(@"Does it transport hazardous materials : {0}
-----------------------------------
Engine volume : {1}
===================================", this.m_DoesItTransportHazardousMaterials, this.m_EngineVolume);
        }
    }
}