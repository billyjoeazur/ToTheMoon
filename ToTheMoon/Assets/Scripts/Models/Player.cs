using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string displayname;
    public float maxHealth;
    public int level;
    public int experience;
    public int targetExperience;
    public int equipedSpaceship;
    
    public Player(string displayname, float maxHealth, int level, int experience, int targetExperience, int equipedSpaceship)
    {
        this.displayname = displayname;
        this.maxHealth = maxHealth;
        this.level = level;
        this.experience = experience;
        this.targetExperience = targetExperience;
        this.equipedSpaceship = equipedSpaceship;
    }
    
    public int GetExperienceToLevelUp(Player player)
    {
        int xp;
        while (player.experience >= player.targetExperience)
        {
            player.experience -= player.targetExperience;
            player.level++;
            player.targetExperience += player.targetExperience / 3;
        }
        xp = player.targetExperience - player.experience;
        return xp;
    }
}
