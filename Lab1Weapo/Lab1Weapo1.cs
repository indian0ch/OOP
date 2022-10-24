using System;
using System.Diagnostics.Metrics;
using static System.Console;

namespace testlab1weapon
{
    internal class Program
    {
        static void Main()
        {

            AK47 ak1 = new AK47();
            ak1.Print();

            /*
            for (int i = 0; i < 50000; i++)
            {
                AK47 ak2 = new AK47();
            }
            WriteLine("Memory before collect: " + GC.GetTotalMemory(false));
            WriteLine("Generation: " + GC.GetGeneration(ak1));
            GC.Collect(2, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();
            Console.WriteLine("\nсборка мусора ...\n");
            WriteLine("Memory after collect: " + GC.GetTotalMemory(false));
            WriteLine("Generation: " + GC.GetGeneration(ak1));
            */
        }
    }
    class Weapon : IDisposable
        {
        protected bool disposed = false;// Для перевірки, чи викликався вже метод Dispose () .
        public int damage;
            public string countrycreater;

           public Weapon()
            {
                damage = 100;
                countrycreater = "Ukraine";
            }
        public int Damage
        {
            get
            {
                return damage;
            }
            set
            {
                if (value > 1000)
                    WriteLine("Its unreal!");
                else
                    damage = value;
            }
        }
        public virtual void Print()
        {
            WriteLine($"Information about the weapon:\nDamage:{damage};\nCountry-Creator:{countrycreater};");
        }
        public  virtual void Shot()
        {
            WriteLine($"You shot from this weapon and get {damage} of damage!");
        }

        public void Dispose()
        {
            // Виклик допоміжного методу. "true" - якщо очищення було викликане користувачем об"єкта. 
            CleanUp(true);
            // повідомляємо про заборону фіналізації, адже некеровані ресурси уже були очищені
            GC.SuppressFinalize(this);
        }
        private void CleanUp(bool disposing)
        {
            // Перевірка, чи було виконано очищення 
            if (!this.disposed)
            {
                if (disposing)
                {
                    // WriteLine("Free any other managed objects here.");
                }
                // WriteLine("Free any unmanaged objects here.");
            }
            disposed = true;
        }
        ~Weapon()
        {
            CleanUp(false);
           // WriteLine("Destructor of class Weapon is working!");
        }

        }
    class StrikeWeapon : Weapon
        {
            public int distanceofstrike;

       public StrikeWeapon()
        {
            distanceofstrike = 400;
        }

        public override void Print()
        {
            base.Print();
            WriteLine($"Distance of Strike:{distanceofstrike};");
        }
        public override void Shot()
        {
            WriteLine($"You shot from this Strike Weapon and get {damage} of damage!");
        }
        ~StrikeWeapon()
        {
          //  WriteLine("Destructor of class StrikeWeapon is working!");
        }
    }
    class AK47 : StrikeWeapon
    {
        static int countsofpatrons;
        static int lenghtofAK47;
        
        static AK47()
        {
            countsofpatrons =32;
            lenghtofAK47 = 123;
        }

        public override void Print()
        {
            base.Print();
            WriteLine($"Counts of patrons:{countsofpatrons};\nLenght of AK47:{lenghtofAK47};");
        }
        public override void Shot()
        {
            WriteLine($"You shot from this AK47 and get {damage} of damage!");
        }
        ~AK47()
        {
           // WriteLine("Destructor of class AK47 is working!");
        }
    }
    class AK47Cleaned : AK47
    {
        private  string dateofcleaning;//дата чистки автомата
        private  AK47Cleaned instance = null;
        private AK47Cleaned()
        {
            dateofcleaning = (DateTime.Now).ToLongDateString();
        }
        public  AK47Cleaned GetInstance()
        {
            if (instance == null)
            {
                instance = new AK47Cleaned();
            }
            return instance;
        }
        public override void Print()
        {
            base.Print();
            WriteLine($"AK-47 cleaned at:{dateofcleaning}");
        }
        ~AK47Cleaned()
        {
            // WriteLine("Destructor of class AK47Cleaned is working!");
        }
    }
    class Tank : Weapon
    {
        public int speed;
        public bool dynamicarmor;
        public Tank()
        {
            speed = 70;
            dynamicarmor = true;
        }
        public override void Print()
        {
            base.Print();
            WriteLine($"Speed:{speed};\nDynamic Armor:{dynamicarmor.ToString()};");
        }
        public override void Shot()
        {
            WriteLine($"You shot from this Tank and get {damage} of damage!");
        }
        ~Tank()
        {
          //  WriteLine("Destructor of class Tank is working!");
        }
    }
    class T64 : Tank
    {
        public int diameterofgun;
        private int thicknessofarmor = 5;

        public T64(int damage, string countrycreator,int speed,bool dynamicarmor,int diameterofgun) : base()
        {
            this.diameterofgun = diameterofgun;
        }

        public override void Print()
        {
            base.Print();
            WriteLine($"Diameter of the gun:{diameterofgun};");
        }
        public override void Shot()
        {
            WriteLine($"You shot from this Tank T-64 and get {damage} of damage!");
        }
        //Properties
        public int Diameterofgun
        {
            get
            {
                return diameterofgun;
            }
            set
            {
                if (value > 200 && value <= 0)
                    WriteLine("Is unreal diameter of the gun of T-64!");
                else
                    diameterofgun = value;
            }
        }
        public int ThicknessofarmorT64
        {
            get
            {
                WriteLine("Is a secret information!");
                return 0;
            }
        }
        ~T64()
        {
           // WriteLine("Destructor of class T64 is working!");
        }
    }
    

}

