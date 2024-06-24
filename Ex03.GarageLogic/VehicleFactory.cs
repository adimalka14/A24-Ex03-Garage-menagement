using System;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        public Vehicle CreateNewVehicle(int i_VehicleType, string i_LicenseNumber)
        {
            Vehicle vehicle = null;

            if (Enum.IsDefined(typeof(eVehicleType), i_VehicleType))
            {
                eVehicleType vehicleType = (eVehicleType)i_VehicleType;
                switch (vehicleType)
                {
                    case eVehicleType.FuelCar:
                    case eVehicleType.ElectricCar:
                        vehicle = new Car(i_LicenseNumber);
                        break;
                    case eVehicleType.FuelMotorcycle:
                    case eVehicleType.ElectricMotorcycle:
                        vehicle = new Motorcycle(i_LicenseNumber);
                        break;
                    case eVehicleType.FuelTruck:
                        vehicle = new Truck(i_LicenseNumber);
                        break;
                }
            }
            else
            {
                throw new FormatException("The type of vehicle you selected is not one of the possible choices.");
            }

            return vehicle;
        }

        public enum eVehicleType
        {
            FuelCar = 1,
            ElectricCar = 2,
            FuelMotorcycle = 3,
            ElectricMotorcycle = 4,
            FuelTruck = 5,
        }
    }
}