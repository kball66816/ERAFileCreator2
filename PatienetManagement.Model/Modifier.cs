using System;

namespace PatientManagement.Model
{
    [Serializable]
    public class Modifier
    {
        public Modifier()
        {
        }

        public Modifier(Modifier Modifier)
        {
            ModifierOne = Modifier.ModifierOne;
            ModifierTwo = Modifier.ModifierTwo;
            ModifierThree = Modifier.ModifierThree;
            ModifierFour = Modifier.ModifierFour;
        }

        public string ModifierOne { get; set; }
        public string ModifierTwo { get; set; }
        public string ModifierThree { get; set; }
        public string ModifierFour { get; set; }
    }
}