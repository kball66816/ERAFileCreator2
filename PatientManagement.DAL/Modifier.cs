using System;

namespace PatientManagement.DAL
{
    [Serializable]
    public class Modifier
    {
        public Modifier()
        {
        }

        public Modifier(Modifier Modifier)
        {
            this.ModifierOne = Modifier.ModifierOne;
            this.ModifierTwo = Modifier.ModifierTwo;
            this.ModifierThree = Modifier.ModifierThree;
            this.ModifierFour = Modifier.ModifierFour;
        }

        public string ModifierOne { get; set; }
        public string ModifierTwo { get; set; }
        public string ModifierThree { get; set; }
        public string ModifierFour { get; set; }
    }
}