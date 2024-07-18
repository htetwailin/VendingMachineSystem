using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendingMachineSystem.Common;
using VendingMachineSystem.Data;
using VendingMachineSystem.Models.Product;
using VendingMachineSystem.Models.Sale;
using VendingMachineSystem.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VendingMachineSystem.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly VendingMachineDBContext _context;
        public DataController( VendingMachineDBContext context)
        {
            this._context = context;
        }
        #region product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductList()
        {
            BaseResponse baseResponse = new BaseResponse();
            List<Product> productlist = await _context.Product.ToListAsync();
            baseResponse.success = true;
            baseResponse.data = productlist;
            return new JsonResult(baseResponse);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductdetail(int id)
        {
            BaseResponse baseResponse= new BaseResponse();
            int productid = Convert.ToInt32(id);
            var product = await _context.Product.Where(x => x.id == productid).FirstOrDefaultAsync();
            baseResponse.success = true;
            baseResponse.data = product;
            return new JsonResult(baseResponse);
        }

        #endregion

        #region Sale
        [HttpPost]
        public async Task<ActionResult<SaleViewModel>> CreateOrder(SaleViewModel order)
        {
            try
            {
                BaseResponse baseReponseModel = new BaseResponse();
                if (order != null)
                {
                    //checktholiday? 
                    var user = await _context.User.Where(x=> x.id == order.userid).FirstOrDefaultAsync();
                    if (user != null)
                    {
                        if (user.roleid == 2)
                        {
                            decimal total = 0;
                            Sale saledata = new Sale();
                            Guid newGuid = Guid.NewGuid();
                            // Set the default time zone to, for example, Eastern Standard Time
                            TimeZoneInfo defaultTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");
                            DateTime currentUtcTime = DateTime.UtcNow;
                            DateTime orderdate = TimeZoneInfo.ConvertTimeFromUtc(currentUtcTime, defaultTimeZone);
                            string orderno = TimeZoneInfo.ConvertTimeFromUtc(currentUtcTime, defaultTimeZone).ToString("yyMMdd") + newGuid.ToString("N").Substring(0, 5);
                            saledata.voucherno = orderno;
                            saledata.userid = order.userid;
                            saledata.address = order.address;
                            saledata.phoneno = order.phoneno;
                            saledata.saledate = orderdate;
                            List<saleDetail> saleDetailsList = new List<saleDetail>();
                            foreach (var detail in order.detailViewModels)
                            {
                                saleDetail saleDetail = new saleDetail();
                                saleDetail.product_id = detail.product_id;
                                saleDetail.qty = detail.qty;
                                var product = new Product();
                                if (detail.product_id != null)
                                {
                                     product = await _context.Product.Where(x => x.id == saleDetail.product_id).FirstOrDefaultAsync();
                                    if (product != null)
                                    {
                                        if (product.qty_abl >= saleDetail.qty)
                                        {
                                            saleDetail.price = product.price;
                                            product.qty_abl -= saleDetail.qty;
                                        }
                                        total += saleDetail.price;
                                        saleDetailsList.Add(saleDetail);
                                    }
                                }
                            }
                            saledata.total = total;
                            _context.Sale.Add(saledata);
                            _context.SaveChanges();
                            int saleid= saledata.id;
                            if(saleid > 0)
                            {
                                foreach(var saledetail in saleDetailsList)
                                {
                                    saleDetail detail = new saleDetail();
                                    detail.product_id =saledetail.product_id;
                                    detail.price = saledetail.price;
                                    detail.qty = saledetail.qty;
                                    detail.saleid = saleid;
                                    _context.SaleDetail.Add(detail);
                                }
                                _context.SaveChanges();
                            }
                            baseReponseModel.success = true;
                            baseReponseModel.message = "Success Order";
                            baseReponseModel.data = saledata;
                        }
                        else
                        {
                            baseReponseModel.success = false;
                            baseReponseModel.message = "Fail Order";
                        }
                    }
                }
                else
                {
                    baseReponseModel.success = false;
                    baseReponseModel.message = "Fail Order";
                }
                return Ok(baseReponseModel);
            }
            catch (Exception ex)
            {
                // Return a 500 Internal Server Error status code
                return StatusCode(500, "Error creating order");
            }
        }
        #endregion
    }
}
