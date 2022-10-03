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
    }
}