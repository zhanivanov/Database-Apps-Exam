//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EF_Mappings
{
    using System;
    using System.Collections.Generic;
    
    public partial class InternationalMatch
    {
        public int Id { get; set; }
        public string HomeCountryCode { get; set; }
        public string AwayCountryCode { get; set; }
        public Nullable<int> HomeGoals { get; set; }
        public Nullable<int> AwayGoals { get; set; }
        public Nullable<System.DateTime> MatchDate { get; set; }
        public Nullable<int> LeagueId { get; set; }
    
        public virtual Country AwayCountry { get; set; }
        public virtual Country HomeCountry { get; set; }
        public virtual League League { get; set; }
    }
}
