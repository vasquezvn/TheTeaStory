using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqProject
{
    public class Program
    {
        public List<Vendor> vendors = new List<Vendor>();

        static void Main(string[] args)
        {
        }

        public Program()
        {
            vendors.Add(new Vendor() { Id = 1, CompanyName = "ABC Corp", Email = "abc@abc.com" });
            vendors.Add(new Vendor() { Id = 2, CompanyName = "ZYX Inc", Email = "xyz@xyz.com" });
            vendors.Add(new Vendor() { Id = 12, CompanyName = "EFG Ltd", Email = "efg@efg.com" });
            vendors.Add(new Vendor() { Id = 17, CompanyName = "HIJ", Email = "hij@hij.com" });
            vendors.Add(new Vendor() { Id = 22, CompanyName = "Amalgamed Toys", Email = "a@abc.com" });
            vendors.Add(new Vendor() { Id = 28, CompanyName = "Toys block inc", Email = "block@abc.com" });
            vendors.Add(new Vendor() { Id = 31, CompanyName = "Home Product Inc", Email = "home@abc.com" });
            vendors.Add(new Vendor() { Id = 25, CompanyName = "Car Toys", Email = "car@abc.com" });
            vendors.Add(new Vendor() { Id = 42, CompanyName = "Toys for Fun", Email = "fun@abc.com" });
        }

        /// <summary>
        /// Method with Lambda notation
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool FilterCompanies(Vendor v) => v.CompanyName.Contains("Toy");

        public string OrderCompaniesByName(Vendor v) => v.CompanyName;
    }
}
