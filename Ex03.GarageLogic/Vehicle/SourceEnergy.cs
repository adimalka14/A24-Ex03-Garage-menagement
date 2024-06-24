namespace Ex03.GarageLogic
{
    public abstract class SourceEnergy
    {
        protected const float k_MinEnergy = 0f;

        public abstract void FillEnergy(float i_Capacity);  

        public abstract float GetEnergyInPersentage();
    }
}
