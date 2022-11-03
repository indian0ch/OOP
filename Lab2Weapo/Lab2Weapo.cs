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
            /*
            T64 tank64 = new T64();
            tank64.Print();
            tank64.Stop("forest");
            */
            /*
            GetLatitudeOfTarget GL = new GetLatitudeOfTarget(T64.CalculationGeoLatitudeOfTarget);
            GL.Invoke(56.78, 51.994);
            */

            /*
            //для демонстрації роботи механізму 3 завдання
            MechanicDriver mchdriver = new MechanicDriver("Ivan");
            TanksDivision tnkdvsn = new TanksDivision(mchdriver);
            mchdriver.Fueling();
            ReadKey();
            */
            /*
            MechanicDriver MD = new MechanicDriver("Andriy");
            FuelEventArgs FEA = new FuelEventArgs();
            //анонімний метод - приклад реалізації
            FuelingHandle FH = (MD, FEA) => WriteLine("Заправка закінчена!");
            FH(MD,FEA);
            */

            /*
            //5 assignment - initialize situation and variation of solution
            T64 tabk;
            try
            {
                 tabk = new T64 { Diameterofgun = -10 };
            }
            catch(T64WrongDiametrException ex) 
            {
                WriteLine($"Error:{ex.Message}");
                WriteLine($"Uncorrect value:{ex.ValueOfDiametr}");
            }
            catch(Exception exe)
            {
                WriteLine($"{exe.Message}");
            }
            */

            ///lambda-вирази
            //  GetLatitudeOfTarget Gl = delegate (double x, double y) { return (y - 500) * (x / 111.2); };
            // GetLatitudeOfTarget GL = (x, y) => { return (y - 500) * (x / 111.2); };
            //GetLatitudeOfTarget GL = (x, y) => (y - 500) * (x / 111.2);
        }
    }
    public delegate double GetLatitudeOfTarget(double X,double Y);
    public delegate void FuelingHandle(MechanicDriver MD,FuelEventArgs CtrlEvArg);
    interface IMoving
    {
        void Move();
        void CurrentSpeed();
    }
    interface IAim//ціль для зброї типу
    {
        int CountsOfPatronsInOurWeapon
        {get;}
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
        public override void Shot() => WriteLine($"You shot from this Strike Weapon and get {damage} of damage!");
        public override void Recharge() 
        {
            Random rnd = new Random();
            WriteLine($"This strike weapon was recharging for {rnd.Next(20,30)} seconds.");
        }
    }
    class AK47 : StrikeWeapon,IAim
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
        public  int CountsOfPatronsInOurWeapon
        {
            get => countsofpatrons - rnd.Next(0, countsofpatrons); 
        }
        public void LocationOfAim(string location) => WriteLine($"Aim at {location};");

        public void CurrentSpeed() => WriteLine($"Current speed of aim is {rnd.Next(20,30)}");
        public static double CalculationGeoLatitudeOfTarget(double X, double Y)//типу визначення гео координат за параметричними
        {
            double geolatitude = (Y - 500) * (X / 111.2);
            return geolatitude;
        }
    }
    class AK47Cleaned : AK47
    {
        private string dateofcleaning;//дата чистки автомата
        private AK47Cleaned instance = null;
        private AK47Cleaned()
        {
            dateofcleaning = (DateTime.Now).ToLongDateString();
        }
        public AK47Cleaned GetInstance()
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
    }
    class Tank : Weapon,IMoving,IAim
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
            get => rnd.Next(0,20);
        }
        public void Move()=> WriteLine($"This Tank start moving and reached {speed} km/h;");
        
        void IAim.CurrentSpeed()=> WriteLine($"Current speed of aim is {rnd.Next(20, 30)} km/h;");
        
        void IMoving.CurrentSpeed()=> WriteLine($"Current speed of tank is {speed} km/h;");
        
        public void LocationOfAim(string location)=> WriteLine($"Aim at {location};");
        public override void Print()
        {
            base.Print();
            WriteLine($"Speed:{speed};\nDynamic Armor:{dynamicarmor.ToString()};");
        }
        public override void Shot()=> WriteLine($"You shot from this Tank and get {damage} of damage!");
        public override void Recharge()=>WriteLine($"This Tank was recharging for {rnd.Next(30, 35)} seconds.");
        public virtual void Fuel(MechanicDriver MD, FuelEventArgs wargs)
        {
            WriteLine($"Mechamic-Driver {MD.name} is Fueling tank.");
            if (wargs.countsoffuel > 67)
                WriteLine("Tank is operated by professional mechanic-driver!");
            else
                WriteLine("Tank is operated by unprofesional mechanic-driver!");
        }
    }
    sealed class  T64 : Tank
    {
        public int diameterofgun;
        private int thicknessofarmor = 5;
        //T64 tank;
        public T64() 
        {
           // tank = new T64 {diameterofgun=205,thicknessofarmor=5 };
           this.diameterofgun = 125;
        }
        
        public  override void Print()
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
        public static double CalculationGeoLatitudeOfTarget(double X,double Y)//типу визначення гео координат за параметричними
        {
            double geolatitude = (Y-500)*(X / 111.2);
            return geolatitude;
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
        public int ThicknessofarmorT64{ get => 0;}
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
       public  static void Stop(this T64 tank,string location)=> WriteLine($"This tank T64 stopped at {location};");
    }
    public class MechanicDriver
    {
        public string name;
        public event FuelingHandle MhDrvFuelingEvent;
        public MechanicDriver(string name)=>this.name = name;
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
        public FuelEventArgs(int countsoffuel)=>this.countsoffuel = countsoffuel;
        public FuelEventArgs() : this(20) { }
    }
    class T64WrongDiametrException : ArgumentException
    {
        public int ValueOfDiametr {get;} 
        public T64WrongDiametrException(T64WrongDiametrArgs argument) : base(argument.message)
        {
            this.ValueOfDiametr = argument.Value;
        }
    }
    class T64WrongDiametrArgs
    {
        public string message { get; set; }
        public int Value { get; set; }
        public T64WrongDiametrArgs(string message,int value)
        {
            this.message = message;
            this.Value = value;
        }
    }

}
