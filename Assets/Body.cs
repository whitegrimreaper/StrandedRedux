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
        //gonna flesh out later maybe
    }

    public Limb[] limbs;
    public Organ[] organs;

    public Body()
    {
        //Limbs will be in order:
        //Head
        //torso
        //Left Arm
        //Right Arm
        //Left Leg
        //Right Leg
        //extra
        limbs = new Limb[6];
        limbs[0] = new Limb(); //head
        limbs[0].affectCognit = true;
        limbs[0].affectConcious = true;

        limbs[1] = new Limb(); //torso
        limbs[1].affectConcious = true;
        limbs[1].affectWalk = true;
        limbs[1].affectManip = true;

        limbs[2] = new Limb(); //l arm
        limbs[2].affectManip = true;

        limbs[3] = new Limb(); //r arm
        limbs[3].affectManip = true;

        limbs[4] = new Limb(); //l leg
        limbs[4].affectWalk = true;

        limbs[5] = new Limb();//r leg
        limbs[5].affectWalk = true;
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
