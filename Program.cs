using System;
using System.Diagnostics.Metrics;
using System.Linq;


namespace Pearson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Person o = new Person(familyName: "Molenda", firstName: "Krzysztof", age: 30);
                Person m = new Person(familyName: "Molenda", firstName: "Ewa", age: 29);
                Child d = new Child(familyName: "Molenda", firstName: "Anna", age: 14, father: o);
                Console.WriteLine(d);
                d = new Child(familyName: "Molenda", firstName: "Anna", age: 14, mother: m);
                Console.WriteLine(d);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }

    public class Person
    {
        private string first;
    
        public string FirstName {
            get
            {
                return first;
            }
            protected set 
            {
                first = value.Trim();

                for (int i = 0; i < first.Length;i++)
                {
                    if (char.IsWhiteSpace(first[i]))
                    {
                        first = first.Remove(i,1);
                        i--;
                    }
                    else if (!char.IsLetter(first[i]))
                    {
                        throw new ArgumentException("Wrong name!");
                    }
                }
                
                if (first.Length == 0)
                {
                    throw new ArgumentException("Wrong name!");
                }      
                else if (first.Length == 1) 
                {
                    first = first.ToUpper(); 
                }
                else
                {
                    first = Convert.ToString(char.ToUpper(first[0])+ first.Substring(1).ToLower());
                }                
            } 
        }
        private string family;
        public string FamilyName
        {
            get
            {
                return family;
            }
             protected set
            {
                family = value.Trim();
                for (int i = 0; i < family.Length; i++)
                {
                    if (char.IsWhiteSpace(family[i]))
                    {
                        family = family.Remove(i, 1);
                        i--;
                    }
                    else if (!char.IsLetter(family[i]))
                    {
                        throw new ArgumentException("Wrong name!");
                    }
                }

                if (family.Length == 0)
                {
                    throw new ArgumentException("Wrong name!");
                }
                else if (family.Length == 1)
                {
                    family = family.ToUpper();
                }
                else
                {
                    family = Convert.ToString(char.ToUpper(family[0]) + family.Substring(1).ToLower());
                }
            }
        }

        private int age1;
        public int Age
        {
            get
            {
                return age1;
            }
            protected set
            {
                if(value<0)
                {
                    throw new ArgumentException("Age must be positive!");
                }
                else
                {
                    age1 = value;
                }
            }
        }


        public Person(string firstName, string familyName, int age)
        {
            FirstName = firstName;
            FamilyName = familyName;
            Age = age;
        }

        public override string ToString()
        {
            return $"{FirstName} {FamilyName} ({Age})";
        }

        public void modifyFirstName(string name)
        {
            FirstName = name;
        }

        public void modifyFamilyName(string name)
        {
            FamilyName = name;
        }

        public virtual void modifyAge(int age)
        {
            Age = age;
        }

        public string getFamilyName()
        {
            return FamilyName;
        }

        public string getFirstName()
        {
            return FirstName;
        }

        public int getAge()
        {
            return Age;
        }

    }
    
    public class Child : Person
    {
        Person Mother;
        Person Father;
        public Child(string firstName, string familyName, int age, Person mother = null, Person father = null) : base(firstName, familyName, age)
        {
            Mother = mother;
            Father = father;

            if (age > 15 || age < 0)
            {
                throw new ArgumentException("Child’s age must be less than 15!");
            }
        }

        public override void modifyAge(int age)
        {
            if(age < 15 && age > 0)
            {
                Age = age;
            }
            else
            {
                throw new ArgumentException("Child’s age must be less than 15!");
            }
        }

            public override string ToString()
        {
            if (Mother == null && Father == null)
            {
                return base.ToString() + "\n" + "mother: No data" + "\n" + "father: No data";
            }
            else if (Mother == null)
            {
                return base.ToString() + "\n" + "mother: No data" + "\n" + $"father: {Father.getFirstName()} {Father.getFamilyName()} ({Father.getAge()}) ";
            }
            else if (Father == null)
            {
                return base.ToString() + "\n" + $"mother: {Mother.getFirstName()} {Mother.getFamilyName()} ({Mother.getAge()}) " + "\n" + "father: No data";
            }
            else
            {
                return base.ToString() + "\n" + $"mother: {Mother.getFirstName()} {Mother.getFamilyName()} ({Mother.getAge()}) " + "\n" + $"father: {Father.getFirstName()} {Father.getFamilyName()} ({Father.getAge()}) ";
            }     
    }
    }  
}