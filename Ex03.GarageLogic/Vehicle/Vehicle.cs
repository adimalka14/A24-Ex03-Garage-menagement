using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName;
        private readonly string r_LicenseNumber;
        private List<Wheel> m_Wheels;
        protected float m_PercentageOfRemainingEnergy;
        protected SourceEnergy m_SourceEnergy;

        public Vehicle(string i_LicenseNumber)
        {
            this.r_LicenseNumber = i_LicenseNumber;
        }

        public string LicenseNumber
        {
            get { return r_LicenseNumber; }
        }

        public SourceEnergy SourceEnergy
        {
            get { return m_SourceEnergy; }
        }

        public string ModelName
        {
            set { m_ModelName = value; }
        }

        public override int GetHashCode()
        {
            return this.r_LicenseNumber.GetHashCode();
        }

        public string GetVehicleDetails()
        {
            return string.Format(@"Model name : {0}
-----------------------------------
License number : {1}
-----------------------------------
Percentage of remaining energy : {2:0.00}
===================================
{3}
{4}
Wheels details:
{5}"
, m_ModelName, r_LicenseNumber, m_PercentageOfRemainingEnergy, m_SourceEnergy.ToString(), this.ToString(), getWheelsDetails());
        }

        private string getWheelsDetails()
        {
            StringBuilder sb = new StringBuilder();
            int wheelNumber = 1;

            foreach (Wheel wheel in m_Wheels)
            {
                sb.AppendLine(string.Format(@"wheel number {0} :", wheelNumber++));
                sb.AppendLine(wheel.ToString());
            }

            return sb.ToString();
        }

        public void FillEnergy(float i_Capacity)
        {
            this.m_SourceEnergy.FillEnergy(i_Capacity);
            updateEnergyPercentage();
        }

        private void updateEnergyPercentage()
        {
            this.m_PercentageOfRemainingEnergy=this.m_SourceEnergy.GetEnergyInPersentage();
        }

        public void InflateTheWheelsToTheMaximum()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.CurrentAirPressure = wheel.MaxAirPressure;
            }
        }

        protected void CreateWheelList(int i_NumbersOfWheels, float i_MaxAirPressure, string i_ManufacturerName, float i_CurrentAirPressure)
        {
            this.m_Wheels = new List<Wheel>();

            for (int i = 0; i < i_NumbersOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_MaxAirPressure, i_ManufacturerName, i_CurrentAirPressure));
            }
        }

        public abstract void CreateWheels(string i_ManufacturerName, float i_CurrentAirPressure);

        public abstract void CreateSourceEnergy(float i_CurrentQuantity, eVehicleType i_VehicleType);

        public abstract void SetUniqueDetails(List<string> i_UniqueDetails);
    }
}