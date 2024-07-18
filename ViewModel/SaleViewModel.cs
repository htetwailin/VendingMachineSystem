namespace VendingMachineSystem.ViewModel
{
    public class SaleViewModel
    {
            public int userid { get; set; }
            public string phoneno { get; set; }
            public string address { get; set; }
            public List<saleDetailViewModel> detailViewModels { get; set; }
    }
    public class saleDetailViewModel
    {
        public int product_id { get; set; }
        public int qty { get; set; }
    }
}
