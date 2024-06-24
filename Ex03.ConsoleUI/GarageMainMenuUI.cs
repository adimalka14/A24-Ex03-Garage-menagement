using Ex03.GarageLogic;
using System;
using static Ex03.GarageLogic.CustomerCard;

namespace Ex03.ConsoleUI
{
    public class GarageMainMenuUI
    {
        private GarageManagement m_Garage = new GarageManagement();

        public void OpenTheGarageServicesMenu()
        {
            eGarageServices selectedService = eGarageServices.Exit;

            do
            {
                Console.WriteLine(@"Garage Services Menu 
==========================================================
----------------------------------------------------------
Please select a service : 

Putting a new car in the garage for repair         Enter 1
Display of garage vehicles.                        Enter 2
Changing the status of an existing vehicle.        Enter 3
Inflating the vehicle's wheels to the maximum.     Enter 4
Refueling a vehicle powered by gasoline.           Enter 5
Electric vehicle charging                          Enter 6
Displaying the details of a particular vehicle     Enter 7
Exit                                               Enter 8
----------------------------------------------------------
==========================================================
                                                          ");
                int.TryParse(Console.ReadLine(), out int userinput);
                selectedService = (eGarageServices)userinput;
                switch (selectedService)
                {
                    case eGarageServices.PutCarInForRepair:
                        puttingNewCarInTheGarageForRepair();
                        break;
                    case eGarageServices.ShowingTheGarageVehicles:
                        showingTheGarageVehicles();
                        break;
                    case eGarageServices.ChangeTheStatusOfAVehicleInTheGarage:
                        changeTheStatusOfAVehicleInTheGarage();
                        break;
                    case eGarageServices.ToInflateTheWheelsToTheMaximum:
                        toInflateTheWheelsToTheMaximum();
                        break;
                    case eGarageServices.RefuelAVehiclePoweredByFuel:
                        refuelAVehiclePoweredByFuel();
                        break;
                    case eGarageServices.ChargeAnElectricVehicle:
                        chargeAnElectricVehicle();
                        break;
                    case eGarageServices.DisplayCompleteVehicleDataByLicenseNumber:
                        displayCompleteVehicleDataByLicenseNumber();
                        break;
                    case eGarageServices.Exit:
                        Console.WriteLine("Goodbye,press enter to close the program.");
                        break;
                    default:
                        Console.WriteLine("Invaild input.");
                        break;
                }

                if (selectedService != eGarageServices.Exit)
                {
                    Console.WriteLine("Press enter to return to the main menu");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (selectedService != eGarageServices.Exit);

            Console.ReadKey();
        }

        private void puttingNewCarInTheGarageForRepair()
        {
            bool isThere = false;
            string licenseNumber;

            Console.WriteLine("please enter vehicle license number");
            licenseNumber = Console.ReadLine();
            try
            {
                int inProcess = 1;
                m_Garage.ChangeVehicleStatus(licenseNumber, inProcess);
                isThere = true;
                Console.WriteLine(@"In process!!");
                Console.WriteLine();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
            }

            if (!isThere)
            {
                RegisterNewVehicleForTheGarage newVehicleForTheGarage = new RegisterNewVehicleForTheGarage(licenseNumber, m_Garage);
                newVehicleForTheGarage.RegisterNewVehicle();
            }
        }

        private void showingTheGarageVehicles()
        {
            int userInput;
            bool isSucceeded = false;

            while (!isSucceeded)
            {
                try
                {
                    Console.WriteLine(@"please choose vehicle status:
------------------------------
Show all               Enter 0
In process             Enter 1
Fixed                  Enter 2
Paid                   Enter 3
------------------------------");
                    while (!int.TryParse(Console.ReadLine(), out userInput))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                    }

                    Console.WriteLine(m_Garage.GetGarageVehiclesStatus(userInput));
                    isSucceeded = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private void changeTheStatusOfAVehicleInTheGarage()
        {
            int userInput;
            string licenseNumber;
            bool isSucceeded = false;

            while (!isSucceeded)
            {
                Console.WriteLine("please enter vehicle license number");
                licenseNumber = Console.ReadLine();
                Console.WriteLine(@"please choose vehicle status to change:
------------------------------
In process             Enter 1
Fixed                  Enter 2
Paid                   Enter 3
------------------------------");
                while (!int.TryParse(Console.ReadLine(), out userInput))
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                }

                try
                {
                    m_Garage.ChangeVehicleStatus(licenseNumber, userInput);
                    Console.WriteLine($"Status of vehicle with license number {licenseNumber} changed to {(eVehicleStatus)userInput}.");
                    isSucceeded = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private void toInflateTheWheelsToTheMaximum()
        {
            string licenseNumber;
            bool isSucceeded = false;

            while (!isSucceeded)
            {
                Console.WriteLine("please enter vehicle license number");
                licenseNumber = Console.ReadLine();
                try
                {
                    m_Garage.InflateTheWheelsToTheMaximum(licenseNumber);
                    Console.WriteLine($"Wheels of vehicle with license number {licenseNumber} inflated to maximum.");
                    isSucceeded = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private void refuelAVehiclePoweredByFuel()
        {
            string licenseNumber;
            float capicityToRefuel;
            int fuelType;
            bool isSucceeded = false;

            while (!isSucceeded)
            {
                Console.WriteLine("please enter vehicle license number");
                licenseNumber = Console.ReadLine();
                Console.WriteLine(@"please choose fuel type:
------------------------------
Soler             Enter 1
Octan95           Enter 95
Octan96           Enter 96
Octan 98          Enter 98
------------------------------");
                while (!int.TryParse(Console.ReadLine(), out fuelType))
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                }

                Console.WriteLine("Please enter capicity. {format : 0.00}");
                while (!float.TryParse(Console.ReadLine(), out capicityToRefuel))
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                }

                try
                {
                    m_Garage.RefuelAVehiclePoweredByFuel(licenseNumber, capicityToRefuel, fuelType);
                    Console.WriteLine("The refuel Succeeded.");
                    isSucceeded = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private void chargeAnElectricVehicle()
        {
            string licenseNumber;
            float minutesToCharge;
            bool isSucceeded = false;

            while (!isSucceeded)
            {
                Console.WriteLine("please enter vehicle license number");
                licenseNumber = Console.ReadLine();
                Console.WriteLine("Please enter capicity in minutes. {format : 0.00}");
                while (!float.TryParse(Console.ReadLine(), out minutesToCharge))
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                }

                try
                {
                    m_Garage.ChargeAVehiclePoweredByElectric(licenseNumber, minutesToCharge);
                    Console.WriteLine("The charge Succeeded.");
                    isSucceeded = true;
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
            }
        }

        private void displayCompleteVehicleDataByLicenseNumber()
        {
            string licenseNumber;
            bool isSucceeded = false;

            while (!isSucceeded)
            {
                Console.WriteLine("please enter vehicle license number");
                licenseNumber = Console.ReadLine();
                try
                {
                    Console.WriteLine(m_Garage.GetVehicleDeails(licenseNumber));
                    isSucceeded = true;
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Error:{0}", ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Error:{0}", ex.Message);
                }
            }
        }

        private enum eGarageServices
        {
            PutCarInForRepair = 1,
            ShowingTheGarageVehicles = 2,
            ChangeTheStatusOfAVehicleInTheGarage = 3,
            ToInflateTheWheelsToTheMaximum = 4,
            RefuelAVehiclePoweredByFuel = 5,
            ChargeAnElectricVehicle = 6,
            DisplayCompleteVehicleDataByLicenseNumber = 7,
            Exit = 8,
        }
    }
}