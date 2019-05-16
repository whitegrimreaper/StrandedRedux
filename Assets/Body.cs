using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body {

    public class Limb
    {
        //public bool bone;
        //public double boneDamage;
        //public bool muscle;
        //public double muscleDamage;
        //public bool nerve;
        //public double nerveDamage;
        //public bool skin;
        //public double skinDamage;

        public double damage;

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
            //bone = true;
            //boneDamage = 0.0f;
            //muscle = true;
            //muscleDamage = 0.0f;
            //nerve = true;
            //nerveDamage = 0.0f;
            //skin = true;
            //skinDamage = 0.0f;

            damage = 0.0f;

            bleeding = false;
            bleedingAmt = 0.0f;


            affectWalk = false;
            affectManip = false;
            affectCognit = false;
            affectConcious = false;
        }
    }

    //public class Organ
    //{
    //    //gonna flesh out later maybe
    //}

    public double totalBlood = 100f;
    private double currBlood;

    public Limb[] limbs;
    //public Organ[] organs;

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

        currBlood = totalBlood;
    }

    public Body(string race)
    {
        switch (race) {
            case "human":
                new Body();
                break;
        }
    }

    public void damagePart(int part, double damage, int type)
    {
        limbs[part].bleeding = true;
        limbs[part].bleedingAmt += damage; // will depend on type

        limbs[part].damage = +damage; //will need to factor in armor and shit later
    }

    public void loseBlood(double damage)
    {
        this.currBlood -= damage;
    }

    public double getBlood()
    {
        return currBlood;
    }

    public bool checkDead()
    {
        if(currBlood <= 0)
        {
            return true;
        }
        else if(limbs[0].damage >= 10.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
