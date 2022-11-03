using System;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Console;

namespace testlab1weapon
{
    internal class Program
    {
        static void Main()
        {
            Funcfunc4Assignment();
        }
        static void Show3Assignment()
        {
            //для демонстрації роботи механізму 3 завдання
            MechanicDriver mchdriver = new MechanicDriver("Ivan");
            TanksDivision tnkdvsn = new TanksDivision(mchdriver);
            mchdriver.Fueling();
            ReadKey();
        }
        static void InitSituat5Assignment()
        {
            //5 assignment - initialize situation and variation of solution
            T64 tabk;
            try
            {
                tabk = new T64 { Diameterofgun = -10 };
            }
            catch (T64WrongDiametrException ex)
            {
                WriteLine($"Error:{ex.Message}");
                WriteLine($"Uncorrect value:{ex.ValueOfDiametr}");
            }
            catch (Exception exe)
            {
                WriteLine($"{exe.Message}");
            }
        }
        static void AnonimusMethod4Assignment()
        {
            ///anonim-method
            StrikeWeapon stw = new StrikeWeapon();
            stw.NotifyAboutShot += delegate (string message) { WriteLine($"{message}"); };
            stw.Shot();
        }
        static void Lambda4Assignment()
        {
            ///lambda
            StrikeWeapon stw = new StrikeWeapon();
            stw.NotifyAboutShot += message=>WriteLine($"{message}");
            stw.Shot();
        }
        static void Actfunc4Assignment()
        {
            ///Action 
            StrikeWeapon stw = new StrikeWeapon();
            stw.NotifyAboutShot += message => WriteLine($"{message}");
            void DoShot(int distance,int damage, Action<int,int> shot) => shot(distance,damage);
            DoShot(300, 500, stw.ShotAction);
        }
        static void Funcfunc4Assignment()
        {
            //Func
            StrikeWeapon stw = new StrikeWeapon();
            int speedofpatron = 7;
            stw.NotifyAboutShot += message => WriteLine($"{message}");
            Func<int, int, string> CreateNotify = (distance, speed) => $"You shot from this Strike Weapon from distance of {distance} and get {distance * speed} of damage!";
            stw.ShotFunc(CreateNotify(stw.distanceofstrike, speedofpatron));
        }
    }
    public delegate void FuelingHandle(MechanicDriver MD, FuelEventArgs CtrlEvArg);
    public delegate void ShotHandler(string message);
    interface IMoving
    {
        void Move();
        void CurrentSpeed();
    }
    interface IAim//ціль для зброї типу
    {
        int CountsOfPatronsInOurWeapon{ get; }
        void LocationOfAim(string location);
        void CurrentSpeed();
    }
    abstract class Weapon
    {
        protected int damage;
        protected string countrycreater;
        public Weapon()
        {
            damage = 100;
            countrycreater = "Ukraine";
        }
        public virtual void Print() => WriteLine($"Information about the weapon:\nDamage:{damage};\nCountry-Creator:{countrycreater};");
        public abstract void Shot();
        public abstract void Recharge();
    }
    class StrikeWeapon : Weapon
    {
        public event ShotHandler NotifyAboutShot;
        public int distanceofstrike;
        public StrikeWeapon() => distanceofstrike = 400;
        public override void Print()
        {
            base.Print();
            WriteLine($"Distance of Strike:{distanceofstrike};");
        }
        public override void Shot() => NotifyAboutShot?.Invoke($"You shot from this Strike Weapon from distance of {distanceofstrike} and get {damage} of damage!");
        public void ShotAction(int distance, int damage) => NotifyAboutShot?.Invoke($"You shot from this Strike Weapon from distance of {distance} and get {damage} of damage!");
        public void ShotFunc(string message) => NotifyAboutShot?.Invoke($"{message}");
        public override void Recharge()
        {
            Random rnd = new Random();
            WriteLine($"This strike weapon was recharging for {rnd.Next(20, 30)} seconds.");
        }
    }
    class AK47 : StrikeWeapon, IAim
    {
        static int countsofpatrons;
        static int lenghtofAK47;

        static AK47()
        {
            countsofpatrons = 32;
            lenghtofAK47 = 123;
        }
        public override void Print()
        {
            base.Print();
            WriteLine($"Counts of patrons:{countsofpatrons};\nLenght of AK47:{lenghtofAK47};");
        }
        public override void Shot() => WriteLine($"You shot from this AK47 and get {damage} of damage!");
        Random rnd = new Random();
        public int CountsOfPatronsInOurWeapon
        {
            get => countsofpatrons - rnd.Next(0, countsofpatrons);
        }
        public void LocationOfAim(string location) => WriteLine($"Aim at {location};");
        public void CurrentSpeed() => WriteLine($"Current speed of aim is {rnd.Next(20, 30)}");
    }
    class Tank : Weapon, IMoving, IAim
    {
        public int speed;
        public bool dynamicarmor;
        public Tank()
        {
            speed = 70;
            dynamicarmor = true;
        }
        public int CurrentSpeed
        {
            get => speed;
        }
        Random rnd = new Random();
        public int CountsOfPatronsInOurWeapon
        {
            get => rnd.Next(0, 20);
        }
        public void Move() => WriteLine($"This Tank start moving and reached {speed} km/h;");

