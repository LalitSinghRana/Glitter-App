using BL;
using DAL;
using EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGround
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new GlitterDB())
            {
                //Insert values in database  
                context.Tweets.Add(new EntityModel.Entities.Tweet { TweetId = 1, Message = "krishna", Date = DateTime.Now });
                //context.Employees.Add(new Employee { EmployeeID = 2, EmpName = "radha" });
                context.SaveChanges();

                Console.Write("Student saved successfully!");

                //Get all values from database  
                foreach (var emp in context.Tweets)
                {
                    Console.WriteLine("Employee ID : " + emp.TweetId);
                    Console.WriteLine("Employee Name : " + emp.Message);
                }


                foreach (var usr in context.Users)
                {
                    Console.WriteLine("User ID : " + usr.EmailId);
                    Console.WriteLine("User Name : " + usr.Name);
                }
            }



        }
    }
}
