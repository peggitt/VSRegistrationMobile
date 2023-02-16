using System;
using System.Collections.Generic;

namespace HSNP.Models
{
    public class SubscriptionVm : ErrorApi
    {
        public int StoreId { get; set; }
        public string UserId { get; set; }

        public Subscription Subscription { get; set; }
        public List<Subscription> Subscriptions { get; set; }
        public SubscriptionPeriod SubscriptionPeriod { get; set; }
        public List<SubscriptionPeriod> SubscriptionPeriods { get; set; }
        public int SubscriptionPeriodId { get; set; }
        public ClientSubscription StoreSubscription { get; set; }
    }
    public class ErrorApi
    {
        public string Error { get; set; }
        public string Message { get; set; }
    }
    public class SubscriptionPeriod
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Months { get; set; }
        public decimal CostMultiplier { get; set; }
        public decimal Amount { get; set; }
    }
    public class ClientSubscription
    {
        public int Id { get; set; }
        public int? SubscriptionId { get; set; }
        public int? SubscriptionPeriodId { get; set; }
        public decimal Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; }
        public int Products { get; set; }
        public string Date { get; set; }
        public int StatusId { get; set; }

    }
}

