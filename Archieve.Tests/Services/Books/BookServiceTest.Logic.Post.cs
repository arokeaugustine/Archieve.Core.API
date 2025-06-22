using Archieve.Core.Contracts;
using Archieve.Core.Contracts.TransferObjects.Books;
using Archieve.Infrastructure.Models;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Archieve.Tests.Unit.Services.Bookss
{
    public partial class BookServiceTest
    {
        [Fact]
        public async Task ShouldAddBookAsync()
        {
           // given 
           Books randomBooks = CreateRandomBooks();
          
            Book inputBook = CreateRandomBook(randomBooks);
            Book persistedBook = inputBook;

            var expectedResponse = CreateResponse();

            this.storageBrokerMock.Setup(x =>
            x.InsertBookAsync(It.Is(SameBookAs(inputBook))))
                .ReturnsAsync(persistedBook);

            // when

            ResponseModel<string> res = await this.bookService.CreateBooks(randomBooks);



            // then
            res.Should().BeEquivalentTo(expectedResponse);

            this.storageBrokerMock.Verify(x =>
            x.InsertBookAsync(It.Is(SameBookAs(inputBook))),
            Times.Once);

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
