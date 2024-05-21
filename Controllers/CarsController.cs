using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentACarProject.Data;
using RentACarProject.Models;

namespace RentACarProject.Controllers
{

    [Authorize]
    public class CarsController : Controller
    {
        private readonly ApplicationContext _context;//veritabanındakileri eşleştirmek için context i kullanırız

        public CarsController(ApplicationContext context)
        {
            _context = context;
        }

        // Arabaları listele
        public async Task<IActionResult> Index()//index = getCars() fonksiyonudur. Yani arabaları ve özelliklerini gösterir.
        {
            if (_context.Cars != null)//context.Cars = veritabanındaki bütün arabaları ifade eder.
            {
                return View(await _context.Cars.ToListAsync());//_context.Cars.ToListAsync() veri tabanındaki arabaları listele
            }
            else
            {
                return Problem("Entity set 'ApplicationContext.Cars' is null.");
            }
        }


        // GET: Cars/Details/5
        //id sine göre arabaları getirme
        public async Task<IActionResult> Details(int? id)//arabanın detaylarını gösterir.
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.FirstOrDefaultAsync(m => m.Id == id);//FirstOrDefaultAsync = parantez içindeki şartı sağlayan ilk değeri alır.
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        [HttpGet]//sunucudan veri almak için kullanılırbi
        [Authorize]
        public IActionResult Create()//arabayı oluşturmak için kullanılır.
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]//sunucuya veri göndermek için kullanılır  
        [ValidateAntiForgeryToken]
        [Authorize]//methodu kullanmak için giriş gereksinimini sağlar
        public async Task<IActionResult> Create([Bind("Id,Brand,Model,ModelYear,Description,GearBox,Color,IsAvailiable,Price,ImageUrl")] Car car)//bind = sadece parantez içinde yazılanların bağlanmasına izin verir
        {
            if (ModelState.IsValid == true) //verinin doğruluğunun sorgulandığı son kontrol alanıdır(örneğin araba 1 karakterli olamaz)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();//veritabanına kaydedilmesini sağlar.
                return RedirectToAction(nameof(Index));//RedirectToAction = bizi index e gönderir.(index de arabaları gösterir.)
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();//IActionResult notfound mesajını getirmesini sağlar
            }

            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Brand,Model,ModelYear,Description,GearBox,Color,IsAvailiable,Price,ImageUrl")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingCar = await _context.Cars!.FindAsync(id);//_context.Cars! kesinlikle dolu olacağı anlamına geliyor

                existingCar!.Color = car.Color;
                existingCar.Brand = car.Brand;
                existingCar.GearBox = car.GearBox;
                existingCar.Price = car.Price;
                existingCar.Description = car.Description;
                existingCar.Model = car.Model;
                existingCar.ModelYear = car.ModelYear;

                _context.Update(existingCar);
                await _context.SaveChangesAsync();//asenkron olduğu için başına await koyduk

                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cars == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(_context.Cars == null)
            {
                return NotFound();
            }
            var car = await _context.Cars.FindAsync(id);
            if(car != null)
            {
                _context.Cars.Remove(car);//yani veritabanındaki cars tablosuna gi ve id ile eşleşen car ı sil
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
