using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body {

    public class Limb
    {
        public bool bone;
        public double boneDamage;
        public bool muscle;
        public double muscleDamage;
        public bool nerve;
        public double nerveDamage;
        public bool skin;
        public double skinDamage;

        public bool bleeding;
        public double bleedingAmt;

        public bool affectWalk;
        public bool affectManip;
        public bool affectCognit;
        public bool affectConcious;

        public Limb ()
        {
            //Base init for human limb
            //just change the affect vars for different
            //types of limbs for now
            bone = true;
            boneDamage = 0.0f;
            muscle = true;
            muscleDamage = 0.0f;
            nerve = true;
            nerveDamage = 0.0f;
            skin = true;
            skinDamage = 0.0f;

            bleeding = false;
            bleedingAmt = 0.0f;

            affectWalk = false;
            affectManip = false;
            affectCognit = false;
            affectConcious = false;
        }
    }

    public class Organ
    {
        //gonna flesh out later probably
    }

    public Limb[] limbs;
    public Organ[] organs;

    public Body()
    {
        //Limbs will be in order:
        //Head
        //Left Arm
        //Right Arm (+addl arms here)
        //Left Leg
        //Right Leg (+addl arms here)
        //3rd leg (+tails and other special shit here)
        limbs = new Limb[1];
        limbs[0] = new Limb();
    }

    public Body(string race)
    {
        switch (race) {
            case "human":
                new Body();
                break;
        }
    }
	
}
