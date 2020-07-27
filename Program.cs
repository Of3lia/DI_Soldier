using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DI_SoldierAndTank
{
    class Program
    {
        static void Main(string[] args)
        {
            Soldier ryan = new Soldier("Ryan", 120, new MachineGun(), new Granade(), new Knife());
            Soldier rambo = new Soldier("rambo", 180, new MachineGun());
            Soldier mangosta = new Soldier("mangosta", 30, new Knife(), new MachineGun());
            Soldier Chewbaca = new Soldier("Chewbaca", 250, new Arbalest());

            ryan.Attack(rambo);
            rambo.Attack(ryan);
            ryan.SwitchWeapon(1);
            ryan.Attack(Chewbaca);
            mangosta.Attack(ryan);
            Chewbaca.Attack(rambo);
            Chewbaca.Attack(ryan);
            mangosta.SwitchWeapon(1);
        }
    }

    #region Soldier
    class Soldier
    {
        public Soldier(string name, int hp, params IWeapon[] wpns)
        {
            weapons = new List<IWeapon>();
            foreach (IWeapon wpn in wpns) { weapons.Add(wpn); }
            selectedWeapon = weapons[0];
            healthPoints = hp;
            Name = name;
        }

        public void Attack(Soldier enemy)
        {
            if (selectedWeapon == null) { Console.WriteLine("No weapons in the inventory"); }
            else {
                Console.WriteLine($"{Name} attacks {enemy.Name} with {selectedWeapon.GetType().Name}");
                if (Name == "rambo") { Console.WriteLine("TA-TA-TA-TA-TA-TA"); }
                selectedWeapon.Hit(enemy); }
            Task.Delay(4000).Wait();
        }

        public void TakeDamage(int damage)
        {
            if (healthPoints > damage) { healthPoints -= damage; Console.WriteLine($"{Name} has {healthPoints} health points left\n"); }
            else Die();
        }

        public void SwitchWeapon(int i)
        {
            selectedWeapon = weapons[i];
            Console.WriteLine($"{Name} switched to {selectedWeapon.GetType().Name}");
        }

        private void Die()
        {
            Console.WriteLine($"{Name} died");
        }

        readonly List<IWeapon> weapons;
        IWeapon selectedWeapon;
        private int healthPoints;
        private string Name { get; set; }
    }

    interface IWeapon
    {
        public void Hit(Soldier enemy) { Console.WriteLine("Weapon not initialized"); }
    }

    class MachineGun : IWeapon
    {
        public void Hit(Soldier enemy) { enemy.TakeDamage(12); }
    }

    class Granade : IWeapon
    {
        public void Hit(Soldier enemy) { enemy.TakeDamage(30); }
    }
    class Knife : IWeapon
    {
        public void Hit(Soldier enemy) { enemy.TakeDamage(5); }
    }
    class Arbalest : IWeapon
    {
        public void Hit(Soldier enemy) { enemy.TakeDamage(120); }
    }

    #endregion
}
