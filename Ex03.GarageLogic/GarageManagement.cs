using System;
using System.Collections.Generic;
using System.Text;
using static Ex03.GarageLogic.CustomerCard;
using static Ex03.GarageLogic.Fuel;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.GarageLogic
{
    public class GarageManagement
    {
        private Dictionary<int, CustomerCard> m_VehiceList = new Dictionary<int, CustomerCard>();
        private CustomerCard m_CurrentCustomerCard = null;

        private void IsVehicleInGarage(string i_LicenseNumber)
        {
            bool isThere = false;

            isThere = m_VehiceList.TryGetValue(i_LicenseNumber.GetHashCode(), out m_CurrentCustomerCard);
            if (!isThere)
            {
                throw new ArgumentException("The licenseNumber not found.");
            }
        }

        private void addNewVehicleToMemory()
        {
            int key = m_CurrentCustomerCard.GetHashCode();

            m_VehiceList.Add(key, m_CurrentCustomerCard);
        }

        public void AddNewVehicleCard(int i_VehicleType, string i_LicenseNumber)
        {
            Vehicle vehicle = null;
            VehicleFactory factory = new VehicleFactory();
            vehicle = factory.CreateNewVehicle(i_VehicleType, i_LicenseNumber);
            m_CurrentCustomerCard = new CustomerCard(vehicle);
        }

        public void SetOwnerDetailsAndVehicleModel(string i_OwnerName, string i_OwnerPhoneNumber, string i_ModelName)
        {
            m_CurrentCustomerCard.OwnerName = i_OwnerName;
            m_CurrentCustomerCard.OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_CurrentCustomerCard.SetVehicleModel(i_ModelName);
        }

        public void CreateWheels(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            m_CurrentCustomerCard.CreateWheels(i_ManufacturerName, i_CurrentAirPressure);
        }

        public void CreateSourceEnergy(float i_CurrentQuantity, eVehicleType i_VehicleType)
        {
            m_CurrentCustomerCard.CreateSourceEnergy(i_CurrentQuantity, i_VehicleType);
        }

        public string[] GetVehicleOptions()
        {
            return UniqDetailsForVehicle.GetVehicleOptions();
        }

        public void SetUniqueDetails(List<string> i_UniqueDetails)
        {
            m_CurrentCustomerCard.SetUniqueDetails(i_UniqueDetails);
            addNewVehicleToMemory();
        }

        public string GetVehicleDeails(string i_VehicleId)
        {
            string VehicleDetails;

            IsVehicleInGarage(i_VehicleId);
            VehicleDetails = m_CurrentCustomerCard.GetVehicleDetails();

            return VehicleDetails;
        }

        public List<string> GetObjectDetails()
        {
            return UniqDetailsForVehicle.GetObjectDetails(m_CurrentCustomerCard.GetVehicleType());
        }

        public string GetGarageVehiclesStatus(int i_FilterStatus)
        {
            StringBuilder vehiclesStatusList = new StringBuilder();

            if (i_FilterStatus == 0)
            {
                foreach (CustomerCard customerCard in m_VehiceList.Values)
                {
                    vehiclesStatusList.AppendLine($"License Number: {customerCard.GetLicenseNumber()} | Status: {customerCard.VehicleStatus}");
                }
            }
            else
            {
                validateStatus(i_FilterStatus);
                foreach (CustomerCard customerCard in m_VehiceList.Values)
                {
                    if (customerCard.VehicleStatus == (eVehicleStatus)i_FilterStatus)
                    {
                        vehiclesStatusList.AppendLine($"License Number: {customerCard.GetLicenseNumber()} | Status: {customerCard.VehicleStatus}");
                    }
                }
            }

            return vehiclesStatusList.ToString();
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, int i_UserInput)
        {
            IsVehicleInGarage(i_LicenseNumber);
            validateStatus(i_UserInput);
            m_CurrentCustomerCard.VehicleStatus = (eVehicleStatus)i_UserInput;
        }

        public void InflateTheWheelsToTheMaximum(string i_LicenseNumber)
        {
            IsVehicleInGarage(i_LicenseNumber);
            m_CurrentCustomerCard.InflateTheWheelsToTheMaximum();
        }

        public void RefuelAVehiclePoweredByFuel(string i_LicenseNumber, float i_CapicityToRefuel, int i_FuelType)
        {
            IsVehicleInGarage(i_LicenseNumber);
            makeSureTheFuelTypeIsDefine(i_FuelType);
            Fuel fuel = m_CurrentCustomerCard.GetSourceEnergy() as Fuel;
            if (fuel != null)
            {
                checkfueltypecompatibility(fuel.FuelType, (eFuelTypes)i_FuelType);
                m_CurrentCustomerCard.FillEnergy(i_CapicityToRefuel);
            }
            else
            {
                throw new ArgumentException("It is impossible to refuel an electric vehicle");
            }
        }

        public void ChargeAVehiclePoweredByElectric(string i_LicenseNumber, float i_MinutesToCharge)
        {
            IsVehicleInGarage(i_LicenseNumber);
            Electric electric = m_CurrentCustomerCard.GetSourceEnergy() as Electric;
            if (electric != null)
            {
                float hoursToCharge = i_MinutesToCharge / 60;
                m_CurrentCustomerCard.FillEnergy(hoursToCharge);
            }
            else
            {
                throw new ArgumentException("It is impossible to charge an fuel vehicle");
            }       
        }

        private void validateStatus(int i_FilterStatus)
        {
            if (!Enum.IsDefined(typeof(eVehicleStatus), i_FilterStatus))
            {
                throw new FormatException("The entered input vehicle status is not one of the options.");
            }
        }

        private void makeSureTheFuelTypeIsDefine(int i_FuelType)
        {
            if (!Enum.IsDefined(typeof(eFuelTypes), i_FuelType))
            {
                throw new FormatException("The selected fuel type does not exist in the system.");
            }
        }

        private void checkfueltypecompatibility(eFuelTypes i_ActualFuelType, eFuelTypes i_FuelSelected)
        {
            if (i_ActualFuelType != i_FuelSelected)
            {
                throw new ArgumentException($"Invalid fuel type. Selected: {i_FuelSelected}, Actual: {i_ActualFuelType}");
            }
        }
    }
}