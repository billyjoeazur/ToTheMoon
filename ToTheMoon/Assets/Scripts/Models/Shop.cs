using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop
{
    public List<Spaceship> spaceships = new List<Spaceship>();
    public List<Essentials> essentials = new List<Essentials>();
    public List<Extras> extras = new List<Extras>();
    
    public Shop(List<Spaceship> spaceships, List<Essentials> essentials, List<Extras> extras)
    {
        this.spaceships = spaceships;
        this.essentials = essentials;
        this.extras = extras;
    }
}
