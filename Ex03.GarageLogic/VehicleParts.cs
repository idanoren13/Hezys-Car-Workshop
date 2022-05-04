using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class VehicleParts
    {
        public class Wheel
        {
            private float m_CurrentAirPressure;
            private readonly float r_MaxAirPressure; //read o ?> todo 
            private readonly string r_ManufacturerName; //read o ?> todo 

            public Wheel(string i_ManufacturerName, float i_MaxAirPressure)
            {
                r_MaxAirPressure = i_MaxAirPressure;
                r_ManufacturerName = i_ManufacturerName;
            }

            public void RefillAir() // get curr air?
            {
                m_CurrentAirPressure = r_MaxAirPressure;
            }

            public float CurrentAirPressure //TODO 
            {
                get => m_CurrentAirPressure;
                set => m_CurrentAirPressure = value;
            }

        }

        public class Engine
        {
            public enum eEngineType
            {
                Electricty,
                Fuel,
            }

            private float m_EnergyPercent;

            private class ElectricEngine : Engine
            {
                private float m_BatteryTimeLeft; // in hours.
                private readonly float m_MaxBatteryCapacity; // in hours.

                void SuperCharge() //todo
                {

                }

                void CalculateEnergyPercent()
                {

                }

                void CalculateCurrentEnergy()
                {

                }

                void GetAmountOfEnergy()
                {

                }

                void GetMaxAmountOfEnergy()
                {

                }
            }

            private class FuelEngine : Engine
            {
                enum eFuelType
                {
                    Soler,
                    Octan95,
                    Octan96,
                    Octan98
                }

                private float m_CurrentFuelAmount;
                private readonly float m_PetrolTankCapacity;
                private eFuelType m_FuelType;

                void RefillFuel()
                {

                }

                void CalculateEnergyPercent()
                {

                }

                void CalculateCurrentEnergy()
                {

                }

                void GetAmountOfEnergy()
                {

                }

                void GetMaxAmountOfEnergy()
                {

                }


            }
        }
    }
}

