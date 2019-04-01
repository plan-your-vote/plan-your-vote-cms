using System;
using System.Collections.Generic;
using System.Text;
using Web;
using Web.Data;
using Xunit;

namespace BackEndTests
{
    public class CandidatesControllerTest
    {
        // private readonly ApplicationDbContext _context;
        
        [Fact]
        public void Equal_Random_Image_Id_Value()
        {
            string image_value_1 = CandidatesController.GenerateImageId();
            string image_value_2 = CandidatesController.GenerateImageId();

            Assert.NotEqual(image_value_1, image_value_2);
        }

        [Fact]
        public void NotNull_Random_Image_Id_Value()
        { 
            string image_value_1 = CandidatesController.GenerateImageId();

            Assert.NotNull(image_value_1);
        }
    }
}
