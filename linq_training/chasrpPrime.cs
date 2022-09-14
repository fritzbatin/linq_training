using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Linq_training
{
     class chasrpPrime
    {
        static void MultipleLegs(PetStruct petStruct, PetClass petClass)
        {
            petStruct.Legs = petStruct.Legs * 2;
            petClass.Legs = petClass.Legs * 2;
            Console.WriteLine("Internal Method - A {0} has {1} Legs", petStruct.Type, petStruct.Legs); //Internal Method - A Dog has 8 Legs
            Console.WriteLine("Internal Method - A {0} has {1} Legs", petClass.Type, petClass.Legs); //Internal Method - A Duck has 4 Legs

        }

        public static void Prime()
        {
            /// Values Versus Reference Types
            /// 	In STRUCT, each variable contains its own copy of the data (except in the case of the ref and out parameter variables), 
            /// 	and an operation on one variable does not affect another variable
            /// 	
            ///     In CLASS, two variables can contain the reference of the same object 
            /// 	and any operation on one variable can affect another variable
            /// 	
            PetStruct dog = new PetStruct();
            dog.Type = PetType.Dog;
            dog.hasFur = true;
            dog.Legs = 4;
            dog.Name = "Pulgoso";

            PetClass duck = new PetClass();
            duck.Type = PetType.Duck;
            duck.hasFur = false;
            duck.Legs = 2;
            duck.Name = "Donalduck";


            Console.WriteLine("{2} {0} has {1} Legs", dog.Type, dog.Legs, dog.Name); // A Dog has 4 Legs
            Console.WriteLine("{2} {0} has {1} Legs", duck.Type, duck.Legs, duck.Name); //A Duck has 2 Legs
            Console.WriteLine();

            MultipleLegs(dog, duck);
            Console.WriteLine();

            Console.WriteLine("{2} {0} has {1} Legs", dog.Type, dog.Legs, dog.Name); // A Dog has 4 Legs
            Console.WriteLine("{2} {0} has {1} Legs", duck.Type, duck.Legs, duck.Name); //A Duck has 4 Legs
            Console.WriteLine();
            /// Arrays and Collections
            /// 

            int[] intArray = new int[5];
            string[] stringArray = new string[5];
            int[] populatedInArray = new int[] { 0, 1, 2, 3, 4, 5 };
            string[] populatedStringArray = new string[] { "one", "two", "three" };

            intArray[0] = 5;
            intArray[2] = 15;

            int firstValue = intArray[0];
            int[,] multiInt = new int[2, 3];
            int[,] multiPopulatedInt = { { 1, 2, 3 }, { 5, 6, 7 } };
            //       (0)  ,    (1)
            //multiPopulatedInt[0, 0]
            //                  |
            //                  index number of Array Group
            //multiPopulatedInt[0, 0]
            //                     |
            //                     index number of Value
            // output would be 1



            int firstMultiValue = multiPopulatedInt[0, 0]; //value 1
            int secondMultiValue = multiPopulatedInt[1, 2]; // value 7

            List<string> listOfString = new List<string>();
            listOfString.Add("first string");
            Console.WriteLine("listOfString.Add(\"first string\")");
            listOfString.ToList().ForEach(l => Console.WriteLine(l));
            Console.WriteLine();

            Console.WriteLine("listOfString.Insert(0,\"Inserted String\")");
            listOfString.Insert(0, "Inserted String");
            listOfString.ToList().ForEach(l => Console.WriteLine(l));
            Console.WriteLine();

            Console.WriteLine("listOfString.Sort()");
            listOfString.Sort();
            listOfString.ToList().ForEach(l => Console.WriteLine(l));
            Console.WriteLine();

            Console.WriteLine("listOfString.Remove(\"first string\")");
            listOfString.Remove("first string");
            listOfString.ToList().ForEach(l => Console.WriteLine(l));
            Console.WriteLine();

            Console.WriteLine("listOfString.RemoveAt(0)");
            listOfString.RemoveAt(0);
            listOfString.ToList().ForEach(l => Console.WriteLine(l));
            Console.WriteLine("listOfString.Count() = {0}", listOfString.Count());
            Console.WriteLine();

            //var thefirstStirngs = listOfString[0];


            Dictionary<string, string> names = new Dictionary<string, string>();
            names.Add("James", "Bond");
            names.Add("Money", "Peny");
            names.Add("Jane", "Bond");

            Console.WriteLine($"The name is {names["James"]}");
            Console.WriteLine();

            Console.WriteLine("names.Remove(\"James\")");
            names.Remove("James");
            names.ToList().ForEach(na => Console.WriteLine(na));
            Console.WriteLine();
            ///LINQ
            ///
            List<PetClass> pets = new List<PetClass>();
            pets.Add(new PetClass { hasFur = false, Legs = 2, Name = "Donald", Type = PetType.Duck });
            pets.Add(new PetClass { hasFur = true, Legs = 4, Name = "Pluto", Type = PetType.Dog });

            foreach (var item in pets)
            {
                Console.WriteLine($"Name: {item.Name} \n  Type: {item.Type}");
            }
            Console.WriteLine();

            List<PetClass> petResults = (from p in pets
                                         where p.Type == PetType.Dog
                                         select p).ToList();

            Console.WriteLine($"found {petResults.Count()} Dogs");
            Console.WriteLine();

            PetClass petClassResult = pets.FirstOrDefault(pfd => pfd.Type == PetType.Dog);
            Console.WriteLine($"Name: {petClassResult.Name} \n -{petClassResult.Type}");
        }
        struct PetStruct
        {
            public int Legs;
            public PetType Type;
            public string Name;
            public bool hasFur;

        }

        class PetClass
        {
            public int Legs;
            public PetType Type;
            public string Name;
            public bool hasFur;
        }

        enum PetType
        {
            Dog,
            Duck
        }

    }
}
