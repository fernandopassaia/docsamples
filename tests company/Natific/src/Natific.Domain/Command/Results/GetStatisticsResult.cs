namespace Natific.Domain.Command.Results
{
    public class GetStatisticsResult
    {
        //total weight, total price, most item in stock, most weight in stock
        public decimal TotalWeight { get; set; }
        public decimal TotalPrice { get; set; }
        public string MostItemStock { get; set; }
        public string MostWeightStock { get; set; }
    }
}