        void IAim.CurrentSpeed() => WriteLine($"Current speed of aim is {rnd.Next(20, 30)} km/h;");

        void IMoving.CurrentSpeed() => WriteLine($"Current speed of tank is {speed} km/h;");

        public void LocationOfAim(string location) => WriteLine($"Aim at {location};");
        public override void Print()
        {
            base.Print();
            WriteLine($"Speed:{speed};\nDynamic Armor:{dynamicarmor.ToString()};");
        }
        public override void Shot() => WriteLine($"You shot from this Tank and get {damage} of damage!");
        public override void Recharge() => WriteLine($"This Tank was recharging for {rnd.Next(30, 35)} seconds.");
        public virtual void Fuel(MechanicDriver MD, FuelEventArgs wargs)
        {
            WriteLine($"Mechamic-Driver {MD.name} is Fueling tank.");
            if (wargs.countsoffuel > 67)
                WriteLine("Tank is operated by professional mechanic-driver!");
            else
                WriteLine("Tank is operated by unprofesional mechanic-driver!");
        }
    }
    sealed class T64 : Tank
    {
        public int diameterofgun;
        private int thicknessofarmor = 5;
        //T64 tank;
        public T64()
        {
            // tank = new T64 {diameterofgun=205,thicknessofarmor=5 };
            this.diameterofgun = 125;
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
        public override void Recharge()
        {
            Random rnd = new Random();
            WriteLine($"This T-64 was recharging for {rnd.Next(30, 35)} seconds.");
        }
        public override void Fuel(MechanicDriver MD, FuelEventArgs wargs)
        {
            WriteLine($"Mechamic-Driver {MD.name} is Fueling T-64.");
            if (wargs.countsoffuel > 125)
                WriteLine("T-64 is fully refueled!");
            else
                WriteLine("T-64 not enough refueled!");
        }
        //Properties
        public int Diameterofgun
        {
            get => diameterofgun;
            set
            {
                if (value > 200)
                    throw new T64WrongDiametrException(new T64WrongDiametrArgs("Diametr of Tank T64 bigger than 200 ml, there are no tanks of this model with such gun!", value));
                else if (value <= 0)
                    throw new Exception("Diametr of Tank T64 smaller than 0 - its unreal!");
                else
                    diameterofgun = value;
            }
        }
        public int ThicknessofarmorT64 { get => 0; }
    }
    class T72 : Tank
    {
        public string typeofwheel;
        public T72()
        {
            typeofwheel = "winter's";
        }
        public override void Shot()
        {
            WriteLine($"You shot from this Tank T-72 and get {damage} of damage!");
        }
        public override void Recharge()
        {
            Random rnd = new Random();
            WriteLine($"This T-72 was recharging for {rnd.Next(30, 35)} seconds.");
        }
        public override void Fuel(MechanicDriver MD, FuelEventArgs wargs)
        {
            WriteLine($"Mechamic-Driver {MD.name} is Fueling T-72.");
            if (wargs.countsoffuel > 155)
                WriteLine("T-72 is fully refueled!");
            else
                WriteLine("T-72 not enough refueled!");
        }
    }
    class TanksDivision
    {
        Tank[] tanks;
        public TanksDivision(MechanicDriver md)
        {
            tanks = new Tank[2];
            tanks[0] = new T64();
            tanks[1] = new T72();
            foreach (Tank t in tanks)
                md.MhDrvFuelingEvent += new FuelingHandle(t.Fuel);
        }
    }
    static class T64Extension//розширення
    {
        public static void Stop(this T64 tank, string location) => WriteLine($"This tank T64 stopped at {location};");
    }
    public class MechanicDriver
    {
        public string name;
        public event FuelingHandle MhDrvFuelingEvent;
        public MechanicDriver(string name) => this.name = name;
        public void Fueling()
        {
            int countoffuel;
            FuelEventArgs CtrlEvgAr;
            try
            {
                Write("Enter counts of fuel in iters:");
                countoffuel = Int32.Parse(ReadLine());
                CtrlEvgAr = new FuelEventArgs(countoffuel);
            }
            catch
            {
                CtrlEvgAr = new FuelEventArgs();
            }
            if (MhDrvFuelingEvent != null)
                MhDrvFuelingEvent((MechanicDriver)this, CtrlEvgAr);
        }
    }
    public class FuelEventArgs : EventArgs
    {
        public int countsoffuel;
        public FuelEventArgs(int countsoffuel) => this.countsoffuel = countsoffuel;
        public FuelEventArgs() : this(20) { }
    }
    class T64WrongDiametrException : ArgumentException
    {
        public int ValueOfDiametr { get; }
        public T64WrongDiametrException(T64WrongDiametrArgs argument) : base(argument.message)
        {
            this.ValueOfDiametr = argument.Value;
        }
    }
    class T64WrongDiametrArgs
    {
        public string message { get; set; }
        public int Value { get; set; }
        public T64WrongDiametrArgs(string message, int value)
        {
            this.message = message;
            this.Value = value;
        }
    }
}
