using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentACarProject.Data;
using RentACarProject.Models;

namespace RentACarProject.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationContext _context;//veritabanı eşleşmeleri için kullanılır
        private readonly SignInManager<ApplicationUser> _signInManager;//.net de kimlik doğrulamak ve oturumu yönetmek için SignInManager<ApplicationUser> kullanılır.
        public UsersController(ApplicationContext context, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }
        // GET: UsersController1
        //kullanıcıları listele
        public ActionResult Index()
        {
            var user = _signInManager.UserManager.Users.ToList();//kullanıcılar bu şekilde listelenir.(Users = bütün kullanıcıları getirir)
            return View(user);
        }

        // GET: UsersController1/Details/5

        public ActionResult Details(string id)
        {
            var user = _signInManager.UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View();
        }

        // GET: UsersController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController1/Create
        //kullanıcı oluşturma
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationUser applicationUser, string password)
        {
            _signInManager.UserManager.CreateAsync(applicationUser, password).Wait();//kullanıcı oluşsun sonra alt satıra geçsin
            _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: UsersController1/Edit/5
        //kullanıcıları düzenleme
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicationUser applicationUser)
        {
            //FindByIdAsync(applicationUser.id) --> aradığımız belirli id deki kullanıcıyı bulmak için kullanılır
            ApplicationUser existApplicationUser = _signInManager.UserManager.FindByIdAsync(applicationUser.Id).Result;//mevcuttaki kullanıcıyı yakalama. 

            existApplicationUser.PhoneNumber = applicationUser.PhoneNumber;
            existApplicationUser.Address = applicationUser.Address;
            existApplicationUser.UserName = applicationUser.UserName;
            existApplicationUser.Email = applicationUser.Email;

            _signInManager.UserManager.UpdateAsync(existApplicationUser);//kullanıcının değerlerini güncellemesi için yazılan method.

            return View("İşlem başarılı");
        }

        // GET: UsersController1/Delete/5
        public ActionResult Delete(int id)
        {

            return View();
        }

        // POST: UsersController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            var user = _signInManager.UserManager.FindByIdAsync(id).Result;//result un amacı bu satırı tamamlamadan alt satıra geçme
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var result = _signInManager.UserManager.DeleteAsync(user);
                return RedirectToAction(nameof(Delete));
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        //kullanıcı giriş işlemleri.
        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            Microsoft.AspNetCore.Identity.SignInResult signInResult;//Microsoft.AspNetCore.Identity.SignInResult, bir kullanıcının oturum açma işleminin sonucunu temsil eden bir sınıftır. Başarılı bir oturum açma işlemi sonucunda Succeeded özelliği true olurken, başarısız bir oturum açma işlemi sonucunda Succeeded özelliği false olur.
            ApplicationUser applicationUser = _signInManager.UserManager.FindByNameAsync(userName).Result;//kullanıcı bulunur
            if (applicationUser == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı");
                return View();
            }
            signInResult = _signInManager.PasswordSignInAsync(userName, password, false, false).Result;//şifrenin doğru olup olmadığı kontrol edilir(passwordSign)
            if (signInResult.Succeeded == true)
            {
                return RedirectToAction("Index","Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı adı veya şifre hatalı");
                return View();
            }
        }

        //kullanıcı çıkışı
       
        public ActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }

    }
}
