using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.GarageLogic
{
    public class CustomerCard
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private readonly Vehicle r_Vehicle = null;
        private eVehicleStatus m_VehicleStatus = eVehicleStatus.InProcess;

        public CustomerCard(Vehicle i_Vehicle)
        {
            r_Vehicle = i_Vehicle;
        }

        public eVehicleStatus VehicleStatus { get => m_VehicleStatus; set => m_VehicleStatus = value; }

        public string OwnerName { get => m_OwnerName; set => m_OwnerName = value; }

        public string OwnerPhoneNumber { get => m_OwnerPhoneNumber; set => m_OwnerPhoneNumber = value; }

        public void SetVehicleModel(string i_VehicleModel)
        {
            r_Vehicle.ModelName = i_VehicleModel;
        }

        public Type GetVehicleType()
        {
            return r_Vehicle.GetType();
        }

        public void CreateWheels(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            r_Vehicle.CreateWheels(i_ManufacturerName, i_CurrentAirPressure);

        }

        public void CreateSourceEnergy(float i_CurrentQuantity, eVehicleType i_VehicleType)
        {
            r_Vehicle.CreateSourceEnergy(i_CurrentQuantity, i_VehicleType);
        }

        public void SetUniqueDetails(List<string> i_UniqueDetails)
        {
            r_Vehicle.SetUniqueDetails(i_UniqueDetails);
        }

        public override int GetHashCode()
        {
            return r_Vehicle.GetHashCode();
        }

        public string GetVehicleDetails()
        {
            return string.Format(@"Vehicle details
===================================
Owner name : {0}
-----------------------------------
Owner Phone number : {1}
-----------------------------------
Vehicle status : {2}
===================================
{3}", m_OwnerName, m_OwnerPhoneNumber, m_VehicleStatus, r_Vehicle.GetVehicleDetails());
        }

        public string GetLicenseNumber()
        {
            return this.r_Vehicle.LicenseNumber;
        }

        public void InflateTheWheelsToTheMaximum()
        {
            this.r_Vehicle.InflateTheWheelsToTheMaximum();
        }

        public void FillEnergy(float i_CapicityToRefuel)
        {
            this.r_Vehicle.FillEnergy(i_CapicityToRefuel);
        }

        public SourceEnergy GetSourceEnergy()
        {
            return this.r_Vehicle.SourceEnergy;
        }

        public enum eVehicleStatus
        {
            InProcess = 1,
            Fixed,
            Paid,
        }
    }
}