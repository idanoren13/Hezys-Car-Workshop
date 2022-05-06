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
            
            public float CurrentAirPressure 
            {
                get => m_CurrentAirPressure;
                set => m_CurrentAirPressure = value;
            }

            public float MaxAirPressure
            {
                get => r_MaxAirPressure;
            }

            public void AddAir(int i_AddedPressure) 
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

            public void FillMaxPressure()
            {
                m_CurrentAirPressure = r_MaxAirPressure;
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
            protected float m_MaxEnergyCapacity;
            protected readonly eEngineType r_EngineType;

            protected Engine(eEngineType i_EngineType)
            {
                //m_MaxEnergyCapacity = i_MaxEnergyCapacity;
                r_EngineType = i_EngineType;
            }

            public float GetEnergyPrecent()
            {
                return m_CurrentEnergyCapacity / m_MaxEnergyCapacity;
            }

            protected void addEnergy(float i_AddedVolume)
            {
                if (m_CurrentEnergyCapacity + i_AddedVolume > m_MaxEnergyCapacity ||
                    m_CurrentEnergyCapacity + i_AddedVolume < 0)
                {
                    throw new ValueOutOfRangeException(m_MaxEnergyCapacity, 0);
                }
                else
                {
                    m_CurrentEnergyCapacity += i_AddedVolume;
                    m_CurrentEnergyCapacity = m_MaxEnergyCapacity;
                }
            }
        }

        public class ElectricEngine : Engine
        {
            public ElectricEngine() : base(eEngineType.Electricty) { }

            public float BatteryTimeLeft 
            { 
                get => m_CurrentEnergyCapacity; 
                set => m_CurrentEnergyCapacity = value; 
            }

            public float MaxBatteryCapacity
            {
                get => m_MaxEnergyCapacity;
                set => m_MaxEnergyCapacity = value;
            }

            void SuperCharge(float i_AddEnergy)
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

            private eFuelType m_FuelType;

            public CombustionEngine() : base(eEngineType.Fuel) { }

            public float CurrentFuelAmount 
            { 
                get => m_CurrentEnergyCapacity; 
                set => m_CurrentEnergyCapacity = value; 
            }

            public float MaxTankCapacity
            {
                get => m_MaxEnergyCapacity;
                set => m_MaxEnergyCapacity = value;
            }

            public eFuelType FuelType
            {
                get => m_FuelType;
                set => m_FuelType = value;
            }

            public void addFuel(float i_AddVolume)
            {
                addEnergy(i_AddVolume);
            }
        }
    }
}

