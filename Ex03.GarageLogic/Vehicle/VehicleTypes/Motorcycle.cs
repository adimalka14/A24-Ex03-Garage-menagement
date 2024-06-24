using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Fuel;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolumeInCc;

        public Motorcycle(string i_LicenseNumber)
          : base(i_LicenseNumber) { }

        public eLicenseType LicenseType { get => m_LicenseType; }

        public int EngineVolumeInCC { get => m_EngineVolumeInCc; }

        public override void SetUniqueDetails(List<string> i_UniqueDetails)
        {
            setLicenseType(i_UniqueDetails[0]);
            setEngineVolumeInCc(i_UniqueDetails[1]);
        }

        private void setLicenseType(string i_LicenseType)
        {
            int licenseType = validateInput(i_LicenseType, "License Type");

            if (Enum.IsDefined(typeof(eLicenseType), licenseType))
            {
                this.m_LicenseType = (eLicenseType)licenseType;
            }
            else
            {
                throw new FormatException("The selected license type does not exist in the system.");
            }
        }

        private void setEngineVolumeInCc(string i_EngineVolumeInCc)
        {
            int engineVolumeInCc = validateInput(i_EngineVolumeInCc, "Engine Voulume");

            this.m_EngineVolumeInCc = engineVolumeInCc;
        }

        private int validateInput(string i_Input, string i_PropertyName)
        {
            if (!int.TryParse(i_Input, out int vaidateInput))
            {
                throw new FormatException($"Invalid input. Please enter a valid numeric value. {i_PropertyName}");
            }

            return vaidateInput;
        }

        public override void CreateWheels(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            int numbersOfWheels = 2;
            float maxAirPressure = 29f;
            base.CreateWheelList(numbersOfWheels, maxAirPressure, i_ManufacturerName, i_CurrentAirPressure);
        }

        public override void CreateSourceEnergy(float i_AmountOfCurrentEnergy, eVehicleType i_VehicleType)
        {
            float maxAmount;

            if (i_VehicleType == eVehicleType.FuelMotorcycle)
            {
                maxAmount = 5.8f;
                base.m_SourceEnergy = new Fuel(maxAmount, eFuelTypes.Octan98, i_AmountOfCurrentEnergy);
            }
            else
            {
                maxAmount = 2.8f;
                base.m_SourceEnergy = new Electric(maxAmount, i_AmountOfCurrentEnergy);
            }

            base.m_PercentageOfRemainingEnergy = (i_AmountOfCurrentEnergy / maxAmount) * 100;
        }

        public override string ToString()
        {
            return string.Format(@"License type:{0}
-----------------------------------
Engine volume in CC : {1}
===================================", m_LicenseType, m_EngineVolumeInCc);
        }

        public enum eLicenseType
        {
            A1 = 1,
            A2,
            AB,
            B2,
        }
    }
}