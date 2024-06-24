using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.VehicleFactory;

namespace Ex03.ConsoleUI
{
    public class RegisterNewVehicleForTheGarage
    {
        private readonly string r_LicenseNumber;
        private eVehicleType m_VehicleType;
        private GarageManagement m_GarageManagement;

        public RegisterNewVehicleForTheGarage(string i_LicenseNumber, GarageManagement i_GarageManagement)
        {
            this.r_LicenseNumber = i_LicenseNumber;
            this.m_GarageManagement = i_GarageManagement;
        }

        public void RegisterNewVehicle()
        {
            Console.WriteLine(@"Registering a new vehicle
Please enter new Vehicle details:
===============================
------------------------------");
            getAndCreateVehicleType();
            setOwnerDetailsAndModelName();
            setWheels();
            setSourceEnergy();
            setUniqueDetails();
        }

        private void getAndCreateVehicleType()
        {
            int vehicleType;
            bool isCreated = false;

            while (!isCreated)
            {
                string[] options = m_GarageManagement.GetVehicleOptions();
                int i = 1;
                Console.WriteLine("Choose Vehicle type : ");
                foreach (string option in options)
                {
                    Console.WriteLine("For {0} Enter {1}", option, i++);
                }

                while (!int.TryParse(Console.ReadLine(), out vehicleType))
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                }

                try
                {
                    m_GarageManagement.AddNewVehicleCard(vehicleType, r_LicenseNumber);
                    m_VehicleType = (eVehicleType)vehicleType;
                    isCreated = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private void setOwnerDetailsAndModelName()
        {
            string ownerName;
            string ownerPhoneNumber;
            string modelName;

            Console.Write("Enter owner name : ");
            ownerName = Console.ReadLine();
            Console.Write("Enter Phone number:");
            ownerPhoneNumber = Console.ReadLine();
            Console.Write("Enter the vehicle model name : ");
            modelName = Console.ReadLine();
            m_GarageManagement.SetOwnerDetailsAndVehicleModel(ownerName, ownerPhoneNumber, modelName);
        }

        private void setWheels()
        {
            string manufacturerName;
            float currentAirPressure;
            bool isAdded = false;

            Console.Write("Enter the name of the vehicle wheel manufacturer : ");
            manufacturerName = Console.ReadLine();
            while (!isAdded)
            {
                try
                {
                    Console.Write("Enter the current air pressure in the wheels :");
                    while (!float.TryParse(Console.ReadLine(), out currentAirPressure))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                        Console.Write("Enter the current air pressure in the wheels :");
                    }

                    m_GarageManagement.CreateWheels(manufacturerName, currentAirPressure);
                    isAdded = true;

                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private void setSourceEnergy()
        {
            float amountOfCurrentEnergy;
            bool isAdded = false;

            while (!isAdded)
            {
                try
                {
                    Console.Write(@"Enter the amount of current energy {Fuel in liters/Electric in hours} : ");
                    while (!float.TryParse(Console.ReadLine(), out amountOfCurrentEnergy))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                    }

                    m_GarageManagement.CreateSourceEnergy(amountOfCurrentEnergy, m_VehicleType);
                    isAdded = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private void setUniqueDetails()
        {
            List<string> uniqueDetails = new List<string>();
            List<string> uniqueMessages = m_GarageManagement.GetObjectDetails();
            bool isAdded = false;

            while (!isAdded)
            {
                foreach (string message in uniqueMessages)
                {
                    Console.Write($"{message}");
                    uniqueDetails.Add(Console.ReadLine());
                    Console.WriteLine("------------------------------");
                }

                try
                {
                    m_GarageManagement.SetUniqueDetails(uniqueDetails);
                    isAdded = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                    uniqueDetails.Clear();
                }
            }
        }
    }
}