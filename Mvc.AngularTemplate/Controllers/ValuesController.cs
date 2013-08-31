using System;
using System.Collections.Generic;
using System.Web.Http;


namespace Mvc.AngularTemplate.Controllers
{
    public class ValuesController : ApiController
    {
        private const string nameFormatLNCommaFNMN = "{0}, {1} {2}.";
        private const string nameFormatLNCommaFN = "{0}, {1}";
        private const string nameFormatSingle = "{0}";
        private const string ageFormat = "{0} years and {1} months. Bithday is in {2} days";
        private const string addressFormat = "{0} {1}, {2}, {3} {4}-{5}";

        public ApiDemoResult Get([FromUri]ApiDemoPerson ApiDemo)
        {
            ApiDemoResult result = null;

            if (ApiDemo != null)
            {
                result = determineName(ApiDemo, result);

                result = calculateAge(ApiDemo, result);

                result = processAddresses(ApiDemo, result);
            }

            return result;
        }

        private static ApiDemoResult processAddresses(ApiDemoPerson ApiDemo, ApiDemoResult result)
        {
            if (ApiDemo.Addresses != null && ApiDemo.Addresses.Count > 0)
            {
                foreach (var address in ApiDemo.Addresses)
                {
                    if (!string.IsNullOrWhiteSpace(address.Address1)
                        || !string.IsNullOrWhiteSpace(address.Address2)
                        || !string.IsNullOrWhiteSpace(address.City)
                        || !string.IsNullOrWhiteSpace(address.State)
                        || !string.IsNullOrWhiteSpace(address.Zip5))
                    {
                        result = result ?? new ApiDemoResult();
                        if (result.Addresses == null) result.Addresses = new List<string>();
                        result.Addresses.Add(string.Format(addressFormat, address.Address1, address.Address2, address.City, address.State, address.Zip5, address.Zip4));
                    }
                }
            }
            return result;
        }

        private static ApiDemoResult determineName(ApiDemoPerson ApiDemo, ApiDemoResult result)
        {
            if (!string.IsNullOrWhiteSpace(ApiDemo.LastName)
                && !string.IsNullOrWhiteSpace(ApiDemo.FirstName)
                && !string.IsNullOrWhiteSpace(ApiDemo.MiddleName))
            {
                result = result ?? new ApiDemoResult();
                result.FullName = string.Format(nameFormatLNCommaFNMN, ApiDemo.LastName, ApiDemo.FirstName, ApiDemo.MiddleName.Substring(0, 1));
            }
            else if (!string.IsNullOrWhiteSpace(ApiDemo.LastName)
               && !string.IsNullOrWhiteSpace(ApiDemo.FirstName))
            {
                result = result ?? new ApiDemoResult();
                result.FullName = string.Format(nameFormatLNCommaFN, ApiDemo.LastName, ApiDemo.FirstName);
            }
            else if (!string.IsNullOrWhiteSpace(ApiDemo.LastName))
            {
                result = result ?? new ApiDemoResult();
                result.FullName = string.Format(nameFormatSingle, ApiDemo.LastName);
            }
            else if (!string.IsNullOrWhiteSpace(ApiDemo.FirstName))
            {
                result = result ?? new ApiDemoResult();
                result.FullName = string.Format(nameFormatSingle, ApiDemo.FirstName);
            }
            return result;
        }

        private static ApiDemoResult calculateAge(ApiDemoPerson ApiDemo, ApiDemoResult result)
        {
            if (ApiDemo.DateOfBirth.HasValue && ApiDemo.DateOfBirth.Value < DateTime.Now)
            {
                var today = DateTime.Now;
                result = result ?? new ApiDemoResult();
                var age = today.Subtract(ApiDemo.DateOfBirth.Value);
                var nextBirthday = new DateTime(today.Year, ApiDemo.DateOfBirth.Value.Month, ApiDemo.DateOfBirth.Value.Day);
                if (nextBirthday < today) nextBirthday = nextBirthday.AddYears(1);
                var nextBirthdayDays = nextBirthday.Subtract(today);
                result.Age = string.Format(ageFormat, Math.Round(age.TotalDays / 365.25), Math.Round(age.TotalDays % 365.25) / 30.0, nextBirthdayDays.TotalDays);
            }

            return result;
        }
    }

    public class ApiDemoPerson
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<ApiDemoAddress> Addresses { get; set; }
    }

    public class ApiDemoAddress
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip5 { get; set; }
        public string Zip4 { get; set; }
    }

    public class ApiDemoResult
    {
        public string FullName { get; set; }
        public string Age { get; set; }
        public List<string> Addresses { get; set; }
    }
}
