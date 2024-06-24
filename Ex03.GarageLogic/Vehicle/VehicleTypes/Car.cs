using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Fuel;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private eNumberOfDoors m_NumOfDoors;

        public Car(string i_LicenseNumber)
           : base(i_LicenseNumber) { }

        public eCarColor CarColor
        {
            get { return this.m_CarColor; }
        }

        public eNumberOfDoors NumOfDoors
        {
            get { return this.m_NumOfDoors; }
        }

        public override void SetUniqueDetails(List<string> i_UniqueDetails)
        {
            setColor(i_UniqueDetails[0]);
            setNumOfDoors(i_UniqueDetails[1]);
        }

        private void setColor(string i_Color)
        {
            int color = validateInput(i_Color, "Car color");

            if (Enum.IsDefined(typeof(eCarColor), color))
            {
                this.m_CarColor = (eCarColor)color;
            }
            else
            {
                throw new FormatException("The selected car color does not exist in the system.");
            }
        }

        private void setNumOfDoors(string i_NumOfDoors)
        {
            int numOfDoors = validateInput(i_NumOfDoors, "Num Of doors");

            if (Enum.IsDefined(typeof(eNumberOfDoors), numOfDoors))
            {
                this.m_NumOfDoors = (eNumberOfDoors)numOfDoors;
            }
            else
            {
                throw new FormatException("The selected number of doors does not exist in the system.");
            }
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
            int numbersOfWheels = 5;
            float maxAirPressure = 30f;
            base.CreateWheelList(numbersOfWheels, maxAirPressure, i_ManufacturerName, i_CurrentAirPressure);
        }

        public override void CreateSourceEnergy(float i_AmountOfCurrentEnergy, eVehicleType i_VehicleType)
        {
            float maxAmount;

            if (i_VehicleType == eVehicleType.FuelCar)
            {
                maxAmount = 58f;
                base.m_SourceEnergy = new Fuel(maxAmount, eFuelTypes.Octan95, i_AmountOfCurrentEnergy);
            }
            else
            {
                maxAmount = 4.8f;
                base.m_SourceEnergy = new Electric(maxAmount, i_AmountOfCurrentEnergy);
            }

            base.m_PercentageOfRemainingEnergy = (i_AmountOfCurrentEnergy / maxAmount) * 100;
        }


        public override string ToString()
        {
            return string.Format(@"The color of the car : {0} 
-----------------------------------
Number of doors : {1}
===================================", CarColor, NumOfDoors);
        }

        public enum eCarColor
        {
            Red = 1,
            Blue = 2,
            Yellow = 3,
            White = 4,
        }

        public enum eNumberOfDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
        }
    }
}