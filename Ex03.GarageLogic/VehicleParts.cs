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
            private readonly float r_MaxAirPressure;
            private readonly string r_ManufacturerName;

            public Wheel(string i_ManufacturerName, float i_MaxAirPressure)
            {
                r_MaxAirPressure = i_MaxAirPressure;
                r_ManufacturerName = i_ManufacturerName;
            }

            public void RefillAir(int i_AddedPressure) 
            {

                if (m_CurrentAirPressure + i_AddedPressure > r_MaxAirPressure ||
                    m_CurrentAirPressure + i_AddedPressure < 0)
                {
                    throw new ValueOutOfRangeException(r_MaxAirPressure, 0);
                }
                else {
                    m_CurrentAirPressure += i_AddedPressure;
                    m_CurrentAirPressure = r_MaxAirPressure;
                }
            }

            public float CurrentAirPressure 
            {
                get => m_CurrentAirPressure;
                set => m_CurrentAirPressure = value;
            }
        }

        public abstract class Engine
        {
            public enum eEngineType
            {
                Electricty,
                Fuel,
            }

            protected float m_CurrentEnergyCapacity;
            protected readonly float r_MaxEnergyCapacity;
            protected readonly eEngineType r_EngineType;

            protected Engine(float i_MaxEnergyCapacity, eEngineType i_EngineType)
            {
                r_MaxEnergyCapacity = i_MaxEnergyCapacity;
                r_EngineType = i_EngineType;
            }

            public float GetEnergyPrecent()
            {
                return m_CurrentEnergyCapacity / r_MaxEnergyCapacity;
            }

            protected void addEnergy(float i_AddedVolume)
            {
                if (m_CurrentEnergyCapacity + i_AddedVolume > r_MaxEnergyCapacity ||
                    m_CurrentEnergyCapacity + i_AddedVolume < 0)
                {
                    throw new ValueOutOfRangeException(r_MaxEnergyCapacity, 0);
                }
                else
                {
                    m_CurrentEnergyCapacity += i_AddedVolume;
                    m_CurrentEnergyCapacity = r_MaxEnergyCapacity;
                }
            }
        }

        public class ElectricEngine : Engine
        {
            public ElectricEngine(float i_MaxBatteryCapacity) : base(i_MaxBatteryCapacity, eEngineType.Electricty) { }

            public float BatteryTimeLeft 
            { 
                get => m_CurrentEnergyCapacity; 
                set => m_CurrentEnergyCapacity = value; 
            }

            public float MaxBatteryCapacity
            {
                get => r_MaxEnergyCapacity;
            }

            void SuperCharge(float i_AddEnergy) //todo
            {
                addEnergy(i_AddEnergy);   
            }
        }

        public class CombustionEngine : Engine
        {
            public enum eFuelType
            {
                Soler,
                Octan95,
                Octan96,
                Octan98
            }

            private readonly eFuelType r_FuelType;

            public CombustionEngine(float i_MaxFuelCapcity, eFuelType i_FuelType) : base(i_MaxFuelCapcity, eEngineType.Fuel)
            {
                r_FuelType = i_FuelType;
            }

            public float CurrentFuelAmount 
            { 
                get => m_CurrentEnergyCapacity; 
                set => m_CurrentEnergyCapacity = value; 
            }

            public float MaxTankCapacity
            {
                get => r_MaxEnergyCapacity;
            }

            public eFuelType FuelType
            {
                get => r_FuelType;
            }

            public void addFuel(float i_AddVolume)
            {
                addEnergy(i_AddVolume);
            }
        }
    }
}

