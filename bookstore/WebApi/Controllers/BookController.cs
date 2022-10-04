using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WebApi.AddControllers{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>(){
            new Book{
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1,// Personal Growth
                PageCount = 200,
                PublishDate = new DateTime(2001,06,12)
            },
            new Book{
                Id = 2,
                Title = "Herland",//Science Fiction
                GenreId = 2,
                PageCount = 250,
                PublishDate = new DateTime(2010,05,23)
            },
            new Book{
                Id = 3,
                Title = "Dune",//Science Fiction
                GenreId = 2,
                PageCount = 540,
                PublishDate = new DateTime(2001,12,21)
            }
        };


        //tüm veriyi döndüren bir endpoint tir
        [HttpGet]
        //Book listesini dönecek bir metod oluşturuyoruz
        public List<Book> GetBooks() //bize bir List<Book> döenen bir metod yani Book içindeki verileri döndürecek
        {
            var bookList = BookList.OrderBy(x => x.Id).ToList();//Id ye göre getiriyoruz // Linq dili SQL ifadelerine yakın ifadeler yazmamıza kolaylık sağlıyor
            return bookList;
        }
        


        //Id ile çağırma yöntemi bu şekilde daha mantıklı FromQury ile yaparsak verilerin tümünü çekme ihtimali de var
        [HttpGet("{id}")]//Id parametresi ile veri döndürme, burada id yi root tan alıyoruz
        //Book listesini dönecek bir metod oluşturuyoruz
        public Book GetById(int id) //bize bir List<Book> döenen bir metod yani Book içindeki verileri Id ye göre döndürecek
        {
            var book = BookList.Where(book => book.Id == id).SingleOrDefault();//Id ye göre getiriyoruz 
            return book;
        }

        //FromQuery yöntemiyle Id ile verileri çağırma
        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //      var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();//Id ye göre getiriyoruz 
        //      return book;
        //  }


        //Post
        [HttpPost]
        //request body alır kodumuz, parametre değil.
        //try it out üzerinden request body yi doldurabiliriz ve get ile tüm veriyi çağırdığımızda post ile girilen veriyi gözlemleyebiliriz.
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = BookList.SingleOrDefault(x=> x.Title == newBook.Title); //burada eklenecek kitabın mevcut olup olmadığını Title üzerinden validasyon işlemi yapıyoruz

            //if(book != null)//kitap mevcutsa hata döndürür
            if(book is not null)
                return BadRequest();

            BookList.Add(newBook);//mevcut değilse Ok() döndürülür.
            return Ok();// yani 200 code mesajı döner
        }

        //Put
        [HttpPut("{id}")] // hangi id üzerinden güncelleme yapacağımızı belirtmemiz lazım ve güncel değerleri göndermemiz gerekir
        public IActionResult UpdateBook(int id, [FromBody] Book updateBook) //Book tipinde olacak yani Book tipinde olduğu için tüm nesneyi alıyoruz, update te IActionResult kullanılır
        {
            //book umuzun listemizde mevcut olup olmadığını kontrol etmemiz gerekiyor çünkü mevcut olan veri için güncelleme yapbiliriz. Bundan dolayı validasyon işlemi yapmamız gerekir.
            var book = BookList.SingleOrDefault(x=> x.Id == id);// burada bir id de tek bir değer olacağından dolayı SingleOrDefault() kullanıyoruz

            if(book is null)
                return BadRequest();

            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId; //Burada GenreId default değilse yani doldurulmuşsa GenreId yi updateBook.GenreId eşitle doldurulmamışsa book.GenreId ye eşitle yani kendi GenreId sini al.
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;

            return Ok();
        }

        
    }
}