using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_training
{
    /// <summary>
    /// IEnumerable - 1
    /// to interface and return value from a class
    /// </summary>
    public class Params : IEnumerable<int>
    {
        private int a, b, c;
        public Params(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public IEnumerator<int> GetEnumerator()
        {
            yield return a;
            yield return b;
            yield return c;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    /// <summary>
    /// IEnumerable - 2
    /// to expose some parts or fields from different type
    /// </summary>
    public class Person
    {
        private string Fname, Mname, Lname;

        public Person(string fname, string mname, string lname)
        {
            this.Fname = fname;
            this.Mname = mname;
            this.Lname = lname;
        }

        public IEnumerable<string> Names
        {
            get
            {
                yield return Fname;
                yield return Mname;
                yield return Lname;

                //Uncomment the line below and place somewhere to jump out of a method 

                //yield break;
            }
        }
    }

    /// <summary>
    /// Language Integrated Query -
    /// </summary>
    public static class linqTraining
    {
        public static void linq()
        {
            // IEnumerable - Inherit something like a list or a collection and return a value

            var p = new Params(1, 2, 3);
            foreach (var x in p)
            {
                Console.WriteLine(x);
            }

            var person = new Person("Fritz", "Jeremiah", "Batin");
            foreach (var name in person.Names)
            {
                Console.WriteLine(name);
            }

            /// LINQ to Objects
            // Enumerable static class
            //Enumerable.Empty<int>();

            foreach (var item in Enumerable.Repeat("Hello", 3))
            {
                Console.WriteLine(item);
            }

            foreach (var item in Enumerable.Range(1, 10))
            {
                Console.WriteLine(item);
            }

            var i = Enumerable.Range('a', 'z' - 'a').Select(c => (char)c);

            foreach (var item in i)
            {
                Console.WriteLine(item);
            }

            var index = Enumerable.Range(1, 10).Select(ii => new string('x', ii));
            foreach (var item in index)
            {
                Console.WriteLine(item);
            }


            /// Sequence(IEnumerable or IQueryable Colletion)

            var result = from s in index
                         where s.Contains("xxxxx") //Conditional Expression
                         select s;


            ///Standard Query Operation

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            List<string> greetings = new List<string>() { "Hi", "Hello", "Yow", "Howdy" };
            var res = from g in greetings
                      where g.StartsWith("H") &&
                            g.Length == 5
                      select g;

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }


            ///Filtering Data OfType and Where

            var numbers = Enumerable.Range(1, 10);
            var evenNumbers = numbers.Where(n => n % 2 == 0);
            foreach (var item in evenNumbers)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            object[] values = { 1, 2.5, 3, 4.56f };
            foreach (var item in values.OfType<int>())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            foreach (var item in values.OfType<double>())
            {
                Console.WriteLine(item);
            }

            foreach (var item in values.OfType<float>())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();


            ///Sorting Data OrderBy, ThenBy, Reverse

            var rand = new Random();
            var randomValues = Enumerable.Range(1, 10).Select(_ => rand.Next(10));
            foreach (var item in randomValues)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            foreach (var item in Enumerable.Range(1, 10).Select(_ => rand.Next(10)))
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var csvString = new Func<IEnumerable<int>, string>(values1 =>
            {
                return string.Join(",", values1.Select(v => v.ToString()).ToArray());
            });
            var mydata = csvString(randomValues);
            Console.WriteLine(mydata);

            var mydata1 = csvString(randomValues).OrderBy(x => x);
            string _mydata1 = "";
            foreach (var item in mydata1)
            {
                int n;
                var isNumeric = int.TryParse(item.ToString().Replace(",", ""), out n);
                if (isNumeric)
                {
                    _mydata1 += n + ",";
                }
            }




            Console.WriteLine(_mydata1.TrimEnd(new Char[] { ',' }));
            Console.WriteLine();

            var customer = new List<Customer>
                {
                    new Customer { Name = "Adam", Age = 36, Gender = "Male"},
                    new Customer { Name = "Adam", Age = 31, Gender = "Male" },
                    new Customer { Name = "Jack", Age = 40, Gender = "Male" },
                    new Customer { Name = "Claire", Age = 20, Gender = "Female"},
                    new Customer { Name = "Boris", Age = 18 , Gender = "Male"},
                    new Customer { Name = "Albert", Age = 21 , Gender = "Male"},
                    new Customer { Name = "Adam", Age = 40 , Gender = "Male"}
                };

            foreach (var item in customer)
            {
                Console.WriteLine("Name:{0} Age:{1}", item.Name, item.Age);
            }
            Console.WriteLine();

            ///Order

            // IOrderedEnumerable<Customer> sortedCustomer = customer.OrderBy(c => c.Name);
            var sortedCustomer = from c in customer
                                 orderby c.Name
                                 select c;
            foreach (var item in sortedCustomer)
            {
                Console.WriteLine("Name:{0} Age:{1}", item.Name, item.Age);
            }
            Console.WriteLine();

            foreach (var item in sortedCustomer.OrderBy(s => s.Age).ThenByDescending(s => s.Name))
            {
                Console.WriteLine("Name:{0} Age:{1}", item.Name, item.Age);
            }
            Console.WriteLine();

            string s1 = "This is a test";
            Console.WriteLine(new string(s1.Reverse().ToArray()));
            Console.WriteLine();

            ///Grouping

            // IEnumerable<IGrouping<string, Customer>> byName = customer.GroupBy(c => c.Name);
            var byName = from c in customer
                         group c by c.Name into cg
                         select new { cg.Key, cg };

            foreach (var item in byName)
            {
                Console.WriteLine("Key:{0}", item.Key);
                item.cg.ToList().ForEach(ct => Console.WriteLine(ct.Age));
                //item.ToList().ForEach(ct => Console.WriteLine(ct.Age));

            }

            var byAge = customer.GroupBy(ca => ca.Age < 30, ca => ca.Name);
            foreach (var item in byAge)
            {
                if (item.Key)
                {
                    Console.WriteLine("Ages below 30");
                    foreach (var name in item.OrderBy(o => o))
                    {
                        Console.WriteLine($" -{name}");
                    }
                }
                else
                {
                    Console.WriteLine("Ages above 30");
                    foreach (var name in item.OrderBy(o => o))
                    {
                        Console.WriteLine($" -{name}");
                    }
                }

                //foreach (var name in item.OrderBy(o => o))
                //{
                //    Console.WriteLine($" -{name}");
                //}

            }
            Console.WriteLine();

            List<string> customerName = new List<string>();

            var byAgeName = customer.GroupBy(c => c.Age, c => c.Name);
            foreach (var itemResult in byAgeName)
            {
                Console.WriteLine($"These customer are {itemResult.Key} years old");
                foreach (var name in itemResult.OrderBy(o => o))
                {
                    Console.Write($" -{ name }  ||  ");
                    name.ToList().ForEach(gg => customerName.Add(name));

                    var customerGender = from g in customer
                                         where g.Name == name && g.Age == itemResult.Key
                                         select new { gender = g.Gender };

                    customerGender.ToList().ForEach(cg => Console.WriteLine(cg.gender));

                }
            }
            Console.WriteLine();

            customerName.Distinct().OrderBy(od => od).ToList().ForEach(c => Console.WriteLine(c));


            ///Set Operation
            string word1 = "helloooo";
            string word2 = "help";
            Console.WriteLine($"Word1 {word1}");
            Console.WriteLine($"Word2 {word2}");
            Console.WriteLine();

            Console.WriteLine("Distinct word1");
            word1.Distinct().ToList().ForEach(w1 => Console.WriteLine(w1));
            Console.WriteLine();

            Console.WriteLine("Intersect word1 to word2");
            var lettersInBoth = word1.Intersect(word2);
            lettersInBoth.ToList().ForEach(l => Console.WriteLine(l));
            Console.WriteLine();

            Console.WriteLine("Union word1 to word2");
            word1.Union(word2).ToList().ForEach(u => Console.WriteLine(u));
            Console.WriteLine();

            Console.WriteLine("Except word1 to word2");
            word1.Except(word2).ToList().ForEach(e => Console.WriteLine(e));

            ///Quantifier

            int[] numbers1 = { 1, 2, 3, 4, 5 };
            Console.WriteLine("Numbers: ");
            numbers1.ToList().ForEach(x => Console.WriteLine(x));
            Console.WriteLine();

            /// All() is to checks whether every single element in the collection satisfy the operation

            Console.WriteLine("Are all numbers > 0? \n{0}", numbers1.All(x => x > 0));
            Console.WriteLine();

            Console.WriteLine("Are all numbers odd? \n{0}", numbers1.All(x => x % 2 == 1));
            Console.WriteLine();

            // Any() is to check the Collection has any elements at all

            Console.WriteLine("Any number < 2? \n{0}", numbers1.Any(x => x < 2));
            Console.WriteLine();

            Console.WriteLine(new int[] { }.Any());
            Console.WriteLine();

            Console.WriteLine(new int[] { 2 }.Any());
            Console.WriteLine();

            Console.WriteLine("Contains 5? " + numbers1.Contains(5));
            Console.WriteLine();

            ///Partitioning

            var numbers2 = new[] { 3, 3, 2, 2, 1, 1, 2, 2, 3, 3 };

            Console.WriteLine("Numbers are..");
            numbers2.ToList().ForEach(n => Console.Write("{0} ", n));
            Console.WriteLine("\n");

            Console.WriteLine("Skip:2 Take:6");
            numbers2.Skip(2).Take(6).ToList().ForEach(n => Console.WriteLine(n));
            Console.WriteLine();

            Console.WriteLine("Skip:2 Take:60");
            numbers2.Skip(2).Take(60).ToList().ForEach(n => Console.WriteLine(n));
            Console.WriteLine();

            Console.WriteLine("SkipWhile (Skip Start of the list): i==3");
            numbers2.SkipWhile(ii => ii == 3).ToList().ForEach(n => Console.WriteLine(n));
            Console.WriteLine();

            Console.WriteLine("TakeWhile (Take data of the list): i==3");
            numbers2.TakeWhile(ii => ii == 3).ToList().ForEach(n => Console.WriteLine(n));
            Console.WriteLine();

            ///Join and GroupJoin Operations
            var employee = new Employee[]
            {
                new Employee("Jane", "jane@foo.com"),
                new Employee("John", "john@foo.com"),
                new Employee("Chris", string.Empty)

            };
            Console.WriteLine("Employee Information");
            employee.ToList().ForEach(e => Console.WriteLine("Name:{0} | Email:{1}", e.Name, e.Email));
            Console.WriteLine();

            var records = new Records[]
            {
                new Records("jane@foo.com","JaneAtFoo"),
                new Records("jane@foo.com","JaneAtHome"),
                new Records("john@foo.com","John1980"),
                new Records("Chris","ChrisNothing"),

            };
            Console.WriteLine("Employee Records");
            records.ToList().ForEach(r => Console.WriteLine("Mail:{0} | SkypeID:{1}", r.Mail, r.SkypeId));
            Console.WriteLine();

            ///Join
            Console.WriteLine("Join");
            var query = employee.Join(records,
                x => x.Email,
                y => y.Mail,
                (emp, rec) =>
                new { Name = emp.Name, SkypeId = rec.SkypeId }
            );
            query.ToList().ForEach(q => Console.WriteLine(q));
            Console.WriteLine();

            foreach (var item in query)
            {
                Console.WriteLine("{0} has skype ID {1}", item.Name, item.SkypeId);
            }
            Console.WriteLine();

            ///GroupJoin
            Console.WriteLine("GroupJoin");
            var queryGroup = employee.GroupJoin(records,
                x => x.Email,
                y => y.Mail,
                (emp, rec) =>
                new { Name = emp.Name, SkypeIds = rec.Select(r => r.SkypeId).ToArray() });

            List<string> myString = new List<string> { };
            foreach (var item in queryGroup)
            {
                Console.WriteLine("Name {0}, \n SkypIds", item.Name);
                item.SkypeIds.ToList().ForEach(iii => Console.WriteLine(" -{0}", iii));
                item.SkypeIds.ToList().ForEach(iii => myString.Add(iii));
            }
            Console.WriteLine();

            ///Equality Operations(SequenceEqual, NUnit comparisons)

            var arr1 = new[] { 1, 2, 3, 4 };
            Console.WriteLine("arr1:");
            arr1.ToList().ForEach(a => Console.Write("{0} ", a));
            Console.WriteLine("\n");

            var arr2 = new[] { 1, 2, 3, 4 };
            Console.WriteLine("arr2:");
            arr2.ToList().ForEach(a => Console.Write("{0} ", a));
            Console.WriteLine("\n");


            var list1 = new List<int> { 1, 2, 3, 4 };
            Console.WriteLine("list1:");
            list1.ToList().ForEach(a => Console.Write("{0} ", a));
            Console.WriteLine("\n");


            Console.WriteLine("Is arr1 == arr2 ? \n {0}", arr1 == arr2);
            Console.WriteLine("Is arr1.Equals(arr2) ? \n {0}", arr1.Equals(arr2));

            Console.WriteLine("Is arr1.SequenceEqual(arr2) ? \n {0}", arr1.SequenceEqual(arr2));

            Console.WriteLine("Is arr1.SequenceEqual(list1) ? \n {0}", arr1.SequenceEqual(list1));

            ///Element Operation First,Last, Single, Elements


            var n1 = new List<int> { 1, 2, 3 };
            Console.Write("n1: ");
            n1.ToList().ForEach(n => Console.Write("{0}, ", n));

            var strList = n1.Select(x => x.ToString()).ToList();
            Console.WriteLine(strList.Aggregate((pp, x) => pp + "," + x));

            Console.WriteLine();

            Console.WriteLine("n1.First(): {0}", n1.First());

            Console.WriteLine("n1.First(x => x > 2): {0}", n1.First(x => x > 2));

            Console.WriteLine("n1.FirstOrDefault(x => x > 10): {0}", n1.FirstOrDefault(x => x > 10));

            Console.WriteLine("n1.Last(): {0}", n1.Last());

            Console.WriteLine("n1.Last(x => x < 3): {0}", n1.Last(x => x < 3));

            var sn1 = new List<int> { 123 };
            Console.Write("sn1: ");
            sn1.ToList().ForEach(sn => Console.Write("{0} ", sn));
            Console.WriteLine();

            Console.WriteLine("sn1.Single(): {0}", sn1.Single());
            sn1.Remove(123);
            Console.WriteLine("sn1.SingleOrDefault(): {0}", sn1.SingleOrDefault());
            Console.Write("new int[] { }.SingleOrDefault(): ");
            Console.WriteLine(new int[] { }.SingleOrDefault());

            Console.WriteLine("n1 Element position " + (char)34 + 1 + (char)34 + " at: {0}", n1.ElementAt(1));
            Console.WriteLine("n1 Element position " + (char)34 + 4 + (char)34 + " at: {0}", n1.ElementAtOrDefault(4));


            ///Agregate

            var nn1 = Enumerable.Range(1, 10);
            Console.WriteLine(nn1.Count());
            Console.WriteLine("Sum = {0}", nn1.Sum());
            Console.WriteLine($"nn1.Aggregate((a,b) => a + b): {nn1.Aggregate((a, b) => a + b)}");
            Console.WriteLine();

            Console.WriteLine($"nn1.Aggregate((a,b) => a * b): {nn1.Aggregate((a, b) => a * b)}");
            Console.WriteLine();

            Console.WriteLine("Average = {0}", nn1.Average());
            Console.WriteLine();

            var words = new[] { "one", "two", "three" };
            Console.WriteLine(words.Aggregate("hello", (ppp, x) => ppp + "," + x));
            Console.WriteLine();

            var myStringList = nn1.Select(x => x.ToString()).ToList(); // Convert Enumerable<int> to List<string>
            Console.WriteLine(myStringList.Aggregate((ppp, x) => ppp + "," + x)); //Aggregate
            Console.WriteLine();

        }

        class Customer
        {
            public string Name;
            public int Age;
            public string Gender;
        }
    }
}

public class Employee
{
    public string Name, Email;

    public Employee(string name, string email)
    {
        Name = name;
        Email = email;
    }
}

public class Records
{
    public string Mail, SkypeId;
    public Records(string mail, string skypeid)
    {
        Mail = mail;
        SkypeId = skypeid;
    }
}

