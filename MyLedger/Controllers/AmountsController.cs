using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLedger.Models;
using MyLedger.Models.Account;
using MyLedger.Web.Data;

namespace MyLedger.Controllers
{

    public class AmountsController : Controller
    {

        private readonly LedgerDbContext dbContext;

        public AmountsController(LedgerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: AmountsController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var amounts = await dbContext.Amounts.ToListAsync();
            var viewModel = new AmountViewModel
            {
                Amount = amounts,
                TotalAmount = amounts.Sum(a => a.AmountValue)
            };
            return View(viewModel);
        }



        [HttpGet("Amounts/Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Amounts/Create")]
        public async Task<IActionResult> Create(AddAmountViewModel viewModel)
        {
            var amount = new Amount
            {
                Bank = viewModel.Bank,
                AmountValue = viewModel.AmountValue,
                CreatedAt = viewModel.CreatedAt
            };
            Console.WriteLine(amount.Bank);
            await dbContext.AddAsync(amount);
            await dbContext.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Amount updatedAmount)
        {
            if (id != updatedAmount.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(updatedAmount);
            }

            try
            {
                dbContext.Update(updatedAmount);
                await dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await dbContext.Amounts.AnyAsync(a => a.Id == id))
                {
                    return NotFound();
                }
                throw;
            }
        }


        //// GET: AmountsController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: AmountsController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
