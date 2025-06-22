using Archieve.Core.Contracts;
using Archieve.Core.Contracts.TransferObjects.Books;
using Archieve.Domain.Brokers;
using Archieve.Domain.Interfaces;
using Archieve.Domain.Services;
using Archieve.Infrastructure.Models;
using KellermanSoftware.CompareNetObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;

namespace Archieve.Tests.Unit.Services.Bookss
{
    public partial class BookServiceTest
    {
        private Mock<ArchieveContext> context;
        private Mock<IStorageBroker> storageBrokerMock;
        private IBookService bookService;
        private ICompareLogic compareLogic;

        public BookServiceTest()
        {
            this.context = new Mock<ArchieveContext>();
            this.storageBrokerMock = new Mock<IStorageBroker>();
            this.compareLogic = new CompareLogic();

            this.bookService = new BookServices(
                context: this.context.Object,
                storageBroker:this.storageBrokerMock.Object );
        }

        private static Books CreateRandomBooks() =>
            CreateBooksFiller().Create();

        private static Book CreateRandomBook(Books books)
        {
            return new Book
            {
                Title = books.Title,
                Author = books.Author,
                DateCreated = DateTime.Now,
                Description = books.Description
            };
        }

        private Func<Expression<Book>, bool> SameBookAs(Book expectedBook)
        {
            return actualBook =>
                this.compareLogic.Compare(
                    actualBook,
                    expectedBook).AreEqual;
        }

        private static ResponseModel<string> CreateResponse()
        {
           return new ResponseModel<string>
            {
                IsSuccessful = true,
                Message = "successful"
            };
        }
             
        private static Filler<Books> CreateBooksFiller()
        {
            var filler = new Filler<Books>();

            return filler;
        }
    }
}
