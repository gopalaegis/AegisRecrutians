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
    
    public partial class tblApplyJobExperience
    {
        public int Id { get; set; }
        public Nullable<int> ApplyJobDetailId { get; set; }
        public string Employer { get; set; }
        public string JobTitle { get; set; }
        public Nullable<int> ActiveYear { get; set; }
        public string City { get; set; }
        public Nullable<int> StateId { get; set; }
        public Nullable<int> CountryId { get; set; }
    }
}