﻿namespace RopeyDVD.Models
{
    public class MembershipCategory
    {
        public int MembershipCategoryId { get; set; }
        public string MembershipCategoryDescription { get; set; } = string.Empty;
        public int MembershipCategoryTotalLoans { get; set; }
    }
}

