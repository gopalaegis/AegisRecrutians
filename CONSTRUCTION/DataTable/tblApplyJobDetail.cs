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
    
    public partial class tblApplyJobDetail
    {
        public int Id { get; set; }
        public Nullable<int> JobId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Mobile1 { get; set; }
        public string AddressType { get; set; }
        public string HouseName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> StateId { get; set; }
        public string Resume { get; set; }
        public Nullable<bool> isActive { get; set; }
    }
}
