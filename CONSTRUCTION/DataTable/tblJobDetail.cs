//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CONSTRUCTION.DataTable
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblJobDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string CompanyLogo { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> SubCategoryId { get; set; }
        public string Profession { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> CityId { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public Nullable<int> MinExperience { get; set; }
        public Nullable<int> MaxExperience { get; set; }
        public string Type { get; set; }
        public Nullable<int> MinSalary { get; set; }
        public Nullable<int> MaxSalary { get; set; }
        public string JobEducation { get; set; }
        public string JobQualification { get; set; }
    }
}
