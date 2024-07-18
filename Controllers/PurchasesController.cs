using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendingMachineSystem.Data;
using VendingMachineSystem.Models.Product;
using VendingMachineSystem.Models.Purchase;

namespace VendingMachineSystem.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly VendingMachineDBContext _context;
        private readonly ILogger<PurchasesController> _loggerFactory;


        public PurchasesController(VendingMachineDBContext context, ILogger<PurchasesController> loggerFactory)
        {
            _context = context;
            _loggerFactory = loggerFactory;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
              return _context.Purchase != null ? 
                          View(await _context.Purchase.ToListAsync()) :
                          Problem("Entity set 'VendingMachineDBContext.Purchase'  is null.");
        }


        // GET: Purchases/Create
        public IActionResult Create()
        {
            List<Product> products = _context.Product.ToList();
            return View(products);
        }
        [HttpPost]
        public IActionResult CreatePurchase(List<CartItemViewModel> cartItems)
        {
            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index");
            }
            TimeZoneInfo defaultTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");
            DateTime currentUtcTime = DateTime.UtcNow;
            DateTime purchasedate = TimeZoneInfo.ConvertTimeFromUtc(currentUtcTime, defaultTimeZone);
            var purchaseview = new PurchaseViewModel
            {
                purchase_no = GeneratePurchaseNo(), // Generate a unique purchase number
                puchanse_date = purchasedate,
                total = cartItems.Sum(item => item.TotalPrice),
                items = cartItems.Select(item => new PurchaseDetail
                {
                    //add abl to product
                    product_id = item.ProductId,
                    qty = item.Quantity,
                    price = item.Price
                }).ToList()
            };
            // Start a transaction to ensure all operations are atomic
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Purchase purchase = new Purchase();
                    purchase.purchase_no = purchaseview.purchase_no;
                    purchase.puchanse_date = purchaseview.puchanse_date;
                    purchase.total = purchaseview.total;
                    // Save purchase to the database
                    _context.Purchase.Add(purchase);
                    _context.SaveChanges();
                    int purchase_id = purchase.id;
                    foreach (var detail in purchaseview.items)
                    {
                        PurchaseDetail pdetail = new PurchaseDetail();
                        pdetail.purchase_id = purchase_id;
                        pdetail.product_id = detail.product_id;
                        //check availableqty
                        var product = _context.Product.Where(x => x.id == detail.product_id).FirstOrDefault();
                        if (product != null)
                        {
                            product.qty_abl += detail.qty;
                        }
                        pdetail.qty = detail.qty;
                        pdetail.price = detail.price;
                        _context.PurchaseDetail.Add(pdetail);
                    }
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of error
                    transaction.Rollback();
                    _loggerFactory.LogError(ex, "An error occurred while creating the order.");
                    return RedirectToAction("Index");
                }
            }
            // Optionally, you can clear the cart here if it's stored in the session

            return RedirectToAction("Index");
        }

        private string GeneratePurchaseNo()
        {
            // Set the default time zone to, for example, Eastern Standard Time
            TimeZoneInfo defaultTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Myanmar Standard Time");
            DateTime currentUtcTime = DateTime.UtcNow;
            string purchaseno = TimeZoneInfo.ConvertTimeFromUtc(currentUtcTime, defaultTimeZone).ToString("yyMMdd") + Guid.NewGuid().ToString("N").Substring(0, 5);
            // Generate a unique purchase number
            return "PO-" + purchaseno;
        }


        // POST: Purchases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,purchase_no,total,puchanse_date")] Purchase purchase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(purchase);
        }

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Purchase == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase.FindAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,purchase_no,total,puchanse_date")] Purchase purchase)
        {
            if (id != purchase.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Purchase == null)
            {
                return NotFound();
            }

            var purchase = await _context.Purchase
                .FirstOrDefaultAsync(m => m.id == id);
            if (purchase == null)
            {
                return NotFound();
            }

            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Purchase == null)
            {
                return Problem("Entity set 'VendingMachineDBContext.Purchase'  is null.");
            }
            var purchase = await _context.Purchase.FindAsync(id);
            if (purchase != null)
            {
                _context.Purchase.Remove(purchase);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return (_context.Purchase?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
