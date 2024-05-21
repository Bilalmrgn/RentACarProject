using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACarProject.Data;
using RentACarProject.Models;

namespace RentACarProject.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ApplicationContext _context; //veritabanından bilgi çekmek için

        public RentalsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Rentals
        //kiralanan arabaları göster
        public async Task<IActionResult> Index()
        {                  //rentals tablosunu getir  applicationuser ları ve arabaları dahil etti. Yani arabaları ve kimin kiraladıklarını gösterir
            var applicationContext = _context.Rentals!.Include(r => r.ApplicationUser).Include(r => r.Car);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Rentals/Details/5
        //id ye göre kiralamayı getir
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }
            //LINQ SORGUSU
            var rental = await _context.Rentals//veritabanından rentals tablosunu getir
                .Include(r => r.ApplicationUser)
                .Include(r => r.Car)
                .FirstOrDefaultAsync(m => m.Id == id);//rental için rental ın id si bizim verdiğimiz id ye eşit olduğu zaman
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        //CREATE İN GÖRÜNTÜSÜNÜ OLUŞTURUR 
        //kiralama oluşturma
        public IActionResult Create()
        {
            //                                              veritabanından kullanıcıların id lerini getirir        
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");//selectList = HTML form elemanını göstermek için kullanılır
            ViewData["CarId"] = new SelectList(_context.Cars, "Brand", "Brand");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", rental.ApplicationUserId);
            ViewData["CarId"] = new SelectList(_context.Cars, "Brand", "Brand", rental.Car!.Brand);
            return View(rental);
        }

        // GET: Rentals/Edit/5
        //kiralamayı düzenleme
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", rental.ApplicationUserId);
            ViewData["CarBrand"] = new SelectList(_context.Cars, "Brand", "Brand", rental.Car!.Brand);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rental rental)
        {
            if (rental.Id != id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existRental = await _context.Rentals!.FindAsync(id);
                if (existRental == null)
                {
                    return NotFound();
                }
                existRental.ReturnDate = rental.ReturnDate;
                existRental.RentDate = rental.RentDate;
                existRental.CarId = rental.CarId;
                existRental.Price = rental.Price;
                existRental.ApplicationUserId = rental.ApplicationUserId;

                return RedirectToAction("Index");
            }
            return View(rental);

        }

        // GET: Rentals/Delete/5
        //get yazacağımız şeyleri gösterirken post silmemizi sağlar
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rentals == null)
            {
                return NotFound();
            }

            //bizim gönderdiğimiz id nin rental ın id si ile eşit olduğundaki ilk değeri alıyor(FirstOrDefoultAsync) ve applicationUser ve Car ı dahil ederek rental a eşitliyo
            var rental = await _context.Rentals
                .Include(r => r.ApplicationUser)
                .Include(r => r.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }


        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals!.FindAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
