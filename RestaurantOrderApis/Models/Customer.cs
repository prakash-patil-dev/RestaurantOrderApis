namespace RestaurantOrderApis.Models
{
    //public class Customer
    //{
    //}

    public class CustomerFilter
    {
        public string? Mode { get; set; }
        public Customer? ObjCustomer { get; set; }
    }

    public class Customer
    {
        public string? BranchCode { get; set; }
        public string? Code { get; set; }
        public string? Prefix { get; set; }
        public string? PartyId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string? City { get; set; }
        public string? Area { get; set; }
        public string? Telephone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Nationalty { get; set; }
        public string? Status { get; set; }
        public string? Contact { get; set; }
        public DateTime? DateJoin { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public float RegAmount { get; set; }
        public string? SalePer { get; set; }
        public string? CompName { get; set; }
        public string? CardNo { get; set; }
        public string? Updated { get; set; }
        public string? LastUser { get; set; }
        public DateTime LastDate { get; set; }
        public string? LastTime { get; set; }
        public string? CustType { get; set; }
        public float Discount { get; set; }
        public string? PriceType { get; set; }
        public string? CustImage { get; set; }
        public string? Type { get; set; }
        //public string? Mode { get; set; }
    }

}
